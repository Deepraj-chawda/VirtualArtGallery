using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.DAO;
using VirtualArtGallery.Entity;


namespace VirtualArtGallery
{
    internal class MainModule
    {
        static void Main(string[] args)
        {
            VirtualArtGalleryImpl virtualArtGalleryImpl = new VirtualArtGalleryImpl();
            
          

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("======================================================================================================================");
                Console.WriteLine("                                          VIRTUAL ART GALLERY CONSOLE APP        ");
                Console.WriteLine("======================================================================================================================");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("                                   Welcome to the Virtual Art Gallery Menu      ");
                Console.WriteLine("======================================================================================================================");
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("3. Exit");
                Console.ResetColor();
                
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("\nPlease Enter Your Choice : ");
                Console.ResetColor();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        try
                        {
                           
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("\nEnter Email : ");
                            Console.ResetColor();
                            string email = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("Enter Password : ");
                            Console.ResetColor();
                            string password = Console.ReadLine();
                            try
                            {
                                User userlogin = virtualArtGalleryImpl.UserLogin(email, password);
                                if (userlogin != null)
                                {
                                  
                                    AfterLogin(userlogin);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                
                                Console.WriteLine($"||    {ex.Message}  ||");
                                
                                Console.ResetColor();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("\n" + ex.Message);
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                        break;

                    case "2":
                        try
                        {
                            Console.WriteLine("");
                            bool RegistrationStatus = virtualArtGalleryImpl.UserRegister();
                            if (RegistrationStatus)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;

                                Console.WriteLine("||   Registration Successfull!!   ||");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("||   Registration Failed, Try again!!  ||");
                                Console.ResetColor();
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("||   Registration Failed, Try again!!  ||");
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.ResetColor();
                        }
                        break;

                    case "3":
                        Console.ForegroundColor = ConsoleColor.Green;
                       
                        Console.WriteLine("||   Exiting Virtual Art Gallery   ||");
                        
                        Console.ResetColor();
                        Environment.Exit(0);
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        
                        Console.WriteLine("||   Invalid choice. Please select between 1-3 ||");
                        Console.ResetColor();
                        break;
                }
            }


        }
        static void AfterLogin(User user) {
            
            Console.Clear();
            
            VirtualArtGalleryImpl virtualArtGalleryImpl = new VirtualArtGalleryImpl();
            
           

            
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("======================================================================================================================");
            Console.WriteLine("                                          VIRTUAL ART GALLERY CONSOLE APP        ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("\t\t||\t     Login SuccessFull!!\t   ||");
            Console.WriteLine($"\t\t||\t     Your USER ID is {user.UserID} \t   ||");
            Console.ResetColor();

            bool exit = false;
            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("======================================================================================================================");
                
                Console.WriteLine("                                   Welcome to the Virtual Art Gallery Dashboard      ");
                Console.WriteLine("======================================================================================================================");
                Console.ResetColor();
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nArtwork Management --");
                Console.ResetColor();
                Console.WriteLine("  1. Add Artwork");
                Console.WriteLine("  2. Update Artwork");
                Console.WriteLine("  3. Remove Artwork");
                Console.WriteLine("  4. Get Artwork by ID");
                Console.WriteLine("  5. Search Artworks");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nUser Favorites --");
                Console.ResetColor();
                Console.WriteLine("  6. Add Artwork to Favorites");
                Console.WriteLine("  7. Remove Artwork from Favorites");
                Console.WriteLine("  8. Get User Favorite Artworks");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nGallery Management --");
                Console.ResetColor();
                Console.WriteLine("  9. Add Gallery");
                Console.WriteLine("  10. Update Gallery");
                Console.WriteLine("  11. Remove Gallery");
                Console.WriteLine("  12. Get Gallery by ID");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAdditional Methods --");
                Console.ResetColor();
                Console.WriteLine(" 13. Show All Artwork");
                Console.WriteLine(" 14. Show All Artist");
                Console.WriteLine(" 15. Show All Gallery");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n16. Logout");
                Console.ResetColor();
                Console.WriteLine();
                Console.Write("Please Enter your choice : ");
                
                string selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        try
                        {
                            Artwork artwork = virtualArtGalleryImpl.GetArtworkDetailsFromUser();
                            bool status;
                            if (artwork != null)
                            {
                                status = virtualArtGalleryImpl.AddArtwork(artwork);
                            }
                            else
                            {
                                status = false;
                            }
                            if (status)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;

                                Console.WriteLine("║  Artwork Added Successfully in DB  ║");

                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;

                                Console.WriteLine("║  Failed to Add Artwork in DataBase  ║");
                                Console.ResetColor();
                            }

                           
                        }
                        catch(Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.WriteLine($"║ ERROR: {ex.Message} ║");
                            Console.ResetColor();
                        }
                        break;

                    case "2":
                        try
                        {
                           

                            Console.Write("Enter Artwork ID: ");
                            int ArtworkID = int.Parse(Console.ReadLine());

                            Artwork artwork2 = virtualArtGalleryImpl.GetArtworkDetailsFromUser();
                            artwork2.ArtworkID = ArtworkID;

                            bool status2 = virtualArtGalleryImpl.UpdateArtwork(artwork2);
                            if (status2)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;

                                Console.WriteLine("║  Artwork Update Successful  ║");

                                Console.ResetColor();
                            }
                           
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.WriteLine("║   Artwork Update Failed   ║");

                            Console.ResetColor();
                        }
                        break;

                    case "3":
                        try
                        {
                            Console.WriteLine();
                            Console.Write("Enter artwork ID to Remove : ");
                            int id3 = int.Parse(Console.ReadLine());
                            bool status3 = virtualArtGalleryImpl.RemoveArtwork(id3);
                            if (status3)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                               
                                Console.WriteLine("║   Artwork Removed Successfully   ║");
                                
                                Console.ResetColor();
                            }
                            
                           
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error : {ex.Message}");
                            Console.ForegroundColor = ConsoleColor.Red;
                            
                            Console.WriteLine("║   Failed to Remove Artwork   ║");
                            
                            Console.ResetColor();
                        }
                        break;

