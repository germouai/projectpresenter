using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using System.ComponentModel;
using System.Dynamic;
using TPI_ProjectPresenter.Models.DAO;
using TPI_ProjectPresenter.Models.ProjectContent;
using TPI_ProjectPresenter.Models.DataTx;

namespace TPI_ProjectPresenter.DataAdapters
{
    public abstract class ContentItemDataAdapter
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

        public static Models.DAO.ContentItem ItemRowFromDataObject(ViewItemData data)
        {
            Models.DAO.ContentItem item = new Models.DAO.ContentItem
            {
                Pid = data.PID,
                Tid = data.TID,
                Sid = data.SID,
                Iid = data.ItemData.IID,
                Title = data.ItemData.ItemTitle,
                Text = data.ItemData.ItemText,
                Type = data.ItemData.ItemType
            };
            switch (data.ItemData.ItemType)
            {
                case "TextOnly":
                    break;
                case "SingleImage":
                    item.ContentSingleImages.Add(new ContentSingleImage { ImageRef = (data.ItemData as ContentItemSingleImage).ImageRef });
                    break;
                case "SingleComparison":
                    var litem = new ContentSingleComparison
                    {
                        Lr = "l",
                        Title = data.SingleComparisonData.LeftItem.ItemTitle,
                        Detail = data.SingleComparisonData.LeftItem.ItemDetail
                    };
                    int i = 0;
                    foreach (var inf in data.SingleComparisonData.LeftItem.ItemizedInfo)
                    {
                        litem.ComparisonItemInfos.Add(new ComparisonItemInfo
                        {
                            OrderNo = i,
                            Info = inf
                        });
                        i++;
                    }
                    i = 0;
                    var ritem = new ContentSingleComparison
                    {
                        Lr = "r",
                        Title = data.SingleComparisonData.RightItem.ItemTitle,
                        Detail = data.SingleComparisonData.RightItem.ItemDetail
                    };
                    foreach (var inf in data.SingleComparisonData.RightItem.ItemizedInfo)
                    {
                        ritem.ComparisonItemInfos.Add(new ComparisonItemInfo
                        {
                            OrderNo = i,
                            Info = inf
                        });
                        i++;
                    }
                    item.ContentSingleComparisons.Add(litem);
                    item.ContentSingleComparisons.Add(ritem);
                    break;
                // Add other cases as needed
            }
            return item;
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
