﻿using Microsoft.AspNetCore.Mvc;
using TPI_ProjectPresenter.Models.DAO;
using TPI_ProjectPresenter.Models.ProjectContent;
using TPI_ProjectPresenter.Models.Projects;
using TPI_ProjectPresenter.Models.DataTx;
using TPI_ProjectPresenter.DataAdapters;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Microsoft.AspNetCore.Hosting;
using System.IO;

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

            var tabCount = _DBContext.ProjectTabs.Where(t => t.Pid == PID).Count();
            _DBContext.ProjectTabs.Add(new Models.DAO.ProjectTab() { Pid = PID, Tid = tabCount + 1, Name = TabName });

            _DBContext.SaveChanges();
            return RedirectToAction($"ViewProject", new { ppid = PID, ptid = tabCount+1 });
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

            ViewBag.activeTab = pTID;
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
