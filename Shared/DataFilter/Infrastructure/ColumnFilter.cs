using Shared.Linq;
using System.Text.Json.Serialization;
using static Shared.DataFilter.Infrastructure.ColumnFilterConst;

namespace Shared.DataFilter.Infrastructure
{
    [Serializable]
    public class ColumnFilter
    {
        [JsonPropertyName("field")]
        public string field { get; set; }
        [JsonPropertyName("value")]
        public string? value { get; set; }
        [JsonPropertyName("op")]
        public string? op { get; set; } = OperatorNames.EQUAL;//operator, when not set is OperatorNames.EQUAL the default Operator

        [JsonIgnore]
        public OPERATOR Op { get; set; }

        [JsonIgnore]
        public bool IsInvalidOperator => Op == OPERATOR.INVALID;

        [JsonIgnore]
        public static readonly Dictionary<string, OPERATOR> OperatorMapping = new Dictionary<string, OPERATOR>() {
            {OperatorNames.EQUAL, OPERATOR.EQUAL},
            {OperatorNames.CONTAINS, OPERATOR.CONTAINS},
            {OperatorNames.NOT_EQUAL, OPERATOR.NOT_EQUAL},
            {OperatorNames.LESS_THAN, OPERATOR.LESS_THAN},
            {OperatorNames.GREATER_THAN, OPERATOR.GREATER_THAN},
            {OperatorNames.LESS_THAN_OR_EQUAL, OPERATOR.LESS_THAN_OR_EQUAL},
            {OperatorNames.GREATER_THAN_OR_EQUAL, OPERATOR.GREATER_THAN_OR_EQUAL},
        };
        public static OPERATOR GetOperator(string value)
        {
            if(OperatorMapping.ContainsKey(value))
            {
                return OperatorMapping[value];
            }
            return OPERATOR.INVALID;
        }
        public OPERATOR GetOperatorFromOp() => GetOperator(op);
    }
}
