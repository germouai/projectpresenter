using System;
using System.Collections.Generic;

namespace TPI_ProjectPresenter.Models.DAO;

public partial class ComparisonItemInfo
{
    public int Pid { get; set; }

    public int Tid { get; set; }

    public int Sid { get; set; }

    public int Iid { get; set; }

    public string Lr { get; set; } = null!;

    public int OrderNo { get; set; }

    public string? Info { get; set; }

    public virtual ContentSingleComparison ContentSingleComparison { get; set; } = null!;
}
