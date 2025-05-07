namespace TPI_ProjectPresenter.Models.ProjectContent
{
    public abstract class ContentItem
    {
        public int IID { get; internal set; }
        public string? ItemText { get; set; }

        internal string ItemType;
    }
}
