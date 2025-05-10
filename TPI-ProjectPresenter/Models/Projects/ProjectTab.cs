using TPI_ProjectPresenter.Models.ProjectContent;

namespace TPI_ProjectPresenter.Models.Projects
{
    public class ProjectTab
    {
        public int TID { get; private set; }
        public string TabName { get; set; }
        List<ContentSection>? _Sections { get; set; }

        public ProjectTab() 
        {
            _Sections = new List<ContentSection>();
        }

        public void AddSection(ContentSection section)
        {
            _Sections.Add(section);
        }

        public ContentSection GetFirstSection()
        {
            return _Sections.FirstOrDefault();
        }
    }
}
