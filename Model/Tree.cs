using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TopicBites.Model.DataManagment;
using static TopicBites.Model.StudyTopic; 

namespace TopicBites.Model
{
    public class Tree
    {
        public int ParentId { get; set; } 
        public int ItemId { get; set; }
        public List<Tree> Children { get; set; } = new List<Tree>();

        //Json Serialization Properties
        [JsonIgnore]
        public StudyTopic? Item => GetItem();
        [JsonIgnore]
        public Tree? Parent => GetParent();
        [JsonIgnore]
        Database? databaseInstance;
        [JsonConstructor]
        internal Tree(int ParentId, int ItemId, List<Tree> Children)
        {
            this.ParentId = ParentId;
            this.ItemId = ItemId;
            this.Children = Children;
        }
        public Tree(int itemId, int parentId = -1)
        {
            ItemId = itemId;
            ParentId = parentId;
        }
        public Tree(int itemId, Tree? parent = null)
        {
            ItemId = itemId;
            ParentId = parent == null ? -1 : parent.ItemId;
        }
        public void AddChild(Tree child)
        {
            child.ParentId = this.ItemId;
            Children.Add(child);
        }
        public void RemoveChild(int id)
        {
            Children.RemoveAll(c => c.ItemId == id);
        }
        public Tree? LookForStudyTopic(int id)
        {
            if (ItemId == id) return this;
            foreach (var child in Children)
            {
                var found = child.LookForStudyTopic(id);
                if (found != null) return found;
            }
            return null;
        }
        public Tree? NavigateUp() => Parent;
        public Tree? NavigateDown(int id) => Children.FirstOrDefault(c => c.ItemId == id);
        public Tree? NavigateToAddress(string address)
        {
            if (string.IsNullOrEmpty(address)) return this;
            var parts = address.Split('/');
            Tree? current = this;
            foreach (var part in parts)
            {
                if (!int.TryParse(part, out int id)) return null;
                current = current.NavigateDown(id);
                if (current == null) return null;
            }
            return current;
        }
        public void AssignDatabaseInstance(Database database)
        {
            databaseInstance = database;
            foreach (var child in Children)
            {
                child.AssignDatabaseInstance(database);
            }
        }
        public StudyTopic? GetItem() => databaseInstance == null ? null : databaseInstance.GetStudyTopicById(ItemId);
        public Tree? GetParent() => databaseInstance == null ? null : databaseInstance.GetTree(ParentId);
        public string GetAddress()
        {
            return (Parent == null || Parent.ItemId == -1) ? $"{ItemId}" : $"{Parent.GetAddress()}/{ItemId}";
        }
    }
}
