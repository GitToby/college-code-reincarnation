Imports MySql. Data. MySqlClient
Imports System. Text. RegularExpressions
Public Class MemberList
#Region "DB connection strings and such"
Public connectionString As String =
-
"Database=Computing2; " &
"Data Source=localhost;" & _
"User Id=toby; " &
"Password=chicken;"
"database=Computing" selects database from the list of mysql databases, use
"show databases" in mysql
"data Source = localhost" uses the local machine ip as the data source for the
database
"User ID=toby" selects the user from table mysql. users called toby with all the
permissions it has
' "password=chicken" uses the password "chicken" to check against the password
given int the table above for a login
'connects using mySQL using the connectionstring described above
Dim connection As New MySqlConnection(connectionString)
' is the command that descrives the data retrieved
Dim query As String = "SELECT * FROM CONTACTS"
this is the command exicuted, using the query described above in the connection 2
above
Dim command As New MySqlCommand (query, connection)
#End Region
' two lists of bouncers are needed for the search function
Public localAlljumpers As List(Of bouncer)
Dim DisplayList As List(Of bouncer)
Dim ContactsList As New List(Of bouncer)
Private checkedItems() As Integer
Dim LastInputLength As Integer
Public Sub New(ByRef allJumpers As List(Of bouncer))
This call is required by the designer.
InitializeComponent()
' Add any initialization after the InitializeComponent() call.
'creates the local set of all the jumpers
localAlljumpers = allJumpers
End Sub
Private Sub MemberList_Load(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles MyBase. Load
'creates the display list
DisplayList = localAlljumpers
Menu
'Writes out the list of all the members that have been retrieved by the Main
WriteOutLists(DisplayList)
'Auto sizes columns to width of content
ListViewMemberList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
ListViewMemberList.GridLines = True
End SubPrivate Sub ButtonshowMemberPage_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonShowMemberPage.Click
this line makes sure the item is is the correct data type that is returnes
by the . checkedItems method below
Dim item As ListViewItem
For Each item In ListViewMemberList. CheckedItems
Dim index As Integer
index = item. SubItems. Item(0). Text
'passes the jumper whos page has been requested and all the jumpers in the
list.
Dim createdPage As New MemberPage(localAlljumpers(index - 1),
localAlljumpers)
createdPage. Show( )
Next
'Me. Close()
End Sub
Private Sub ButtonAddNewMember_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonAddNewMember. Click
Dim createdPage As New AddNewMember(localAlljumpers, ContactsList)
createdPage.Show( )
Me. Close( )
End Sub
Public Sub WriteOutLists(ByVal List)
ListViewMemberList.Items.Clear()
creates an instance of a bouncer
Dim b As bouncer
` iterates throught the instances of each bouncer
For Each b In List
'creates the table with details and such using each instances information
Dim item As ListViewItem = New ListViewItem(b.GetContactID. ToString)
ListViewMemberList.Items.Add (item)
item. SubItems. Add (b. GetFirstName)
item.SubItems.Add (b.GetLastName)
item. SubItems. Add (b.GetDoB)
item. SubItems. Add(b. GetEmail)
item. SubItems. Add (b.GetPhone)
Next
End Sub
#Region "Search functions"
Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles TextBoxSearch. TextChanged
' This piece of code is used to translate (only) the first character into its
capital cousin and shift the cursour to the right side of the character
If TextBoxSearch. Text. Length = 1 Then
TextBoxSearch.Text = TextBoxSearch. Text . ToUpper
TextBoxSearch. SelectionStart = 1
End If
' Created the Regex string from the search text box
Dim regexMatch As String = TextBoxSearch. Text
' uses The self made regex string to search for the entry in the table
If regexMatch = "" Then
'reset the display list
DisplayList = localAlljumpers
' if theres no input, write the original list
WriteOutLists(DisplayList)
Else28089
Godalming College - Centre number
COMP4 Project
' THE BELOW IF STATEMENT selects wether to use the main stored list, or
wether to use the list being shown
' the selection is done by counting the number of inputs the user has
made, if the number of characters
in the textbox is les than the last number of charaters inputted then
the backspace has been pressed
If TextBoxSearch. Text. Length < LastInputLength Then
'when backspace is pressed:
DisplayList = NewList(regexMatch, localAlljumpers)
Else
'if any other key is presses
DisplayList = NewList (regexMatch, DisplayList)
End If
' writes out the new list that has been searched
WriteOutLists(DisplayList)
' as long as minimal spelling mistakes are made this is multitudes faster
than the original for large sets of data
End If
'set the last number of letters typed in
LastInputLength = TextBoxSearch. Text. Length
End Sub
Function NewList(ByVal RegexString As String, ByVal List1 As List(Of bouncer)) As
List (Of bouncer)
Dim b As bouncer
Dim i As Integer = 0
Dim localList As New List(Of bouncer)
' iterates throught the instances of each bouncer in the list given
For Each b In List1
find a match in the input that complies with the regex string in search
bar
Dim m1 As Match = Regex.Match(List1(i).GetContactID. ToString, RegexString)
Dim m2 As Match = Regex. Match( List1(i) . GetFirstName, RegexString)
Dim m3 As Match = Regex. Match(List1(i). GetLastName, RegexString)
' m1: IDs | m2: First Names | m3: last Names
' Adding an m4 for emails, phones or any other piece in the system is done
by this section: "List1(i) . GetVARIABLE"
If m1.Success Or m2.Success Or m3. Success Then
'if sucessful, add to the list to be written up, otherwise do nothing
localList.Add(List1(i))
End If
' increment to change the bouncer being searched
i = i + 1
Next
NewList = localList
End Function
#End Region
Private Sub ButtonBack_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonBack.Click
MainMenu. Show( )
Me. Close( )
End Sub
Private Sub ButtonRefresh_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonRefresh. Click
ListViewMemberList. Refresh( )
End Sub
End Class
189