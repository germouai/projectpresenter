namespace TPI_ProjectPresenter.Models.ProjectContent
{
    public class ContentItemTextOnly : ContentItem
    {


        public ContentItemTextOnly()
        {
            ItemType = "TextOnly";
        }
        public ContentItemTextOnly(string text) : this()
        {
            ItemText = text;
        }
    }
}
