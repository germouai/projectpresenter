using TPI_ProjectPresenter.Models.ProjectContent;

namespace TPI_ProjectPresenter.Models.Projects
{
    public class ProjectTab
    {
        public int TID { get; private set; }
        public string TabName { get; set; }
        List<ContentSection>? _Sections { get; set; }

    }
}
