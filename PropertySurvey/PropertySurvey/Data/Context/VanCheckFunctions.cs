using System;
using System.Collections.Generic;
using System.Text;

namespace PropertySurvey
{
    public partial class Functions
    {
        public void SetVanCheckVanSent(int item_no, string item_no_4D)
        {
            foreach (var item in database.Table<WeeklyVanCheckSheet>().Where(i => i.item_no == item_no).ToList())
            {
                item.bSent = true;
                item.item_no_4D = item_no_4D;

                foreach (var item2 in database.Table<VanChecksHeader>().Where(p => p.unique_id == item.CheckID).ToList())
                {
                    item2.bSent = true;
                    database.Update(item2);
                }
                database.Update(item);
            }
        }

        public void SetVanCheckCarSent(int item_no, string item_no_4D)
        {
            foreach (var item in database.Table<CarPanelSheet>().Where(i => i.item_no == item_no).ToList())
            {
                item.bSent = true;
                item.item_no_4D = item_no_4D;

                foreach (var item2 in database.Table<VanChecksHeader>().Where(p => p.unique_id == item.CheckID).ToList())
                {
                    item2.bSent = true;
                    database.Update(item2);
                }
                database.Update(item);
            }
        }

        public void SetVanCheckDeliverySent(int item_no, string item_no_4D)
        {
            foreach (var item in database.Table<DeliveryVehicleCheckList>().Where(i => i.item_no == item_no).ToList())
            {
                item.bSent = true;
                item.item_no_4D = item_no_4D;

                foreach (var item2 in database.Table<VanChecksHeader>().Where(p => p.unique_id == item.CheckID).ToList())
                {
                    item2.bSent = true;
                    database.Update(item2);
                }
                database.Update(item);
            }
        }

        public void SetVanCheckDeliveryVanSent(int item_no, string item_no_4D)
        {
            foreach (var item in database.Table<DeliveryVanVehicleCheckList>().Where(i => i.item_no == item_no).ToList())
            {
                item.bSent = true;
                item.item_no_4D = item_no_4D;

                foreach (var item2 in database.Table<VanChecksHeader>().Where(p => p.unique_id == item.CheckID).ToList())
                {
                    item2.bSent = true;
                    database.Update(item2);
                }
                database.Update(item);
            }
        }

        public void SetVanCheckHeaderSent(string CheckID)
        {
            foreach (var item2 in database.Table<VanChecksHeader>().Where(p => p.unique_id == CheckID).ToList())
            {
                item2.bSent = true;
                database.Update(item2);
            }
        }

        public List<VanChecksHeader> GetUnsentChecks()
        {
            return database.Table<VanChecksHeader>().Where(i => i.bSent == false & i.bComplete == true).ToList();
        }

        public void DeleteDamageLabels()
        {
            List<DamageLabels> labels = new List<DamageLabels>();

            switch (App.CurrentApp.CurrentItem)
            {
                case "deliveryvan":
                    labels = database.Table<DamageLabels>().Where(i => i.item_no == App.CurrentApp.DeliveryVanVehicleCheckList.item_no & i.CheckID == App.CurrentApp.VanChecksHeader.unique_id & i.vehicle_type == "deliveryvan" & i.angle_num == App.CurrentApp.current_van_picture).ToList();break;
                case "delivery":
                    labels = database.Table<DamageLabels>().Where(i => i.item_no == App.CurrentApp.DeliveryVehicleCheckList.item_no & i.CheckID == App.CurrentApp.VanChecksHeader.unique_id & i.vehicle_type == "delivery" & i.angle_num == App.CurrentApp.current_van_picture).ToList(); break;
                case "van":
                    labels = database.Table<DamageLabels>().Where(i => i.item_no == App.CurrentApp.WeeklyVanCheckSheet.item_no & i.CheckID == App.CurrentApp.VanChecksHeader.unique_id & i.vehicle_type == "van" & i.angle_num == App.CurrentApp.current_van_picture).ToList(); break;
                case "car":
                    labels = database.Table<DamageLabels>().Where(i => i.item_no == App.CurrentApp.CarPanelSheet.item_no & i.CheckID == App.CurrentApp.VanChecksHeader.unique_id & i.vehicle_type == "car" & i.angle_num == App.CurrentApp.current_van_picture).ToList(); break;
            }

            foreach(var label in labels)
            {
                database.Delete(label);
            }
        }

        public List<VanChecksHeader> GetOldVanCheckHeaders()
        {
            DateTime daysago = DateTime.Today.AddDays(-30);

            return database.Query<VanChecksHeader>("SELECT * FROM [VanChecksHeader] WHERE DATE([check_date]) < DATE(" + daysago.ToShortDateString() + ")");
        }

