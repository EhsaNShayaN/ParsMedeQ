using System.Linq.Expressions;

namespace SRH.Maply;

// Helper class to replace the lambda expression parameter with the input parameter
class ParameterReplacer : ExpressionVisitor
{
    private readonly ParameterExpression _parameter;

    public ParameterReplacer(ParameterExpression parameter)
    {
        _parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return _parameter;
    }
}