using Microsoft.Maui.Storage;
using System.IO;
using System.Threading.Tasks;
using SQLite;

namespace DBConnectedFinalProjectThing.Services
{
    public class GymDBService
    {
        private readonly string _databasePath;
        public GymDBService()
        {
            var localFolder = FileSystem.AppDataDirectory;
            _databasePath = Path.Combine(localFolder, "gymfiledb.db");

            if (!File.Exists(_databasePath))
            {
                var resourcePath = Path.Combine(FileSystem.AppDataDirectory, "Resources", "DB", "gymfiledb.db");
                File.Copy(resourcePath, _databasePath);
            }
        }
        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_databasePath);
        }
        public void AddEquipment(Equipment equipment)
        {
            using (var db = GetConnection())
            {
                db.Insert(equipment);
            }
        }
    }

}
