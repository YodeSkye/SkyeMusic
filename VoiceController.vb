
Imports System.Speech.Recognition

Friend Class VoiceController
    Implements IDisposable

    Private recognizer As SpeechRecognitionEngine
    Private isListening As Boolean = False
    Friend Event CommandRecognized(ByVal command As String)
    Friend Event PlayTargetRequested(ByVal targetKey As String)

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

    Private Sub OnSpeechRecognized(ByVal sender As Object, ByVal e As SpeechRecognizedEventArgs)
        Debug.WriteLine($"[VOICE DETECTED] Text: '{e.Result.Text}' | Confidence: {e.Result.Confidence}")

        ' 1. Ignore low confidence hits
        If e.Result.Confidence < 0.75F Then Return

        Dim grammarName As String = e.Result.Grammar?.Name
        Dim rawText As String = e.Result.Text.Trim()

        Select Case grammarName
            Case "Controls"
                ' Matches "skye play", "skye pause", etc.
                Dim commandText As String = rawText
                If commandText.StartsWith("hey skye ", StringComparison.OrdinalIgnoreCase) Then
                    commandText = commandText.Substring(9).Trim().ToLowerInvariant()
                End If

                RaiseEvent CommandRecognized(commandText)

            Case "DynamicPlaylist"
                ' Matches "skye {Song Title}"
                If rawText.StartsWith("hey skye ", StringComparison.OrdinalIgnoreCase) Then
                    Dim recognizedPhrase As String = rawText.Substring(9).Trim()

                    ' Look up the file path / key from the map
                    Dim targetKey As String = ""
                    If App.VoicePhraseToKeyMap IsNot Nothing AndAlso App.VoicePhraseToKeyMap.TryGetValue(recognizedPhrase, targetKey) Then
                        RaiseEvent PlayTargetRequested(targetKey)
                    Else
                        Debug.WriteLine($"[VOICE DEBUG] Phrase '{recognizedPhrase}' matched grammar but was missing from dictionary.")
                    End If
                End If
        End Select
    End Sub

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
                               System.Diagnostics.Debug.WriteLine($"[VOICE WARNING] UnloadAllGrammars non-fatal exception: {ex.Message}")
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
                               System.Diagnostics.Debug.WriteLine("[VOICE SUCCESS] Controls grammar loaded.")
                           Catch ex As Exception
                               System.Diagnostics.Debug.WriteLine($"[VOICE ERROR] Controls grammar failed: {ex.Message}")
                           End Try

                           ' -------------------------------------------------------------
                           ' 3. Build and Load Dynamic Playlist
                           ' -------------------------------------------------------------
                           Try
                               If App.VoicePhraseToKeyMap Is Nothing OrElse App.VoicePhraseToKeyMap.Count = 0 Then
                                   System.Diagnostics.Debug.WriteLine("[VOICE WARNING] App.phraseToKeyMap is NULL or EMPTY! Skipping DynamicPlaylist.")
                                   Return
                               End If

                               System.Diagnostics.Debug.WriteLine($"[VOICE DEBUG] Building grammar for {App.VoicePhraseToKeyMap.Count} songs...")

                               Dim songChoices As New Choices()
                               For Each phrase In App.VoicePhraseToKeyMap.Keys
                                   If Not String.IsNullOrWhiteSpace(phrase) Then
                                       ' Clean out quotes/brackets that break SAPI compilation
                                       Dim cleanPhrase As String = phrase.Replace("""", "").Replace("&", "and").Trim()
                                       If cleanPhrase.Length > 0 Then
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
                               System.Diagnostics.Debug.WriteLine("[VOICE SUCCESS] DynamicPlaylist grammar loaded successfully!")

                           Catch ex As Exception
                               System.Diagnostics.Debug.WriteLine($"[VOICE ERROR] DynamicPlaylist grammar failed: {ex.Message}")
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
