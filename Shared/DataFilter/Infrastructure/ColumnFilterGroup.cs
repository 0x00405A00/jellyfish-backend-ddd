using Shared.Linq;
using System.Text.Json.Serialization;
using Shared.DataFilter.Infrastructure;
using static Shared.DataFilter.Infrastructure.ColumnFilterConst;

namespace Shared.DataFilter.Infrastructure
{
    public class ColumnFilterGroup
    {
        [JsonPropertyName("group")]
        public string? Group { get; set; } = OperatorGroupNames.AND;

        [JsonPropertyName("filters")]
        public List<ColumnFilter> Filters { get; set; } = new List<ColumnFilter>();

        public ColumnFilterGroup()
        {

        }
        public ColumnFilterGroup(List<ColumnFilter> columnFilters, string operatorGroup)
        {
            Group= operatorGroup;   
            Filters= columnFilters;
        }


        [JsonIgnore]
        public OPERATOR_GROUP OpGroup { get; set; }

        [JsonIgnore]
        public bool IsInvalidOperatorGroup => OpGroup == OPERATOR_GROUP.INVALID;

        [JsonIgnore]
        public static readonly Dictionary<string, OPERATOR_GROUP> OperatorGroupMapping = new Dictionary<string, OPERATOR_GROUP>() {
            {OperatorGroupNames.AND, OPERATOR_GROUP.AND},
            {OperatorGroupNames.OR, OPERATOR_GROUP.OR},
            {OperatorGroupNames.XOR, OPERATOR_GROUP.XOR},
        };
        public static OPERATOR_GROUP GetOperatorGroup(string value)
        {
            if (OperatorGroupMapping.ContainsKey(value))
            {
                return OperatorGroupMapping[value];
            }
            return OPERATOR_GROUP.INVALID;
        }
        public OPERATOR_GROUP GetOperatorFromOp() => GetOperatorGroup(Group);
    }
}
