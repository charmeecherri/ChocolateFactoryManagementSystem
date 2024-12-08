using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.Models
{
    public class Analytics
    {
        [Key]
        public int ReportId { get; set; }

        public string Type { get; set; }

        public DateTime GeneratedDate { get; set; }

        public string Data { get; set; }

        public string CreatedBy { get; set; }
    }
}
