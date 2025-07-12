using Microsoft.AspNetCore.Components.Forms;

namespace TPI_ProjectPresenter.Models.DataTx
{
    public class ViewProjectEntityData
    {
        public Models.Projects.ProjectEntity ProjectData { get; set; }
        public IFormFile? ImgFile { get; set; }
    }
}
