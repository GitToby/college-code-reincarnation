Send Email
Imports System. Net. Mail
Public Class SendEmail
Dim LocalEmailAdrress As String
Dim LocalName As String
Public Sub New(ByVal EmailAdress As String, ByVal firstname As String, ByVal
lasname As String)
' This call is required by the designer.
InitializeComponent()
LocalEmailAdrress = EmailAdress
LocalName = firstname & " " & lasname
End Sub
Private Sub SendEmail_Load(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles MyBase. Load
Me. Text = "Email To: " & LocalName
LableTo. Text = LocalEmailAdrress
'this is the email from section
LableFrom. Text = "toby@thedevlins.biz"
End Sub
Private Sub ButtonSend_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles ButtonSend. Click
Dim sendmessage As Boolean
If TextBoxMessage. Text. Length = 0 And TextBoxSubject. Text. Length = 0 Then
MsgBox("There is nothing in either field, please Put information into the
fields before sending")
sendmessage = False
ElseIf TextBoxSubject. Text. Length = 0 Then
If MsgBox( "There is no Subject, Send anyway?", MsgBoxStyle. YesNo, "no
subject") = vbYes Then
sendmessage = True
Else
sendmessage = False
End If
ElseIf TextBoxMessage. Text. Length = 0 Then
If MsgBox( "There is no body, Send anyway?", MsgBoxStyle. YesNo, "no
subject") = vbYes Then
sendmessage = True
Else
sendmessage = False
End If
Else
sendmessage = True
End If
If sendmessage Then
SendEmail(TextBoxSubject.Text, TextBoxMessage. Text)
End If
End Sub
Sub SendEmail(ByVal Subject As String, ByVal Body As String)
Try
'creates a variable to handle the sending of the email
Dim smtpserver As New SmtpClient()'creates an instance of a mail, with propertys for everything needed in
the email
Dim Email As New MailMessage
'puts my Gmail as the sending account
smtpserver.Credentials = New
Net. NetworkCredential("toby.devlin@gmail.com", "PASSWORD HERE" )
'sets the port to send from
smtpserver.Port = 587
smtpserver. EnableSsl = True
'the location of the Gmail acount to send the mail message
smtpserver.Host = "smtp.gmail.com"
'this from allows the send address to be changed to something of the user
choice
Email. From = New MailAddress("toby@thedevlins.biz")
'selects the to adress(es) this can have more than one, however in this
case i have
' decided to use only one
Email. To.Add(LocalEmailAdrress.ToString)
ads the subject to the mail being sent
Email. Subject = Subject
' specifies that the text input is plain text, not html. if this was true
then styling
could be applied with the right syntax
Email. IsBodyHtml = False
Email. Body = Body
sends the email
smtpserver.Send (Email)
Me . Close( )
'verification that the mail has been sent
MsgBox( "Mail sent")
Catch ex As Exception
'tells the user if there was an authentication error or send error, then
closes
MsgBox("There is no connection:" & vbNewLine & ex. ToString())
Me.Close( )
End Try
End Sub
Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As
System. EventArgs) Handles Cancel. Click
Me. Close()
End Sub
End Class