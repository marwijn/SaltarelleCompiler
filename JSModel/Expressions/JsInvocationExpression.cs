using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Saltarelle.Compiler.JSModel.Expressions {
    [Serializable]
    public class JsInvocationExpression : JsExpression {
        public JsExpression Method { get; private set; }
        public ReadOnlyCollection<JsExpression> Arguments { get; private set; }

        internal JsInvocationExpression(JsExpression method, IEnumerable<JsExpression> arguments) : base(ExpressionNodeType.Invocation) {
            if (method == null) throw new ArgumentNullException("method");
            if (arguments == null) throw new ArgumentNullException("arguments");
            Method = method;
            Arguments = arguments.AsReadOnly();
        }

        [System.Diagnostics.DebuggerStepThrough]
        public override TReturn Accept<TReturn, TData>(IExpressionVisitor<TReturn, TData> visitor, TData data) {
            return visitor.Visit(this, data);
        }
    }
}