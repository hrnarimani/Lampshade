using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public class ProductPicture:EntityBase
    {
     

        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle{ get; private set; }
        public bool IsRemove { get; private set; }
        public long ProductId { get; private set; }
        public Product Product { get; private set; }


        public ProductPicture(string picture, string pictureAlt, string pictureTitle, long productId)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ProductId = productId;
            IsRemove = false;
        }


        public void Edit(string picture, string pictureAlt, string pictureTitle, long productId)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ProductId = productId;
        }


        public void Remove()
        {
            IsRemove=true;
        }

        public void Restore()
        {
            IsRemove = false;
        }


    }


}
