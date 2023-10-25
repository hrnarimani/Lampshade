﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EF.Core.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EF.Core
{
    public  class CommentContext :DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public CommentContext(DbContextOptions<CommentContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CommentMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
