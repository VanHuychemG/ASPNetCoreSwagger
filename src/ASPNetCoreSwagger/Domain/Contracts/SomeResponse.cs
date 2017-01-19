using Newtonsoft.Json;

namespace ASPNetCoreSwagger.Domain.Contracts
{
    [JsonObject("response")]
    public class SomeResponse
    {
        [JsonProperty("code")]
        public int SomeCode { get; set; }

        [JsonProperty("message")]
        public string SomeMessage { get; set; }
    }
}
