﻿using ToDo.Domain.Models;
using System.Linq.Expressions;

namespace ToDo.Domain.Specifications
{
    public abstract class BaseSpecifications<T> where T : BaseModel
    {
        public List<Expression<Func<T, bool>>> Criteria { get; private set; } = new List<Expression<Func<T, bool>>>();
        public List<Expression<Func<T, object>>> Includes { get; private set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }
        public bool IsTotalCountEnable { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> incldueExpression)
        {
            Includes.Add(incldueExpression);
        }

        protected void AddCriteria(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria.Add(criteriaExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        protected void ApplyPaging(int PageSize, int PageIndex)
        {
            Skip = PageSize * (PageIndex - 1);
            Take = PageSize;
            IsPagingEnabled = true;
            EnableTotalCount();
        }

        protected void EnableTotalCount()
        {
            IsTotalCountEnable = true;
        }
    }
}
