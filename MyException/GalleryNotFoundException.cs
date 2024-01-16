using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtual_Art_Gallery.MyException
{
    public class GalleryNotFoundException : Exception
    {
        public GalleryNotFoundException() : base("Gallery Not Found!")
        {
        }

        public GalleryNotFoundException(string message) : base(message)
        {
        }
    }
}
