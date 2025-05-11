using Microsoft.AspNetCore.Mvc;
using TPI_ProjectPresenter.Models.ProjectContent;
using TPI_ProjectPresenter.Models.Projects;

namespace TPI_ProjectPresenter.Controllers
{
	public class ProjectController : Controller
	{
		public IActionResult ViewProject()
		{
            var tst = new ContentItemTextOnly("TextyText from ContentItem");
            tst.ItemTitle = "OnlyText Item";
            var sectiontest = new ContentSection();
            sectiontest.SectionName = "Section Title Test";
            sectiontest.SectionTooltip = "I really hope this works";
            sectiontest.AddContent(tst);

            var imgtest = new ContentItemSingleImage();
            imgtest.ItemTitle = "Now an Image";
            imgtest.ImageRef = "RoadmapTP.png";

            sectiontest.AddContent(imgtest);

            var projectest = new ProjectEntity();

            projectest.Header = new ProjectEntity.ProjectHeader() 
            { 
                ProjectDescription = "Nos this is the one to make extensive, at least for the testing. You know, maybe it fucks up responsiveness or sth. You can never know until it's fucked up.",
                ProjectImgRef = "ideas.png", 
                ProjectName = "Testing the project title", 
                ProjectTooltip = "This should be a considerably long text but not so much, it's small and in italicz."  
            };

            var tabtest = new ProjectTab();
            tabtest.TabName = "Basic Info";
            tabtest.AddSection(sectiontest);

            projectest.AddTab(tabtest);

            return View(projectest);
        }

        public IActionResult ProjectList()
        {
			var projectest = new ProjectEntity();

			projectest.Header = new ProjectEntity.ProjectHeader()
			{
				ProjectDescription = "Nos this is the one to make extensive, at least for the testing. You know, maybe it fucks up responsiveness or sth. You can never know until it's fucked up.",
				ProjectImgRef = "ideas.png",
				ProjectName = "Testing the project title",
				ProjectTooltip = "This should be a considerably long text but not so much, it's small and in italicz."
			};

			return View(projectest);
        }
	}
}
