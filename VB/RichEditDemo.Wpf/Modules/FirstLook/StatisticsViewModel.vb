Imports DevExpress.Mvvm.POCO
Imports System

Namespace RichEditDemo
    Public Class StatisticsViewModel
        Public Shared Function Create(ByVal includeTextboxes As Boolean, ByVal getsStatisctics As Func(Of Boolean, StatisticsData)) As StatisticsViewModel
            Return ViewModelSource.Create(Function() New StatisticsViewModel(includeTextboxes, getsStatisctics))
        End Function
        Protected Sub New(ByVal includeTextboxes As Boolean, ByVal getsStatisctics As Func(Of Boolean, StatisticsData))
            Me.getsStatisctics = getsStatisctics
            Me.IncludeTextboxes = includeTextboxes
        End Sub
        Private ReadOnly getsStatisctics As Func(Of Boolean, StatisticsData)
        Public Overridable Property Statistics() As StatisticsData

        Private includeTextboxes_Renamed As Boolean
        Public Overridable Property IncludeTextboxes() As Boolean
            Get
                Return includeTextboxes_Renamed
            End Get
            Set(ByVal value As Boolean)
                includeTextboxes_Renamed = value
                Statistics = getsStatisctics(IncludeTextboxes)
            End Set
        End Property
    End Class
    Public Class StatisticsData
        Public Sub New(ByVal noSpacesCharacterCount As Integer, ByVal withSpacesCharacterCount As Integer, ByVal wordCount As Integer, ByVal paragraphCount As Integer)
            Me.NoSpacesCharacterCount = noSpacesCharacterCount
            Me.WithSpacesCharacterCount = withSpacesCharacterCount
            Me.WordCount = wordCount
            Me.ParagraphCount = paragraphCount
        End Sub

        Private privateNoSpacesCharacterCount As Integer
        Public Property NoSpacesCharacterCount() As Integer
            Get
                Return privateNoSpacesCharacterCount
            End Get
            Private Set(ByVal value As Integer)
                privateNoSpacesCharacterCount = value
            End Set
        End Property
        Private privateWithSpacesCharacterCount As Integer
        Public Property WithSpacesCharacterCount() As Integer
            Get
                Return privateWithSpacesCharacterCount
            End Get
            Private Set(ByVal value As Integer)
                privateWithSpacesCharacterCount = value
            End Set
        End Property
        Private privateWordCount As Integer
        Public Property WordCount() As Integer
            Get
                Return privateWordCount
            End Get
            Private Set(ByVal value As Integer)
                privateWordCount = value
            End Set
        End Property
        Private privateParagraphCount As Integer
        Public Property ParagraphCount() As Integer
            Get
                Return privateParagraphCount
            End Get
            Private Set(ByVal value As Integer)
                privateParagraphCount = value
            End Set
        End Property
    End Class
End Namespace
