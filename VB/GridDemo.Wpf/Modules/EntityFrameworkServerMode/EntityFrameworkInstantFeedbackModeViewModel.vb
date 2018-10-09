Imports DevExpress.Xpf.DemoBase
Imports System.Collections.Generic
Imports System.Data.SQLite

Namespace GridDemo
    Public Class EntityFrameworkInstantFeedbackModeViewModel
        Inherits InstantFeedbackModeViewModelBase(Of OutlookDataRecord)

        #Region "DatabaseInfo"
        Public Shared ReadOnly DatabaseInfo As New DatabaseInfo(OutlookDataContext.FileName, "OutlookDataRecords", GetType(OutlookDataRecord), AddressOf CreateValues, Function(sql, connection) New SQLiteCommand(sql, CType(connection, SQLiteConnection)))
        Private Shared Function CreateValues() As Dictionary(Of String, Object)
            Dim subject = OutlookDataGenerator.GetSubject()
            Dim hasAttachment = OutlookDataGenerator.GetHasAttachment()
            Dim size = OutlookDataGenerator.GetSize(hasAttachment)
            Dim [from] = OutlookDataGenerator.GetFrom()
            Dim sent = OutlookDataGenerator.GetSentDate()
            Dim dict = New Dictionary(Of String, Object)()
            dict.Add("Subject", subject)
            dict.Add("HasAttachment", hasAttachment)
            dict.Add("Size", size)
            dict.Add("From", [from])
            dict.Add("Sent", sent)
            Return dict
        End Function
        #End Region

        Protected Sub New()
            MyBase.New(DatabaseInfo, Function() (New OutlookDataContext()).OutlookDataRecords)
        End Sub
    End Class
End Namespace
