namespace Sample.MessageTypes
{
    using System;


    public interface ISimpleRequest
    {
        DateTime Timestamp { get; }

        string CustomerId { get; }
    }
}