using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Diagnostics;

namespace DBConnectedFinalProjectThing.Services
{
    public class GymDbService
    {
        private readonly string _connectionString;

        public GymDbService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Equipment> GetEquipment()
        {
            var equipmentList = new List<Equipment>();

            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM equipment", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                equipmentList.Add(new Equipment
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    UnitSize = reader.GetInt32(reader.GetOrdinal("unit_size")),
                                    Name = reader.GetString(reader.GetOrdinal("name")),
                                    Active = reader.GetString(reader.GetOrdinal("active"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching equipment: {ex.Message}");
            }

            return equipmentList;
        }

        public void AddEquipment(int unitSize, string name, string active)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand("INSERT INTO equipment (unit_size, name, active) VALUES (:unitSize, :name, :active)", connection))
                {
                    command.Parameters.Add(new OracleParameter("unitSize", unitSize));
                    command.Parameters.Add(new OracleParameter("name", name));
                    command.Parameters.Add(new OracleParameter("active", active));

                    command.ExecuteNonQuery();
                }
            }
        }

        public void ToggleEquipmentStatus(int equipmentId)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                connection.Open();

                string currentStatus;
                using (OracleCommand getStatus = new OracleCommand("SELECT active FROM equipment WHERE id = :equipmentId", connection))
                {
                    getStatus.Parameters.Add(new OracleParameter("equipmentId", equipmentId));

                    currentStatus = (string)getStatus.ExecuteScalar();
                }

                string newStatus = currentStatus == "Y" ? "N" : "Y";

                using (OracleCommand updateCommand = new OracleCommand("UPDATE equipment SET active = :newStatus WHERE id = :equipmentId", connection))
                {
                    updateCommand.Parameters.Add(new OracleParameter("newStatus", newStatus));
                    updateCommand.Parameters.Add(new OracleParameter("equipmentId", equipmentId));

                    updateCommand.ExecuteNonQuery();
                }
            }
        }
        public class Equipment
        {
            public int Id { get; set; }
            public int UnitSize { get; set; }
            public string Name { get; set; }
            public string Active { get; set; }
        }
    }
}
