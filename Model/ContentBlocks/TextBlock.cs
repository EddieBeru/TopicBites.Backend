using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicBites.Model.ContentBlocks
{
    public class TextBlock  : ContentBlock
    {
        public override string Type => "Text";
        public string Text { get; set; } = string.Empty;
        public override string Title { get; protected set; }

        public override string DisplayType => "Bloque de texto.";

        public TextBlock() { }
        public TextBlock(string title, string text)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }
    }
}
