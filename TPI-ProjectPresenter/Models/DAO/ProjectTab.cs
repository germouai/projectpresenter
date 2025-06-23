using System;
using System.Collections.Generic;

namespace TPI_ProjectPresenter.Models.DAO;

public partial class ProjectTab
{
    public int Pid { get; set; }

    public int Tid { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<ContentSection> ContentSections { get; set; } = new List<ContentSection>();

    public virtual Project PidNavigation { get; set; } = null!;
}
