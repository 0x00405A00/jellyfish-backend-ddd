namespace Shared.Linq
{
    public class ExpressionFilter
    {
        public string ColumnName { get; set; }
        public OPERATOR Operator { get; set; }
        public string Value { get; set; }
    }
    public enum OPERATOR
    {
        INVALID,
        EQUAL,
        CONTAINS,
        NOT_EQUAL,
        LESS_THAN,
        GREATER_THAN,
        LESS_THAN_OR_EQUAL,
        GREATER_THAN_OR_EQUAL,
    }

}
