using Microsoft.EntityFrameworkCore;
using PropertySurveyService.Models;

namespace PropertySurveyService.ViewModels
{
    public class ItemIndexViewModel
    {
        public AluminiumTable? Alum { get; set; }
        public BifoldTable? Bifold { get; set; }
        public CompositeTable? Comp { get; set; }
        public ConsTable? Cons { get; set; }
        public GarageTable? Garage { get; set; }
        public GlassTable? Glass { get; set; }
        public GreenTable? Green { get; set; }
        public LockingTable? Lockin { get; set; }
        public PanelTable? Panel { get; set; }
        public TimberTable? Timber { get; set; }
        public UPVCTable? UPVC { get; set; }

  
        public IEnumerable<PhotoImage>? Images { get; set; }
    }
}
