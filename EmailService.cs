using System.Text.RegularExpressions;
using MailKit.Net.Smtp;
using MimeKit;
namespace BankConsole;

public static class EmailService{
    public static void SendMail(){
        var message=new MimeMessage();
        message.From.Add(new MailboxAddress("Pedro Pardo","pedropardog009@gmail.com"));
        message.To.Add(new MailboxAddress("Admin","pedropardog009@gmail.com"));
        message.Subject="BankConsole:Usuarios nuevos";
        
        message.Body=new TextPart("plain"){
                Text = GetEmailText()
            };

        using(var client=new SmtpClient()){
            client.Connect("smtp.gmail.com",587,false);
            client.Authenticate("pedropardog009@gmail.com","oyahqscugzdacqfm");// correo y contrasena(proporcionada por google para aplicacion de terceros)
            client.Send(message);
            client.Disconnect(true);
        }
    }    

    private static string GetEmailText(){
        List<User> newUsers=Storage.GetNewUsers();
        
        if(newUsers.Count ==0)
            return "No hay usuarios nuevos.";
        
        string emailText="Usuarios agregados hoy:\n";
        
        foreach(User user in newUsers)
            emailText+="\t+" + user.ShowData()+"\n";
        
        return emailText;
    }       

   

    public static Regex ValidEmailRegex = CreateValidEmailRegex();

    private static Regex CreateValidEmailRegex()
    {
        string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
    }

    internal static bool EmailIsValid(string emailAddress)
    {
        
        bool isValid = ValidEmailRegex.IsMatch(emailAddress);
        if(!isValid) Console.Write("El correo es invalido, intente de nuevo\nEmail:");
        return isValid;
    }
   
}