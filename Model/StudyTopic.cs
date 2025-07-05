using System.Collections.ObjectModel;
using TopicBites.Model.ContentBlocks;
using TopicBites.Model.DataManagment;

namespace TopicBites.Model
{
    public class StudyTopic
    {
        public int Id { get; protected set; }
        public string Title { get; protected set; } = String.Empty;
        public ObservableCollection<ContentBlock> Content { get; protected set; } = new ObservableCollection<ContentBlock>();

        public StudyTopic()
        {
            Id = UIDManager.GetInstance().GetNewId();
        }
        public StudyTopic(string title) : this()
        {
            Title = title;
        }
        public void AddContentBlock(ContentBlock contentBlock)
        {
            if (contentBlock is null) return;
            Content.Add(contentBlock);
        }
    }
}
