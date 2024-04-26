using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using System;

namespace PropertySurvey
{
    public partial class Functions
    {
        public void SaveLadderRecord()
        {
            if (App.net.LadderRecord.RecID != 0)
            {
                database.Update(App.net.LadderRecord);
            }
            else
            {
                database.Insert(App.net.LadderRecord);
            }
        }

        public List<LaddersTable> GetLadderChecks()
        {
            return database.Query<LaddersTable>("SELECT * FROM [LaddersTable]");
        }

        public void DeleteLadderCheck(int id)
        {
            database.Delete(database.Table<LaddersTable>().Where(i => i.RecID == id).FirstOrDefault());
        }

        public void LoadLadderCheck(int id)
        {
            App.net.LadderRecord = database.Table<LaddersTable>().Where(i => i.RecID == id).FirstOrDefault();
        }
    }
}