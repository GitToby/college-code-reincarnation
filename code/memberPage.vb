Member Page
Imports MySql.Data. MySqlClient
Public Class MemberPage
Private DisplayMember As bouncer
Dim ContactRelations As New List(Of bouncer)
Dim LocalAllJumpers As New List(Of bouncer)
Dim JumperSessions As New List(Of Session)
#Region "connection stuffs"
Dim connectionString As String =
"Database=Computing2;" &
- -
"Data Source=localhost;" &
"User Id=toby;" &
"Password=chicken; "
Dim connection As New MySqlConnection(connectionString)
#End Region
Public Sub New(ByRef LocalJumper As bouncer, ByVal Alljumpers As List(Of bouncer))
" This call is required by the designer.
InitializeComponent()
' Add any initialization after the InitializeComponent() call.
'gets the display information
DisplayMember = LocalJumper
gets a local set of all the jumpers
LocalAllJumpers = Alljumpers
End Sub
Private Sub MemberPage_Load (ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles MyBase. Load
Me. Refresh( )
'gets all the contacts that are linked to the display contact (uses sql in the
sub. )
GetRelationsList( )
gets specific information stored in the class instance and displays it in the
right place
Me. Text = DisplayMember. GetContactID. ToString & "-" &
DisplayMember.GetFirstName & " " & DisplayMember. GetLastName
LabelMemberName. Text = DisplayMember. GetFirstName & " " &
DisplayMember.GetLastName
LabelDoB. Text = DisplayMember. GetDoB
LabelEmail. Text = DisplayMember. GetEmail
LableContactID. Text = DisplayMember. GetContactID
LabelPhone. Text = DisplayMember. GetPhone
WriteOutBouncers(ContactRelations, ListViewContactRelations)
WriteOutSessions()
ListViewContactRelations.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
ListViewContactRelations.GridLines = True
ListViewSessions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
ListViewSessions. GridLines = True
End Sub
Private Sub ButtonBack_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonBack. Click
Me. Close( )
End SubPrivate Sub ButtonEditMember_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonEditMember.Click
' creates and inisialises an instance of the addRemoveMembers passing the list
of jumpers previously created
Dim CreatePage As New EditMemberInfo(DisplayMember, LocalAllJumpers,
ContactRelations, Me, JumperSessions)
CreatePage. Show()
'brings up a new page and when the info is edited and saved then this page is
refreshed
Me.Hide( )
End Sub
Sub GetRelationsList()
Dim query As String = "SELECT * FROM 'Relations' JOIN "contacts" ON
Relations.ContactID2 = Contacts. ContactID WHERE (Relations.ContactID1 = " &
DisplayMember.GetContactID & ");"
Dim command As New MySqlCommand (query, connection)
Try
' creates a data reader which reads a specific secton of the database
Dim dr As MySql. Data.MySqlClient. MySqlDataReader
' i is just used as a counter
Dim i As Integer = 0
opens the database
connection. Open()
dr here is the reader created above with the command line "select * from
contacts"
dr = command. ExecuteReader( )
While dr. Read
ContactRelations.Add(New bouncer)
With ContactRelations(i)
' runs bouncer. GetContactId with the value read from the ID
coulumn in row i in the table contacts
then saves the value in part of the list
. SetContactID(dr("ContactID"))
the following do the same as above, but use different areas of
the table
. SetFirstName(dr("FirstName"))
.SetLastName (dr ( "LastName"))
. setDoB (dr ( "DoB") )
. SetEmail(dr("Email"))
. SetPhone (dr("Phone"))
End With
i = i + 1
End While
' closes database
connection.Close()
Catch ex As MySqlException
MsgBox("Error:" & ex. ErrorCode & " --- " & ex.Message. ToString & vbNewLine
& vbNewLine & ex. ToString)
End Try
End Sub
Public Sub WriteOutBouncers(ByVal List As List(Of bouncer), ByRef ListBox As
Object)
ListBox.Items. Clear( )
If List. Count = 0 Then
Else
creates an instance of a bouncerDim b As bouncer
' iterates throught the instances of each bouncer
For Each b In List
information
creates the table with details and such using each instances
Dim item As ListViewItem = New ListViewItem(b.GetContactID. ToString)
ListBox. Items. Add (item)
item. SubItems. Add (b. GetFirstName)
item. SubItems. Add(b. GetLastName)
item. SubItems.Add(b.GetEmail)
item. SubItems. Add (b.GetPhone)
Next
End If
End Sub
Sub WriteOutSessions()
'THIS HALF OF THE SUB IS A VARIENT OF THE GetRelationsList() HOWEVER DUE TO
DIFFERENT CLASS USES AND PROPERTIES THE TWO CANNOT BE MERGED
Dim query As String = "SELECT * FROM " ContactSessions JOIN `SessionTable" ON
ContactSessions. sessionIDRef = SessionTable.SessionID JOIN `MemberTypes" ON
SessionTable. MemberTypeKey = MemberTypes. TypeID WHERE ContactIDRef = " &
DisplayMember.GetContactID & " ORDER BY DayOfWeek;"
Dim command As New MySqlCommand (query, connection)
Try .
connection.Open()
Dim dr As MySql. Data. MySqlClient. MySqlDataReader
dr = command. ExecuteReader()
Dim i As Integer
While dr. Read
JumperSessions.Add(New session)
With JumperSessions (i)
SetSessionID(dr( "SessionID"))
. SetStartTime(dr("StartTime"))
.SetEndTime(dr( "EndTime"))
. SetDayOfWeek (dr("DayOfWeek"))
. SetMemberType(dr("TypeDescription"))
End With
i = i + 1
End While
connection.Close( )
Catch ex As MySqlException
MsgBox( "Error:" & ex. ErrorCode & " --- " & ex. Message. ToString & vbNewLine
& vbNewLine & ex. ToString)
End Try
'THIS HALF WRITES UP THE SESSIONS TO THE LIST
ListViewSessions.Items. Clear()
If JumperSessions. Count = 0 Then
'Do Nothing
Else
creates an instance of a bouncer
Dim s As session
' iterates throught the instances of each bouncer
For Each s In JumperSessions
creates the table with details and such using each instances
information
Dim item As ListViewItem = New ListViewItem(s.GetSessionID. ToString)
ListViewSessions. Items. Add (item)item. SubItems.Add(s. GetDateOfWeek)
item. SubItems.Add(s.GetStartTime. ToString)
item.SubItems.Add(s.GetEndTime. ToString)
item. SubItems. Add(s. GetMemberType)
Next
End If
End Sub
Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles Button1. Click
Dim CreatePage As New SendEmail(DisplayMember.GetEmail,
DisplayMember. GetFirstName, DisplayMember. GetLastName)
CreatePage. Show( )
End Sub
End Class