ssion Info
Imports MySql. Data. MySqlClient
Public Class SessionInfo
#Region "DB connection strings and such"
Public connectionString As String =
"Database=Computing2;" &
-
"Data Source=localhost;" &
"User Id=toby;" &
"Password=chicken;"
"database=Computing" selects database from the list of mysql databases, use
"show databases" in mysql
database
"data Source = localhost" uses the local machine ip as the data source for the
"User ID=toby" selects the user from table mysql. users called toby with all the
permissions it has
"password=chicken" uses the password "chicken" to check against the password
given int the table above for a login
' connects using mySQL using the connectionstring described above
Dim connection As New MySqlConnection(connectionString)
#End Region
Dim LocalSessionID As Integer
Dim LocalSessionJumpers As New List(Of bouncer)
Dim LocalSesionDetails As New Session
Dim localParentpage As SessionTimetable
Dim localAllJumpers As List(Of bouncer)
Public Sub New(ByVal SessionID As Integer, ByVal ParentPage As SessionTimetable,
ByVal allJupers As List(Of bouncer))
' This call is required by the designer.
InitializeComponent()
Add any initialization after the InitializeComponent() call.
LocalSessionID = SessionID
localParentpage = ParentPage
localAllJumpers = allJupers
End Sub
Private Sub SessionInfo_Load(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles MyBase. Load
getMembers()
getSessionDetails()
End Sub
Private Sub ButtonBack_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonBack. Click
DBCommand ("DELETE FROM 'ContactSessions" WHERE SessionIDRef = " &
LocalSessionID & ";")
For count = 0 To ListView. Items. Count - 1
DBCommand ( _
"INSERT INTO 'ContactSessions' (ContactIDRef, SessionIDRef) " &
"VALUES (" & ListView. Items. Item(count). SubItems. Item(0). Text & ", "
& LocalSessionID & ") ;"
-
NextlocalParentpage. Show( )
Me. Close( )
End Sub
Sub DBCommand (ByVal Query)
Dim command As MySqlCommand
command = New MySqlCommand(Query, connection)
'MsgBox( command. CommandText. ToString)
Try
connection. Open()
command . ExecuteNonQuery ()
Catch ex As MySqlException
MsgBox("Error:" & ex. ErrorCode & " --- " & ex.Message. ToString & vbNewLine
& vbNewLine & ex. ToString)
End Try
connection. Close( )
End Sub
Sub getSessionDetails()
is the command that descrives the data retrieved
Dim query As String = "SELECT * FROM "SessionTable JOIN `memberTypes" ON
SessionTable.MemberTypeKey = MemberTypes. TypeID WHERE "sessionID = " & LocalSessionID
& " . "
this is the command exicuted, using the query described above in the
connection 2 above
Dim command As New MySqlCommand (query, connection)
Try
connection . Open( )
Dim dr As MySql. Data. MySqlClient. MySqlDataReader
dr = command. ExecuteReader( )
' saving all the session information
While dr. Read
LocalSesionDetails.SetSessionID(dr("SessionID") )
LocalSesionDetails.SetDayOfWeek(dr ("DayOfWeek"))
LocalSesionDetails.SetStartTime(dr("StartTime"))
LocalSesionDetails.SetEndTime(dr("EndTime"))
LocalSesionDetails.SetMemberType(dr("TypeDescription"))
End While
connection.Close( )
LabelSessionDay. Text = LocalSesionDetails.GetDateOfWeek. ToString
LabelSessionStartTime.Text = LocalSesionDetails.GetStartTime. ToString
LabelSessionEndTime.Text = LocalSesionDetails.GetEndTime. ToString
LabelBouncerType.Text = LocalSesionDetails.GetMemberType. ToString
Catch ex As MySqlException
MsgBox("Error Retreiving Sessoins:" & ex. ErrorCode & " ---
" &
ex. Message. ToString & vbNewLine & vbNewLine & ex. ToString)
End Try
End Sub
Sub getMembers()
is the command that descrives the data retrieved
Dim query As String = "SELECT * FROM ' contacts' JOIN 'ContactSessions" ON
Contacts. ContactID = ContactSessions.ContactIDRef WHERE ContactSessions.SessionIDRef
=" & LocalSessionID & ";"connection 2 above
'this is the command exicuted, using the query described above in the
Dim command As New MySqlCommand (query, connection)
Dim dr As MySql.Data. MySqlClient. MySqlDataReader
' i is just used as a counter
Dim i As Integer = 0
Try
connection. Open()
' dr here is the reader created above with the command query
dr = command. ExecuteReader( )
While dr. Read
LocalSessionJumpers.Add (New bouncer)
With LocalSessionJumpers(i)
runs bouncer. GetId with the value read from the ID coulumn in
row i in the table "contacts'
then saves the value in part of the array
. SetContactID(dr("ContactID"))
the following do the same as abouve, but use different areas of
the table
. SetFirstName(dr ("FirstName"))
. SetLastName(dr ("LastName"))
. setDoB (dr ( "DoB") )
SetEmail(dr("Email"))
. SetPhone (dr ("Phone"))
End With
i = i + 1
End While
closes database
connection. Close( )
Catch ex As MySqlException
MsgBox("Error Retrieving contacts:" & ex. ErrorCode & " --- " &
ex.Message. ToString & vbNewLine & vbNewLine & ex. ToString)
End Try
WriteOutLists()
End Sub
Private Sub WriteOutLists()
ListView. Items. Clear( )
creates an instance of a bouncer
Dim b As bouncer
' iterates throught the instances of each bouncer
For Each b In LocalSessionJumpers
'creates the table with details and such using each instances information
Dim item As ListViewItem = New ListViewItem(b.GetContactID. ToString)
ListView. Items. Add(item)
item. SubItems . Add (b. GetFirstName)
item. SubItems. Add (b. GetLastName)
Next
End Sub
Private Sub ButtonAddRemoveMembers_Click(ByVal sender As System. Object, ByVal e As
System. EventArgs) Handles ButtonAddRemoveMembers. Click
Dim newPage As New AddRemoveMembers(localAllJumpers, LocalSessionJumpers,
AddressOf UpdateSessionMembersCallback)
newPage . Show( )
End Sub
Page- 215 -Public Function UpdateSessionMembersCallback(ByRef Relations As List(Of bouncer))
As Integer
LocalSessionJumpers = Relations
WriteOutLists()
'Auto sizes columns to width of content
ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
End Function
End Class