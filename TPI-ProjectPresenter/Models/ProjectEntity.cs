namespace TPI_ProjectPresenter.Models
{
    public class ProjectEntity
    {
        public int PID { get; private set; }
        public string ProjectName { get; set; }
        public string ProjectDescroption { get; set; }

        List<ProjectTab> _Tabs;
    }
}
