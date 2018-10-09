Imports System.Collections
Imports DevExpress.Xpf.Accordion
Imports DevExpress.Xpf.DemoBase

Namespace NavigationDemo
    <CodeFile("ViewModels/NavigationPaneViewModel.(cs)"), CodeFile("ViewModels/NavigationPane/ContactsNavigationViewModel.(cs)"), CodeFile("Views/Contacts.xaml"), CodeFile("Views/Contacts.xaml.(cs)"), CodeFile("ViewModels/NavigationPane/ContactsViewModel.(cs)"), CodeFile("Views/Mail.xaml"), CodeFile("Views/Mail.xaml.(cs)"), CodeFile("ViewModels/NavigationPane/MailViewModel.(cs)"), CodeFile("Views/Journal.xaml"), CodeFile("Views/Journal.xaml.(cs)"), CodeFile("Views/Notes.xaml"), CodeFile("Views/Notes.xaml.(cs)"), CodeFile("Views/Tasks.xaml"), CodeFile("Views/Tasks.xaml.(cs)")>
    Partial Public Class NavigationPaneDemoModule
        Inherits AccordionDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
    Public Class NavigationChildrenSelector
        Implements IChildrenSelector

        Private Function IChildrenSelector_SelectChildren(ByVal item As Object) As IEnumerable Implements IChildrenSelector.SelectChildren
            If TypeOf item Is GroupDescription Then
                Return DirectCast(item, GroupDescription).Items
            ElseIf TypeOf item Is ItemDescription Then
                Return DirectCast(item, ItemDescription).Items
            End If
            Return Nothing
        End Function
    End Class
End Namespace
