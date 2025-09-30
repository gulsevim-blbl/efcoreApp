using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Kurs> Kurslar => Set<Kurs>();
        public DbSet<Ogrenci> Ogrenciler => Set<Ogrenci>();
        public DbSet<KursKayit> KursKayitlar => Set<KursKayit>();
        public DbSet<Ogretmen> Ogretmenler => Set<Ogretmen>(); //artık uygulama dbContext den dolayı öğretmeni artık burada tanır.Dolayısıyla biz veritabanı şamasında bir güncelleme sağladım ve bunu veritabanına aktarmamız gerekir.

    }
}
//iki yöntem vardır.
//code-first => entity, dbContext => database (sqlLite)
//database-first => sql server



//Veritabanını güncelleme kısmı
/*
    bizim şu an bulunan bir tane migrationumuz var  bu ilk aşamada oluşturduğumuz migration
    bizim yeni bir migration oluşturmamız lazım
*/

/*
    migrations eklemeden önce dotnet build yaparak projede bir hata olup olmadığını öğrenebiliriz 
    terminalde dotnet ef migrations add AddTableOgretmen
    //? yeni migrationumuzu
     uygulama tarafının migrationdan haberi var ama veritabanında bu migrationdan haberi yok 
*/