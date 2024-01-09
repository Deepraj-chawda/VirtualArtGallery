using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.MyException
{
    public class ArtWorkNotFoundException: Exception
    {
        public ArtWorkNotFoundException() : base("Artwork not found in the database.")
        {
            // the error message based on your requirements
        }
    }
}
