
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
        ' Ignore low confidence hits
        If e.Result.Confidence < 0.65F Then Return

        Dim grammarName As String = e.Result.Grammar.Name

        Select Case grammarName
            Case "Controls"
                ' Handle "play", "pause", "stop", "next", "previous"
                Dim commandText As String = e.Result.Text.Trim().ToLowerInvariant()
                If commandText.StartsWith("skye ") Then commandText = commandText.Substring(5).Trim()
                RaiseEvent CommandRecognized(commandText)

            Case "DynamicPlaylist"
                ' Retrieve the unique key (filename) linked to the phrase
                If e.Result.Semantics.Value IsNot Nothing Then
                    Dim targetKey As String = e.Result.Semantics.Value.ToString()
                    RaiseEvent PlayTargetRequested(targetKey)
                End If
        End Select
    End Sub

    ''' <summary>
    ''' Asynchronously builds speech grammars on a background thread.
    ''' </summary>
    Public Async Function LoadGrammarAsync(ByVal phraseToKeyMap As Dictionary(Of String, String)) As Task
        If recognizer Is Nothing Then Return

        Await Task.Run(Sub()
                           Try
                               recognizer.UnloadAllGrammars()

                               ' 1. Static Control Commands ("Skye, play", "Skye, stop", etc.)
                               Dim controls As New Choices()
                               controls.Add({"play", "pause", "stop", "next", "previous"})

                               Dim controlBuilder As New GrammarBuilder()
                               controlBuilder.Append("skye") ' Wake word
                               controlBuilder.Append(controls)

                               Dim controlGrammar As New Grammar(controlBuilder) With {.Name = "Controls"}
                               recognizer.LoadGrammar(controlGrammar)


                               ' 2. Dynamic Playlist Commands ("Skye, play {Song/Artist}")
                               If phraseToKeyMap IsNot Nothing AndAlso phraseToKeyMap.Count > 0 Then
                                   Dim choicesList As New List(Of GrammarBuilder)()

                                   For Each pair In phraseToKeyMap
                                       Dim semVal As New SemanticResultValue(pair.Key, pair.Value)
                                       choicesList.Add(New GrammarBuilder(semVal))
                                   Next

                                   Dim songChoices As New Choices(choicesList.ToArray())

                                   Dim playBuilder As New GrammarBuilder()
                                   playBuilder.Append("skye") ' Wake word
                                   playBuilder.Append("play")
                                   playBuilder.Append(songChoices)

                                   Dim dynamicGrammar As New Grammar(playBuilder) With {.Name = "DynamicPlaylist"}
                                   recognizer.LoadGrammar(dynamicGrammar)
                               End If

                           Catch ex As Exception
                               System.Diagnostics.Debug.WriteLine($"Error loading grammar: {ex.Message}")
                           End Try
                       End Sub)
    End Function

End Class
