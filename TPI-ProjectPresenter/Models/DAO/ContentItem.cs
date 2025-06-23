using System;
using System.Collections.Generic;

namespace TPI_ProjectPresenter.Models.DAO;

public partial class ContentItem
{
    public int Pid { get; set; }

    public int Tid { get; set; }

    public int Sid { get; set; }

    public int Iid { get; set; }

    public string? Title { get; set; }

    public string? Text { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<ContentBudgetList> ContentBudgetLists { get; set; } = new List<ContentBudgetList>();

    public virtual ICollection<ContentCarrousel> ContentCarrousels { get; set; } = new List<ContentCarrousel>();

    public virtual ContentSection ContentSection { get; set; } = null!;

    public virtual ICollection<ContentSingleComparison> ContentSingleComparisons { get; set; } = new List<ContentSingleComparison>();

    public virtual ICollection<ContentSingleImage> ContentSingleImages { get; set; } = new List<ContentSingleImage>();

    public virtual ICollection<ContentStaffList> ContentStaffLists { get; set; } = new List<ContentStaffList>();

    public virtual ICollection<ContentTechList> ContentTechLists { get; set; } = new List<ContentTechList>();
}
