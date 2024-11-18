using System.ComponentModel.DataAnnotations;

namespace DatingSiteApi.Models
{
    public class ImageInfo
    {
        private int id;
        private byte[] data;
        private string type;
        private string title;
        private string description;

        public ImageInfo()
        {
            this.id = 0;
            this.data = new byte[0];
            this.type = string.Empty;
            this.title = string.Empty;
            this.description = string.Empty;
        }

        public ImageInfo(int id, byte[] data, string type, string title, string description)
        {
            this.id = id;
            this.data = data;
            this.type = type;
            this.title = title;
            this.description = description;
        }

        [Required(ErrorMessage = "Image title is required.")]
        public string ImageTitle
        {
            get { return this.title; }
            set { this.title = value; }
        }

        [Required(ErrorMessage = "Image description is required.")]
        public string ImageDescription
        {
            get { return this.description; }
            set { this.description = value; }
        }
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        [Required(ErrorMessage = "Data description is required.")]
        public byte[] Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
    }
}