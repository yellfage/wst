using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Yellfage.Wst.Protocols.NewtonsoftJson
{
    public class NewtonsoftJsonProtocolOptions
    {
        public JsonSerializerSettings SerializerSettings { get; set; } = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    ProcessDictionaryKeys = false,
                    OverrideSpecifiedNames = true
                }
            }
        };
    }
}
