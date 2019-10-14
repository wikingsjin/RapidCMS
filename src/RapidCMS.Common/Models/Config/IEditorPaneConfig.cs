﻿using System;
using System.Linq.Expressions;
using RapidCMS.Common.Data;

namespace RapidCMS.Common.Models.Config
{
    public interface IEditorPaneConfig<TEntity> : IHasButtons<IEditorPaneConfig<TEntity>>
        where TEntity : IEntity
    {
        /// <summary>
        /// Adds a field to the pane.
        /// </summary>
        /// <param name="displayExpression">Expression to edit this field</param>
        /// <param name="configure">Action to configure this field</param>
        /// <returns></returns>
        IEditorFieldConfig<TEntity, TValue> AddField<TValue>(Expression<Func<TEntity, TValue>> propertyExpression, Action<IEditorFieldConfig<TEntity, TValue>>? configure = null);

        /// <summary>
        /// Adds a sub collection to the pane. This sub collection should be a sub collection of the collection to which this pane belongs.
        /// </summary>
        /// <typeparam name="TSubEntity">Type of the sub collections entity</typeparam>
        /// <param name="collectionAlias">Alias of the sub collection</param>
        /// <param name="configure">Action to configure the use of this sub collection</param>
        /// <returns></returns>
        IEditorPaneConfig<TEntity> AddSubCollectionList<TSubEntity>(string collectionAlias, Action<ISubCollectionListConfig<TSubEntity>>? configure = null)
            where TSubEntity : IEntity;

        /// <summary>
        /// Adds a collection to the pane which is used to edit the many-to-many relation between the collection of this pane, and the related collection.
        /// The related collection can by any collection.
        /// </summary>
        /// <typeparam name="TRelatedEntity">Type of the related collections entity</typeparam>
        /// <param name="collectionAlias">Alias of the related collection</param>
        /// <param name="configure">Action to configure the use of this related collection</param>
        /// <returns></returns>
        IEditorPaneConfig<TEntity> AddRelatedCollectionList<TRelatedEntity>(string collectionAlias, Action<IRelatedCollectionListConfig<TEntity, TRelatedEntity>>? configure = null)
            where TRelatedEntity : IEntity;

        /// <summary>
        /// Adds a label at the top of this pane.
        /// </summary>
        /// <param name="label">Text to display in the label</param>
        /// <returns></returns>
        IEditorPaneConfig<TEntity> SetLabel(string label);

        /// <summary>
        /// Expression which determines whether this pane should be visible
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEditorPaneConfig<TEntity> VisibleWhen(Func<TEntity, bool> predicate);
    }
}