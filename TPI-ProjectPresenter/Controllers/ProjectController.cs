using Microsoft.AspNetCore.Mvc;
using TPI_ProjectPresenter.Models.ProjectContent;
using TPI_ProjectPresenter.Models.Projects;

namespace TPI_ProjectPresenter.Controllers
{
	public class ProjectController : Controller
	{
		public IActionResult ViewProject()
		{
            /*Objeto Sección de Contenido*/
            var sectiontest = new ContentSection();
            sectiontest.SectionName = "Section Title Test";
            sectiontest.SectionTooltip = "I really hope this works";

            /*Objeto Contenido de sólo texto*/
            var tst = new ContentItemTextOnly();
            tst.ItemText = "Tortor Pharetra Rhoncus Per Faucibus Fames Pellentesque Metus Porttitor Quam At Integer Laoreet Sem Mauris Curabitur Sem Nulla Enim Himenaeos Nec Ante Molestie Velit Taciti Quis Proin Nisl Interdum Aenean Elementum Vitae Tristique Ligula Phasellus Aliquet Convallis Non Vestibulum Magna Vel Aptent Lobortis Nisi Aliquam Feugiat Mauris Imperdiet Non Arcu Placerat Netus Quis Consectetur Habitasse Eget Hac Morbi Eros Ut Metus Morbi Sodales Fermentum Iaculis Potenti Habitasse A Tortor Tempor Porttitor Bibendum Feugiat Odio Nisi Dictumst";
            tst.ItemTitle = "TextOnly Item";
            
            sectiontest.AddContent(tst);

            /*Objeto Conenido con Imágen*/
            var imgtest = new ContentItemSingleImage();
            imgtest.ItemTitle = "Now an Image";
            imgtest.ItemText = "Images can also have an introductory text :D";
            imgtest.ImageRef = "RoadmapTP.png";

            sectiontest.AddContent(imgtest);

            /*Objetos "Entitad de proyecto" y "Pestaña de Proyecto"*/
            var projectest = new ProjectEntity();

            projectest.Header = new ProjectEntity.ProjectHeader() 
            { 
                ProjectDescription = "Nulla Per Metus Libero Condimentum Diam Curabitur Turpis Sit Habitasse Magna Lacus Justo Maecenas Interdum Nibh Ornare Urna Habitasse Morbi Quisque Duis Tristique Felis Risus Dolor Leo Nostra Ullamcorper Volutpat Viverra Varius Mattis Vitae Adipiscing Feugiat Platea Praesent Ligula Augue Semper Dolor Ullamcorper Nam Sem Venenatis Morbi Sit Curae Curabitur Curabitur Litora Ut Metus Nisi Imperdiet Sapien Malesuada Rutrum Sollicitudin A Proin Dictum Posuere Ac",
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
				ProjectDescription = "Nulla Per Metus Libero Condimentum Diam Curabitur Turpis Sit Habitasse Magna Lacus Justo Maecenas Interdum Nibh Ornare Urna Habitasse Morbi Quisque Duis Tristique Felis Risus Dolor Leo Nostra Ullamcorper Volutpat Viverra Varius Mattis Vitae Adipiscing Feugiat Platea Praesent Ligula Augue Semper Dolor Ullamcorper Nam Sem Venenatis Morbi Sit Curae Curabitur Curabitur Litora Ut Metus Nisi Imperdiet Sapien Malesuada Rutrum Sollicitudin A Proin Dictum Posuere Ac",
				ProjectImgRef = "ideas.png",
				ProjectName = "Testing the project title",
				ProjectTooltip = "This should be a considerably long text but not so much, it's small and in italicz."
			};

            List<ProjectEntity.ProjectHeader> projectlist = new List<ProjectEntity.ProjectHeader>();
            projectlist.Add(projectest.Header);

			return View(projectlist);
        }
	}
}
