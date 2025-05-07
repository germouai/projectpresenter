namespace TPI_ProjectPresenter.Models.ProjectContent
{
    public class ContentSection
    {
        public int SID { get; private set; }
        public string SectionName { get; set; }
        public string SectionTooltip { get; set; }

        List<ContentItem>? _ContentItems;

        public ContentSection() 
        {
            _ContentItems = new List<ContentItem>();
        }

        public ContentItem GetFirstContent()
        {
            ContentItem content = _ContentItems.FirstOrDefault();

            return content != null ? content : new ContentItemTextOnly("No hay elementos");
        }
        public void AddContent(ContentItem content)
        {
            _ContentItems.Add(content);
        }
    }
}
