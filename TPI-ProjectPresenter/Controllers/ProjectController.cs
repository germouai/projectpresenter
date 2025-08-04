using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using System.IO;
using TPI_ProjectPresenter.DataAdapters;
using TPI_ProjectPresenter.Models.DAO;
using TPI_ProjectPresenter.Models.DataTx;
using TPI_ProjectPresenter.Models.ProjectContent;
using TPI_ProjectPresenter.Models.Projects;
using static System.Collections.Specialized.BitVector32;

namespace TPI_ProjectPresenter.Controllers
{
	public class ProjectController : Controller
	{
        private readonly ProjectPresenterPwaContext _DBContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProjectController(ProjectPresenterPwaContext dbContext, IWebHostEnvironment _webHostEnvironment)
        {
            _DBContext = dbContext;
            this.webHostEnvironment = _webHostEnvironment;
        }

        public string UploadImage(IFormFile pImgFile)
        {
            string fileName = null;
            if (pImgFile != null)
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "img");
                fileName = Guid.NewGuid().ToString().Substring(0, 15) + "-" + pImgFile.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    pImgFile.CopyTo(filestream);
                }
            }
            return fileName;
        }
        

        [HttpGet]
        public IActionResult NewProject()
        {
            var aux = new ViewProjectEntityData() { ProjectData = new ProjectEntity() };


            return View(aux);
        }

        [HttpPost]
        public IActionResult NewProject(ViewProjectEntityData np)
        {
            var aux = _DBContext.Projects.OrderByDescending(p => p.Pid).ToList();

            if (aux.Count() != 0) { np.ProjectData.PID = aux.FirstOrDefault().Pid + 1; }
            else { np.ProjectData.PID = 1; }

            string filename = UploadImage(np.ImgFile);
            np.ProjectData.Header.ProjectImgRef = filename;

            var prow = ProjectDataAdapter.ProjectRowFromObject(np.ProjectData);

            _DBContext.Projects.Add(prow);
            _DBContext.SaveChanges();


            return RedirectToAction("ViewProject", new {ppid = np.ProjectData.PID});
        }


        [HttpPost]
        public IActionResult NewTab(string TabName = "", int PID = 0)
        {

            var tabCount = _DBContext.ProjectTabs.Where(t => t.Pid == PID).OrderByDescending(p => p.Tid);
            int TID = tabCount.First().Tid+1;
            _DBContext.ProjectTabs.Add(new Models.DAO.ProjectTab() { Pid = PID, Tid = TID, Name = TabName });

            _DBContext.SaveChanges();
            return RedirectToAction($"ViewProject", new { ppid = PID, ptid = TID });
        }

        [HttpGet]
        public IActionResult NewSection(int pPID, int pTID)
        {
            var aux = new ViewSectionData();
            aux.PID = pPID;
            aux.TID = pTID;

            return View(aux);
        }
        [HttpPost]
        public IActionResult NewSection(ViewSectionData data)
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

            return RedirectToAction("ViewProject", new { ppid =  data.PID, ptid = data.TID });
        }

        [HttpGet]
        public IActionResult NewItem(int pPID, int pTID, int pSID)
        {
            var aux = new ViewItemData();
            aux.PID = pPID;
            aux.TID = pTID;
            aux.SID = pSID;
            return View(aux);
        }
        [HttpPost]
        public IActionResult NewItem(ViewItemData data)
        {
            var aux = _DBContext.ContentItems.Where(p => p.Pid == data.PID && p.Tid == data.TID && p.Sid == data.SID).OrderByDescending(p => p.Iid).ToList();
            int IID;
            if (aux.Count > 0) { IID = aux.FirstOrDefault().Iid + 1; }
            else { IID = 1; }

            if (data.ItemData.ItemType == "SingleImage")
            {
                var imgAux = new Models.ProjectContent.ContentItemSingleImage();
                imgAux.ImageRef = UploadImage(data.ImgFile);
                imgAux.IID = IID;
                imgAux.ItemTitle = data.ItemData.ItemTitle;
                imgAux.ItemText = data.ItemData.ItemText;
                
                data.ItemData = imgAux;
            }
            if (data.ItemData.ItemType == "SingleComparison")
            {
                string[] lItemInfo = [Request.Form["LItemInfo1"], Request.Form["LItemInfo2"], Request.Form["LItemInfo3"]];
                string[] rItemInfo = [Request.Form["RItemInfo1"], Request.Form["RItemInfo2"], Request.Form["RItemInfo3"]];
                data.SingleComparisonData.LeftItem.SetInfoFromArray(lItemInfo);
                data.SingleComparisonData.RightItem.SetInfoFromArray(rItemInfo);
            }

            var newItem = ContentItemDataAdapter.ItemRowFromDataObject(data);
            newItem.Iid = IID;
            newItem.Pid = data.PID;
            newItem.Tid = data.TID;
            newItem.Sid = data.SID;
            _DBContext.ContentItems.Add(newItem);
            _DBContext.SaveChanges();
            return RedirectToAction("ViewProject", new { ppid = data.PID, ptid = data.TID });
        }

        [HttpGet]
        public IActionResult EditProject(int pPID)
        {
            var prj = _DBContext.Projects.Find(pPID);
            if (prj == null) return NotFound();
            var prjObj = ProjectDataAdapter.ProjectHeaderFromProject(prj);
            var aux = new ViewProjectEntityData() { ProjectData = prjObj };
            return View(aux);
        }

        [HttpPost]
        public IActionResult EditProject(ViewProjectEntityData pData)
        {
            var prj = _DBContext.Projects.Find(pData.ProjectData.PID);
            if (prj == null) return NotFound();
            prj.Name = pData.ProjectData.Header.ProjectName;
            prj.Description = pData.ProjectData.Header.ProjectDescription;
            prj.Tooltip = pData.ProjectData.Header.ProjectTooltip;
            if (pData.ImgFile != null)
            {
                prj.ImgRef = UploadImage(pData.ImgFile);
            }
            _DBContext.SaveChanges();
            return RedirectToAction("ViewProject", new { pPID = pData.ProjectData.PID });
        }


        [HttpGet]
        public IActionResult EditTabs(int pPID)
        {
            var prj = _DBContext.Projects.Where(p => p.Pid == pPID).Include(p => p.ProjectTabs);
            if (prj == null) return NotFound();

            var aux = ProjectDataAdapter.ProjectTabsFromRow(prj.First());
            return View(aux);
        }

        [HttpPost]
        public IActionResult EditTabs(int PID, int tabcount)
        {
            var prj = _DBContext.Projects.Find(PID);
            if (prj == null) return NotFound();
            var tabs = _DBContext.ProjectTabs.Where(t => t.Pid == PID).ToList();
            foreach (var tab in tabs)
            {
                var newName = Request.Form[$"Tab-{tab.Tid}"];
                if (!string.IsNullOrEmpty(newName))
                {
                    tab.Name = newName;
                }
            }
            _DBContext.SaveChanges();
            return RedirectToAction("ViewProject", new { pPID = PID });
        }


        [HttpGet]
        public IActionResult EditSection(int pPID, int pTID, int pSID)
        {
            var section = _DBContext.ContentSections.Find(pPID, pTID, pSID);
            if (section == null) return NotFound();
            var aux = new ViewSectionData();
            aux.PID = pPID;
            aux.TID = pTID;
            aux.section = ContentSectionDataAdapter.SectionObjectFromRow(section);
            return View(aux);
        }

        [HttpPost]
        public IActionResult EditSection(ViewSectionData pData)
        {
            var section = _DBContext.ContentSections.Find(pData.PID, pData.TID, pData.section.SID);
            if (section == null) return NotFound();
            section.Name = pData.section.SectionName;
            section.Tooltip = pData.section.SectionTooltip;
            _DBContext.SaveChanges();
            return RedirectToAction("ViewProject", new { pPID = pData.PID, pTID = pData.TID });
        }

        [HttpGet]
        public IActionResult EditItem(int pPID, int pTID, int pSID, int pIID)
        {
            var item = _DBContext.ContentItems.Find(pPID, pTID, pSID, pIID);
            if (item == null) return NotFound();

            var cim = _DBContext.ContentSingleImages.ToList();
            var csc = _DBContext.ContentSingleComparisons.ToList();
            var cinf = _DBContext.ComparisonItemInfos.ToList();

            var aux = ContentItemDataAdapter.DataObjectFromItemRow(item);
            
            
            ViewBag.ItemType = item.Type;
            return View(aux);
        }

        [HttpPost]
        public IActionResult EditItem(ViewItemData pData)
        {
            if (pData.ItemData.IID == 0)
            {
                pData.ItemData.IID = Int32.Parse(Request.Form["ItemData.IID"]);
            }
            var item = _DBContext.ContentItems.Find(pData.PID, pData.TID, pData.SID, pData.ItemData.IID);
            if (item == null) return NotFound();
            item.Title = pData.ItemData.ItemTitle;
            item.Text = pData.ItemData.ItemText;
            if (pData.ImgFile != null && pData.ItemData.ItemType == "SingleImage")
            {
                var cim = _DBContext.ContentSingleImages.ToList();
                var imgAux = item.ContentSingleImages.FirstOrDefault();
                imgAux.ImageRef = UploadImage(pData.ImgFile);
            }
            if (pData.ItemData.ItemType == "SingleComparison")
            {
                var itemlcomp = item.ContentSingleComparisons.Where(c => c.Lr == "l").FirstOrDefault();
                var itemrcomp = item.ContentSingleComparisons.Where(c => c.Lr == "r").FirstOrDefault();

                itemlcomp.Title = pData.SingleComparisonData.LeftItem.ItemTitle;
                itemlcomp.Detail = pData.SingleComparisonData.LeftItem.ItemDetail;
                //itemlcomp.ComparisonItemInfos



                string[] lItemInfo = [Request.Form["LItemInfo-1"], Request.Form["LItemInfo-2"], Request.Form["LItemInfo-3"]];
                string[] rItemInfo = [Request.Form["RItemInfo-1"], Request.Form["RItemInfo-2"], Request.Form["RItemInfo-3"]];
                pData.SingleComparisonData.LeftItem.SetInfoFromArray(lItemInfo);
                pData.SingleComparisonData.RightItem.SetInfoFromArray(rItemInfo);

                var newcomps = ContentItemDataAdapter.ComparisonRowFromDataObject(pData.SingleComparisonData);
                foreach (var comp in newcomps)
                {
                    //var existingComp = item.ContentSingleComparisons.u
                }
            }
            _DBContext.SaveChanges();
            return RedirectToAction("ViewProject", new { pPID = pData.PID, pTID = pData.TID });
        }





        [HttpGet]
        public IActionResult DeleteProject(int pPID)
        {
            var prj = _DBContext.Projects.Find(pPID);
            if (prj == null) return NotFound();
            var aux = new ViewProjectEntityData();
            aux.ProjectData = ProjectDataAdapter.ProjectHeaderFromProject(prj);
            return View(aux);
        }

        [HttpPost]
        public IActionResult DeleteProject(ViewProjectEntityData pData)
        {
            var prj = _DBContext.Projects.Find(pData.ProjectData.PID);
            if (prj == null) return NotFound();


            _DBContext.ComparisonItemInfos.RemoveRange(_DBContext.ComparisonItemInfos.Where(i => i.Pid == prj.Pid));
            _DBContext.ContentSingleComparisons.RemoveRange(_DBContext.ContentSingleComparisons.Where(c => c.Pid == prj.Pid));
            _DBContext.ContentSingleImages.RemoveRange(_DBContext.ContentSingleImages.Where(i => i.Pid == prj.Pid));
            _DBContext.ContentItems.RemoveRange(_DBContext.ContentItems.Where(i => i.Pid == prj.Pid));
            _DBContext.ContentSections.RemoveRange(_DBContext.ContentSections.Where(s => s.Pid == prj.Pid));
            _DBContext.ProjectTabs.RemoveRange(_DBContext.ProjectTabs.Where(t => t.Pid == prj.Pid));

            _DBContext.Projects.Remove(prj);
            _DBContext.SaveChanges();
            return RedirectToAction("ProjectList");
        }


        [HttpPost]
        public IActionResult DeleteTab(int PID, int TID)
        {
            var tab = _DBContext.ProjectTabs.Find(PID, TID);
            if (tab == null) return NotFound();

            _DBContext.ComparisonItemInfos.RemoveRange(_DBContext.ComparisonItemInfos.Where(i => i.Pid == tab.Pid && i.Tid == tab.Tid));
            _DBContext.ContentSingleComparisons.RemoveRange(_DBContext.ContentSingleComparisons.Where(c => c.Pid == tab.Pid && c.Tid == tab.Tid));
            _DBContext.ContentSingleImages.RemoveRange(_DBContext.ContentSingleImages.Where(i => i.Pid == tab.Pid && i.Tid == tab.Tid));
            _DBContext.ContentItems.RemoveRange(_DBContext.ContentItems.Where(i => i.Pid == tab.Pid && i.Tid == tab.Tid));
            _DBContext.ContentSections.RemoveRange(_DBContext.ContentSections.Where(s => s.Pid == tab.Pid && s.Tid == tab.Tid));
            _DBContext.ProjectTabs.Remove(tab);

            _DBContext.SaveChanges();
            return RedirectToAction("EditTabs", new { pPID = PID });
        }


        [HttpGet]
        public IActionResult DeleteSection(int pPID, int pTID, int pSID)
        {
            var section = _DBContext.ContentSections.Find(pPID, pTID, pSID);
            if (section == null) return NotFound();
            var aux = new ViewSectionData();
            aux.PID = pPID;
            aux.TID = pTID;
            aux.section = ContentSectionDataAdapter.SectionObjectFromRow(section);
            return View(aux);
        }
        [HttpPost]
        public IActionResult DeleteSection(ViewSectionData pData)
        {
            var section = _DBContext.ContentSections.Find(pData.PID, pData.TID, pData.section.SID);
            if (section == null) return NotFound();

            _DBContext.ComparisonItemInfos.RemoveRange(_DBContext.ComparisonItemInfos.Where(i => i.Pid == section.Pid && i.Tid == section.Tid && i.Sid == i.Sid));
            _DBContext.ContentSingleComparisons.RemoveRange(_DBContext.ContentSingleComparisons.Where(c => c.Pid == section.Pid && c.Tid == section.Tid && c.Sid == c.Sid));
            _DBContext.ContentSingleImages.RemoveRange(_DBContext.ContentSingleImages.Where(i => i.Pid == section.Pid && i.Tid == section.Tid && i.Sid == i.Sid));
            _DBContext.ContentItems.RemoveRange(_DBContext.ContentItems.Where(i => i.Pid == section.Pid && i.Tid == section.Tid && i.Sid == section.Sid));
            _DBContext.ContentSections.Remove(section);
            
            _DBContext.SaveChanges();
            return RedirectToAction("ViewProject", new { pPID = pData.PID, pTID = pData.TID });
        }



        [HttpGet]
        public IActionResult ViewProject(int? pTID, int pPID=1)
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

            if (pTID != null) ViewBag.activeTab = pTID;
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
