using System.Text.Json;
using System.Text.Json.Serialization;

namespace MobileApp.Handler.Data
{
    public class ExtendedJsonHandler : JsonHandler
    {
        public static JsonSerializerOptions DefaultSerializerOption= new() { 
            
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented= true,
            Converters = {
                new JsonStringEnumConverter()//enum Values als String
            },
            UnknownTypeHandling = JsonUnknownTypeHandling.JsonNode

        };
        public ExtendedJsonHandler() : this(true, DefaultSerializerOption)
        {

        }
        public ExtendedJsonHandler(bool tryToFillValues,JsonSerializerOptions jsonSerializerOptions) : base(tryToFillValues,jsonSerializerOptions)
        {

        }    
    }
}
