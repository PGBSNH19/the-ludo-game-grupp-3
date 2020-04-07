using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LudoEngine
{
    class LudoGameContext : DbContext
    {
        public DbSet<GameState> GameState { get; set; }
        public DbSet<Piece> Piece { get; set; }
        public DbSet<Player> Player { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            var defaultConnectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(defaultConnectionString);
        }
      
    }
}
