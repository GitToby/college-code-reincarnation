Imports MySql.Data. MySqlClient
Public Class MainMenu
#Region "DB connection strings and such"
Public connectionString As String =
"Database=Computing2;" &
-
"Data Source=localhost;" &
-
"User Id=toby; " &
"Password=chicken; "
' "database=Computing" selects database from the list of mysql databases, use
"show databases" in mysql
"data Source = localhost" uses the local machine ip as the data source for the
database
' "User ID=toby" selects the user from table mysql.users called toby with all the
permissions it has
"password=chicken" uses the password "chicken" to check against the password
given int the table above for a login
'connects using mySQL using the connectionstring described above
Dim connection As New MySqlConnection(connectionString)
' is the command that descrives the data retrieved
Dim query As String = "SELECT * FROM CONTACTS"
'this is the command exicuted, using the query described above in the connection 2
above
Dim command As New MySqlCommand (query, connection)
#End Region
creates a new 'list' of instances of the class ' bouncer' this piece
of storeage will be referenced by nearley every other page in the project
this is done using paraeter passing
Dim allJumpers As New List(Of bouncer)
Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System. EventArgs)
Handles MyBase. Load
getMemberList()
End Sub
Sub getMemberList()
' creates a data reader which reads a specific secton of the database
Dim dr As MySql. Data. MySqlClient. MySqlDataReader
' i is just used as a counter
Dim i As Integer = 0
Try
'opens the database
connection. Open()
dr here is the reader created above with the command line "select *
from contacts"
dr = command. ExecuteReader()
While dr. Read
allJumpers. Add (New bouncer)
With allJumpers(i)
runs bouncer. GetId with the value read from the ID coulumn
in row i in the table 'contacts
then saves the value in part of the array. SetContactID(dr ("ContactID"))
the following do the same as abouve, but use different areas
of the table
. SetFirstName (dr ("FirstName" ) )
SetLastName(dr ( "LastName"))
setDoB(dr ( "DoB"))
SetEmail(dr("Email"))
. SetPhone(dr ("Phone"))
End With
i = i + 1
End While
closes database
connection. Close()
Catch ex As MySqlException
MsgBox("No Connection To Database" & vbNewLine & vbNewLine & "Error:" &
ex. ErrorCode & "
__ " & ex. Message. ToString & vbNewLine & ex. ToString)
End Try
End Sub
Private Sub ButtonSeeMemberList_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonSeeMemberList. Click
'Creates the instance of the nre page, after this line the 'New( )' sub in the
' memberlist form is run.
Dim createdPage As New MemberList(allJumpers)
'show the newly created class instance
createdPage. Show( )
'close the current form class instance
Me. Close()
End Sub
Private Sub ButtonSeeSessionTable_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonSeeSessionTable. Click
Dim createdPage As New SessionTimetable(allJumpers)
createdPage . Show()
Me. Close()
End Sub
End Class