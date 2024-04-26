using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using System;

namespace PropertySurvey
{
    public partial class Functions
    {
        public void DeleteAccident(int recID)
        {
            Accident_sheet accident = database.Table<Accident_sheet>().Where(i => i.RecID == recID).FirstOrDefault();
            if (accident != null)
            {
                foreach (var item in App.files.GetFileList("Photos/", String.Format("9{0:0000000}", accident.RecID) + "*.*", "Photos/"))
                {
                    App.files.DeleteFile(item);
                }
                foreach (var item in App.files.GetFileList("Drawings/", String.Format("9{0:0000000}", accident.RecID) + "*.*", "Drawings/"))
                {
                    App.files.DeleteFile(item);
                }
                foreach (var item in App.files.GetFileList("Signatures/", String.Format("9{0:0000000}", accident.RecID) + "*.*", "Signatures/"))
                {
                    App.files.DeleteFile(item);
                }

                database.Delete(accident);
            }
        }
        public List<Accident_sheet> GetUnsentAccidents()
        {
            return database.Table<Accident_sheet>().Where(i => i.bSent == false & i.bComplete == true).ToList();
        }

        public List<Accident_sheet> GetVehicleAccidents()
        {
            return database.Query<Accident_sheet>("SELECT * FROM Accident_sheet order by date_time desc");
        }

        public Accident_sheet GetVehicleAccident(int AccidentID)
        {
            return database.Table<Accident_sheet>().Where(i => i.RecID == AccidentID).FirstOrDefault();
        }

        public void SaveVehicleAccident()
        {
            if (App.net.AccidentRecord.RecID != 0)
                database.Update(App.net.AccidentRecord);
            else
                database.Insert(App.net.AccidentRecord);
        }

        public void DeleteVehicleAccident(int AccidentRecID)
        {
            database.Query<WhitnessesData>("delete from WhitnessesData where AccidentRecID = '" + AccidentRecID + "'");
            database.Query<Accident_sheet>("delete from Accident_sheet where RecId = '" + AccidentRecID + "'");
        }

        public List<WhitnessesData> GetVehicleWitnesses(int AccidentRecID)
        {
            return database.Query<WhitnessesData>("SELECT * FROM WhitnessesData where AccidentRecID = '" + AccidentRecID + "'");
        }

        public WhitnessesData GetVehicleWitness(int WitnessID)
        {
            return database.Table<WhitnessesData>().Where(i => i.RecID == WitnessID).FirstOrDefault();
        }

        public void SaveVehicleWitness()
        {
            if (App.net.WitnessRecord.RecID != 0)
                database.Update(App.net.WitnessRecord);
            else
                database.Insert(App.net.WitnessRecord);
        }

        public void DeleteVehicleWitness(int WitnessID)
        {
            database.Query<WhitnessesData>("delete from WhitnessesData where RecID = '" + WitnessID + "'");
        }

        public List<FAccidentsTable> GetWorkAccidents()
        {
            return database.Query<FAccidentsTable>("SELECT * FROM FAccidentsTable order by date_time desc");
        }

        public FAccidentsTable GetWorkAccident(int AccidentID)
        {
            return database.Table<FAccidentsTable>().Where(i => i.RecID == AccidentID).FirstOrDefault();
        }

        public void SaveWorkAccident()
        {
            if (App.net.FAccidentsRecord.RecID != 0)
                database.Update(App.net.FAccidentsRecord);
            else
                database.Insert(App.net.FAccidentsRecord);
        }

        public void DeleteWorkAccident(int AccidentRecID)
        {
            database.Query<FAccidentsTable>("delete from FAccidentsTable where RecId = '" + AccidentRecID + "'");
        }

    }
}