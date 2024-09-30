using Microsoft.Data.SqlClient;
using StoreWebApp_DAL.DAO.Interfaces;
using StoreWebApp_Model.Models;

namespace StoreWebApp_DAL.DAO.AdoRep
{
    public class AdoInventoryRep : AdoRepo, IRepInventory
    {
        public void CreateInventory(Inventory inventory)
        {
            string query = "INSERT INTO Inventory" +
                "(inventoryQty, inventoryLastUpdate, productId)" +
                "VALUES" +
                "(@qty, @date, @productId);";

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@qty", inventory.Quantity);
                command.Parameters.AddWithValue("@date", inventory.Date);
                command.Parameters.AddWithValue("@productId", inventory.ProductId);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        public void DeleteInventory(int id)
        {
            string query = "DELETE FROM Inventory WHERE inventoryId = @id";

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Inventory> GetInventories()
        {
            string query = "SELECT * FROM Inventory";
            List<Inventory> inventories = new List<Inventory>();

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        inventories.Add(new Inventory
                        {
                            Id = (int)reader[0],
                            Quantity = (int)reader[1],
                            Date = (DateTime)reader[2],
                            ProductId = (int)reader[3],
                            Product = new AdoProductRep().GetProductById((int)reader[3])
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return inventories;
        }

        public Inventory? GetInventoryById(int? id)
        {
            string query = "SELECT * FROM Inventory WHERE inventoryId = @id;";
            Inventory? inventory = null;

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        inventory = new Inventory
                        {
                            Id = (int)reader[0],
                            Quantity = (int)reader[1],
                            Date = (DateTime)reader[2],
                            ProductId = (int)reader[3],
                        };
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return inventory;
        }

        public bool InventoryExists(int? id)
        {
            if (GetInventoryById(id) != null) return true;

            return false;
        }

        public void UpdateInventory(Inventory inventory)
        {
            string query = "UPDATE Inventory SET " +
                "inventoryQty = @qty," +
                "inventoryLastUpdate = @date," +
                "productId = @product " +
                "WHERE inventoryId = @id";

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@id", inventory.Id);
                command.Parameters.AddWithValue("@qty", inventory.Quantity);
                command.Parameters.AddWithValue("@date", inventory.Date);
                command.Parameters.AddWithValue("@product", inventory.ProductId);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
