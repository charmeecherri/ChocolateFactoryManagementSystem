using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.Models
{
    
    public class Quality
    {
        [Key]
        public int CheckId { get; set; } 

        public int BatchId { get; set; }

        public ProductionSchedule Batch {  get; set; }
        public int Inspectorid { get; set; }

        public DateTime InspectionDate { get; set; }

        public string TestResults { get; set; }

        public string status { get; set; }

    }
}
