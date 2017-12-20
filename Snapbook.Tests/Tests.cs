namespace Snapbook.Tests
{
    using AutoMapper;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Snapbook.Web.Infrastructure.Mapping;
    using System;

    public class Tests
    {
        private static bool testsInitialized = false;

        public static void Initialize()
        {
            if (testsInitialized) return;

            Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
            testsInitialized = true;
        }

        public static SnapbookDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<SnapbookDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new SnapbookDbContext(dbOptions);
        }
    }
}
