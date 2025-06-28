using Microsoft.AspNetCore.Mvc;
using TPI_ProjectPresenter.Models.DAO;
using TPI_ProjectPresenter.Models.ProjectContent;
using TPI_ProjectPresenter.Models.Projects;
using TPI_ProjectPresenter.DataAdapters;
using Microsoft.EntityFrameworkCore;

namespace TPI_ProjectPresenter.Controllers
{
	public class ProjectController : Controller
	{
        private readonly ProjectPresenterPwaContext _DBContext;

        public ProjectController(ProjectPresenterPwaContext dbContext)
        {
            _DBContext = dbContext;
        }

        public IActionResult ViewProject()
		{
            //Querys que deberian ser suficientes pero no
            var prj = _DBContext.Projects.Find([1]);

            var sect = _DBContext.ProjectTabs.Find([1, 1]);
            var qry = _DBContext.ContentSections.Where(s => s.Pid == 1 && s.Tid == 1).ToList();
            var it = _DBContext.ContentItems.Where(i => i.Pid == 1);
            var itmg = _DBContext.ContentSingleImages.Where(i => i.Pid == 1);

            //Querys forzadas para que lea todo
            var pj = _DBContext.Projects.ToList();
            var pjt = _DBContext.ProjectTabs.ToList();
            var ts = _DBContext.ContentSections.ToList();
            var ci = _DBContext.ContentItems.ToList();
            var cim = _DBContext.ContentSingleImages.ToList();
            var csc = _DBContext.ContentSingleComparisons.ToList();
            var cinf = _DBContext.ComparisonItemInfos.ToList();

            var prjObj = ProjectDataAdapter.ProjectEntityFromProject(prj);

            //var qry = _DBContext.ContentSections.FromSql($"select * from ContentSection where PID={1} AND TID={1}").ToList();

            return View(prjObj);
        }

        public IActionResult ProjectList()
        {
            List<Models.DAO.Project> lista = _DBContext.Projects.ToList();
            
            var objList = ProjectDataAdapter.ProjectRowsToHeaderObjects(lista);
            
            /*_DBContext.ProjectTabs.ToList();
            Models.DAO.Project tst = _DBContext.Projects.FirstOrDefault();*/

			return View(objList);
        }
	}
}
