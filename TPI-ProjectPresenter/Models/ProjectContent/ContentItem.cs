namespace TPI_ProjectPresenter.Models.ProjectContent
{
    public class ContentItem
    {
        public int IID { get; internal set; }
        public string? ItemTitle { get; set; }
        public string? ItemText { get; set; }

        public string ItemType { get; set; }

        public void setItemType(string itemType) 
        {
            itemType = itemType.Trim().Substring(11);
            ItemType = itemType;
        }
    }
}
