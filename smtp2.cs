using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Text;


namespace MailKitSend
{
  class Program
  {
    static void Main(string[] args)
    {
      // メールの送信に必要な情報
      var smtpHostName = "sg-smtp.qcloudmail.com";
      var smtpPort = 465;                         // or 25
      var smtpAuthUser = "noreply";
      var smtpAuthPassword = "12CrossfiRE#$";

      // メールの内容
      var from = "noreply@info.gameclub.ph";
      var to = "kyh3895@gamil.com";

      var subject = "test";
      var body = "This is test mail. \n 改行です。";
      var textFormat = TextFormat.Text;

      // MailKit におけるメールの情報
      var message = new MimeMessage();

      // 送り元情報  
      message.From.Add(MailboxAddress.Parse(from));

      // 宛先情報  
      message.To.Add(MailboxAddress.Parse(to));

      // 表題  
      message.Subject = subject;

      // 内容  
      var textPart = new TextPart(textFormat)
      {
        Text = body,
      };
      message.Body = textPart;

      using var client = new SmtpClient();

      // SMTPサーバに接続  
      client.Connect(smtpHostName, smtpPort, SecureSocketOptions.Auto);

      if (string.IsNullOrEmpty(smtpAuthUser) == false)
      {
        var EncodeUser = Encoding.UTF8.GetBytes(smtpAuthUser);
        var base64smtpAuthUser = Convert.ToBase64String(EncodeUser);
        // SMTPサーバ認証  
        var EncodePwd = Encoding.UTF8.GetBytes(smtpAuthPassword);
        var base64smtpAuthPassword = Convert.ToBase64String(EncodePwd);
        client.Authenticate(base64smtpAuthUser, base64smtpAuthPassword);
      }

      // 送信  
      client.Send(message);

      // 切断  
      client.Disconnect(true);
    }
  }
}

// body utf-8 encoding
