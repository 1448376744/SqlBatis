﻿using System.Collections.Generic;
using System.Linq.Expressions;

namespace SqlBatis.Expressions
{
    /// <summary>
    /// 分组表达式解析解析
    /// </summary>
    public class GroupExpressionResovle : ExpressionResovle
    {
        private readonly bool _single;
        private readonly Expression _expression;
        private readonly List<string> _list = new List<string>();

        public GroupExpressionResovle(bool isSingleTable, Expression expression)
            : base(isSingleTable)
        {
            _single = isSingleTable;
            _expression = expression;
        }

        protected override Expression VisitNew(NewExpression node)
        {
            foreach (var item in node.Arguments)
            {
                Visit(item);
            }
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var name = GetDbColumnNameAsAlias(node);
            _list.Add(name);
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var result = new FunctionExpressionResovle(_single,node).Resovle();
            _list.Add(result);
            return node;
        }

        public override string Resovle()
        {
            Visit(_expression);
            return string.Join(",",_list);
        }
    }
}
