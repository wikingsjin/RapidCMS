﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RapidCMS.Core.Abstractions.Data;
using RapidCMS.Core.Abstractions.Metadata;
using RapidCMS.Core.Enums;

namespace RapidCMS.Core.Models.Data
{
    internal class TypedQuery<TEntity> : IQuery, IQuery<TEntity>
    {
        private readonly Query _query;

        public static IQuery<TEntity> Convert(IQuery query)
        {
            return query switch
            {
                Query q => new TypedQuery<TEntity>(q),
                TypedQuery<TEntity> q => q,
                _ => throw new InvalidOperationException()
            };
        }

        public TypedQuery(Query query)
        {
            _query = query;
        }

        public int Skip => _query.Skip;

        public int Take => _query.Take;

        public string? SearchTerm => _query.SearchTerm;

        public int? ActiveTab => _query.ActiveTab;

        public bool MoreDataAvailable => _query.MoreDataAvailable;

        public Expression<Func<TEntity, bool>>? DataViewExpression => _query.DataView?.QueryExpression as Expression<Func<TEntity, bool>>;

        public void HasMoreData(bool hasMoreData)
        {
            _query.HasMoreData(hasMoreData);
        }

        public void SetDataView(IDataView dataView)
        {
            _query.SetDataView(dataView);
        }

        public IQueryable<TEntity> ApplyDataView(IQueryable<TEntity> queryable)
        {
            if (!(_query.DataView?.QueryExpression is Expression<Func<TEntity, bool>> validQueryExpression))
            {
                return queryable;
            }

            return queryable.Where(validQueryExpression);
        }

        public IQueryable<TEntity> ApplyOrder(IQueryable<TEntity> queryable)
        {
            if (_query.OrderBys == null)
            {
                return queryable;
            }

            var first = true;
            foreach (var orderBy in _query.OrderBys)
            {
                if (orderBy.OrderByType == OrderByType.None || orderBy.OrderByExpression == null)
                {
                    continue;
                }

                var method = first
                    ? orderBy.OrderByType == OrderByType.Descending ? nameof(Queryable.OrderByDescending) : nameof(Queryable.OrderBy)
                    : orderBy.OrderByType == OrderByType.Descending ? nameof(Queryable.ThenByDescending) : nameof(Queryable.ThenBy);

                queryable = ApplyOrderBy(queryable, orderBy.OrderByExpression, method);

                first = false;
            }

            return queryable;
        }

        private static IQueryable<T> ApplyOrderBy<T>(IQueryable<T> source, IPropertyMetadata ordering, string method)
        {
            if (typeof(T) != ordering.ObjectType)
            {
                throw new InvalidOperationException($"The type of TEntity was not the same as the type used in the order expression (TEntity: {typeof(T)}, order expression: {ordering.ObjectType}). " +
                    $"Use the correct SetOrderByExpression overload when configuring.");
            }

            var resultExp = Expression.Call(
                typeof(Queryable),
                method,
                new Type[] { ordering.ObjectType, ordering.PropertyType },
                source.Expression,
                Expression.Quote(ordering.OriginalExpression));

            return source.Provider.CreateQuery<T>(resultExp);
        }

        public IDataView? ActiveDataView => _query.DataView;

        public IEnumerable<IOrderBy> ActiveOrderBys => _query.OrderBys ?? Enumerable.Empty<OrderBy>();

        public string? CollectionAlias => _query.CollectionAlias;
    }
}
