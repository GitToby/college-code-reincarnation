Imports MySql. Data. MySqlClient
Imports System. Text. RegularExpressions
Public Class EditMemberInfo
Dim connectionString As String =
"Database=Computing2; " &
"Data Source=localhost;" &
-
"User Id=toby;" &
"Password=chicken; "
Dim connection As New MySqlConnection(connectionString)
Dim DoBValid As Boolean
Dim EmailValid As Boolean
Dim PhoneValid As Boolean
Dim regexName As String = ("^[A-Z]{1} [a-z]+$")
Dim regexEmail As String = ("^[a-zA-Z][\w. - ]*[a-zA-Z0-9]@[a-zA-z0-9][\w - - ]*[a-
ZA-Z0-9]. [a-zA-Z][a-zA-Z. ]*[a-zA-Z]$")
Dim regexPhone As String = ("^0[0-9]{10}$")
Dim Localalljumpers As List(Of bouncer)
Dim DisplayRelations As List(Of bouncer)
Dim DisplayJumper As bouncer
Dim localJumperSessions As List(Of Session)
Dim LocalParentPage As MemberPage
Public Sub New(ByRef jumper As bouncer, ByRef Alljumpers As List(Of bouncer),
ByRef relations As List(Of bouncer), ByVal parentpage As MemberPage, ByRef
jumperSessions As List(Of Session))
' This call is required by the designer.
InitializeComponent()
' Add any initialization after the InitializeComponent() call.
creates referencefor the local display member, all the jumpers and the
relations for the display member
DisplayJumper = jumper
Localalljumpers = Alljumpers
DisplayRelations = relations
LocalParentPage = parentpage
localjumperSessions = jumperSessions
End Sub
Private Sub EditMemberInfo_Load(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles MyBase. Load
Me. Refresh( )
'sets the headlines of the page and the lables
Me. Text = DisplayJumper.GetContactID. ToString & "-" &
DisplayJumper. GetFirstName & " " & DisplayJumper. GetLastName
LabelMemberName. Text = DisplayJumper.GetFirstName & " " &
DisplayJumper. GetLastName
resets the page, recreating the original data from the start
reset( )
'creates the list view to show only the right size data
ListViewContactRelations.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)End Sub
Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonCancel. Click
LocalParentPage. Show( )
Me. Close( )
End Sub
Private Sub ButtonSaveToDB_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonUpdateAll. Click
'selects wether the inputsa are true or not.
If EmailValid = True And DoBValid = True And PhoneValid = True Then
'this is the command that tells the database what to do by passing the
cammand into a sepurate db hadlind sub
first statement: Updating infomation into the contacts table
DBCommand (_
"UPDATE "contacts" SET" &
" DoB = "" & DateTimePicker1. Value. ToString("yyyy-MM-dd" ) & " " , " &
-
" Email = '" & TextBoxNewEmail. Text. ToString &
&
" Phone = "" & TextBoxNewPhone. Text. ToString & "" "
&
"WHERE ContactID = " & DisplayJumper. GetContactID & ";
'Second and thrid SQL statments update the
second statement: Removes any current relations inorder to refesh the
contacts relations
DBCommand ( "DELETE FROM `Relations WHERE ContactID1 = " &
DisplayJumper. GetContactID & ";")
' third statement: iterated to insert infomation into the relations table
If ListViewContactRelations.Items. Count - 1 > 0 Then
For count = 0 To ListViewContactRelations.Items.Count - 1
DBCommand (_
"INSERT INTO `Relations (ContactID1, ContactID2)" &
"VALUES (" & Localalljumpers. Item(Localalljumpers.Count -
1).GetContactID + 1 & ", " &
ListViewContactRelations.Items. Item(count).SubItems. Item(0). Text & ") ;" _
Next
Else
MsgBox( "Please Note: No contacts selected for Member " &
DisplayJumper. GetFirstName & " " & DisplayJumper. GetLastName)
End If
' fourth statement: reseting the ContactSession for the specific member
DBCommand ("DELETE FROM 'ContactSessions' WHERE ContactIDRef = " &
DisplayJumper. GetContactID & ";")
' fifth statements: Adds to each session where applicable
Dim SessionAdded As Boolean = False
If CheckBoxAttendingSession1.CheckState = CheckState. Checked Then
DBCommand ("INSERT INTO 'ContactSessions' (ContactIDRef, SessionIDRef)
VALUES (" & Localalljumpers.Item(Localalljumpers.Count - 1).GetContactID + 1 & ", " & 1
& "); ")
SessionAdded = True
End If
If CheckBoxAttendingSession2.CheckState = CheckState. Checked Then
DBCommand("INSERT INTO 'ContactSessions' (ContactIDRef, SessionIDRef)
& ") ; ")
VALUES (" & Localalljumpers.Item(Localalljumpers.Count - 1).GetContactID + 1 & "," & 2SessionAdded = True
End If
If CheckBoxAttendingSession3. CheckState = CheckState. Checked Then
DBCommand ("INSERT INTO 'ContactSessions" (ContactIDRef, SessionIDRef)
& " ); " )
VALUES (" & Localalljumpers.Item(Localalljumpers.Count - 1).GetContactID + 1 & "," & 3
SessionAdded = True
End If
If Not SessionAdded Then
MsgBox ("Please Note: No sessions selected for Member " &
DisplayJumper. GetFirstName & " " & DisplayJumper.GetLastName)
End If
'closes page and restarts into into the member page
LocalParentPage. Close()
Dim newpage As New MemberPage(DisplayJumper, Localalljumpers)
newpage. Show( )
Me. Close()
Else
MsgBox("One or more fields are not valid, please make sure all the fields
are green before pressing save")
End If
End Sub
Sub DBCommand (ByVal Query)
Dim command As MySqlCommand
command = New MySqlCommand(Query, connection)
whitebox test msgbx below to visualise the input statement before code is
run
'MsgBox ( command . CommandText. ToString)
Try
connection. Open()
command . ExecuteNonQuery ()
Catch ex As MySqlException
gives the sql error code and the descrition for sorting out later
MsgBox("Error:" & ex. ErrorCode & " --- " & ex.Message. ToString & vbNewLine
& vbNewLine & ex. ToString)
End Try
connection.Close( )
End Sub
Private Sub ButtonAddContactRelation_Click(ByVal sender As System.Object, ByVal e
As System. EventArgs) Handles ButtonAddContactRelation. Click
'Passes the ADRESSOF the callback function within the EditMemberInfoClass to
be called later
Dim CreatePage As New AddRemoveMembers(Localalljumpers, DisplayRelations,
AddressOf UpdateContactRelationsCallback)
CreatePage. Show( )
End Sub
Private Sub ButtonReset_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonReset. Click
reset()
End Sub
Sub reset( )
'sets the current labels to show the current stored data
LabelCurrentDoB. Text = DisplayJumper. GetDoB
LabelCurrentEmail.Text = DisplayJumper.GetEmail. ToStringEnd Sub
#Region "Textbox changes"
Private Sub DateTimePicker1 ValueChanged(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles DateTimePicker1. ValueChanged
Dim datenow As Date = Date. Now
Dim dateGiven As Date = DateTimePicker1. Text
LabelAge. Text = DateDiff(DateInterval. Year, dateGiven, datenow)
If LabelAge. Text > 5 And LabelAge.Text <= 60 Then
ApplyTrueValidation (LabelAge, DoBValid)
Else
End If
ApplyFalseValidation(LabelAge, DoBValid)
End Sub
Private Sub TextBoxPhone_TextChanged(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles TextBoxNewPhone. TextChanged
If checkValidation(TextBoxNewPhone. Text, regexPhone) = True Then
ApplyTrueValidation (TextBoxNewPhone, PhoneValid)
Else
ApplyFalseValidation(TextBoxNewPhone, PhoneValid)
End If
End Sub
Private Sub TextBoxEmail_TextChanged(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles TextBoxNewEmail. TextChanged
If checkValidation(TextBoxNewEmail.Text, regexEmail) = True Then
ApplyTrueValidation(TextBoxNewEmail, EmailValid)
Else
ApplyFalseValidation(TextBoxNewEmail, EmailValid)
End If
End Sub
Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e
As System. EventArgs)
End Sub
#End Region
Function checkValidation(ByVal Input, ByVal RegexString) As Boolean
' find a match in the input that complies with the Specific regex string
passed into
Dim m As Match = Regex. Match(Input, RegexString)
'checks the validation of the match
checkValidation = (m.Success)
End Function
#Region "TRUE/FALSE Validation Aplications"
Sub ApplyTrueValidation(ByRef Textbox As Object, ByRef validStatement As Boolean)
Textbox. backcolor = Color. LightGreen
validStatement = True
End Sub
Sub ApplyFalseValidation(ByRef Textbox As Object, ByRef validStatement As Boolean)
Textbox. backcolor = Color. Red
validStatement = False
End Sub
#End Region
Public Function UpdateContactRelationsCallback(ByRef Relations As List(Of
bouncer) ) As Integer
DisplayRelations = Relations
WriteOutLists(DisplayRelations, ListViewContactRelations)
'Auto sizes columns to width of contentListViewContactRelations.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
End Function
End Class