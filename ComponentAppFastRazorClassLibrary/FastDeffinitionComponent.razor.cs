using System.ComponentModel.Design.Serialization;
using System.Text;

namespace ComponentAppFastRazorClassLibrary
{
    public partial class FastDeffinitionComponent
    {
        string hello = "The HTML snippet ";
        StringBuilder sb = new StringBuilder();
        string htmlSnippet = string.Empty;
        string htmlListSnippet = string.Empty;
        string htmlDynamicListSnippet = string.Empty;

        string[]? words;
        SnippetHtmlBuilder? snippetHtmlBuilder;
        protected override void OnInitialized()
        {
            words = new[] { "Static snippet", "solved at implementation" };
            sb.Clear();
            sb.Append("<ul>");

            foreach (var word in words)
            {
                sb.AppendFormat("<li>{0}<li>", word);
            }
            sb.Append("</ul>");
            htmlListSnippet = sb.ToString();

            snippetHtmlBuilder = new SnippetHtmlBuilder("ul");
            snippetHtmlBuilder.AddChild("li","Forget");
            snippetHtmlBuilder.AddChild("li","Active");
            snippetHtmlBuilder.AddChild("li","State");
            snippetHtmlBuilder.AddChild("li","Teach");

            string snippet = snippetHtmlBuilder.ToString()?? "No text to show";
            htmlDynamicListSnippet = snippet;
        }
    }
    public class SnippetHtmlElement
    {
        public string? Name, Text;
        public List<SnippetHtmlElement> HtmlElements = new List<SnippetHtmlElement>();
        private const int _indentSize = 2;
        public SnippetHtmlElement() { }
        public SnippetHtmlElement(string name, string text)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Text = text ?? throw new ArgumentNullException(paramName: nameof(name));
        }

        private string ToStringImplementation(int indentation)
        {
            StringBuilder sb = new StringBuilder();
            int indentationFormula = _indentSize * indentation;
            int indentationTextFormula = _indentSize * (indentationFormula + 1);
            string indentSpaces = new string(' ', indentationFormula);
            sb.AppendLine($"{indentSpaces}<{Name}>");
            //if (string.IsNullOrWhiteSpace(Text)) return sb.ToString();
            if (!string.IsNullOrEmpty(Text))
            {
                sb.Append(new string(' ', indentationTextFormula));
                sb.AppendLine(Text);
            }
            foreach (var element in HtmlElements)
                sb.Append(element.ToStringImplementation(indentation + 1));
            sb.AppendLine($"{indentSpaces}<{Name}/>");
            //sb.AppendLine("<img src=\"DrawComponent.svg\" alt=\"The SVG Image\" />\r\n");

            return sb.ToString();
        }
        public override string ToString()
        {
            return ToStringImplementation(0);
        }
    }
    public class SnippetHtmlBuilder
    {
        private readonly string _rootName;
        SnippetHtmlElement _root = new SnippetHtmlElement();
        public SnippetHtmlBuilder(string rootName)
        {
            _rootName = rootName;
            _root.Name = rootName;
        }
        public void AddChild(string childName, string childText)
        {
            SnippetHtmlElement element = new SnippetHtmlElement(childName, childText);
            _root.HtmlElements.Add(element);
        }
        public override string ToString()
        {
            return _root.ToString();
        }
        public void Clear()
        {
            _root = new SnippetHtmlElement();
        }
    }
}
