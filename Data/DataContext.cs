using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Data
{
    public class DataContext : DbContext
    {
           public DataContext(DbContextOptions<DataContext> options): base(options)
        {            
        }
        public DbSet<Kurs> Kurslar => Set<Kurs>();
        public DbSet<Ogrenci> Ogrenciler => Set<Ogrenci>();
        public DbSet<KursKayit> KursKayitlar => Set<KursKayit>();

    }
}
//iki yöntem vardır.
//code-first => entity, dbContext => database (sqlLite)
//database-first => sql server