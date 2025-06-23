using System;
using System.Collections.Generic;

namespace TPI_ProjectPresenter.Models.DAO;

public partial class ContentStaffList
{
    public int Pid { get; set; }

    public int Tid { get; set; }

    public int Sid { get; set; }

    public int Iid { get; set; }

    public int OrderNo { get; set; }

    public string? Name { get; set; }

    public string? Role { get; set; }

    public string? ImageRef { get; set; }

    public virtual ContentItem ContentItem { get; set; } = null!;
}
