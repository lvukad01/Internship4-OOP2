using System.Text.Json.Serialization;

namespace UsersApp.Domain.Common.Validation
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ValidationSeverity
    {
        Info,
        Warning,
        Error
    }
}