        public List<DamageLabels> GetDamageLabels()
        {
            switch (App.CurrentApp.CurrentItem)
            {
                case "deliveryvan":
                    return database.Table<DamageLabels>().Where(i => i.item_no == App.CurrentApp.DeliveryVanVehicleCheckList.item_no & i.CheckID == App.CurrentApp.VanChecksHeader.unique_id & i.vehicle_type == "deliveryvan" & i.angle_num == App.CurrentApp.current_van_picture).ToList();
                case "delivery":
                    return database.Table<DamageLabels>().Where(i => i.item_no == App.CurrentApp.DeliveryVehicleCheckList.item_no & i.CheckID == App.CurrentApp.VanChecksHeader.unique_id & i.vehicle_type == "delivery" & i.angle_num == App.CurrentApp.current_van_picture).ToList();
                case "van":
                    return database.Table<DamageLabels>().Where(i => i.item_no == App.CurrentApp.WeeklyVanCheckSheet.item_no & i.CheckID == App.CurrentApp.VanChecksHeader.unique_id & i.vehicle_type == "van" & i.angle_num == App.CurrentApp.current_van_picture).ToList();
                case "car":
                    return database.Table<DamageLabels>().Where(i => i.item_no == App.CurrentApp.CarPanelSheet.item_no & i.CheckID == App.CurrentApp.VanChecksHeader.unique_id & i.vehicle_type == "car" & i.angle_num == App.CurrentApp.current_van_picture).ToList();
            }
            return null;
        }

        public void SaveDamageLabels(List<DamageLabels> labels)
        {
            foreach(var label in labels)
            {
                database.Insert(label);
            }
        }

        public void DeleteVanCheck()
        {

        }

        public void DeleteOldVanChecks()
        {
            DateTime daysago = DateTime.Today.AddDays(-30);

            List<VanChecksHeader> headers = database.Query<VanChecksHeader>("SELECT * FROM [VanChecksHeader] WHERE DATE([check_date]) < DATE(" + daysago.ToShortDateString() + ")");

            foreach (var header in headers)
            {
                List<WeeklyVanCheckSheet> weekly_checks = database.Table<WeeklyVanCheckSheet>().Where(i => i.CheckID == header.unique_id).ToList();
                foreach (var check in weekly_checks)
                {
                    database.Delete(check);
                }
                List<CarPanelSheet> car_panels = database.Table<CarPanelSheet>().Where(i => i.CheckID == header.unique_id).ToList();
                foreach (var check in car_panels)
                {
                    database.Delete(check);
                }
                List<DeliveryVanVehicleCheckList> deliv_van_veh = database.Table<DeliveryVanVehicleCheckList>().Where(i => i.CheckID == header.unique_id).ToList();
                foreach (var check in deliv_van_veh)
                {
                    database.Delete(check);
                }
                List<DeliveryVehicleCheckList> deliv_veh = database.Table<DeliveryVehicleCheckList>().Where(i => i.CheckID == header.unique_id).ToList();
                foreach (var check in deliv_veh)
                {
                    database.Delete(check);
                }
                database.Delete(header);
            }
        }

        public void ResendVanCheck(string uid)
        {
            VanChecksHeader header = database.Table<VanChecksHeader>().Where(i => i.unique_id == uid).FirstOrDefault();

            header.bSent = false;
            database.Update(header);
        }

        public void LoadVanCheckHeader(string uid)
        {
            App.net.VanChecksHeader = database.Table<VanChecksHeader>().Where(i => i.unique_id == uid).FirstOrDefault();
        }

        public void DeleteVanCheck(string uid)
        {
            VanChecksHeader header = database.Table<VanChecksHeader>().Where(i => i.unique_id == uid).FirstOrDefault();

            List<WeeklyVanCheckSheet> weekly_checks = database.Table<WeeklyVanCheckSheet>().Where(i => i.CheckID == header.unique_id).ToList();
            foreach (var check in weekly_checks)
            {
                database.Delete(check);
            }
            List<CarPanelSheet> car_panels = database.Table<CarPanelSheet>().Where(i => i.CheckID == header.unique_id).ToList();
            foreach (var check in car_panels)
            {
                database.Delete(check);
            }
            List<DeliveryVanVehicleCheckList> deliv_van_veh = database.Table<DeliveryVanVehicleCheckList>().Where(i => i.CheckID == header.unique_id).ToList();
            foreach (var check in deliv_van_veh)
            {
                database.Delete(check);
            }
            List<DeliveryVehicleCheckList> deliv_veh = database.Table<DeliveryVehicleCheckList>().Where(i => i.CheckID == header.unique_id).ToList();
            foreach (var check in deliv_veh)
            {
                database.Delete(check);
            }
            database.Delete(header);
        }

        public List<VanChecksHeader> GetAllVanCheckHeadersSorted()
        {
            return database.Table<VanChecksHeader>().Where(i => i.check_date != "").OrderByDescending(x => x.check_date).ToList();
        }

