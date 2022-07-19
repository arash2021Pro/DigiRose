using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace DigiRose.CoreStructure.MessageService;

public class MessageService:IMessageService
{
    public MessageOption Option;
    public MessageService(IOptionsSnapshot<MessageOption>Options)
    {
        Option = Options.Value;
    }
    private RestClient _client;
    private RestRequest _request;
    private RestClient GetClient()
    {
        if (_client == null)
        {
            _client =  new RestClient("https://api.kavenegar.com/v1/");
        }
        return _client;
    }
    private RestRequest GetRequest()
    {
        if (_request == null)
        {
            _request = new RestRequest("/sms/send.json",Method.Post);
        }
        return _request;
    }
    
    public async Task<MessageResult> SendMessageAsync(string receptor, string sender, string message)
    {
        var client = GetClient();
        var request = GetRequest();
        request.AddHeader("apikey", Option.ApiKey);
        request.AddParameter("receptor", receptor);
        request.AddParameter("sender", sender);
        request.AddParameter("message", message);
        var response = await client.ExecuteAsync(request);
        
        
        var result = !String.IsNullOrEmpty(response.Content)
            ? JsonConvert.DeserializeObject<MessageResult>(response.Content)
            : new MessageResult(){message = "something went wrong",status = 500,statustext = "Server error"};
     
        return new MessageResult() {message = "error found in server", status = 500, statustext = "ServerError"};
    }
}