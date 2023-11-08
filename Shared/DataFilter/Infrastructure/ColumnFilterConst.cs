namespace Shared.DataFilter.Infrastructure
{
    public static class ColumnFilterConst
    {
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
        public enum OPERATOR_GROUP
        {
            INVALID,
            AND,
            OR,
            XOR
        }
        public struct OperatorNames
        {
            public const string EQUAL = "eq";
            public const string CONTAINS = "contains";
            public const string NOT_EQUAL = "neq";
            public const string LESS_THAN = "lt";
            public const string GREATER_THAN = "gt";
            public const string LESS_THAN_OR_EQUAL = "ltoreq";
            public const string GREATER_THAN_OR_EQUAL = "gtoreq";
        }
        public struct OperatorGroupNames
        {
            public const string AND = "and";
            public const string OR = "or";
            public const string XOR = "xor";
        }
    }
}
