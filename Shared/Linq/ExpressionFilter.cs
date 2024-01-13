using static Shared.DataFilter.Infrastructure.ColumnFilterConst;

namespace Shared.Linq
{
    public class ExpressionFilter
    {
        public string ColumnName { get; set; }
        public OPERATOR Operator { get; set; }
        public string[] Values { get; set; }
    }

}
