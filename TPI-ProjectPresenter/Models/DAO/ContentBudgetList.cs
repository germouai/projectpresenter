using System;
using System.Collections.Generic;

namespace TPI_ProjectPresenter.Models.DAO;

public partial class ContentBudgetList
{
    public int Pid { get; set; }

    public int Tid { get; set; }

    public int Sid { get; set; }

    public int Iid { get; set; }

    public int OrderNo { get; set; }

    public string? Item { get; set; }

    public string? Description { get; set; }

    public decimal? Value { get; set; }

    public virtual ContentItem ContentItem { get; set; } = null!;
}
