namespace RequestService
{
    using Sample.MessageTypes;


    public partial class RequestConsumer
    {
        class SimpleResponse : ISimpleResponse
        {
            public string CusomerName { get; set; }
        }
    }
}