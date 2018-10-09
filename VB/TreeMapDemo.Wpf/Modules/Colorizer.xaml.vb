Imports System
Imports System.Globalization
Imports System.Collections.ObjectModel
Imports System.Windows.Data
Imports System.Windows
Imports DevExpress.Xpf.TreeMap

Namespace TreeMapDemo
    Partial Public Class Colorizer
        Inherits TreeMapDemoModule

        Public Sub New()
            InitializeComponent()
            DataContext = DataLoader.LoadXmlDocumentFromResources("/Data/USLargestCompanies2011.xml")
        End Sub
    End Class

    Public Class ColorizersCollection
        Inherits ObservableCollection(Of ColorizerInfo)

    End Class

    Public Class ColorizerInfo
        Inherits DependencyObject

        Public Property Colorizer() As TreeMapColorizerBase
        Public Property ColorizerName() As String
        Public Property Data() As Object
        Public Property Groups() As GroupDefinitionCollection

        Public Overrides Function ToString() As String
            Return ColorizerName
        End Function
    End Class

    Public Class BoolToObjectConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Boolean AndAlso (DirectCast(value, Boolean)) Then
                Return parameter
            End If
            Return Nothing
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class
End Namespace
