namespace TPI_ProjectPresenter.Models.ProjectContent
{
    public class ContentItem3ElementList : ContentItem
    {

        string ListLayout;
        List<ListItem3Element> ListItems;

        public ContentItem3ElementList()
        {
            ItemType = "3ElementList";
        }


        public void SetListLayout (string layout)
        {
            ListLayout = layout;
        }
        
    }
}
