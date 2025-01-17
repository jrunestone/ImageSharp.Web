// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp.Web.Caching.Azure;
using SixLabors.ImageSharp.Web.DependencyInjection;
using SixLabors.ImageSharp.Web.Providers.Azure;

namespace SixLabors.ImageSharp.Web.Tests.TestUtilities
{
    public class AzureBlobStorageCacheTestServerFixture : TestServerFixture
    {
        protected override void ConfigureCustomServices(IServiceCollection services, IImageSharpBuilder builder)
            => builder
               .Configure<AzureBlobStorageImageProviderOptions>(o =>
                    o.BlobContainers.Add(
                    new AzureBlobContainerClientOptions
                    {
                        ConnectionString = TestConstants.AzureConnectionString,
                        ContainerName = TestConstants.AzureContainerName
                    }))
                .AddProvider(AzureBlobStorageImageProviderFactory.Create)
                .Configure<AzureBlobStorageCacheOptions>(o =>
                {
                    o.ConnectionString = TestConstants.AzureConnectionString;
                    o.ContainerName = TestConstants.AzureCacheContainerName;

                    AzureBlobStorageCache.CreateIfNotExists(o, PublicAccessType.None);
                })
                .SetCache<AzureBlobStorageCache>();
    }
}
