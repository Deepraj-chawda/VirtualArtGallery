using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.MyException
{
    public class UserNotFoundException: Exception
    {
        public UserNotFoundException() : base("User not found in the database.")
        {
            // customize the error message based on your requirements
        }

        public UserNotFoundException(string message) : base(message) { }
    }
}
