using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace PropertySurveyService.Models
{
    [Index(nameof(Filename))]
    public class PhotoImage
    {
        [Key]
        public int Id { get; set; }
        public string? Filename { get; set; }

        public string? Data { get; set; }
        public DateTime DateTime { get; set; }
        public string? ContractCode { get; set; }
        public int HeaderId { get; set; }


    }

    public class ImageDTO
    {
        public string? Filename { get; set; }
        public string? Data { get; set; }

    }

}
