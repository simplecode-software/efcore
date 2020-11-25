// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.TestModels.GearsOfWarModel;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public abstract class TPTGearsOfWarQueryRelationalFixture : GearsOfWarQueryFixtureBase
    {
        protected override string StoreName { get; } = "TPTGearsOfWarQueryTest";

        public new RelationalTestStore TestStore
            => (RelationalTestStore)base.TestStore;

        public TestSqlLoggerFactory TestSqlLoggerFactory
            => (TestSqlLoggerFactory)ListLoggerFactory;

        protected override bool ShouldLogCategory(string logCategory)
            => logCategory == DbLoggerCategory.Query.Name;

        // TODO: this is somewhat hacky - TPT inherits from base fixture, which is TPH
        // so we need to remove discriminators that we just added in the relational layer
        // we should consider common fixture base from which TPT, TPH etc would derive
        public override Dictionary<(Type, string), Func<object, object>> GetShadowPropertyMappings()
            => base.GetShadowPropertyMappings()
                .Where(e => e.Key.Item2 != "Discriminator")
                .ToDictionary(d => d.Key, d => d.Value);

        protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
        {
            base.OnModelCreating(modelBuilder, context);

            modelBuilder.Entity<Gear>().ToTable("Gears");
            modelBuilder.Entity<Officer>().ToTable("Officers");

            modelBuilder.Entity<Faction>().ToTable("Factions");
            modelBuilder.Entity<LocustHorde>().ToTable("LocustHordes");

            modelBuilder.Entity<LocustLeader>().ToTable("LocustLeaders");
            modelBuilder.Entity<LocustCommander>().ToTable("LocustCommanders");
        }
    }
}
