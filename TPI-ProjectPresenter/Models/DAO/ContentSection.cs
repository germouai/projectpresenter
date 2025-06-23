using System;
using System.Collections.Generic;

namespace TPI_ProjectPresenter.Models.DAO;

public partial class ContentSection
{
    public int Pid { get; set; }

    public int Tid { get; set; }

    public int Sid { get; set; }

    public string? Name { get; set; }

    public string? Tooltip { get; set; }

    public virtual ICollection<ContentItem> ContentItems { get; set; } = new List<ContentItem>();

    public virtual ProjectTab ProjectTab { get; set; } = null!;
}
