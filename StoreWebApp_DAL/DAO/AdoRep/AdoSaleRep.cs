using Microsoft.Data.SqlClient;
using StoreWebApp_DAL.DAO.Interfaces;
using StoreWebApp_Model.Models;

namespace StoreWebApp_DAL.DAO.AdoRep
{
    public class AdoSaleRep : AdoRepo, IRepSale
    {
        public void CreateSale(Sale sale)
        {
            string query = "INSERT INTO Sale" +
                "(saleQty, saleDate, salePrice, productId)" +
                "VALUES" +
                "(@qty, @date, @price, @productId);";

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@qty", sale.Quantity);
                command.Parameters.AddWithValue("@date", sale.Date);
                command.Parameters.AddWithValue("@price", sale.Price);
                command.Parameters.AddWithValue("@productId", sale.ProductId);

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

        public void DeleteSale(int id)
        {
            string query = "DELETE FROM Sale WHERE saleId = @id";

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

        public Sale? GetSaleById(int? id)
        {
            string query = "SELECT * FROM Sale WHERE saleId = @id;";
            Sale? sale = null;

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
                        sale = new Sale
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
            return sale;
        }

        public List<Sale> GetSales()
        {
            string query = "SELECT * FROM Sale";
            List<Sale> sales = new List<Sale>();

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        sales.Add(new Sale
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
            return sales;
        }

        public bool SaleExists(int? id)
        {
            if (GetSaleById(id) != null) return true;

            return false;
        }

        public void UpdateSale(Sale sale)
        {
            string query = "UPDATE Sale SET " +
                "saleQty = @qty," +
                "saleDate = @date," +
                "salePrice = @price," +
                "productId = @product " +
                "WHERE saleId = @id";

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@id", sale.Id);
                command.Parameters.AddWithValue("@qty", sale.Quantity);
                command.Parameters.AddWithValue("@date", sale.Date);
                command.Parameters.AddWithValue("@price", sale.Price);
                command.Parameters.AddWithValue("@product", sale.ProductId);

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
