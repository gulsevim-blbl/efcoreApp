using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Ogrenci
    {
        //id => primary key
        // [Key]
        public int OgrenciId { get; set; }
        //id ya da classın ismi ile OgrenciId mesela yazılır ancak çok farklı bir şey yazmak istrsek örneğin tckimlik o zaman üstün [Key] eklemeliyiz primary key olarak işaretlenmesini sağlarız böylelikle
        public string? OgrenciAd { get; set; }
        public string? OgrenciSoyad { get; set; }
        //KursKAyıt Sayfasında Ad ve soyadı göstermek için eklendi.
        public string AdSoyad
        {
            get
            {
                return $"{OgrenciAd} {OgrenciSoyad}";
            }
        }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }

        //Her bir öğrencinin katılmış olduğu kurs kayıtlarını almakiçin
        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();

    }

} 