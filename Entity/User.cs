using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Entity
{
    public class User
    {

        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName {  get; set; }
        public string LastName {  get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ProfilePicture { get; set; }
        public List<int> FavoriteArtworks { get; set; }

        public User(int userID, string username, string password, string email, string firstName, string lastName, DateTime dateOfBirth, string profilePicture, List<int> favoriteArtworks)
        {
            UserID = userID;
            Username = username;
            Password = password;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            ProfilePicture = profilePicture;
            FavoriteArtworks = favoriteArtworks;
        }
    }
}
