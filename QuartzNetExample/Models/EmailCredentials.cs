namespace QuartzNetExample.Models;

public class EmailCredentials
{
    public EmailCredentials(string receiver, string sender, string message)
    {
        Receiver = receiver;
        Sender = sender;
        Message = message;
    }

    public string Receiver { get; set; }
    public string Sender { get; set; }
    public string Message { get; set; }
}