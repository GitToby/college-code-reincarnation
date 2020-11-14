emove Members
Imports MySql. Data. MySqlClient
Imports System. Text. RegularExpressions
Public Class AddRemoveMembers
'Declares a function that will send the "UpdatedListOfRelations" to a function in
the parent form when the update button is clicked
Integer
Delegate Function callback(ByRef UpdatedListOfRelations As List(Of bouncer) ) As
#Region "connection stuffs"
Dim connectionString As String = _
"Database=Computing;" &
"Data Source=localhost;" & _
"User Id=toby; " &
"Password=chicken;"
Dim connection As New MySqlConnection(connectionString)
#End Region
Private Localalljumpers As List(Of bouncer)
Private LocalcontactRelations As List(Of bouncer)
Private UpdateCallBack As callback
`creates an undefined size array to save all the members clicked on the listbox
that is selected
Dim items()
Dim ArrayOfBouncerIDs() As Object
Dim findNumbers As String = "[0-9]+"
Public Sub New(ByRef Alljumpers As List(Of bouncer), ByRef contactRelations As
List (Of bouncer), ByVal UpdateCallbackFunction As callback)
' This call is required by the designer.
InitializeComponent()
'creates a local references
Localalljumpers = Alljumpers
LocalcontactRelations = contactRelations
' this passes the adress of the function to be called when updating the page
UpdateCallBack = UpdateCallbackFunction
End Sub
Private Sub Add_RemoveMembers_Load(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles MyBase. Load
DisplayList(Localalljumpers, LocalcontactRelations)
End Sub
#Region "Button Clicks"
Private Sub ButtonCancel_Click(ByVal sender As System. Object, ByVal e As
System. EventArgs) Handles ButtonCancel. Click
Me. Close( )
End Sub
Private Sub ButtonUpdate_Click(ByVal sender As System. Object, ByVal e As
System. EventArgs) Handles ButtonUpdate. Click
recreates the local contacts list using the current listbox
LocalcontactRelations.Clear()
For count = 0 To CheckedListBoxCurrent. Items. Count - 1 ' the minus 1 is
because the checked list box is a 1 based list
' whereas the list view box is a 0 based list.
Dim jumperIndex As IntegerjumperIndex = Regex.Match(CheckedListBoxCurrent. Items. Item( count),
findNumbers).Value - 1
again the - 1 above is beacuse the list of all jumbers works with a 0
based list, if there wasnt a - 1 then on each update click all the jumpers ids would
increase by one.
LocalcontactRelations.Add(Localalljumpers. Item(jumperIndex))
Next
this calls the function within the parent form, passing the updated local
contact relations through
UpdateCallBack(LocalcontactRelations)
'after this function the code can run indapendantly still, the callback
function is run, then the code runs here after.
Me. Close( )
End Sub
Private Sub ButtonAdd_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonAdd. Click
ItemMove (CheckedListBoxPossible, CheckedListBoxCurrent)
End Sub
Private Sub ButtonRemove_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonRemove. Click
ItemMove (CheckedListBoxCurrent, CheckedListBoxPossible)
End Sub
#End Region
#Region "Subs"
Sub ItemMove(ByVal ListBoxSource, ByVal ListBoxDestination)
'resizes the array to the exact number of checked items
ReDim Preserve items(ListBoxSource.CheckedItems. Count - 1)
'copies all the checkeditems into a the new array to copy into the other box
ListBoxSource. CheckedItems. CopyTo(items, 0)
For count = 0 To items. Length - 1
adds selected items to one list
ListBoxDestination. Items. Add (items (count) )
' removes them from the other
ListBoxSource. Items. Remove(items (count))
Next
sortColumn(ListBoxDestination)
End Sub
Sub DisplayList(ByRef Full As List(Of bouncer), ByRef Current As List(Of bouncer))
Dim x As bouncer ' used to search through the full list
Dim y As bouncer ' used to search through the current list
Dim found As Boolean ' used as an identifyer so copies are not written out
For Each x In Full
For Each y In Current
If x.GetContactID = y.GetContactID Then
found = True
Exit For
Else
found = False
End If
Next
If found Then
&
& y . GetLastName)
CheckedListBoxCurrent. Items. Add (y. GetContactID & "-" & y. GetFirstNameElse
& " " & x. GetLastName)
CheckedListBoxPossible. Items. Add (x. GetContactID & "-" & x. GetFirstName
End If
Next
sortColumn(CheckedListBoxCurrent)
End Sub
sortColumn(CheckedListBoxPossible)
#Region "quicksortInputs"
Sub sortColumn(ByVal listBoxToSort As CheckedListBox)
create the array to sort
CreateArrayOfBouncerIDs(listBoxToSort)
sort the array
qsort (ArrayOfBouncerIDs)
'write out the new, sorted, list
listBoxToSort.Items. Clear()
listBoxToSort. Refresh( )
For count = 0 To ArrayOfBouncerIDs. Length - 1
listBoxToSort.Items. Add (Localalljumpers(ArrayOfBouncerIDs (count) -
1).GetContactID & " - " & Localalljumpers(ArrayOfBouncerIDs(count) - 1).GetFirstName &
& Localalljumpers (ArrayOfBouncerIDs(count) - 1).GetLastName)
Next
End Sub
Sub CreateArrayOfBouncerIDs(ByVal checkedlistboxToRead As CheckedListBox)
' creates an array the size of the number of elements in the list boxes
ReDim ArrayOfBouncerIDs(checkedlistboxToRead.Items.Count - 1)
Dim jumperIndex As Integer
'fills in the array with the ID values found using the regex
For count = 0 To ArrayOfBouncerIDs. Length - 1
'find the number in the list to add to the array
jumperIndex = Regex. Match(checkedlistboxToRead. Items. Item(count) ,
findNumbers).Value
ArrayOfBouncerIDs (count) = jumperIndex
Next
End Sub
#End Region
#End Region
#Region "quicksort"
Sub quicksort(ByRef a() As Object, ByVal Top As Integer, ByVal Bottom As Integer)
Dim pivot As Integer
If Bottom - Top > 1 Then
pivot = getpivot(a, Top, Bottom)
pivot = partition(a, Top, Bottom, pivot)
'recustivly sorts the list a()
quicksort(a, Top, pivot)
quicksort(a, pivot + 1, Bottom)
End If
End Sub
Sub qsort(ByRef a() As Object)
Dim i
Dim iiFor i = 0 To a. Length( ) - 1
'Randomise to ensure no logic errors
ii = New System. Random().Next(0, a.Length( ) - 1)
If i <> ii Then
swap(a(i), a(ii))
End If
Next
quicksort(a, 0, a. Length())
End Sub
Function getpivot(ByRef a() As Object, ByVal Top As Integer, ByVal Bottom As
Integer)
Return New System. Random(). Next (Top, Bottom - 1)
End Function
Function partition(ByRef a() As Object, ByVal Top As Integer, ByVal Bottom As
Integer, ByRef pivot As Integer)
Dim i
Dim piv
Dim store
piv = a(pivot)
swap(a(Bottom - 1), a(pivot))
store = Top
For i = Top To Bottom - 2
If a(i) <= piv Then
swap(a(store), a(i))
store = store + 1
End If
Next
swap(a(Bottom - 1), a(store))
Return store
End Function
Sub swap (ByRef v1, ByRef v2)
Dim tmp
tmp = v1
v1 = v2
v2 = tmp
End Sub
#End Region
End Class