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

        public ProjectTab(int pTID, string pTabName) : this()
        {
            TID = pTID;
            TabName = pTabName;
        }

        public void AddSection(ContentSection section)
        {
            _Sections.Add(section);
        }

        public ContentSection GetFirstSection()
        {
            return _Sections.FirstOrDefault();
        }

        public List<ContentSection> GetSections()
        {
            return _Sections;
        }
    }
}
