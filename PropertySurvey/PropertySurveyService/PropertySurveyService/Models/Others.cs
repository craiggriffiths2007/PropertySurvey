namespace PropertySurveyService.Models
{
    public enum enum_item_type
    {
        upvc, panel, glass, alum, garage, timber,
        bifold, cons, lockin, comp, green
    }

    public class SurveyItem
    {
        public int Id { get; set; }
        public enum_item_type ItemType { get; set; }
        public string? ItemName { get; set; }

        public string? ControllerName { get; set; }
        public string NameFromEnumType()
        {
            return NameFromEnumType(ItemType);
        }
        public string NameFromEnumType(enum_item_type type)
        {
            switch (type)
            {
                case enum_item_type.upvc: return "UPVC";
                case enum_item_type.panel: return "Panel";
                case enum_item_type.glass: return "Glass";
                case enum_item_type.alum: return "Aluminium";
                case enum_item_type.garage: return "Garage";
                case enum_item_type.timber: return "Timber";
                case enum_item_type.bifold: return "Bifold";
                case enum_item_type.lockin: return "Lock-mech";
                case enum_item_type.green: return "Greenhouse";
            }
            return "";
        }

        public string ControllerNameFromEnumType(enum_item_type type)
        {
            switch (type)
            {
                case enum_item_type.upvc: return "UPVCTables";
                case enum_item_type.panel: return "PanelTables";
                case enum_item_type.glass: return "GlassTables";
                case enum_item_type.alum: return "AlumTables";
                case enum_item_type.garage: return "GarageTables";
                case enum_item_type.timber: return "TimberTables";
                case enum_item_type.bifold: return "BifoldTables";
                case enum_item_type.lockin: return "LockingTables";
                case enum_item_type.green: return "GreenTables";
            }
            return "";
        }


        public SurveyItem(int id, enum_item_type type)
        {
            Id = id;
            ItemType = type;
            ItemName = NameFromEnumType(type);
            ControllerName = ControllerNameFromEnumType(type);
        }
    }
}
