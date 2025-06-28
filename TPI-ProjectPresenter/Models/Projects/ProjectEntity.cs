namespace TPI_ProjectPresenter.Models.Projects
{
    public class ProjectEntity
    {
        public int PID { get; private set; }
        public ProjectHeader Header { get; set; }

        List<ProjectTab>? _Tabs;

        public ProjectEntity() 
        {
            _Tabs = new List<ProjectTab>();
            Header = new ProjectHeader();
        }

        public ProjectEntity(int pPID, string pName, string pDescription, string pToolTip, string pImgRef) : this()
        {
            PID = pPID;
            Header.ProjectName = pName;
            Header.ProjectDescription = pDescription;
            Header.ProjectTooltip = pToolTip;
            Header.ProjectImgRef = pImgRef;
        }

        public class ProjectHeader
        {
            public string? ProjectName { get; set; }
            public string? ProjectDescription { get; set; }
            public string? ProjectTooltip { get; set; }
            public string? ProjectImgRef { get; set; }
        }

        public void AddTab(ProjectTab tab)
        {
            _Tabs.Add(tab);
        }

        public ProjectTab GetFirstTab()
        {
            return _Tabs.FirstOrDefault();
        }
    }
}
