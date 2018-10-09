Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows
Imports System.Xml.Linq
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Charts
Imports System.Linq

Namespace ChartsDemo
    <CodeFile("Modules/Interactivity/CurrencyExchangeRates.xaml"), CodeFile("Modules/Interactivity/CurrencyExchangeRates.xaml.(cs)"), CodeFile("ViewModels/ExchangeRatesModel.(cs)")>
    Partial Public Class CurrencyExchangeRates
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
End Namespace
