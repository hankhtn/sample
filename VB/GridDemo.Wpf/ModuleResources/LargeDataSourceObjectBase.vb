Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq

Namespace GridDemo
    Public MustInherit Class LargeDataSourceObjectBase
        Protected Class ValuesContainer(Of T)
            Private modifiedValues As Dictionary(Of Integer, T)
            Private ReadOnly getDefaultValue As Func(Of Integer, T)
            Public Sub New(ByVal getDefaultValue As Func(Of Integer, T))
                Me.getDefaultValue = getDefaultValue
            End Sub
            Public Function GetValue(ByVal index As Integer) As T
                If modifiedValues Is Nothing Then
                    Return getDefaultValue(index)
                End If
                Dim value As T = Nothing
                If modifiedValues.TryGetValue(index, value) Then
                    Return value
                End If
                Return getDefaultValue(index)
            End Function
            Public Sub SetValue(ByVal index As Integer, ByVal value As T)
                If modifiedValues Is Nothing Then
                    modifiedValues = New Dictionary(Of Integer, T)()
                End If
                modifiedValues(index) = value
            End Sub
        End Class
        Private Shared ReadOnly Priorities() As Priority = System.Enum.GetValues(GetType(Priority)).Cast(Of Priority)().ToArray()
        Shared Sub New()

        End Sub

        Private Shared Sub GenerateCode()
            Dim sb = New System.Text.StringBuilder()
            For i As Double = 0 To (1000R / 7R) - 1
                Dim startIndex = i * 7 + 1
                sb.AppendFormat("" & ControlChars.CrLf & _
"        [Display(Name = ""From ({0})"", Order = {0})]" & ControlChars.CrLf & _
"        public string From{0} {{ get {{ return fromValues.GetValue({0}); }} set {{ fromValues.SetValue({0}, value); }} }}" & ControlChars.CrLf & _
"        [Display(Name = ""To ({1})"", Order = {1})]" & ControlChars.CrLf & _
"        public string To{1} {{ get {{ return toValues.GetValue({1}); }} set {{ toValues.SetValue({1}, value); }} }}" & ControlChars.CrLf & _
"        [Display(Name = ""Sent ({2})"", Order = {2})]" & ControlChars.CrLf & _
"        public DateTime Sent{2} {{ get {{ return sentValues.GetValue({2}); }} set {{ sentValues.SetValue({2}, value); }} }}" & ControlChars.CrLf & _
"        [Display(Name = ""Has Attachment ({3})"", Order = {3})]" & ControlChars.CrLf & _
"        public bool HasAttachment{3} {{ get {{ return hasAttachmentValues.GetValue({3}); }} set {{ hasAttachmentValues.SetValue({3}, value); }} }}" & ControlChars.CrLf & _
"        [Display(Name = ""Size ({4})"", Order = {4})]" & ControlChars.CrLf & _
"        public int Size{4} {{ get {{ return sizeValues.GetValue({4}); }} set {{ sizeValues.SetValue({4}, value); }} }}" & ControlChars.CrLf & _
"        [Display(Name = ""Priority ({5})"", Order = {5}), GridEditor(TemplateKey = ""priorityColumn"")] " & ControlChars.CrLf & _
"        public Priority Priority{5} {{ get {{ return priorityValues.GetValue({5}); }} set {{ priorityValues.SetValue({5}, value); }} }}" & ControlChars.CrLf & _
"        [Display(Name = ""Subject ({6})"", Order = {6}), GridEditor(TemplateKey = ""subjectEditor"")]" & ControlChars.CrLf & _
"        public string Subject{6} {{ get {{ return subjectValues.GetValue({6}); }} set {{ subjectValues.SetValue({6}, value); }} }}" & ControlChars.CrLf & _
"", startIndex, startIndex + 1, startIndex + 2, startIndex + 3, startIndex + 4, startIndex + 5, startIndex + 6)
            Next i
        End Sub

        Private Const BaseColumnCount As Integer = 7
        Protected ReadOnly fromValues As ValuesContainer(Of String)
        Protected ReadOnly toValues As ValuesContainer(Of String)
        Protected ReadOnly sentValues As ValuesContainer(Of Date)
        Protected ReadOnly hasAttachmentValues As ValuesContainer(Of Boolean)
        Protected ReadOnly sizeValues As ValuesContainer(Of Integer)
        Protected ReadOnly priorityValues As ValuesContainer(Of Priority)
        Protected ReadOnly subjectValues As ValuesContainer(Of String)
        Public Sub New(ByVal id As Integer)
            Me.Id = id
            fromValues = New ValuesContainer(Of String)(Function(index) OutlookData.Users(GetPseudoRandomValue(Me.Id, index, OutlookData.Users.Length)).Name)
            toValues = New ValuesContainer(Of String)(Function(index) OutlookData.Users(GetPseudoRandomValue(Me.Id, index + 5, OutlookData.Users.Length)).Name)
            sentValues = New ValuesContainer(Of Date)(Function(index) Date.Today.AddDays(GetPseudoRandomValue(Me.Id, index, 30)))
            hasAttachmentValues = New ValuesContainer(Of Boolean)(Function(index)If(GetPseudoRandomValue(Me.Id, index, 2) = 0, True, False))
            sizeValues = New ValuesContainer(Of Integer)(Function(index) GetPseudoRandomValue(Me.Id, index, 10000))
            priorityValues = New ValuesContainer(Of Priority)(Function(index) Priorities(GetPseudoRandomValue(Me.Id, index, Priorities.Length)))
            subjectValues = New ValuesContainer(Of String)(Function(index) OutlookDataGenerator.Subjects(GetPseudoRandomValue(Me.Id, index, OutlookDataGenerator.Subjects.Length)))
        End Sub
        Private privateId As Integer
        <Display(Name := "Id (0)", Order := 0)>
        Public Property Id() As Integer
            Get
                Return privateId
            End Get
            Private Set(ByVal value As Integer)
                privateId = value
            End Set
        End Property

        Private Function GetPseudoRandomValue(ByVal rowIndex As Integer, ByVal columnIndex As Integer, ByVal maxValue As Integer) As Integer
            Return (rowIndex + columnIndex) Mod maxValue
        End Function
    End Class
End Namespace
