using CrosswordPuzzle.DataBase.DBEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordPuzzle.DataBase
{
    public class DBContext : DbContext
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=words.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}

