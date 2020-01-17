﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SqlBatis
{
    public partial class DbQuery<T>
    {

        #region sync

        public int Count(int? commandTimeout = null)
        {
            var sql = ResovleCount();
            return _context.ExecuteScalar<int>(sql, _parameters, commandTimeout);
        }

        public int Count<TResult>(Expression<Func<T, TResult>> expression)
        {
            _countExpression = expression;
            return Count();
        }

        public int Insert(T entity)
        {
            ResovleParameter(entity);
            var sql = ResovleInsert(false);
            return _context.ExecuteNonQuery(sql, _parameters);
        }

        public int InsertReturnId(T entity)
        {
            ResovleParameter(entity);
            var sql = ResovleInsert(true);
            return _context.ExecuteScalar<int>(sql, _parameters);
        }

        public int Insert(IEnumerable<T> entitys)
        {
            var count = 0;
            foreach (var item in entitys)
            {
                count += Insert(item);
            }
            return count;
        }

        public int Update(int? commandTimeout = null)
        {
            if (_setExpressions.Count > 0)
            {
                var sql = ResolveUpdate();
                return _context.ExecuteNonQuery(sql, _parameters, commandTimeout);
            }
            return default;
        }

        public int Update(T entity)
        {
            ResovleParameter(entity);
            var sql = ResolveUpdate();
            return _context.ExecuteNonQuery(sql, _parameters);
        }

        public int Delete(int? commandTimeout = null)
        {
            var sql = ResovleDelete();
            return _context.ExecuteNonQuery(sql, _parameters, commandTimeout);
        }

        public int Delete(Expression<Func<T, bool>> expression)
        {
            Where(expression);
            return Delete();
        }

        public bool Exists(int? commandTimeout = null)
        {
            var sql = ResovleExists();
            return _context.ExecuteScalar<bool>(sql, _parameters, commandTimeout);
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            Where(expression);
            return Exists();
        }

        public IDbQuery<T> Set<TResult>(Expression<Func<T, TResult>> column, TResult value, bool condition = true)
        {
            if (true)
            {
                _setExpressions.Add(new SetExpression
                {
                    Column = column,
                    Expression = Expression.Constant(value)
                });
            }
            return this;
        }

        public IDbQuery<T> Set<TResult>(Expression<Func<T, TResult>> column, Expression<Func<T, TResult>> expression, bool condition = true)
        {
            if (true)
            {
                _setExpressions.Add(new SetExpression
                {
                    Column = column,
                    Expression = expression
                });
            }
            return this;
        }

        public IDbQuery<T> GroupBy<TResult>(Expression<Func<T, TResult>> expression)
        {
            _groupExpressions.Add(expression);
            return this;
        }

        public IDbQuery<T> Having(Expression<Func<T, bool>> expression, bool condition = true)
        {
            if (condition)
            {
                _havingExpressions.Add(expression);
            }
            return this;
        }

        public IDbQuery<T> OrderBy<TResult>(Expression<Func<T, TResult>> expression)
        {
            _orderExpressions.Add(new OrderExpression
            {
                Asc = true,
                Expression = expression
            });
            return this;
        }

        public IDbQuery<T> OrderByDescending<TResult>(Expression<Func<T, TResult>> expression)
        {
            _orderExpressions.Add(new OrderExpression
            {
                Asc = false,
                Expression = expression
            });
            return this;
        }

        public IDbQuery<T> Filter<TResult>(Expression<Func<T, TResult>> column)
        {
            _filterExpression = column;
            return this;
        }

        public IDbQuery<T> Page(int index, int count)
        {
            Skip((index - 1) * count, count);
            return this;
        }

        public IDbQuery<T> With(string lockname)
        {
            _lockname = $" {lockname}";
            return this;
        }

        public IEnumerable<T> Select(int? commandTimeout = null)
        {
            var sql = ResolveSelect();
            return _context.ExecuteQuery<T>(sql, _parameters, commandTimeout);
        }

        public (IEnumerable<T>, int) SelectMany(int? commandTimeout = null)
        {
            var sql1 = ResolveSelect();
            var sql2 = ResovleCount();
            using (var multi = _context.ExecuteMultiQuery($"{sql1};{sql2}", _parameters, commandTimeout))
            {
                var list = multi.GetList<T>();
                var count = multi.Get<int>();
                return (list, count);
            }
        }

        public IEnumerable<TResult> Select<TResult>(Expression<Func<T, TResult>> expression, int? commandTimeout = null)
        {
            _selectExpression = expression;
            var sql = ResolveSelect();
            return _context.ExecuteQuery<TResult>(sql, _parameters, commandTimeout);
        }

        public (IEnumerable<TResult>, int) SelectMany<TResult>(Expression<Func<T, TResult>> expression, int? commandTimeout = null)
        {
            _selectExpression = expression;
            var sql1 = ResolveSelect();
            var sql2 = ResovleCount();
            using (var multi = _context.ExecuteMultiQuery($"{sql1};{sql2}", _parameters, commandTimeout))
            {
                var list = multi.GetList<TResult>();
                var count = multi.Get<int>();
                return (list, count);
            }
        }

        public T Single(int? commandTimeout = null)
        {
            Take(1);
            return Select(commandTimeout).FirstOrDefault();
        }

        public TResult Single<TResult>(Expression<Func<T, TResult>> expression, int? commandTimeout = null)
        {
            Take(1);
            return Select(expression, commandTimeout).FirstOrDefault();
        }

        public IDbQuery<T> Skip(int index, int count)
        {
            _page.Index = index;
            _page.Count = count;
            return this;
        }

        public IDbQuery<T> Take(int count)
        {
            Skip(0, count);
            return this;
        }

        public IDbQuery<T> Where(Expression<Func<T, bool>> expression, bool condition = true)
        {
            if (condition)
            {
                _whereExpressions.Add(expression);
            }
            return this;
        }

        #endregion

    }
}
