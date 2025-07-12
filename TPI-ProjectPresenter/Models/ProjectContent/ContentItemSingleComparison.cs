namespace TPI_ProjectPresenter.Models.ProjectContent
{
    public class ContentItemSingleComparison : ContentItem
    {

        public ComparisonItem? LeftItem { get; set; }
        public ComparisonItem? RightItem { get; set; }

        public ContentItemSingleComparison() 
        {
            ItemType = "SingleComparison";
            LeftItem = new ComparisonItem();
            RightItem = new ComparisonItem();
        }
        public ContentItemSingleComparison(ComparisonItem? leftItem, ComparisonItem? rightItem) : this()
        {
            LeftItem = leftItem;
            RightItem = rightItem;
        }

    }
}
