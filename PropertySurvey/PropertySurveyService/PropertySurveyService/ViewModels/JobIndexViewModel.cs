using PropertySurveyService.Models;

namespace PropertySurveyService.ViewModels
{
    public class JobIndexViewModel
    {
        public IEnumerable<Job>? Jobs { get; set; }
        public IEnumerable<Header>? Headers { get; set; }
    }
}