        public List<VanChecksHeader> GetVanChecksByDateandBranch(string check_date, string branch)
        {
            return database.Table<VanChecksHeader>().Where(i => i.check_date == check_date & i.spare_s_1 == branch).ToList();
        }

        public List<WeeklyVanCheckSheet> GetWeeklyVanChecksByIDandReg(string CheckID, string vehicle_reg)
        {
            return database.Table<WeeklyVanCheckSheet>().Where(i => i.CheckID == CheckID & i.vehicle_reg == vehicle_reg).ToList();
        }

        public List<CarPanelSheet> GetCarPanelSheetByIDandReg(string CheckID, string vehicle_reg)
        {
            return database.Table<CarPanelSheet>().Where(i => i.CheckID == CheckID & i.vehicle_reg == vehicle_reg).ToList();
        }

        public List<DeliveryVanVehicleCheckList> GetDeliveryVanVehicleCheckListByIDandReg(string CheckID, string vehicle_reg)
        {
            return database.Table<DeliveryVanVehicleCheckList>().Where(i => i.CheckID == CheckID & i.vehicle_registration == vehicle_reg).ToList();
        }

        public List<DeliveryVehicleCheckList> GetDeliveryVehicleCheckListByIDandReg(string CheckID, string vehicle_reg)
        {
            return database.Table<DeliveryVehicleCheckList>().Where(i => i.CheckID == CheckID & i.vehicle_registration == vehicle_reg).ToList();
        }

        public List<WeeklyVanCheckSheet> GetWeeklyVanChecksByID(string CheckID)
        {
            return database.Table<WeeklyVanCheckSheet>().Where(i => i.CheckID == CheckID).ToList();
        }

        public List<CarPanelSheet> GetCarPanelSheetByID(string CheckID)
        {
            return database.Table<CarPanelSheet>().Where(i => i.CheckID == CheckID).ToList();
        }

        public List<DeliveryVanVehicleCheckList> GetDeliveryVanVehicleCheckListByID(string CheckID)
        {
            return database.Table<DeliveryVanVehicleCheckList>().Where(i => i.CheckID == CheckID).ToList();
        }

        public List<DeliveryVehicleCheckList> GetDeliveryVehicleCheckListByID(string CheckID)
        {
            return database.Table<DeliveryVehicleCheckList>().Where(i => i.CheckID == CheckID).ToList();
        }

        public int SaveVanChecksHeader()
        {
            if (App.net.VanChecksHeader.RecID != 0)
                return database.Update(App.net.VanChecksHeader);
            else
                return database.Insert(App.net.VanChecksHeader);
        }

        public int SaveVanChecksVan()
        {
            if (App.net.WeeklyVanCheckSheet.RecID != 0)
                return database.Update(App.net.WeeklyVanCheckSheet);
            else
                return database.Insert(App.net.WeeklyVanCheckSheet);
        }

        public int SaveVanChecksCar()
        {
            if (App.net.CarPanelSheet.RecID != 0)
                return database.Update(App.net.CarPanelSheet);
            else
                return database.Insert(App.net.CarPanelSheet);
        }

        public int SaveVanChecksDelivery()
        {
            if (App.net.DeliveryVehicleCheckList.RecID != 0)
                return database.Update(App.net.DeliveryVehicleCheckList);
            else
                return database.Insert(App.net.DeliveryVehicleCheckList);
        }

        public int SaveVanChecksDeliveryVan()
        {
            if (App.net.DeliveryVanVehicleCheckList.RecID != 0)
                return database.Update(App.net.DeliveryVanVehicleCheckList);
            else
                return database.Insert(App.net.DeliveryVanVehicleCheckList);
        }

        public bool LoadVan(int recID)
        {
            App.net.WeeklyVanCheckSheet = database.Table<WeeklyVanCheckSheet>().Where(i => i.RecID == recID).FirstOrDefault();
            if(App.net.WeeklyVanCheckSheet!=null)
                return true;
            else
                return false;
        }
        public bool LoadCar(int recID)
        {
            App.net.CarPanelSheet = database.Table<CarPanelSheet>().Where(i => i.RecID == recID).FirstOrDefault();
            if (App.net.CarPanelSheet != null)
                return true;
            else
                return false;
        }
        public bool LoadDelivery(int recID)
        {
            App.net.DeliveryVehicleCheckList = database.Table<DeliveryVehicleCheckList>().Where(i => i.RecID == recID).FirstOrDefault();
            if (App.net.DeliveryVehicleCheckList != null)
                return true;
            else
                return false;
        }
        public bool LoadDeliveryVan(int recID)
        {
            App.net.DeliveryVanVehicleCheckList = database.Table<DeliveryVanVehicleCheckList>().Where(i => i.RecID == recID).FirstOrDefault();
            if (App.net.DeliveryVanVehicleCheckList != null)
                return true;
            else
                return false;
        }

