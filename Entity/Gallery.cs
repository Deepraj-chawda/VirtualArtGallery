using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Entity
{
    public class Gallery
    {
        public int GalleryID {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Curator { get; set; } // Reference to ArtistID
        public string OpeningHours { get; set; }

        public Gallery( string name, string description, string location, int curator, string openingHours,[Optional] int galleryID)
        {
            GalleryID = galleryID;
            Name = name;
            Description = description;
            Location = location;
            Curator = curator;
            OpeningHours = openingHours;
        }
    }
}
