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
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }
    

    }

} 