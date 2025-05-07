namespace TPI_ProjectPresenter.Models.ProjectContent
{
    public class ContentItemSingleImage : ContentItem
    {
        public string? ImageRef { get; set; }

        public ContentItemSingleImage()
        {
            ItemType = "SingleImage";
        }
    }
}
