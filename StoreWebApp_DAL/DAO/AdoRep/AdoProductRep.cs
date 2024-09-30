using Microsoft.Data.SqlClient;
using StoreWebApp_DAL.DAO.Interfaces;
using StoreWebApp_Model.Models;

namespace StoreWebApp_DAL.DAO.AdoRep
{
    public class AdoProductRep : AdoRepo, IRepProduct
    {
        public void CreateProduct(Product product)
        {
            string query = "INSERT INTO Product" +
                "(productName, productDescription, productPrice, productStock)" +
                "VALUES" +
                "(@name, @desc, @price, @stock);";

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@desc", product.Description);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@stock", product.QuantityInStock);

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

        public void DeleteProduct(int id)
        {
            string query = "DELETE FROM Product WHERE productId = @id";

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

        public Product? GetProductById(int? id)
        {
            string query = "SELECT * FROM Product WHERE productId = @id;";
            Product? product = null;

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
                        product = new Product
                        {
                            Id = (int)reader[0],
                            Name = (string)reader[1],
                            Description = reader[2] is DBNull ? null : reader[2].ToString(),
                            Price = (decimal)reader[3],
                            QuantityInStock = (int)reader[4]
                        };
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return product;
        }

        public List<Product> GetProducts()
        {
            string query = "SELECT * FROM Product";
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = (int)reader[0],
                            Name = (string)reader[1],
                            Description = reader[2] is DBNull ? null : reader[2].ToString(),
                            Price = (decimal)reader[3],
                            QuantityInStock = (int)reader[4]
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return products;
        }

        public bool ProductExists(int? id)
        {
            if (GetProductById(id) != null) return true;

            return false;
        }

        public void UpdateProduct(Product product)
        {
            string query = "UPDATE Product SET " +
                "productName = @name," +
                "productDescription = @desc," +
                "productPrice = @price," +
                "productStock = @stock " +
                "WHERE productId = @id";

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@id", product.Id);
                command.Parameters.AddWithValue("@desc", product.Description);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@stock", product.QuantityInStock);
                command.Parameters.AddWithValue("@name", product.Name);

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
