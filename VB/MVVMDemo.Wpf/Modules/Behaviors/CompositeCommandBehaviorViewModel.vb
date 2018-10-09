Imports System.Windows

Namespace MVVMDemo.Behaviors
    Public Class CompositeCommandBehaviorViewModel
        Public Sub Register(ByVal userName As String)
            MessageBox.Show("Registered: " & userName)
        End Sub
        Public Function CanRegister(ByVal userName As String) As Boolean
            Return Not String.IsNullOrEmpty(userName)
        End Function

        Private logEntryIndex As Integer
        Public Sub Log(ByVal log As String)
            LogText = String.Format("Log entry {0}: {1}", logEntryIndex, log)
            logEntryIndex += 1
        End Sub
        Public Overridable Property LogText() As String
    End Class
End Namespace
