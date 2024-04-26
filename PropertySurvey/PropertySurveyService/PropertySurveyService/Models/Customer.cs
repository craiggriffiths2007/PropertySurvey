using System.ComponentModel.DataAnnotations;

namespace PropertySurveyService.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Display(Name = "Fullname")]
        public string? Name { get; set; }
   
        [Display(Name = "Address 1")]
        public string? Add1 { get; set; }
        [Display(Name = "Address 2")]
        public string? Add2 { get; set; }
        [Display(Name = "Address 3")]
        public string? Add3 { get; set; }
 
        [Display(Name = "Postcode")]
        public string? Postcode { get; set; }
   
        [Display(Name = "Mobile Phone")]
        public string? Phone1 { get; set; }
        [Display(Name = "Home Phone")]
        public string? Phone2 { get; set; }
        [Display(Name = "Work Phone")]
        public string? Phone3 { get; set; }
    }
}
