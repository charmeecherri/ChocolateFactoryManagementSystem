using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChocolateFactoryApi.Models
{
    public class ProductionSchedule
    {
        [Key]
        public int ScheduleId { get; set; }

        public int ProductId {  get; set; }

        public Product Product { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Shift {  get; set; }

        public int SupervisorId {  get; set; }

        public string Status { get; set; }

        [JsonIgnore]
        public Quality Quality { get; set; }


    }
}
