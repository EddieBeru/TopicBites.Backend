using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicBites.Model.DataManagment
{
    public class Database
    {
        private List<StudyTopic> StudyTopics { get; set; }
        private Tree MainTree { get; set; } // Root node with itemId -1

        public Database(List<StudyTopic>? studyTopics, Tree? tree)
        {
            StudyTopics = studyTopics ?? new List<StudyTopic>();
            MainTree = tree ?? new Tree(-1, -1); // Ensure MainTree is initialized
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
