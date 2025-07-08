using Microsoft.AspNetCore.Mvc;
using TPI_ProjectPresenter.Models.DAO;
using TPI_ProjectPresenter.Models.ProjectContent;
using TPI_ProjectPresenter.Models.Projects;
using TPI_ProjectPresenter.Models.DataTx;
using TPI_ProjectPresenter.DataAdapters;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace TPI_ProjectPresenter.Controllers
{
	public class ProjectController : Controller
	{
        private readonly ProjectPresenterPwaContext _DBContext;

        public ProjectController(ProjectPresenterPwaContext dbContext)
        {
            _DBContext = dbContext;
        }

        [HttpPost]
        public IActionResult NewTab(string TabName = "", int PID = 0)
        {

            var tabCount = _DBContext.ProjectTabs.Where(t => t.Pid == PID).Count();
            _DBContext.ProjectTabs.Add(new Models.DAO.ProjectTab() { Pid = PID, Tid = tabCount + 1, Name = TabName });

            _DBContext.SaveChanges();
            return RedirectToAction($"ViewProject", new { ppid = PID });
        }

        [HttpGet]
        public IActionResult NewSection(int pPID, int pTID)
        {
            var aux = new NewSectionData();
            aux.PID = pPID;
            aux.TID = pTID;

            return View(aux);
        }
        [HttpPost]
        public IActionResult NewSection(NewSectionData data)
        {
            var aux = _DBContext.ContentSections.Where(p => p.Pid == data.PID && p.Tid == data.TID).OrderByDescending(p => p.Sid).ToList();
            int SID;
            if (aux.Count > 0) { SID = aux.FirstOrDefault().Sid + 1; }
            else { SID = 1; }

            var newSection = ContentSectionDataAdapter.SectionRowFromObject(data.section);
            newSection.Sid = SID;
            newSection.Pid = data.PID;
            newSection.Tid = data.TID;

            _DBContext.ContentSections.Add(newSection);
            _DBContext.SaveChanges();

            return RedirectToAction("ViewProject", new { ppid =  data.PID });
        }



        [HttpGet]
        public IActionResult ViewProject(int pPID=1, int pTID=1)
		{
            //Querys que deberian ser suficientes pero no
            var prj = _DBContext.Projects.Find([pPID]);

            var tt = _DBContext.Projects.Where(p => p.Pid == pPID).Include(pt => pt.ProjectTabs).ThenInclude(cs => cs.ContentSections)
                     .ThenInclude(ci => ci.ContentItems);

            var sect = _DBContext.ProjectTabs.Find([pPID, 1]);
            var qry = _DBContext.ContentSections.Where(s => s.Pid == pPID && s.Tid == 1).ToList();
            var it = _DBContext.ContentItems.Where(i => i.Pid == pPID);
            var itmg = _DBContext.ContentSingleImages.Where(i => i.Pid == pPID);

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
            
            var objList = ProjectDataAdapter.ProjectRowsToObjects(lista);
            
            /*_DBContext.ProjectTabs.ToList();
            Models.DAO.Project tst = _DBContext.Projects.FirstOrDefault();*/

			return View(objList);
        }
	}
}
