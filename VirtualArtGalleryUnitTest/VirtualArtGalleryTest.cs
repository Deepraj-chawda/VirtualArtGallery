using VirtualArtGallery.DAO;
using VirtualArtGallery.Entity;
using System.Data.SqlClient;

namespace UnitTest
{
    public class VirtualArtGalleryTest
    {
        private VirtualArtGalleryImpl virtualArtGalleryImpl;

        [SetUp]
        public void Setup()
        {
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", String.Format("{0}\\App.config", AppDomain.CurrentDomain.BaseDirectory));
            virtualArtGalleryImpl = new VirtualArtGalleryImpl();
        }

        [Test]
        public void AddArtworkTest()
        {
            Artwork artwork = new Artwork("Self-Portrait", "A self-portrait by me", Convert.ToDateTime("2024-01-08"), "Oil on Canvas", "myportrait.jpg", 2);
            bool result = virtualArtGalleryImpl.AddArtwork(artwork);
            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateArtworkTest()
        {

            Artwork artwork = new Artwork("New Artwork", "New artwork updated by me", Convert.ToDateTime("2024-01-11"), "paint on canva", "portrait.png", 2,2);
            bool result = virtualArtGalleryImpl.UpdateArtwork(artwork);
            Assert.IsTrue(result);
        }

        [Test]
        public void GetArtworkById()
        {
            Artwork artwork = virtualArtGalleryImpl.GetArtworkById(2);
            Assert.IsNotNull(artwork);
          
        }

        [Test]
        public void RemoveArtworkTest()
        {
            bool result = virtualArtGalleryImpl.RemoveArtwork(1);
            Assert.IsTrue(result);
        }

        [Test]
        public void AddGalleryTest()
        {
            Gallery gallery = new Gallery("New gallery Art", "Showcasing new artworks.", "123 Main Street, Cityville", 2, "Tue-Sun, 10am-6pm");
            bool res = virtualArtGalleryImpl.AddGallery(gallery);
            Assert.IsTrue(res);
        }

        [Test]
        public void UpdateGalleryTest()
        {
            Gallery gallery = new Gallery("updated gallery Art", "Showcasing new artworks.", " Main Street, Cityville", 2, "Tue-Sun, 9am-5pm",3);
            bool res = virtualArtGalleryImpl.UpdateGallery(gallery);
            Assert.IsTrue(res);
        }

        [Test]
        public void GetGalleryTest()
        {
            Gallery gallery = virtualArtGalleryImpl.GetGalleryById(1);
            Assert.IsNotNull(gallery);
        }

        [Test]
        public void RemoveGalleryTest() {
            bool result = virtualArtGalleryImpl.RemoveGallery(1);
            Assert.IsTrue(result);

        }
    }
}