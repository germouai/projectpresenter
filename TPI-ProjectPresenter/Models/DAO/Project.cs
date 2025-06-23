using System;
using System.Collections.Generic;

namespace TPI_ProjectPresenter.Models.DAO;

public partial class Project
{
    public int Pid { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Tooltip { get; set; }

    public string? ImgRef { get; set; }

    public virtual ICollection<ProjectTab> ProjectTabs { get; set; } = new List<ProjectTab>();
}
