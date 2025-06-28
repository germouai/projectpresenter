using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using System.ComponentModel;
using System.Dynamic;
using TPI_ProjectPresenter.Models.DAO;
using TPI_ProjectPresenter.Models.ProjectContent;

namespace TPI_ProjectPresenter.DataAdapters
{
    public abstract class ContentItemsDataAdapter
    {

        public static List<Models.ProjectContent.ContentItem> ItemsFromItemRows(List<Models.DAO.ContentItem> pItems)
        {
            List<Models.ProjectContent.ContentItem> aux = new List<Models.ProjectContent.ContentItem>();

            foreach (var item in pItems)
            {
                var tmp = CreateInstance($"ContentItem{item.Type}");
                if (tmp != null )
                {
                    tmp.IID = item.Iid;
                    tmp.ItemTitle = item.Title;
                    tmp.ItemText = item.Text;
                    tmp.setItemType($"ContentItem{item.Type}");

                    switch (item.Type)
                    {
                        case "TextOnly":
                            break;
                        case "SingleImage":
                            tmp.ImageRef = item.ContentSingleImages.FirstOrDefault().ImageRef;
                            break;
                        case "SingleComparison":
                            var lr = item.ContentSingleComparisons.FirstOrDefault();
                            var rr = item.ContentSingleComparisons.LastOrDefault();
                            var li = new Models.ProjectContent.ComparisonItem(lr.Title, lr.Detail);
                            var ri = new Models.ProjectContent.ComparisonItem(rr.Title, rr.Detail);

                            li.SetInfoFromRowArray(lr.ComparisonItemInfos.OrderBy(inf => inf.OrderNo).ToArray());
                            ri.SetInfoFromRowArray(rr.ComparisonItemInfos.OrderBy(inf => inf.OrderNo).ToArray());

                            tmp.LeftItem = li;
                            tmp.RightItem = ri;

                            break;
                    }
                    aux.Add(tmp);
                }
            }

            return aux;
        }

        public static dynamic? CreateInstance(string className)
        {
            var type = Type.GetType($"TPI_ProjectPresenter.Models.ProjectContent.{className}");
            return type != null ? Activator.CreateInstance(type) : null;
        }

    }      
        
        
        /*<Models.ProjectContent.ContentItem> _itemObjects;

        List<Models.DAO.ContentItem> _itemRows;

        public ContentItemsDataAdapter()
        {
            _itemObjects = new List<Models.ProjectContent.ContentItem>();
            _itemRows = new List<Models.DAO.ContentItem>();
        }
        public void SetItemObjects(List<Models.ProjectContent.ContentItem> itemObjects)
        {
            _itemObjects = itemObjects;
        }
        public void SetItemRows(List<Models.DAO.ContentItem> rowObjects)
        {
            _itemRows = rowObjects;
        }
        public bool LoadItemObjects()
        {
            List<Models.ProjectContent.ContentItem> auxlist = new List<Models.ProjectContent.ContentItem>();

            foreach (var row in _itemRows)
            {
                Models.ProjectContent.ContentItem item = CreateInstance(row.Type);
                //item.setItemType(row.Type);
                item.ItemTitle = row.Title;
                item.ItemText = row.Text;
                item.IID = row.Iid;

                string itemType = row.Type;
                switch (itemType)
                {
                    case "ContentItemTextOnly":
                        break;
                    case "ContentItemSingleImage":
                        Models.ProjectContent.ContentItemSingleImage tmp = item as Models.ProjectContent.ContentItemSingleImage;
                        tmp.ImageRef = row.ContentSingleImages.FirstOrDefault().ImageRef;
                        break;
                    case "ContentItemCarrousel":
                        break;
                    case "ContentItemSingleComparison":
                        break;
                    case "ContentItemTechList":
                        break;
                    case "ContentItemStaffList":
                        break;
                    case "ContentItemBudgetList":
                        break;
                    default:
                        break;

                }

                auxlist.Add(item);
            }


            return true;
        }*/

        
}
