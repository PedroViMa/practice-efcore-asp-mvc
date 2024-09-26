using Microsoft.Data.SqlClient;
using StoreWebApp_DAL.DAO.Interfaces;
using StoreWebApp_Model.Models;

namespace StoreWebApp_DAL.DAO.AdoRep
{
    public class AdoPurchaseRep : AdoRepo, IRepPurchase
    {
        public void CreatePurchase(Purchase purchase)
        {
            string query = "INSERT INTO Purchase" +
                "(purchaseQty, purchaseDate, purchasePrice, productId)" +
                "VALUES" +
                "(@qty, @date, @price, @productId);";

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@qty", purchase.Quantity);
                command.Parameters.AddWithValue("@date", purchase.Date);
                command.Parameters.AddWithValue("@price", purchase.Price);
                command.Parameters.AddWithValue("@productId", purchase.ProductId);

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

        public void DeletePurchase(int id)
        {
            string query = "DELETE FROM Purchase WHERE purchaseId = @id";

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

        public Purchase? GetPurchaseById(int? id)
        {
            string query = "SELECT * FROM Purchase WHERE purchaseId = @id;";
            Purchase? purchase = null;

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
                        purchase = new Purchase
                        {
                            Id = (int)reader[0],
                            Quantity = (int)reader[1],
                            Date = (DateTime)reader[2],
                            Price = (decimal)reader[3],
                            ProductId = (int)reader[4],
                        };
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return purchase;
        }

        public List<Purchase> GetPurchases()
        {
            string query = "SELECT * FROM Purchase";
            List<Purchase> purchases = new List<Purchase>();

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        purchases.Add(new Purchase
                        {
                            Id = (int)reader[0],
                            Quantity = (int)reader[1],
                            Date = (DateTime)reader[2],
                            Price = (decimal)reader[3],
                            ProductId = (int)reader[4],
                            Product = new AdoProductRep().GetProductById((int)reader[4])
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return purchases;
        }

        public void UpdatePurchase(Purchase purchase)
        {
            string query = "UPDATE Purchase SET " +
                "purchaseQty = @qty," +
                "purchaseDate = @date," +
                "purchasePrice = @price," +
                "productId = @product " +
                "WHERE purchaseId = @id";

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@id", purchase.Id);
                command.Parameters.AddWithValue("@qty", purchase.Quantity);
                command.Parameters.AddWithValue("@date", purchase.Date);
                command.Parameters.AddWithValue("@price", purchase.Price);
                command.Parameters.AddWithValue("@product", purchase.ProductId);

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
