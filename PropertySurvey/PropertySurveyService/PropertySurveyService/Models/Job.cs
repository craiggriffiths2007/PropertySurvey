using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertySurveyService.Models
{
    public class Job
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        [Display(Name = "Contract Code")]
        public string? ContractCode { get; set; }

        [Display(Name = "Job Date")]
        [DataType(DataType.Date)]

        public DateTime Date { get; set; } 

        [Display(Name = "Job Time")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; } 

        [Display(Name = "Damage")]
        public string? DamageDesc { get; set; }
        [Display(Name = "Instructions")]
        public string? Instructions { get; set; }

        public int? CustomerId { get; set; }

        public int? SurveyorId { get; set; }

        public Surveyor? Surveyor { get; set; }

        public Customer? Customer { get; set; }
        /*
        public Job()
        {
            Date = DateTime.Today;
            Time = DateTime.Now;
        }*/
    }

    public class GetSurveysDTO
    {
        public string? SurveyorCode { get; set; }

    }

    public class JobDTO
    {
        public int Id { get; set; }
        public int ContractId { get; set; }

        public string? ContractCode { get; set; }
        public string? udi_cont { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public string? Name { get; set; }
        public string? Add1 { get; set; }
        public string? Add2 { get; set; }
        public string? Add3 { get; set; }
        public string? Postcode { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? Phone3 { get; set; }
        public string? DamageDesc { get; set; }
        public string? Instructions { get; set; }
        public JobDTO() { }
        public JobDTO(Job jobItem,Customer custItem)
        { 
            

            (Id, ContractId, ContractCode, Date, Time, Name, Add1, Add2, Add3, Postcode, Phone1, Phone2, Phone3, DamageDesc, Instructions) =

            (jobItem.Id, jobItem.ContractId, jobItem.ContractCode, jobItem.Date.ToShortDateString(), jobItem.Time.ToString(), custItem.Name,
                custItem.Add1, custItem.Add2, custItem.Add3, custItem.Postcode, custItem.Phone1,
                custItem.Phone2, custItem.Phone3, jobItem.DamageDesc, jobItem.Instructions);

            udi_cont = ContractCode;
        }
    }

}
