using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.Models
{
    public class Maintanence
    {
        [Key]
        public int RecordId { get; set; }

        public int EquipmentId { get; set; }

        public DateTime MaintanenceDate { get; set; }

        public string Technician { get ; set; }

        public string Details { get; set; }

        public DateTime NextSchedulingDate { get; set; }


    }
}
