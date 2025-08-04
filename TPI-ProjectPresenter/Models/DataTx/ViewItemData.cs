namespace TPI_ProjectPresenter.Models.DataTx
{
    public class ViewItemData
    {
        public int PID { get; set; }
        public int TID { get; set; }
        public int SID { get; set; }
        public Models.ProjectContent.ContentItem ItemData { get; set; }
        public IFormFile? ImgFile { get; set; }
        public string? ImgRef { get; set; }
        public Models.ProjectContent.ContentItemSingleComparison? SingleComparisonData { get; set; }
    }
}
