// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Threading.Tasks;
using SixLabors.ImageSharp.Web.Synchronization;
using Xunit;

namespace SixLabors.ImageSharp.Web.Tests.Synchronization
{
    public class AsyncLockTests
    {
        [Fact]
        public async Task OneAtATime()
        {
            AsyncLock l = new();

            Task<IDisposable> first = l.LockAsync();
            Assert.True(first.IsCompletedSuccessfully);

            Task<IDisposable> second = l.LockAsync();
            Assert.False(second.IsCompleted);

            // Release first hold on the lock and then await the second task to confirm it completes.
            first.Result.Dispose();

            // Await the second task to make sure we get the lock.
            await second;

            l.Dispose();
        }
    }
}
