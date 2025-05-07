using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TopicBites
{
    public class StudyTopic
    {
        public string Id { get; set; } = String.Empty;
        public string Title { get; set; } = String.Empty ;
        public string Subtitle { get { return $"{SubTopics.Count} subtema/s disponibles."; } }
        public string Description { get; set; } = String.Empty;
        public Dictionary<DateTime, string> ChangeLog { get; set; } = new Dictionary<DateTime, string>();
        public List<Resource> Resources { get; set; } = new List<Resource>();
        public List<StudyTopic> SubTopics { get; set; } = new List<StudyTopic>();
        public List<StudyMethod> Methods { get; set; } = new List<StudyMethod>(); 
        public string Address { get { return ((Parent == null) ? "/root" : Parent.Address) + "/" + Id; } }

        // -------------------------------------------
        [JsonIgnore]
        public StudyTopic Parent { get; set; } = null;
        [JsonIgnore]
        public bool IsRoot { get { return Parent == null; } }
        public Uri MainIcon { get; set; } = new Uri("https://media.istockphoto.com/id/471625472/photo/3d-man-standing-and-having-no-idea.jpg?s=612x612&w=0&k=20&c=Q47xX_LinTdMJ1r1EAyzVZ5rjvcjdSwOAe-xlfl_t9o=");

        public StudyTopic() { } // Default constructor for serialization
        public StudyTopic(string Title)
        {
            this.Title = Title;
            this.Id = Title.ToLower().Replace(" ", "_");
        }
        public StudyTopic(string Id, string Title, string Description = "", string Author = "") {
            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            ChangeLog.Add(DateTime.Now, Author);
        }

        public void AddSubTopic(StudyTopic NewSubTopic)
        {
            NewSubTopic.Parent = this;
            SubTopics.Add(NewSubTopic);
        }

        public StudyTopic GetRoot()
        {
            if (Parent == null)
            {
                return this;
            }
            else
            {
                return Parent.GetRoot();
            }
        }
    }

}
