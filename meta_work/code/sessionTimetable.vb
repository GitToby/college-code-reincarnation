Imports MySql. Data. MySqlClient
Public Class SessionTimetable
Dim LocalAllJupers As List(Of bouncer)
#Region "DB connection strings and such"
Public connectionString As String = _
"Database=Computing; " &
"Data Source=localhost; " &
1 -
"User Id=toby; " &
"Password=chicken;"
' "database=Computing" selects database from the list of mysql databases, use
"show databases" in mysql
database
"data Source = localhost" uses the local machine ip as the data source for the
"User ID=toby" selects the user from table mysql.users called toby with all the
permissions it has
"password=chicken" uses the password "chicken" to check against the password
given int the table above for a login
'connects using mySQL using the connectionstring described above
Dim connection As New MySqlConnection(connectionString)
' is the command that descrives the data retrieved
Dim query As String = ""
'this is the command exicuted, using the query described above in the connection 2
above
Dim command As New MySqlCommand (query, connection)
#End Region
Dim SessionDetails As Object
Public Sub New(ByVal allJumpers)
' This call is required by the designer.
InitializeComponent()
LocalAllJupers = allJumpers
' Add any initialization after the InitializeComponent() call.
End Sub
Private Sub SessionTimetable_Load(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles MyBase. Load
End Sub
Private Sub ButtonBack_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonBack. Click
MainMenu. Show( )
Me. Hide( )
End Sub
#Region "ButtonClick"
Private Sub ButtonSessionOne_Click(ByVal sender As System. Object, ByVal e As
System. EventArgs) Handles ButtonSessionOne. Click
ButtonClick(1)
End Sub
Private Sub ButtonSessionTwo_Click(ByVal sender As System. Object, ByVal e As
System. EventArgs) Handles ButtonSessionTwo. Click
ButtonClick(2)
End SubPrivate Sub ButtonSessionThree_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonSessionThree.Click
ButtonClick(3)
End Sub
#End Region
Sub ButtonClick(ByVal SessionID)
'creates the new instnace of the "sessionInfo" and passes itself as a
parameter
Dim NewPage As New SessionInfo(SessionID, Me, LocalAllJupers)
shows the new page
NewPage. Show( )
'hides the current page
Me. Hide( )
End Sub
End Class