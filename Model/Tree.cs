using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopicBites.Model.DataManagment;
using static TopicBites.Model.StudyTopic; 

namespace TopicBites.Model
{
    public class Tree
    {
        public int ItemId { get; }
        public Tree? Parent { get; private set; }
        private readonly List<Tree> _children = new();
        public IReadOnlyList<Tree> Children => _children.AsReadOnly();
        public StudyTopic? Item => GetItem();

        public Tree(int itemId, Tree? parent = null)
        {
            ItemId = itemId;
            Parent = parent;
        }

        public void AddChild(Tree child)
        {
            child.Parent = this;
            _children.Add(child);
        }

        public void RemoveChild(int id)
        {
            _children.RemoveAll(c => c.ItemId == id);
        }

        public Tree? LookForStudyTopic(int id)
        {
            if (ItemId == id) return this;
            foreach (var child in _children)
            {
                var found = child.LookForStudyTopic(id);
                if (found != null) return found;
            }
            return null;
        }

        public Tree? NavigateUp() => Parent;

        public Tree? NavigateDown(int id) => _children.FirstOrDefault(c => c.ItemId == id);

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

        public StudyTopic? GetItem() => DataBase.GetInstance().Value.GetStudyTopicById(ItemId);

        public string GetAddress()
        {
            return (Parent == null || Parent.ItemId == -1) ? $"{ItemId}" : $"{Parent.GetAddress()}/{ItemId}";
        }
    }
}
