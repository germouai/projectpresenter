namespace TPI_ProjectPresenter.Models.Projects
{
    public class ProjectEntity
    {
        public int PID { get; private set; }
        public string ProjectName { get; set; }
        public string ProjectDescroption { get; set; }

        List<ProjectTab>? _Tabs;
    }
}
