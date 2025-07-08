using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicBites.Model.DataManagment
{
    public class DataBase
    {
        private List<StudyTopic> StudyTopics { get; set; } = new List<StudyTopic>();
        private Tree MainTree { get; set; } = new Tree(-1, -1); // Root node with itemId -1

        private static readonly Lazy<DataBase> _lazyInstance = new Lazy<DataBase>(() => new DataBase());
        private DataBase()
        {
            
        }
        public static Lazy<DataBase> GetInstance()
        {
            return _lazyInstance;
        }
        public void Configure(Tree newTree, List<StudyTopic> topics)
        {
            MainTree = newTree;
            StudyTopics = topics;
        }
        public void AddStudyTopic(StudyTopic topic, string address)
        {
            string[] parts = address.Split('/');
            int parentId = String.IsNullOrEmpty(address) ? -1 : int.Parse(parts[parts.Length - 1]);
            var parentTree = MainTree.NavigateToAddress(address);
            if (parentTree != null)
            {
                Tree newTree = new Tree(topic.Id, parentTree);
                parentTree.AddChild(newTree);
            }
            StudyTopics.Add(topic);
        }
        public void RemoveStudyTopic(int id, string address)
        {
            StudyTopics.RemoveAll(t => t.Id == id);
            MainTree.NavigateToAddress(address)?.RemoveChild(id);

        }
        public void AsignStudyTopicAsChildren(int topicId, int childId)
        {
            var topic = MainTree.LookForStudyTopic(topicId);
            if (topic != null)
            {
                Tree childTree = new Tree(childId, topic);
                topic.AddChild(childTree);
            }
        }
        public Tree? GetTree(int id)
        {
            return MainTree.LookForStudyTopic(id);
        }
        public Tree GetMainTree()
        {
            return MainTree;
        }
        public StudyTopic? GetStudyTopicById(int id)
        {
            return StudyTopics.FirstOrDefault(t => t.Id == id);
        }
        public List<StudyTopic> GetAllStudyTopics()
        {
            return StudyTopics;
        }
        public string GetAddressOfTopic(StudyTopic topic)
        {
            Tree? tree = GetTree(topic.Id);
            if (tree == null)
                return "";
            return tree.GetAddress();
        }
    }
}
