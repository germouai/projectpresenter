namespace TPI_ProjectPresenter.DataAdapters
{
    public abstract class ProjectDataAdapter
    {

        public static List<Models.Projects.ProjectEntity> ProjectRowsToObjects(List<Models.DAO.Project> projectRows)
        {
            List<Models.Projects.ProjectEntity> aux = new List<Models.Projects.ProjectEntity>();

            foreach (var row in projectRows)
            {
                var tmp = new Models.Projects.ProjectEntity(row.Pid, row.Name, row.Description, row.Tooltip, row.ImgRef);
                aux.Add(tmp);
            }

            return aux;
        }

        public static List<Models.Projects.ProjectEntity.ProjectHeader> ProjectRowsToHeaderObjects(List<Models.DAO.Project> projectRows)
        {
            List<Models.Projects.ProjectEntity.ProjectHeader> aux = new List<Models.Projects.ProjectEntity.ProjectHeader>();

            foreach (var row in projectRows)
            {
                var tmp = new Models.Projects.ProjectEntity(row.Pid, row.Name, row.Description, row.Tooltip, row.ImgRef);
                aux.Add(tmp.Header);
            }

            return aux;
        }

        public static Models.Projects.ProjectEntity ProjectEntityFromProject(Models.DAO.Project pProject)
        {
            Models.Projects.ProjectEntity aux = new Models.Projects.ProjectEntity(pProject.Pid, pProject.Name, pProject.Description, pProject.Tooltip, pProject.ImgRef);

            foreach (var tab in pProject.ProjectTabs)
            {
                var tabObj = new Models.Projects.ProjectTab(tab.Tid, tab.Name);

                foreach (var sect in tab.ContentSections)
                {
                    var sectObj = new Models.ProjectContent.ContentSection();

                    sectObj.SID = sect.Sid;
                    sectObj.SectionName = sect.Name;
                    sectObj.SectionTooltip = sect.Tooltip;
                    sectObj.setContent(ContentItemsDataAdapter.ItemsFromItemRows(sect.ContentItems.ToList()));



                    tabObj.AddSection(sectObj);
                }



                aux.AddTab(tabObj);
            }




            return aux;
        }
        
    }
}