                    case "4":
                        try
                        {
                            Console.WriteLine();
                            Console.Write("Enter Artwork Id : ");
                            int id4 = int.Parse(Console.ReadLine());
                            Artwork artwork4 = virtualArtGalleryImpl.GetArtworkById(id4);
                            if (artwork4 != null)
                            {
                                virtualArtGalleryImpl.DisplayArtwork(artwork4);
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"|| Error : {ex.Message} ||");
                            Console.ResetColor();
                        }
                        break;

                    case "5":
                        try
                        {
                            string keyword;
                            Console.Write("Enter Artwork you want to search: ");
                            keyword = Console.ReadLine();
                            List<Artwork> artworks = virtualArtGalleryImpl.SearchArtworks(keyword);
                            Console.WriteLine("");
                            if (artworks != null)
                            {
                                foreach (Artwork artwork1 in artworks)
                                {
                                    virtualArtGalleryImpl.DisplayArtwork(artwork1);
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("|| No Artwork found....||");
                                Console.ResetColor();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Artwork not found: {ex.Message}");
                            Console.ResetColor();
                        }
                        break;

                    case "6":
                        try
                        {
                            Console.WriteLine("Enter Artwork ID to add to favorites:");
                            if (int.TryParse(Console.ReadLine(), out int artworkId))
                            {
                                Console.WriteLine("Enter User ID:");
                                if (int.TryParse(Console.ReadLine(), out int userId))
                                {
                                    bool addToFavoritesStatus = virtualArtGalleryImpl.AddArtworkToFavorite(userId, artworkId);

                                    if (addToFavoritesStatus)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        
                                        Console.WriteLine("║   Artwork added to favorites successfully  ║");
                                        
                                        Console.ResetColor();
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;

                                        Console.WriteLine("║   Failed to add artwork to favorites   ║");

                                        Console.ResetColor();
                                    }
                                    
                                }
                                else
                                {
                                    Console.WriteLine("Invalid User ID. Please enter a valid numeric ID.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Artwork ID. Please enter a valid numeric ID.");
                            }

                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine($"ERROR: {ex.Message}");
                           
                        }
                        break;
                    
                    case "7":
                        try
                        {
                            Console.WriteLine("Enter Artwork ID to Remove from favorites:");
                            if (int.TryParse(Console.ReadLine(), out int artworkId))
                            {
                                Console.Write("Enter User ID:");
                                if (int.TryParse(Console.ReadLine(), out int userId))
                                {
                                    bool removeFromFavoritesStatus = virtualArtGalleryImpl.RemoveArtworkFromFavorite(userId, artworkId);

                                    if (removeFromFavoritesStatus)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        
                                        Console.WriteLine("║   Artwork removed from favorites successfully  ║");
                                        
                                        Console.ResetColor();
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                       
                                        Console.WriteLine("║   Failed to remove artwork to favorites  ║");
                                   
                                        Console.ResetColor();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid User ID. Please enter a valid numeric ID.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Artwork ID. Please enter a valid numeric ID.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"ERROR: {ex.Message}");
                        }
                        break;

                    case "8":
                        try
                        {
                            List<int> list = new List<int>();
                            Console.Write("Enter User ID : ");
                            int id = int.Parse(Console.ReadLine());
                            list = virtualArtGalleryImpl.GetUserFavoriteArtworks(id);

                            Console.ForegroundColor = ConsoleColor.Green;
                            foreach (int artworkID in list)
                            {
                                Console.WriteLine($"ArtwordID : {artworkID}");
                                Artwork artwork = virtualArtGalleryImpl.GetArtworkById(artworkID);
                                virtualArtGalleryImpl.DisplayArtwork(artwork);
                                
                            }
                            Console.ResetColor();

                        }
                        catch(Exception ex )
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"|| ERROR : {ex.Message} ||");
                            Console.ResetColor();
                        }
                        break;

                    case "9":
                        
                        Gallery gallery = virtualArtGalleryImpl.GetGalleryDetailsFromUser();
                        bool statusgallery = virtualArtGalleryImpl.AddGallery(gallery);
                        if (statusgallery)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;

                            Console.WriteLine("║  Gallery Added Successfully  ║");

                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.WriteLine("║  Failed to Add Gallery in DB  ║");
                            Console.ResetColor();
                        }
                        break;

