using System.ComponentModel.DataAnnotations;

namespace ChocolateFactoryApi.DTO.request
{
    public class QualityRequestDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Batch Id can't be null or zero")]
        public int BatchId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Inspector Id can't be null or zero")]
        public int InspectorId { get; set; }

        [Required]
        public DateTime InspectionDate { get; set; }

        [Required]
        public string TestResults { get; set; }

        //[Required]
        //public string status { get; set; }
    }
}
