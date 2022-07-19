namespace DigiRose.CoreStructure.MessageService;

public interface IMessageService
{
    Task<MessageResult> SendMessageAsync(string receptor, string sender, string message);
}