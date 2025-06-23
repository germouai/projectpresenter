using System;
using System.Collections.Generic;

namespace TPI_ProjectPresenter.Models.DAO;

public partial class ContentSingleComparison
{
    public int Pid { get; set; }

    public int Tid { get; set; }

    public int Sid { get; set; }

    public int Iid { get; set; }

    public string Lr { get; set; } = null!;

    public string? Title { get; set; }

    public string? Detail { get; set; }

    public virtual ICollection<ComparisonItemInfo> ComparisonItemInfos { get; set; } = new List<ComparisonItemInfo>();

    public virtual ContentItem ContentItem { get; set; } = null!;
}
