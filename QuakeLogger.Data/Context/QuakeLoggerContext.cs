﻿
using Microsoft.EntityFrameworkCore;
using QuakeLogger.Domain.Models;
using QuakeLogger.Models;


namespace QuakeLogger.Data.Context
{
    public class QuakeLoggerContext : DbContext
    {
        public QuakeLoggerContext(DbContextOptions<QuakeLoggerContext> options) : base(options) { }


        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<GamePlayer> GamePlayers { get; set; }
        public DbSet<KillMethod> KillMethods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("InMemoryProvider");

            //optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KillMethod>()
                .HasOne(g => g.Game)
                .WithMany(km => km.KillMethods)
                .HasForeignKey(km => km.GameId);                

            modelBuilder.Entity<GamePlayer>()
                .HasKey(gp => new { gp.GameId, gp.PlayerId });

            modelBuilder.Entity<GamePlayer>()
                .HasOne(gp => gp.Game)
                .WithMany(p => p.GamePlayers)
                .HasForeignKey(gp => gp.GameId);

            modelBuilder.Entity<GamePlayer>()
                .HasOne(gp => gp.Player)
                .WithMany(c => c.PlayerGames)
                .HasForeignKey(gp => gp.PlayerId);
        }

    }
}