﻿Imports Microsoft.VisualBasic
Imports System
Public Class XPResourceViewModel
	Inherits BaseViewModel(Of XPResource)
	Private privateName As String
	Public Property Name() As String
		Get
			Return privateName
		End Get
		Set(ByVal value As String)
			privateName = value
		End Set
	End Property

	Public Overrides Sub GetData(ByVal model As XPResource)
		model.Name = Name
	End Sub
End Class