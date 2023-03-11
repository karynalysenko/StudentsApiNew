using System.Text.Json.Serialization;

namespace StudentsNew.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Genre
    {
         Femenine = 1,
         Masculine = 2,
         Other = 3
    }
}
