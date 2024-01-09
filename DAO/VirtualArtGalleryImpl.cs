using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Entity;
using VirtualArtGallery.MyException;
using VirtualArtGallery.Util;

namespace VirtualArtGallery.DAO
{
    public class VirtualArtGalleryImpl:IVirtualArtGallery
    {
        private static SqlConnection connection;

        public VirtualArtGalleryImpl()
        {
            // Initialize the connection
            connection = DBConnection.GetConnection();
        }

        // Artwork Management
        public bool AddArtwork(Artwork artwork)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT INTO Artwork (Title, Description, CreationDate, Medium, ImageURL, ArtistID)" +
                        "values(@Title,@Discription,@creationDate,@medium,@imageURL,@artistId)";
                cmd.Parameters.AddWithValue("@Title", artwork.Title);
                cmd.Parameters.AddWithValue("@Discription", artwork.Description);
                cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                cmd.Parameters.AddWithValue("@medium", artwork.Medium);
                cmd.Parameters.AddWithValue("@imageURL", artwork.ImageURL);
                cmd.Parameters.AddWithValue("@artistId", artwork.ArtistID);

                connection.Open();

                int rowsCount = cmd.ExecuteNonQuery();
               

                if (rowsCount > 0)
                {
                   
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred during Insert operation: {ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();

            }
        }



        public bool UpdateArtwork(Artwork artwork)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "UPDATE Artwork SET Title = @Title, Description = @Description, CreationDate = @CreationDate, Medium = @Medium, ImageURL = @ImageURL, ArtistID = @ArtistID WHERE ArtworkID = @ArtworkID";
                cmd.Parameters.AddWithValue("@Title", artwork.Title);
                cmd.Parameters.AddWithValue("@Description", artwork.Description);
                cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                cmd.Parameters.AddWithValue("@Medium", artwork.Medium);
                cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                cmd.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);
                cmd.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);
                connection.Open();

                int updateStatus = cmd.ExecuteNonQuery();
                return updateStatus > 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while Updating artwork details: {ex.Message}");
                throw new ArtWorkNotFoundException();
                
            }
            finally
            {
                connection.Close();
            }

        }

        public bool RemoveArtwork(int artworkID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = $"DELETE FROM Artwork WHERE ArtworkID = {artworkID}";

                connection.Open();

                int removeArtworkStatus = cmd.ExecuteNonQuery();
                return removeArtworkStatus > 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ArtWorkNotFoundException();
                
            }
            finally
            {
                connection.Close();
            }
        }

        public Artwork GetArtworkById(int artworkID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"Select * from Artwork where ArtworkID= {artworkID}";
            connection.Open();
            
            SqlDataReader data = cmd.ExecuteReader();

            if (data.Read())
            {
                Artwork artwork = new Artwork
                 (
                    
                     data["Title"].ToString(),
                     data["Description"].ToString(),
                     Convert.ToDateTime(data["CreationDate"]),
                     data["Medium"].ToString(),
                     data["ImageURL"].ToString(),
                     Convert.ToInt32(data["ArtistID"]),
                      Convert.ToInt32(data["ArtworkID"])
                 );
                data.Close();
                connection.Close();
                return artwork;
            }
            else
            {
                data.Close();
                connection.Close();

                throw new ArtWorkNotFoundException();
            }
        }

        public List<Artwork> SearchArtworks(string keyword)
        {
            try
            {
                List<Artwork> artworkList = new List<Artwork>();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Artwork WHERE Title LIKE @keyword OR Description LIKE @keyword";
                cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");
                connection.Open();

                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    while (data.Read())
                    {
                        artworkList.Add(new Artwork(

                             data["Title"].ToString(),
                             data["Description"].ToString(),
                             Convert.ToDateTime(data["CreationDate"]),
                             data["Medium"].ToString(),
                             data["ImageURL"].ToString(),
                             Convert.ToInt32(data["ArtistID"]),
                             Convert.ToInt32(data["ArtworkID"])
                            ));
                    }
                }


                connection.Close();

                if (artworkList.Count > 0)
                {
                    return artworkList;
                }
                else
                { return null; }

            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while getting artwork details: {ex.Message}");
                return null;
            }
        }



        // User Favorites
        public bool AddArtworkToFavorite(int userId, int artworkId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = $"Insert into User_Favorite_Artwork(UserID, ArtworkID) VALUES ({userId}, {artworkId})";
                connection.Open();
                int addFavoriteStatus = cmd.ExecuteNonQuery();
                return (addFavoriteStatus > 0);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred during Insert operation: {ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            }

        }
        public List<int> GetUserFavoriteArtworks(int userId)
        {
            List<int> userFavorites = new List<int>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"Select * from User_Favorite_Artwork where UserID = {userId}";
            connection.Open();

            SqlDataReader data = cmd.ExecuteReader();

            while (data.Read())
            {
                userFavorites.Add(Convert.ToInt32(data["ArtworkID"]));
            }
           
            if (userFavorites.Count< 1)
           
            {
                throw new UserNotFoundException();
            }
            data.Close();
            connection.Close();

            return userFavorites;
        }


        public bool RemoveArtworkFromFavorite(int userId, int artworkId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = $"DELETE FROM User_Favorite_Artwork  WHERE UserID = {userId} AND ArtworkID = { artworkId }";
                connection.Open();
                int removeFavoriteStatus = cmd.ExecuteNonQuery();
                return removeFavoriteStatus > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during Delete operation: {ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }


        //getting input from user
        public Artwork GetArtworkDetailsFromUser()
        {
            try
            {
                Console.Write("Enter Title:");
                string title = Console.ReadLine();

                Console.Write("Enter Description:");
                string description = Console.ReadLine();

                Console.Write("Enter Creation Date (yyyy-MM-dd):");
                DateTime creationDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);

                Console.Write("Enter Medium:");
                string medium = Console.ReadLine();

                Console.Write("Enter Image URL:");
                string imageURL = Console.ReadLine();

                Console.Write("Enter Artist ID:");
                int artistID = int.Parse(Console.ReadLine());

                return new Artwork(title, description, creationDate, medium, imageURL, artistID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }

        }

        //display artwork
        public void DisplayArtwork(Artwork artwork)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine($"ArtwordID : {artwork.ArtworkID}");
            Console.WriteLine($"Title : {artwork.Title}");
            Console.WriteLine($"Description : {artwork.Description}");
            Console.WriteLine($"CreationDate : {artwork.CreationDate}");
            Console.WriteLine($"Medium : {artwork.Medium}");
            Console.WriteLine($"ImageURL : {artwork.ImageURL}");
            Console.WriteLine($"ArtistID : {artwork.ArtistID}");
        }


        }
}
