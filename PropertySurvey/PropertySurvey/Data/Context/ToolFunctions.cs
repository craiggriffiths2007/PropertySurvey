using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using System;

namespace PropertySurvey
{
    public partial class Functions
    {
        public void SaveToolsRecord()
        {
            if (App.net.ToolsRecord.RecID != 0)
            {
                database.Update(App.net.ToolsRecord);
            }
            else
            {
                database.Insert(App.net.ToolsRecord);
            }
        }

        public List<ToolsTable> GetToolsChecks()
        {
            return database.Query<ToolsTable>("SELECT * FROM [ToolsTable]");
        }

        public void DeleteToolsCheck(int id)
        {
            database.Delete(database.Table<ToolsTable>().Where(i => i.RecID == id).FirstOrDefault());
        }

        public void LoadToolsCheck(int id)
        {
            App.net.ToolsRecord = database.Table<ToolsTable>().Where(i => i.RecID == id).FirstOrDefault();
        }

    }
}