                    case "10":
                        try
                        {


                            Console.Write("Enter Gallery ID: ");
                            int GalleryID = int.Parse(Console.ReadLine());

                            Gallery gallery1 = virtualArtGalleryImpl.GetGalleryDetailsFromUser();
                            gallery1.GalleryID = GalleryID;

                            bool status2Gallery = virtualArtGalleryImpl.UpdateGallery(gallery1);
                            if (status2Gallery)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;

                                Console.WriteLine("║  Gallery Update Successful  ║");

                                Console.ResetColor();
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.WriteLine("║   Gallery Update Failed   ║");

                            Console.ResetColor();
                        }

                        break;

                    case "11":
                        try
                        {
                            Console.WriteLine();
                            Console.Write("Enter Gallery ID to Remove : ");
                            int galleryId = int.Parse(Console.ReadLine());
                            bool status3Gallery = virtualArtGalleryImpl.RemoveGallery(galleryId);
                            if (status3Gallery)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;

                                Console.WriteLine("║   Gallery Removed Successfully   ║");

                                Console.ResetColor();
                            }


                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error : {ex.Message}");
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.WriteLine("║   Failed to Remove Gallery   ║");

                            Console.ResetColor();
                        }
                        break;

                    case "12":
                        try
                        {
                            Console.WriteLine();
                            Console.Write("Enter Gallery Id : ");
                            int galleryID2 = int.Parse(Console.ReadLine());
                            Gallery gallery2 = virtualArtGalleryImpl.GetGalleryById(galleryID2);
                            if (gallery2 != null)
                            {
                                virtualArtGalleryImpl.DisplayGallery(gallery2);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"|| Error : {ex.Message} ||");
                            Console.ResetColor();
                        }
                        break;

                    case "13":
                        try
                        {

                            List<Artwork> artworks = virtualArtGalleryImpl.GetAllArtwork();
                            if (artworks != null)
                            {
                                foreach (Artwork artwork in artworks)
                                {
                                    virtualArtGalleryImpl.DisplayArtwork(artwork);
                                }
                            }
                            else
                            {
                                Console.WriteLine("NO Artwork FOUND");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"|| Error : {ex.Message} ||");
                            Console.ResetColor();
                        }
                        break;

                    case "14":
                        try
                        {

                            List<Artist> artists = virtualArtGalleryImpl.GetAllArtist();
                            if (artists != null)
                            {
                                foreach (Artist artist in artists)
                                {
                                    virtualArtGalleryImpl.DisplayArtist(artist);
                                }
                            }
                            else
                            {
                                Console.WriteLine("NO Artist FOUND");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"|| Error : {ex.Message} ||");
                            Console.ResetColor();
                        }
                        break;

                    case "15":
                        try
                        {
                            
                            List<Gallery> galleries = virtualArtGalleryImpl.GetAllGallery();
                            if (galleries != null)
                            {
                                foreach (Gallery gal in galleries)
                                {
                                    virtualArtGalleryImpl.DisplayGallery(gal);
                                }
                            }
                            else
                            {
                                Console.WriteLine("NO Gallery Found");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"|| Error : {ex.Message} ||");
                            Console.ResetColor();
                        }
                        break;

                    case "16":
                        exit = true;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n|| Invalid choice. Please enter a number between 1 and 16.||");
                        Console.ResetColor();
                        break;
                }

            }

        }
    }
}
