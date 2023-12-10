using System.Text.Json.Serialization;

namespace Shared.DataFilter.Infrastructure
{
    public class ColumnSorting
    {
        public string? field { get; set; }
        public bool desc { get; set; }

        [JsonIgnore]
        public bool FoundFieldInDto { get; set; } = false;
    }
}
