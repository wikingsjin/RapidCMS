﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RapidCMS.Core.Abstractions.Config;
using RapidCMS.Core.Abstractions.Data;
using RapidCMS.Core.Abstractions.Resolvers;
using RapidCMS.Core.Extensions;
using RapidCMS.Core.Models.Config.Api;

namespace RapidCMS.Core.Resolvers.Data
{
    [Obsolete]
    internal class ApiDataViewResolver : IDataViewResolver
    {
        private readonly IApiConfig _apiConfig;
        private readonly IServiceProvider _serviceProvider;

        public ApiDataViewResolver(
            IApiConfig apiConfig,
            IServiceProvider serviceProvider)
        {
            _apiConfig = apiConfig;
            _serviceProvider = serviceProvider;
        }

        public async Task ApplyDataViewToQueryAsync(IQuery query, string collectionAlias)
        {
            var dataViews = await GetDataViewsAsync(collectionAlias).ConfigureAwait(false);
            var dataView = dataViews.FirstOrDefault(x => x.Id == query.ActiveTab)
                    ?? dataViews.FirstOrDefault();

            if (dataView != null)
            {
                query.SetDataView(dataView);
            }
        }

        public Task<IEnumerable<IDataView>> GetDataViewsAsync(string collectionAlias)
        {
            //if (!_apiConfig.Repositories.TryGetValue(collectionAlias, out var collection))
            //{
            //    throw new InvalidOperationException($"Could not find collection with alias '{collectionAlias}'.");
            //}

            //if (collection.DataViewBuilderType == default)
            //{
                return Task.FromResult(Enumerable.Empty<IDataView>());
            //}

            //var builder = _serviceProvider.GetService<IDataViewBuilder>(collection.DataViewBuilderType);
            //return builder.GetDataViewsAsync();
        }
    }
}
