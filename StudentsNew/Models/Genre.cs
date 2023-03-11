using System.Text.Json.Serialization;

namespace StudentsNew.Models
{
    //[JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Genre
    {   
         NotSelected, Female, Male, Other
    }
}
