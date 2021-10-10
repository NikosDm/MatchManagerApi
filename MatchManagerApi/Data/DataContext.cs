using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatchManagerApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchManagerApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchOdds> MatchesOdds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);

            builder.Entity<MatchOdds>()
                .HasOne(b => b.Match)
                .WithMany(b => b.MatchOdds)
                .HasForeignKey(b => b.MatchId);
        }
    }
}