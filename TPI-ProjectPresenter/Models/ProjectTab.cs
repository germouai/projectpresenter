namespace TPI_ProjectPresenter.Models
{
    public class ProjectTab
    {
        public int TID { get; private set; }
        public string TabName { get; set; }
        List<ContentSection> _Sections { get; set; }

    }
}
