using System.ComponentModel.DataAnnotations;

namespace PropertySurveyService.Models
{
    public class Surveyor
    {
        [Key]
        public int SurveyorId { get; set; }
        [Required]
        [Display(Name = "Code")]
        public string? SurveyorCode { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string? Name { get; set; }
    }
}
