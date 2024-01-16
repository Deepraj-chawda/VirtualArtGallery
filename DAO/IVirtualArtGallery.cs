using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Entity;

namespace VirtualArtGallery.DAO
{
    public interface IVirtualArtGallery
    {
        // Artwork Management
        bool AddArtwork(Artwork artwork);
        bool UpdateArtwork(Artwork artwork);
        bool RemoveArtwork(int artworkID);
        Artwork GetArtworkById(int artworkID);
        List<Artwork> SearchArtworks(string keyword);

        // User Favorites
        bool AddArtworkToFavorite(int userId, int artworkId);
        bool RemoveArtworkFromFavorite(int userId, int artworkId);
        List<int> GetUserFavoriteArtworks(int userId);


        //gallery management
        bool AddGallery(Gallery gallery);
        Gallery GetGalleryById(int galleryID);
        bool RemoveGallery(int galleryID);
        bool UpdateGallery(Gallery gallery);


        //user management
        User UserLogin(string username, string password);
        bool UserRegister();

        //additional methods
        List<Artwork> GetAllArtwork();
        List<Gallery> GetAllGallery();
        List<Artist> GetAllArtist();

        //display details
        void DisplayArtist(Artist artist);
        void DisplayArtwork(Artwork artwork);
        void DisplayGallery(Gallery gallery);


    }
}
