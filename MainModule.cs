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

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("======================================================================================================================");
            Console.WriteLine("                                          VIRTUAL ART GALLERY CONSOLE APP        ");
            Console.WriteLine("======================================================================================================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                                   Welcome to the Virtual Art Gallery Dashboard       ");
            Console.WriteLine("======================================================================================================================");
            Console.ResetColor();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("Enter Your Choice");
                Console.WriteLine("1. Add Artwork");
                Console.WriteLine("2. Update Artwork");
                Console.WriteLine("3. Remove Artwork");
                Console.WriteLine("4. Get Artwork by ID");
                Console.WriteLine("5. Search Artworks");
                Console.WriteLine("6. Add Artwork to Favorites");
                Console.WriteLine("7. Remove Artwork from Favorites");
                Console.WriteLine("8. Get User Favorite Artworks");
                Console.WriteLine("9. Exit");
                Console.WriteLine();
                Console.Write("Please Enter your choice : ");
                
                string selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":

                        Artwork artwork = virtualArtGalleryImpl.GetArtworkDetailsFromUser();
                        bool status = virtualArtGalleryImpl.AddArtwork(artwork);
                        if (status)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;

                            Console.WriteLine("║  Artwork Added Successfully  ║");

                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.WriteLine("║  Failed to Add Artwork in DB  ║");
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
                                Console.WriteLine("No Artwork found....");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Artwork not found: {ex.Message}");
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
                            Console.WriteLine("Enter Artwork ID to remove from favorites:");
                            if (int.TryParse(Console.ReadLine(), out int artworkId))
                            {
                                Console.WriteLine("Enter User ID:");
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
                            Console.WriteLine("Enter User ID");
                            int id = int.Parse(Console.ReadLine());
                            list = virtualArtGalleryImpl.GetUserFavoriteArtworks(id);
                           
                            foreach (int artwordID in list)
                            {
                                Console.WriteLine($"ArtwordID : {artwordID}");
                                
                            }
                            
                        }
                        catch(Exception ex )
                        {
                            Console.WriteLine($"ERROR : {ex.Message}");
                        }
                        break;

                    case "9":
                        exit = true;
                        break;

                    
                }

            }

        }
    }
}
