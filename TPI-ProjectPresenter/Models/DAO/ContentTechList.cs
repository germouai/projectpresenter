using System;
using System.Collections.Generic;

namespace TPI_ProjectPresenter.Models.DAO;

public partial class ContentTechList
{
    public int Pid { get; set; }

    public int Tid { get; set; }

    public int Sid { get; set; }

    public int Iid { get; set; }

    public int OrderNo { get; set; }

    public string? TechName { get; set; }

    public string? TechUses { get; set; }

    public string? IconRef { get; set; }

    public virtual ContentItem ContentItem { get; set; } = null!;
}
