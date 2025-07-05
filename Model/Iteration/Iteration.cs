using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicBites.Model.Iteration
{
    public abstract class Iteration
    {
        public abstract bool IsNext();
        public abstract StudyTopic Current();
        public abstract void Next();
    }
}
