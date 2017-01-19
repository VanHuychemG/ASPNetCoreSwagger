using Newtonsoft.Json;

namespace ASPNetCoreSwagger.Domain.Contracts
{
    [JsonObject("request")]
    public class SomeRequest
    {
        [JsonProperty("id")]
        public int SomeId { get; set; }
    }
}
