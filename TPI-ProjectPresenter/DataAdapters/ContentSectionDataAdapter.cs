namespace TPI_ProjectPresenter.DataAdapters
{
    public abstract class ContentSectionDataAdapter
    {
        public static Models.DAO.ContentSection SectionRowFromObject(Models.ProjectContent.ContentSection pSection)
        {
            Models.DAO.ContentSection sectionRow = new Models.DAO.ContentSection()
            {
                Sid = pSection.SID,
                Name = pSection.SectionName,
                Tooltip = pSection.SectionTooltip
            };

            return sectionRow;
        }
    }
}
