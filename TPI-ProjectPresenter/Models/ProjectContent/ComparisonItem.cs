namespace TPI_ProjectPresenter.Models.ProjectContent
{
    public class ComparisonItem
    {
        public string ItemTitle { get; set; }
        public string ItemDetail { get; set; }
        public List<string> ItemizedInfo { get; set; }

        public ComparisonItem() 
        {
            ItemTitle = string.Empty;
            ItemDetail = string.Empty;
            ItemizedInfo = new List<string>();
        }
        public ComparisonItem(string itemTitle, string itemDetail)
        {
            ItemTitle = itemTitle;
            ItemDetail = itemDetail;
            ItemizedInfo = new List<string>();
        }
        public ComparisonItem(string itemTitle, string itemDetail, List<string> itemizedInfo) : this(itemTitle, itemDetail)
        {
            ItemizedInfo = itemizedInfo;
        }

        public void SetInfoFromArray(string[] infoArray)
        {
            ItemizedInfo.Clear();
            foreach (var item in infoArray)
            {
                ItemizedInfo.Add(item);
            }
        }
    }
}
