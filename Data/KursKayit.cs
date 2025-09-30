using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    //Kurslar ile öğrencilerin kesiştirme kısmı
    public class KursKayit
    {
        [Key]
        public int KayitId { get; set; }

        public int OgrenciId { get; set; }
        public Ogrenci Ogrenci { get; set; } = null!;
        //tablolar arasında join işlemleri gerçekleştirirz Bu şekilde.

        public int KursId { get; set; }

        public Kurs Kurs { get; set; } = null!;

        public DateTime KayitTarihi { get; set; }

    }
}