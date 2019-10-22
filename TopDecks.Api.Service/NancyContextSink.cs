using System.Runtime.Remoting.Messaging;
using Nancy;

namespace TopDecks.Api.Service
{
    public class NancyContextSink
    {
        public static NancyContextSink Current
        {
            get => CallContext.LogicalGetData("NancyContext") as NancyContextSink;
            set => CallContext.LogicalSetData("NancyContext", value);
        }

        internal NancyContextSink(string traceId)
        {
            TraceId = traceId;
        }

        public NancyContext NancyContext { get; set; }

        public string TraceId { get; set; }
    }
}
