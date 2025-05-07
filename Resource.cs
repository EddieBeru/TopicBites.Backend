using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicBites
{
    public class Resource
    {
        public string Id { get; set; }

        public Resource() { }
        public Resource(string id)
        {
            this.Id = id;
        }
    }

    public class ImageResource : Resource
    {
        public string Url { get; set; } = String.Empty;
    }

    public class PointedImageResource : Resource
    {
        public string Url { get; set; } = String.Empty;
        public List<PointerResource> Pointers { get; set; } = new List<PointerResource>();
    }

    public class PointerResource : Resource
    {
        public string PointedImageResource { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        
        public PointerResource(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

}
