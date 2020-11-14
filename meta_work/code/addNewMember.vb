Imports MySql. Data. MySqlClient
Imports System. Text. RegularExpressions
Public Class AddNewMember
#Region "DB connection strings and such"
Dim connectionString As String =
"Database=Computing2;" &
"Data Source=localhost;" &
"User Id=toby;" &
"Password=chicken;'
. ..
' "database=Computing" selects database from the list of mysql databases, use
"show databases" in mysql
"data Source = localhost" uses the local machine ip as the data source for the
database
' "User ID=toby" selects the user from table mysql.users called toby with all the
permissions it has
' "password=chicken" uses the password "chicken" to check against the password
given int the table above for a login
'connects using mySQL using the connectionstring described
Dim connection As New MySqlConnection(connectionString)
#End Region
Dim EmailValid As Boolean = False
Dim FirstValid As Boolean = False
Dim LastValid As Boolean = False
Dim DoBValid As Boolean = False
Dim PhoneValid As Boolean = False
Dim regexName As String = ("^[A-Z]{1} [a-z]+$")
Dim regexEmail As String = ("^[a-zA-Z][w . - ]*[a-zA-z0-9]@[a-zA-z0-9][w . - ]*[a-
zA-Z0-9]. [a-zA-Z][a-zA-Z.]*[a-zA-Z]$")
Dim regexPhone As String = ("^0[0-9]{10}$")
Dim LocalAllJumpers As List(Of bouncer)
Dim LocalContactRelations As New List(Of bouncer)
Public Sub New(ByRef Alljumpers As List(Of bouncer), ByVal ContactRelations As
List (Of bouncer))
' This call is required by the designer.
InitializeComponent()
' Add any initialization after the InitializeComponent() call.
LocalAllJumpers = Alljumpers
LocalContactRelations = ContactRelations
End Sub
Private Sub AddNewMember_Load(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles MyBase. Load
TextBoxEmail. BackColor = Color. Red
TextBoxFirst. BackColor = Color. Red
TextBoxLast. BackColor = Color. Red
TextBoxPhone. BackColor = Color. Red
LabelAge. BackColor = Color.Red
End Sub
Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonCancel. Click
Dim createdPage As New MemberList(LocalAllJumpers)createdPage. Show( )
Me. Close( )
End Sub
Private Sub ButtonAddContactRelation_Click(ByVal sender As System.Object, ByVal e
As System. EventArgs) Handles ButtonAddContactRelation. Click
creates and inisialises an instance of the addRemoveMembers passing the list
of jumpers previously created
Dim CreatePage As New AddRemoveMembers(LocalAllJumpers, LocalContactRelations,
AddressOf UpdateContactRelationsCallback)
CreatePage. Show( )
'Auto sizes columns to width of content
ListViewContactRelations.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
End Sub
Private Sub ButtonSave_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonSave. Click
` THIS SUB IS RUN TO SAVE ALL THE NEW INFO TO THE DATABASE
' checks all the validation
If EmailValid = True And FirstValid = True And LastValid = True And DoBValid =
True And PhoneValid = True Then
'this is the command that tells the database what to do by passing the
cammand into a sepurate db hadlind sub
first statement: inserting infomation into the contacts table
DBCommand (
"INSERT INTO `Contacts (FirstName, LastName, DoB, Email, Phone)" &
"VALUES ( '" & TextBoxFirst. Text & "', '" & TextBoxLast. Text & "".
&
DateTimePicker1.Value. ToString("yyyy-MM-dd") & "', '" & TextBoxEmail. Text &
&
TextBoxPhone. Text & "' ) ; "
second statement: iterated to insert infomation into the relations table
If ListViewContactRelations.Items. Count - 1 > 0 Then
For count = 0 To ListViewContactRelations.Items. Count - 1
DBCommand (
INSERT INTO Relations (ContactID1, ContactID2)" &
"VALUES (" & LocalAllJumpers. Item(LocalAllJumpers. Count -
1).GetContactID + 1 & ", " &
ListViewContactRelations.Items.Item(count).SubItems. Item(0).Text & ") ; " _
Next
Else
MsgBox("Please Note: No contacts added for New Member " &
TextBoxFirst. Text & " " & TextBoxLast. Text)
End If
' Third statements: Adds to each session where applicable
Dim SessionAdded As Boolean = False
If CheckBoxAttendingSession1.CheckState = CheckState. Checked Then
DBCommand ("INSERT INTO 'ContactSessions" (ContactIDRef, SessionIDRef)
VALUES (" & LocalAllJumpers.Item(LocalAllJumpers.Count - 1).GetContactID + 1 & ", " & 1
& " ) ; " )
SessionAdded = True
End If
If CheckBoxAttendingSession2.CheckState = CheckState. Checked Then
DBCommand ("INSERT INTO 'ContactSessions' (ContactIDRef, SessionIDRef)
VALUES (" & LocalAllJumpers.Item(LocalAllJumpers.Count - 1).GetContactID + 1 & ", " & 2
& "); ")
SessionAdded = True
End IfIf CheckBoxAttendingSession3.CheckState = CheckState. Checked Then
DBCommand ("INSERT INTO `ContactSessions" (ContactIDRef, SessionIDRef)
VALUES (" & LocalAllJumpers.Item(LocalAllJumpers.Count - 1).GetContactID + 1 & "," & 3
& ") ; ")
SessionAdded = True
End If
If Not SessionAdded Then
MsgBox( "Please Note: No sessions added for New Member " &
TextBoxFirst. Text & " " & TextBoxLast. Text)
End If
TextBoxEmail. Text = ""
TextBoxFirst. Text =
TextBoxLast. Text = ""
TextBoxPhone. Text =
LabelAge. Text = ""
ListViewContactRelations.Clear()
Dim newpage As New MainMenu( )
newpage. Show( )
Me. Close( )
Else
MsgBox( "One or more fields are not valid, please make sure all the fields
are green before pressing save")
End If
End Sub
Sub DBCommand (ByVal Query)
Dim command As MySqlCommand
creates the command with the querey passed
command = New MySqlCommand(Query, connection)
' MsgBox ( command. CommandText. ToString)
Try
connection. Open()
command . ExecuteNonQuery ()
Catch ex As MySqlException
MsgBox("Error:" & ex. ErrorCode & " --- " & ex. Message. ToString & vbNewLine
& vbNewLine & ex. ToString)
End Try
connection.Close( )
End Sub
#Region "value Changes"
Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles DateTimePicker1. ValueChanged
'finds dated to caluculate age
Dim datenow As Date = Date. Now
Dim dateGiven As Date = DateTimePicker1. Text
'caluculates age
LabelAge. Text = DateDiff(DateInterval.Year, dateGiven, datenow) - 1
'enforces age limit
If LabelAge. Text > 8 And LabelAge. Text <= 60 Then
ApplyTrueValidation(LabelAge, DoBValid)
DoBValid = True
Else
ApplyFalseValidation(LabelAge, DoBValid)
DoBValid = False
End If
End SubPrivate Sub TextBoxEmail_TextChanged(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles TextBoxEmail. TextChanged
' checks wether the input is true
If checkValidation(TextBoxEmail. Text, regexEmail) Then
ApplyTrueValidation(TextBoxEmail, EmailValid)
EmailValid = True
Else
ApplyFalseValidation(TextBoxEmail, EmailValid)
End If
End Sub
Private Sub TextBoxFirst_TextChanged(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles TextBoxFirst. TextChanged
` checks wether the input is true by calling Check validation
If checkValidation(TextBoxFirst. Text, regexName) Then
ApplyTrueValidation(TextBoxFirst, FirstValid)
FirstValid = True
Else
ApplyFalseValidation(TextBoxFirst, FirstValid)
End If
End Sub
Private Sub TextBoxLast_TextChanged(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles TextBoxLast. TextChanged
" checks wether the input is true
If checkValidation(TextBoxLast. Text, regexName) Then
ApplyTrueValidation(TextBoxLast, LastValid)
LastValid = True
Else
ApplyFalseValidation(TextBoxLast, LastValid)
End If
End Sub
Private Sub TextBoxPhone_TextChanged (ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles TextBoxPhone. TextChanged
' checks wether the input is true
If checkValidation(TextBoxPhone. Text, regexPhone) Then
ApplyTrueValidation(TextBoxPhone, PhoneValid)
PhoneValid = True
Else
ApplyFalseValidation(TextBoxPhone, PhoneValid)
End If
End Sub
#End Region
#Region "TRUE/FALSE Validation Aplications"
Sub ApplyTrueValidation(ByVal Textbox As Object, ByVal validStatement As Boolean)
'changes coulour to show the change in validatioin
Textbox. backcolor = Color. LightGreen
'changes the valid staement passed
validStatement = True
'the Falsesub is a copy but with Red and False respectivy
End Sub
Sub ApplyFalseValidation(ByVal Textbox As Object, ByVal validStatement As Boolean)
Textbox.backcolor = Color. Red
validStatement = False
End Sub
#End Region
Function checkValidation(ByVal Input, ByVal RegexString) As Boolean
find a match in the input that complies with the Specific regex string
passed intoDim m As Match = Regex.Match(Input, RegexString)
'checks the validation of the match
checkValidation = (m.Success)
End Function
Public Function UpdateContactRelationsCallback(ByRef Relations As List(Of
bouncer) ) As Integer
ListViewContactRelations.Items. Clear( )
LocalContactRelations = Relations
Dim b As bouncer
' iterates throught the instances of each bouncer
For Each b In LocalContactRelations
'creates the table with details and such using each instances information
Dim item As ListViewItem = New ListViewItem(b.GetContactID. ToString)
ListViewContactRelations.Items.Add(item)
item. SubItems. Add (b.GetFirstName. ToString)
item. SubItems. Add (b. GetLastName)
item.SubItems.Add (b.GetEmail)
Next
ListViewContactRelations.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
End Function
End Class