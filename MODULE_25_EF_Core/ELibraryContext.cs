using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using MODULE25_EF.DAL.Entities;

namespace EntityFrWorkBD
{
    public class ELibraryContext : DbContext
    {
        public string _nameDataSouce;
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<BookEntity> Books { get; set; } = null!;
        public DbSet<AuthorEntity> Authors { get; set; } = null!;
        public DbSet<GenreEntity> Genres { get; set; } = null!;

        public ELibraryContext(bool isIniDB = false, string nameDataSouce = @"DESKTOP-QK3UCEL\SQLEXPRESS")
        {
            _nameDataSouce = nameDataSouce;
            if (isIniDB) Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(@"Server=DESKTOP-QK3UCEL\SQLEXPRESS;Database=EF;Trusted_Connection=True;");
            // optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-QK3UCEL\SQLEXPRESS;Database=EF_Finaly;Trusted_Connection=True;TrustServerCertificate=True;");
            optionsBuilder.UseSqlServer(@"Data Source=" + _nameDataSouce + @";Database=EF_Finaly;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
