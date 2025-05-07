using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TopicBites.Misc;

namespace TopicBites
{
    public class StudyTopic
    {
        //Public attributes
        public string Id { get; protected set; } = String.Empty;
        public string Title { get; protected set; } = String.Empty ;
        public string Subtitle { get { return $"{SubTopics.Count} subtema/s disponibles."; } }
        public string Description { get; protected set; } = String.Empty;
        public Dictionary<NounAttribute, string> Nouns { get; protected set; } = new Dictionary<NounAttribute, string>();
        public string Address { get { return ((Parent == null) ? "/root" : Parent.Address) + "/" + Id; } }
        public StudyTopic? Parent { get; protected set; } = null;
        public bool IsRoot { get { return Parent == null; } }

        //More techy attributes
        protected Dictionary<DateTime, string> ChangeLog { get; set; } = new Dictionary<DateTime, string>();
        protected List<Resource> Resources { get; set; } = new List<Resource>();
        protected List<StudyTopic> SubTopics { get; set; } = new List<StudyTopic>();
        protected List<StudyMethod> Methods { get; set; } = new List<StudyMethod>();

        //Temporal
        public Uri MainIcon { get; set; } = new Uri("https://media.istockphoto.com/id/471625472/photo/3d-man-standing-and-having-no-idea.jpg?s=612x612&w=0&k=20&c=Q47xX_LinTdMJ1r1EAyzVZ5rjvcjdSwOAe-xlfl_t9o=");
        
        // Default constructor for serialization
        public StudyTopic() { }
        //Single argument constructor, used for creating a new topic with a title
        public StudyTopic(string Title)
        {
            this.Title = Title;
            this.Id = Title.ToLower().Replace(" ", "_");
        }
        //Full constructor
        public StudyTopic(string Id, string Title, string Description = "", string Author = "") {
            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            ChangeLog.Add(DateTime.Now, Author);
        }
        public StudyTopic(StudyTopic From)
        {
            if (From == null)
                throw new ArgumentNullException(nameof(From), "Cannot clone a null topic");
            this.Id = From.Id;
            this.Title = From.Title;
            this.Description = From.Description;
            this.Nouns = new Dictionary<NounAttribute, string>(From.Nouns);
            this.MainIcon = From.MainIcon;
            this.Parent = From.Parent;
            this.Resources = new List<Resource>(From.Resources);
            this.SubTopics = new List<StudyTopic>(From.SubTopics);
            this.Methods = new List<StudyMethod>(From.Methods);
        }

        //Adds a subtopic into the current topic
        public void AddSubTopic(StudyTopic NewSubTopic)
        {
            if (NewSubTopic == null)
                throw new ArgumentNullException(nameof(NewSubTopic), "New subtopic cannot be null");
            if (SubTopics.Contains(NewSubTopic))
                throw new ArgumentException("Subtopic already exists in the list", nameof(NewSubTopic));
            if (NewSubTopic.Parent != null)
                NewSubTopic = new StudyTopic(NewSubTopic);
            NewSubTopic.Parent = this;
            SubTopics.Add(NewSubTopic);
        }

        public StudyTopic GetSubTopic(int index)
        {
            if (index < 0 || index >= SubTopics.Count)
                throw new IndexOutOfRangeException("Index out of range");
            return SubTopics[index];
        }

        public int GetSubTopicCount() { return SubTopics.Count; }
        //Returns the root topic of the whole tree
        public StudyTopic GetRoot() { return (Parent == null) ? this : Parent.GetRoot(); }
    }

}
