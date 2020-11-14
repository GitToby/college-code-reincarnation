es
Imports MySql. Data. MySqlClient
Public Class MemberTypes
End Class
Public Class member
Private ContactID As Integer
Private FirstName As String
Private LastName As String
Private DoB As Date
Private Email As String
Private phone As String
` These are used when creating the instance of the class inorder to save each
detail
#Region "Setters"
Sub SetContactID(ByRef x)
ContactID = x
End Sub
Sub SetFirstName(ByRef x)
FirstName = x
End Sub
Sub SetLastName (ByRef x)
LastName = X
End Sub
Sub setDoB (ByRef x)
DoB = X
End Sub
Sub SetEmail(ByRef x)
Email = x
End Sub
Sub SetPhone(ByRef x)
phone = x
End Sub
#End Region
' The getters are used when recalling data from the specific instance of the class
#Region "Getters"
Function GetContactID()
GetContactID = ContactID
End Function
Function GetFirstName()
GetFirstName = FirstName
End Function
Function GetLastName( )
GetLastName = LastName
End Function
Function GetDoB( )
GetDoB = DoB
End Function
Function GetEmail()
GetEmail = Email
End Function
Function GetPhone( )
GetPhone = phone
End Function
#End Region
End Class
Public Class coachInherits member
Private CoachingLevel As Integer
End Class
Public Class bouncer
Inherits member
Private MemberLevel As Integer
End Class
Public Class session
Private SessionID As Integer
Private StartTime As TimeSpan
Private EndTime As TimeSpan
Private DayOfWeek As String
Private MemberType As String
#Region "Setters"
Sub SetSessionID(ByRef x)
SessionID = x
End Sub
Sub SetStartTime(ByRef x)
StartTime = x
End Sub
Sub SetEndTime(ByRef x)
EndTime = x
End Sub
Sub SetDayOfWeek(ByRef x)
DayOfWeek = x
End Sub
Sub SetMemberType(ByRef x)
MemberType = x
End Sub
#End Region
#Region "Getter"
Function GetSessionID( )
GetSessionID = SessionID
End Function
Function GetStartTime()
GetStartTime = StartTime
End Function
Function GetEndTime()
GetEndTime = EndTime
End Function
Function GetDateOfWeek( )
GetDateOfWeek = DayOfWeek
End Function
Function GetMemberType( )
GetMemberType = MemberType
End Function
#End Region
End Class