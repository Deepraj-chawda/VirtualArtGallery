using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtual_Art_Gallery.MyException;
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
        
        /// <summary>
        /// This Method is used to add Artwork In DB.
        /// </summary>
        public bool AddArtwork(Artwork artwork)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT INTO Artwork (Title, Description, CreationDate, Medium, ImageURL, ArtistID)" +
                        "values(@Title,@Discription,@CreationDate,@Medium,@ImageURL,@ArtistId)";
                cmd.Parameters.AddWithValue("@Title", artwork.Title);
                cmd.Parameters.AddWithValue("@Discription", artwork.Description);
                cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                cmd.Parameters.AddWithValue("@Medium", artwork.Medium);
                cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                cmd.Parameters.AddWithValue("@ArtistId", artwork.ArtistID);

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


        /// <summary>
        /// This Method is used to Update the details of Artwork In DB.
        /// </summary>
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

        /// <summary>
        /// This Method is used to remove Artwork by ID from DB.
        /// </summary>
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

        /// <summary>
        /// This Method is used to Get the detail of Artwork by ID from DB.
        /// </summary>
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

        /// <summary>
        /// This Method is used to Search Artwork In DB using a keyword.
        /// </summary>
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

        /// <summary>
        /// This Method is used to add Artwork to Favorite In DB.
        /// </summary>
        public bool AddArtworkToFavorite(int userId, int artworkId)
        {
            try
            {   //check artwork present or not
                Artwork checkArtwork = GetArtworkById(artworkId);

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

        /// <summary>
        /// This Method is used to get user favorite Artwork In DB.
        /// </summary>
        public List<int> GetUserFavoriteArtworks(int userId)
        {
            try
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

                if (userFavorites.Count < 1)

                {
                    throw new UserNotFoundException();
                }
                data.Close();
                

                return userFavorites;
            }
            catch(UserNotFoundException userex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"|| ERROR : {userex.Message} ||");
                Console.ResetColor();
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// This Method is used to Remove Arwork from user favoriteIn DB.
        /// </summary>
        public bool RemoveArtworkFromFavorite(int userId, int artworkId)
        {
            try
            {

                //check artwork present or not
                Artwork checkArtwork = GetArtworkById(artworkId);

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

        // Gallery management
        /// <summary>
        /// This Method is used to Add gallery In DB.
        /// </summary>
        public bool AddGallery(Gallery gallery)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO Gallery (Name, Description, Location, Curator, OpeningHours)" +
                        "VALUES(@Name, @Description, @Location, @Curator, @OpeningHours)";
                    cmd.Parameters.AddWithValue("@Name", gallery.Name);
                    cmd.Parameters.AddWithValue("@Description", gallery.Description);
                    cmd.Parameters.AddWithValue("@Location", gallery.Location);
                    cmd.Parameters.AddWithValue("@Curator", gallery.Curator);
                    cmd.Parameters.AddWithValue("@OpeningHours", gallery.OpeningHours);
                    

                    connection.Open();

                    int rowsCount = cmd.ExecuteNonQuery();
                    return rowsCount > 0;
                    

                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during Insert operation: {ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();

            }
        }

        /// <summary>
        /// This Method is used to get gallery details by ID from DB.
        /// </summary>
        public Gallery GetGalleryById(int galleryID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = $"Select * from Gallery where GalleryID= {galleryID}";
                connection.Open();

                using (SqlDataReader data = cmd.ExecuteReader())
                {

                    if (data.Read())
                    {
                        Gallery gallery = new Gallery
                         (

                             data["Name"].ToString(),
                             data["Description"].ToString(),
                             data["Location"].ToString(),
                             Convert.ToInt32(data["Curator"]),
                             data["OpeningHours"].ToString(),
                             Convert.ToInt32(data["GalleryID"])
                         );

                        
                        return gallery;
                    }
                    else
                    {
                        throw new GalleryNotFoundException();
                    }

                }
            }catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"|| Error : {ex.Message} ||");
                Console.ResetColor();
            }
            finally
            {
                connection.Close();
            }
            return null;

        }

        /// <summary>
        /// This Method is used to Remove Gallery from DB.
        /// </summary>
        public bool RemoveGallery(int galleryID)
        {
            try
            {
                // check gallery present or not
                Gallery checkgallery = GetGalleryById(galleryID);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = $"DELETE FROM Gallery WHERE GalleryID = {galleryID}";

                connection.Open();

                int Status = cmd.ExecuteNonQuery();
                return Status > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                

            }
            finally
            {
                connection.Close();
            }
            return false;
        }

        /// <summary>
        /// This Method is used to update gallery details In DB.
        /// </summary>
        public bool UpdateGallery(Gallery gallery)
        {
            try
            {
                //check gallery present or not
                Gallery checkgallery = GetGalleryById(gallery.GalleryID);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "UPDATE Gallery SET Name = @Name, Description = @Description, Location = @Location, Curator = @Curator, OpeningHours = @OpeningHours WHERE GalleryID = @GalleryID";
                cmd.Parameters.AddWithValue("@Name", gallery.Name);
                cmd.Parameters.AddWithValue("@Description", gallery.Description);
                cmd.Parameters.AddWithValue("@Location", gallery.Location);
                cmd.Parameters.AddWithValue("@Curator", gallery.Curator);
                cmd.Parameters.AddWithValue("@OpeningHours", gallery.OpeningHours);
                cmd.Parameters.AddWithValue("@GalleryID", gallery.GalleryID);
                
                connection.Open();

                int updateStatus = cmd.ExecuteNonQuery();
                return updateStatus > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while Updating artwork details: {ex.Message}");
                return false;

            }
            finally
            {
                connection.Close();
            }

        }


        //user management
        /// <summary>
        /// This Method is used to Login a user.
        /// </summary>
        public User UserLogin(string email, string password)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand()) {
                    cmd.Connection = connection;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = $"SELECT * FROM [User] WHERE Email = '{email}'";
                    connection.Open();
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        if (reader.Read())
                        {
                            User user = new User()
                            {
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Username = reader["Username"].ToString(),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString(),
                                UserID = Convert.ToInt32(reader["UserID"]),
                                ProfilePicture = reader["ProfilePicture"].ToString(),
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"])
                            };

                            
                            string Passwordreal = reader["Password"].ToString();
                            if (Passwordreal == password)
                            {
                                return user;
                            }
                            else
                            {
                                throw new UserNotFoundException($"Password Mismatch, Please enter correct password");
                            }
                        }
                        else
                        {
                            throw new UserNotFoundException($"User with Email : {email} does not exist, Please register first");
                        }
                    }
                 }
                

            }
            catch(UserNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
               
                Console.WriteLine($"|| {ex.Message}  ||");
                
                Console.ResetColor();
                return null;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                
                Console.WriteLine($"||  An error occurred during database operation at Login: {ex.Message}  ||");
                
                Console.ResetColor();
                return null;
            }
            finally
            {
                connection.Close();
            }
            
        }

        /// <summary>
        /// This Method is used to Register a user .
        /// </summary>
        public bool UserRegister()
        {
            User newUser = GetUsersDetails();
            if (newUser != null)
            {
                try
                {

                   
                    
                    string insertQuery = "INSERT INTO [User] (Username, Password, Email, FirstName, LastName, DateOfBirth, ProfilePicture) " +
                                            "VALUES (@Username, @Password, @Email, @FirstName, @LastName, @DateOfBirth, @ProfilePicture)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Username", newUser.Username);
                        command.Parameters.AddWithValue("@Password", newUser.Password);
                        command.Parameters.AddWithValue("@Email", newUser.Email);
                        command.Parameters.AddWithValue("@FirstName", newUser.FirstName);
                        command.Parameters.AddWithValue("@LastName", newUser.LastName);
                        command.Parameters.AddWithValue("@DateOfBirth", newUser.DateOfBirth);
                        command.Parameters.AddWithValue("@ProfilePicture", newUser.ProfilePicture);

                        int rowsAffected = command.ExecuteNonQuery();
                        
                        return rowsAffected > 0;
                    }
                    
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                   
                    Console.WriteLine($"   An error occurred during database operation : {ex.Message}  ");
                    Console.ResetColor();
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                return false;
            }
            
        }


        //getting user details
        /// <summary>
        /// This Method is used get user the details from user as input.
        /// </summary>
        public User GetUsersDetails()
        {
            User newUser;
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("User Registration:");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Email : ");
                Console.ResetColor();
                string email = Console.ReadLine();

                //Validation
                bool isAvailable = CheckEmail(email);
                if (!isAvailable)
                {
                    throw new Exception("Email already exist, Please enter unique Email");
                }


                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Password: ");
                Console.ResetColor();
                string password = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Username : ");
                Console.ResetColor();
                string username = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("First Name: ");
                Console.ResetColor();
                string firstName = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Last Name: ");
                Console.ResetColor();
                string lastName = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Date of Birth (yyyy-MM-dd): ");
                Console.ResetColor();
                DateTime dateOfBirth;
                while (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
                {
                    Console.WriteLine("Invalid date format. Please enter again (yyyy-MM-dd): ");
                }
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Profile Picture: ");
                Console.ResetColor();
                string profilePicture = Console.ReadLine();


                newUser = new User
                {
                    Username = username,
                    Password = password,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth,
                    ProfilePicture = profilePicture
                };
                return newUser;
            }
            catch (FormatException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                
                Console.WriteLine($"||    Invalid input format : {ex.Message}  ||");
               
                Console.ResetColor();
                return null;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                
                Console.WriteLine($"||    {ex.Message}  ||");
                
                Console.ResetColor();
                return null;
            }

        }

        /// <summary>
        /// This Method is used to check the email present in DB or not.
        /// </summary>
        public bool CheckEmail(string email)
        {
            try
            {
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM [User] WHERE Email = @Email";
                cmd.Parameters.AddWithValue("@Email", email);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return false;
                    }
                }
            
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }


        /// <summary>
        /// This Method is used get artwork details input from user.
        /// </summary>
        public Artwork GetArtworkDetailsFromUser()
        {
            try
            {
                Console.Write("Enter Title : ");
                string title = Console.ReadLine();

                Console.Write("Enter Description : ");
                string description = Console.ReadLine();

                Console.Write("Enter Creation Date (yyyy-MM-dd) : ");
                DateTime creationDate;
                while (!DateTime.TryParse(Console.ReadLine(), out creationDate))
                {
                    Console.WriteLine("Invalid date format. Please enter again (yyyy-MM-dd): ");
                }

                Console.Write("Enter Medium : ");
                string medium = Console.ReadLine();

                Console.Write("Enter Image URL : ");
                string imageURL = Console.ReadLine();

                Console.Write("Enter Artist ID : ");
                int artistID = int.Parse(Console.ReadLine());
                
                //Validation
                bool isAvailable = CheckArtistID(artistID);

                if (!isAvailable)
                {
                    throw new Exception("Artist ID not Found");
                }

                return new Artwork(title, description, creationDate, medium, imageURL, artistID);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"|| Error: {ex.Message} ||");
                Console.ResetColor();
                return null;
            }

        }

        /// <summary>
        /// This Method is used to check the Artist ID .
        /// </summary>
        public bool CheckArtistID(int artistID)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Artist WHERE ArtistID = @ID";
                cmd.Parameters.AddWithValue("@ID", artistID);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return true;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// This Method is used to display the artwork
        /// </summary>
        public void DisplayArtwork(Artwork artwork)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("------------------------");
            Console.WriteLine($"ArtwordID : {artwork.ArtworkID}");
            Console.WriteLine($"Title : {artwork.Title}");
            Console.WriteLine($"Description : {artwork.Description}");
            Console.WriteLine($"CreationDate : {artwork.CreationDate}");
            Console.WriteLine($"Medium : {artwork.Medium}");
            Console.WriteLine($"ImageURL : {artwork.ImageURL}");
            Console.WriteLine($"ArtistID : {artwork.ArtistID}");
            Console.ResetColor();
        }

        /// <summary>
        /// This Method is used to display the Artist.
        /// </summary>
        public void DisplayArtist(Artist artist)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("------------------------");
            Console.WriteLine($"ArtistID : {artist.ArtistID}");
            Console.WriteLine($"Name : {artist.Name}");
            Console.WriteLine($"Biography : {artist.Biography}");
            Console.WriteLine($"BirthDate : {artist.BirthDate}");
            Console.WriteLine($"Nationality : {artist.Nationality}");
            Console.WriteLine($"Website : {artist.Website}");
            Console.WriteLine($"ContactInformation : {artist.ContactInformation}");
            Console.ResetColor();
        }



        /// <summary>
        /// This Method is used to get gallery input from user.
        /// </summary>
     
        public Gallery GetGalleryDetailsFromUser()
        {
            try
            {
                Console.Write("Enter Name : ");
                string name = Console.ReadLine();

                Console.Write("Enter Description : ");
                string description = Console.ReadLine();

                Console.Write("Enter Location : ");
                string location = Console.ReadLine();

                Console.Write("Enter Curator (Reference to ArtistID) : ");
                int curator = int.Parse(Console.ReadLine());

                Console.Write("Enter OpeningHours : ");
                string openingHours = Console.ReadLine();

                return new Gallery(name, description, location, curator, openingHours);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }

        }

        /// <summary>
        /// This Method is used to display gallery details.
        /// </summary>
        public void DisplayGallery(Gallery gallery)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("------------------------");
            Console.WriteLine($"GalleryID : {gallery.GalleryID}");
            Console.WriteLine($"Name : {gallery.Name}");
            Console.WriteLine($"Description : {gallery.Description}");
            Console.WriteLine($"Location : {gallery.Location}");
            Console.WriteLine($"Curator: {gallery.Curator}");
            Console.WriteLine($"OpeningHours : {gallery.OpeningHours}");
            Console.ResetColor();
        }


        /// <summary>
        /// This Method is used to get all the Artist.
        /// </summary>

        public List<Artist> GetAllArtist()
        {
            try
            {
                List<Artist> artistList = new List<Artist>();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Artist";
           
                connection.Open();

                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    while (data.Read())
                    {
                        artistList.Add(new Artist(

                             Convert.ToInt32(data["ArtistID"]),
                             data["Name"].ToString(),                           
                             data["Biography"].ToString(),
                             Convert.ToDateTime(data["BirthDate"]),
                             data["Nationality"].ToString(),
                             data["Website"].ToString(),
                             data["ContactInformation"].ToString()

                            ));
                    }
                }


                connection.Close();

                if (artistList.Count > 0)
                {
                    return artistList;
                }
                else
                { return null; }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting Artist details: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// This Method is used to get all the Artworks.
        /// </summary>
        public List<Artwork> GetAllArtwork()
        {
            try
            {
                List<Artwork> artworkList = new List<Artwork>();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Artwork";
               
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
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting artwork details: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// This Method is used to get all the Gallery.
        /// </summary>

        public List<Gallery> GetAllGallery()
        {
            try
            {
                List<Gallery> galleryList = new List<Gallery>();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Gallery";

                connection.Open();

                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    while (data.Read())
                    {
                        galleryList.Add(new Gallery(

                            data["Name"].ToString(),
                             data["Description"].ToString(),
                             data["Location"].ToString(),
                             Convert.ToInt32(data["Curator"]),
                             data["OpeningHours"].ToString(),
                             Convert.ToInt32(data["GalleryID"])

                            ));
                    }
                }


                connection.Close();

                if (galleryList.Count > 0)
                {
                    return galleryList;
                }
                else
                { return null; }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting Gallery details: {ex.Message}");
                return null;
            }
        }

    }
}
