
Imports System.Speech.Recognition

Friend Class VoiceController
    Implements IDisposable

    Private recognizer As SpeechRecognitionEngine
    Private isListening As Boolean = False
    Friend Event CommandRecognized(ByVal command As String)
    Friend Event PlayTargetRequested(ByVal targetKey As String)

    ' Base Methods
    Friend Sub New()
        Try
            recognizer = New SpeechRecognitionEngine()
            recognizer.SetInputToDefaultAudioDevice()
            AddHandler recognizer.SpeechRecognized, AddressOf OnSpeechRecognized
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"Voice engine init error: {ex.Message}")
        End Try
    End Sub
    Friend Sub Dispose() Implements IDisposable.Dispose
        If recognizer IsNot Nothing Then
            [Stop]()
            RemoveHandler recognizer.SpeechRecognized, AddressOf OnSpeechRecognized
            If App.VoicePhraseToKeyMap IsNot Nothing Then App.VoicePhraseToKeyMap.Clear()
            Try
                recognizer.UnloadAllGrammars()
            Catch ex As Exception
            End Try
            recognizer.Dispose()
            recognizer = Nothing
        End If
    End Sub
    Friend Sub Start()
        If recognizer IsNot Nothing AndAlso Not isListening Then
            Try
                recognizer.RecognizeAsync(RecognizeMode.Multiple)
                isListening = True
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine($"Error starting voice listening: {ex.Message}")
            End Try
        End If
    End Sub
    Friend Sub [Stop]()
        If recognizer IsNot Nothing AndAlso isListening Then
            Try
                recognizer.RecognizeAsyncStop()
                isListening = False
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine($"Error stopping voice listening: {ex.Message}")
            End Try
        End If
    End Sub

    ' Handlers
    Private Sub OnSpeechRecognized(ByVal sender As Object, ByVal e As SpeechRecognizedEventArgs)
        If App.Settings.VoicePushToTalk AndAlso Not App.VoicePushToTalkActive Then
            Debug.WriteLine("[VOICE IGNORED] Push-To-Talk is inactive.")
            Return
        End If
        Debug.WriteLine($"[VOICE DETECTED] Text: '{e.Result.Text}' | Confidence: {e.Result.Confidence:P2}")

        ' 1. Ignore low confidence hits
        If e.Result.Confidence < 0.75F Then Return

        Dim grammarName As String = e.Result.Grammar?.Name
        Dim rawText As String = e.Result.Text.Trim()

        ' 2. Guard Clause: Block standalone wake word triggers ("hey skye" or "skye" alone)
        If rawText.Equals("hey skye", StringComparison.OrdinalIgnoreCase) OrElse rawText.Equals("skye", StringComparison.OrdinalIgnoreCase) Then
            'Debug.WriteLine("[VOICE IGNORED] Pure wake word detected without a command or song payload.")
            Return
        End If

        ' 3. Mandatory Wake-Word Prefix Verification
        If Not rawText.StartsWith("hey skye ", StringComparison.OrdinalIgnoreCase) Then
            'Debug.WriteLine($"[VOICE REJECTED] Phrase missing 'hey skye' prefix: '{rawText}'")
            Return
        End If

        ' Extract payload phrase after "hey skye " (length 9)
        Dim payload As String = rawText.Substring(9).Trim()

        ' Ensure payload isn't empty space
        If String.IsNullOrWhiteSpace(payload) Then Return

        Select Case grammarName
            Case "Controls"
                ' Raise command in lowercase for clean handling in your player (e.g., "play music", "next song")
                RaiseEvent CommandRecognized(payload.ToLowerInvariant())

            Case "DynamicPlaylist"
                ' Case-insensitive dictionary lookup against App.VoicePhraseToKeyMap
                Dim targetKey As String = ""
                If App.VoicePhraseToKeyMap IsNot Nothing Then
                    ' Try direct lookup first
                    If Not App.VoicePhraseToKeyMap.TryGetValue(payload, targetKey) Then
                        ' Fallback: Case-insensitive search if exact dictionary casing differs
                        Dim kvp = App.VoicePhraseToKeyMap.FirstOrDefault(Function(x) x.Key.Equals(payload, StringComparison.OrdinalIgnoreCase))
                        If kvp.Key IsNot Nothing Then
                            targetKey = kvp.Value
                        End If
                    End If
                End If

                If Not String.IsNullOrEmpty(targetKey) Then
                    RaiseEvent PlayTargetRequested(targetKey)
                Else
                    'Debug.WriteLine($"[VOICE DEBUG] Phrase '{payload}' matched grammar but missing from dictionary map.")
                End If
        End Select
        If App.Settings.VoicePushToTalk Then
            App.VoicePushToTalkActive = False
        End If
    End Sub

    ' Methods
    ''' <summary>
    ''' Asynchronously builds speech grammars on a background thread.
    ''' </summary>
    Friend Async Function LoadGrammarAsync() As Task
        If recognizer Is Nothing Then Return

        Await Task.Run(Sub()

                           ' -------------------------------------------------------------
                           ' 1. Isolated Unload Step (Prevents SAPI COM exceptions from stopping the build)
                           ' -------------------------------------------------------------
                           Try
                               recognizer.UnloadAllGrammars()
                           Catch ex As Exception
                               'Debug.WriteLine($"[VOICE WARNING] UnloadAllGrammars non-fatal exception: {ex.Message}")
                           End Try

                           ' -------------------------------------------------------------
                           ' 2. Build and Load Static Controls
                           ' -------------------------------------------------------------
                           Try
                               Dim controls As New Choices()
                               ' Use distinct, multi-syllable commands to prevent phonetic overlap
                               controls.Add(New String() {"play music", "pause music", "stop music", "previous song", "next song"})

                               Dim controlBuilder As New GrammarBuilder() With {.Culture = recognizer.RecognizerInfo.Culture}
                               controlBuilder.Append("hey skye") ' Wake word
                               controlBuilder.Append(controls)

                               Dim controlGrammar As New Grammar(controlBuilder) With {
                               .Name = "Controls",
                               .Weight = 0.8F
                           }
                               recognizer.LoadGrammar(controlGrammar)
                               'Debug.WriteLine("[VOICE SUCCESS] Controls grammar loaded.")
                           Catch ex As Exception
                               'Debug.WriteLine($"[VOICE ERROR] Controls grammar failed: {ex.Message}")
                           End Try

                           ' -------------------------------------------------------------
                           ' 3. Build and Load Dynamic Playlist
                           ' -------------------------------------------------------------
                           Try
                               If App.VoicePhraseToKeyMap Is Nothing OrElse App.VoicePhraseToKeyMap.Count = 0 Then
                                   'Debug.WriteLine("[VOICE WARNING] App.phraseToKeyMap is NULL or EMPTY! Skipping DynamicPlaylist.")
                                   Return
                               End If

                               'Debug.WriteLine($"[VOICE DEBUG] Building grammar for {App.VoicePhraseToKeyMap.Count} songs...")

                               Dim songChoices As New Choices()
                               For Each phrase In App.VoicePhraseToKeyMap.Keys
                                   If Not String.IsNullOrWhiteSpace(phrase) Then
                                       ' Clean out quotes/brackets that break SAPI compilation
                                       Dim cleanPhrase As String = phrase.Replace("""", "").Replace("&", "and").Trim()
                                       If cleanPhrase.Length > 1 Then
                                           songChoices.Add(cleanPhrase)
                                       End If
                                   End If
                               Next

                               Dim playBuilder As New GrammarBuilder() With {.Culture = recognizer.RecognizerInfo.Culture}
                               playBuilder.Append("hey skye") ' Wake word
                               playBuilder.Append(songChoices) ' Direct song title match

                               Dim dynamicGrammar As New Grammar(playBuilder) With {
                               .Name = "DynamicPlaylist",
                               .Weight = 1.0F
                           }
                               recognizer.LoadGrammar(dynamicGrammar)
                               'Debug.WriteLine("[VOICE SUCCESS] DynamicPlaylist grammar loaded successfully!")

                           Catch ex As Exception
                               'Debug.WriteLine($"[VOICE ERROR] DynamicPlaylist grammar failed: {ex.Message}")
                           End Try
                       End Sub)
    End Function
    Friend Sub ClearGrammars()
        If recognizer IsNot Nothing Then
            Try
                recognizer.RecognizeAsyncCancel()
                recognizer.UnloadAllGrammars()
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine($"Error unloading grammars: {ex.Message}")
            End Try
        End If
    End Sub

End Class