        public void LoadVanCheck(string uid)
        {
            App.net.VanChecksHeader = database.Table<VanChecksHeader>().Where(i => i.unique_id == uid).FirstOrDefault();
            App.net.DeliveryVehicleCheckLists = database.Table<DeliveryVehicleCheckList>().Where(i => i.CheckID == uid).ToList();
            App.net.DeliveryVanVehicleCheckLists = database.Table<DeliveryVanVehicleCheckList>().Where(i => i.CheckID == uid).ToList();
            App.net.WeeklyVanCheckSheets = database.Table<WeeklyVanCheckSheet>().Where(i => i.CheckID == uid).ToList();
            App.net.CarPanelSheets = database.Table<CarPanelSheet>().Where(i => i.CheckID == uid).ToList();
        }

        public void SaveVanChecks()
        {
            // Check if all completed
            int complete = 0, inconplete = 0;

            App.CurrentApp.total_delivery = 0;
            App.CurrentApp.total_delivery_van = 0;
            App.CurrentApp.total_vans = 0;
            App.CurrentApp.total_cars = 0;

            App.CurrentApp.total_incomplete_delivery = 0;
            App.CurrentApp.total_incomplete_delivery_van = 0;
            App.CurrentApp.total_incomplete_vans = 0;
            App.CurrentApp.total_incomplete_cars = 0;

            if (App.CurrentApp.VanChecksHeader != null)
            {
                List<DeliveryVanVehicleCheckList> delivery_van_vehicle = App.data.GetDeliveryVanVehicleCheckListByID(App.net.VanChecksHeader.unique_id);

                foreach (var item in delivery_van_vehicle)
                {
                    if (item.bComplete == true)
                    {
                        App.CurrentApp.total_delivery_van++;
                        complete++;
                    }
                    else
                    {
                        App.CurrentApp.total_incomplete_delivery_van++;
                        inconplete++;

                    }
                }

                List<DeliveryVehicleCheckList> delivery_vehicle = App.data.GetDeliveryVehicleCheckListByID(App.net.VanChecksHeader.unique_id);

                foreach (var item in delivery_vehicle)
                {
                    if (item.bComplete == true)
                    {
                        App.CurrentApp.total_delivery++;
                        complete++;
                    }
                    else
                    {
                        App.CurrentApp.total_incomplete_delivery++;
                        inconplete++;
                    }
                }

                List<WeeklyVanCheckSheet> weekly_van = App.data.GetWeeklyVanChecksByID(App.net.VanChecksHeader.unique_id);

                foreach (var item in weekly_van)
                {
                    if (item.bComplete == true)
                    {
                        App.CurrentApp.total_vans++;
                        complete++;
                    }
                    else
                    {
                        App.CurrentApp.total_incomplete_vans++;
                        inconplete++;
                    }
                }

                List<CarPanelSheet> car_panel = App.data.GetCarPanelSheetByID(App.net.VanChecksHeader.unique_id);

                foreach (var item in car_panel)
                {
                    if (item.bComplete == true)
                    {
                        App.CurrentApp.total_cars++;
                        complete++;
                    }
                    else
                    {
                        App.CurrentApp.total_incomplete_cars++;
                        inconplete++;
                    }
                }

                if (complete > 0 && inconplete == 0)
                {
                    App.CurrentApp.VanChecksHeader.bComplete = true;
                }
                else
                {
                    App.CurrentApp.VanChecksHeader.bComplete = false;
                }

                App.CurrentApp.VanChecksHeader.total_delivery_van = App.CurrentApp.total_delivery_van + App.CurrentApp.total_incomplete_delivery_van;
                App.CurrentApp.VanChecksHeader.total_delivery = App.CurrentApp.total_delivery + App.CurrentApp.total_incomplete_delivery;
                App.CurrentApp.VanChecksHeader.total_vans = App.CurrentApp.total_vans + App.CurrentApp.total_incomplete_vans;
                App.CurrentApp.VanChecksHeader.total_cars = App.CurrentApp.total_cars + App.CurrentApp.total_incomplete_cars;

                App.CurrentApp.VanChecksHeader.total_incomplete_delivery_van = App.CurrentApp.total_incomplete_delivery_van;
                App.CurrentApp.VanChecksHeader.total_incomplete_delivery = App.CurrentApp.total_incomplete_delivery;
                App.CurrentApp.VanChecksHeader.total_incomplete_vans = App.CurrentApp.total_incomplete_vans;
                App.CurrentApp.VanChecksHeader.total_incomplete_cars = App.CurrentApp.total_incomplete_cars;

                SaveVanChecksHeader();
            }
        }
    }
}
