using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicBites.Model.ContentBlocks
{
    public class ImageBlock : ContentBlock
    {
        public override string Type => "Image";
        public Uri ImageUrl { get; set; } = new Uri("ms-appx:///Assets/StoreLogo.png");
        public string AltText { get; set; } = string.Empty;
        public override string Title { get; protected set; }

        public override string DisplayType => "Bloque de imagen.";

        public ImageBlock() { }
        public ImageBlock(Uri imageUrl, string altText)
        {
            ImageUrl = imageUrl ?? throw new ArgumentNullException(nameof(imageUrl));
            AltText = altText;
        }
    }
}
