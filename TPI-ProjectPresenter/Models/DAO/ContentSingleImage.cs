using System;
using System.Collections.Generic;

namespace TPI_ProjectPresenter.Models.DAO;

public partial class ContentSingleImage
{
    public int Pid { get; set; }

    public int Tid { get; set; }

    public int Sid { get; set; }

    public int Iid { get; set; }

    public string ImageRef { get; set; } = null!;

    public virtual ContentItem ContentItem { get; set; } = null!;
}
