using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicBites.Model.ContentBlocks
{
    public abstract class ContentBlock
    {
        public abstract string Title { get; protected set; }
        public abstract string Type { get; }
    }
}
