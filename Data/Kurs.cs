namespace efcoreApp.Data
{
    public class Kurs
    {
        public int KursId { get; set; }
        public string? Baslik { get; set; }

        public int? OgretmenId { get; set; }
        public Ogretmen Ogretmen { get; set; } = null!; //her kursa bir öğretmen atadık.
        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();

    }

}


/*
    Bizim kurs tablomuzun yanınına birde öğretmen id kolonu gelicek ve öğretmen id kolon içerisinde henüz bir değer yok yani var olan veriler için ekleneck olan kolonun içerisine çünkü null olan bir değeri kabul etmedik =>public int OgretmenId { get; set; } burada olduğu gibi mutlaka bir id gelsin dedik
    Fakat mevcut kayıtlarımız bulunmakta  onlara null değer gelecek ancak null değeri kabul etmeyen bir kolon yapısı oluşturduk Bu aşamada yapmamız gereken 

    =>Veritabanınını silebilirz ve tekrar ilk oluşturuyormuş gibi oluşturabilirz fakat verilrimiz de gider.
    2.yol
    =>dotnet ef database update diyelim terminalde 
    burada SQLite Error 19: 'FOREIGN KEY constraint failed'. hatası alacağız çünkü kurslar tablosuna ögretmenId null değer gidecek  ancak burada bir hata alacak çünkü tablolar arasında bir bağlantı vaar
    ve bu bağlantıdan dolayı mutlaka öğretmen tablosunda olan bir id yi kurslar kaydı için veriyor olmalıyız.
*/
/*
    Bu durumda ne yapmamız gerekiyor?
    2yöntem var dediğimiz gibi 
    1. veritabanını silip sanki hiç migration olmamış gibi tekrar database update yaparız.
    2.Veritabanını silmeden veri kaybına neden olmak istemezsem de
     public int? OgretmenId { get; set; } null olabilir işareti vermemiz gerek tabi bu farklı bir migration olur  dolayısıyla son migrationu silmemiz lazım.Sonuçta veritabanına henüz aktarılmadı
    
    terminal de 
    dotnet ef migrations remove dersek son oluşturulan migration silinir. !Veritabanına akatarılmamış bir migration olduğuna dikkat et veritabanına akatarılan bir migration için farklı bir yöntem  kullanmamız gerkiyor.

    tekrar aynı migrationu ekleyelim
    dotnet ef migrations add AddTableOgretmen
    sonra da
    dotnet ef database update diyelim terminalde son değişikliklerle birlikte veritabanına eklenir

*/