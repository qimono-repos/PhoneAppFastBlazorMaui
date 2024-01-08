using System.Text;

namespace ComponentAppFastRazorClassLibrary
{
    public partial class FastDeffinitionComponent
    {
        string hello = "The HTML snippet ";
        StringBuilder sb = new StringBuilder();
        string htmlSnippet = string.Empty;
        string htmlListSnippet = string.Empty;

        string[]? words;

        protected override void OnInitialized()
        {
            sb.Append("<h1>");
            sb.Append(hello);
            sb.Append("</h1>");
            htmlSnippet = sb.ToString();

            words = new[] { "word  1", "word 2" };

            sb.Clear();
            sb.Append("<ul>");

            foreach (var word in words)
            {
                sb.AppendFormat("<li>{0}<li>", word);
            }
            sb.Append("</ul>");
            htmlListSnippet = sb.ToString();
        }
    }
}
