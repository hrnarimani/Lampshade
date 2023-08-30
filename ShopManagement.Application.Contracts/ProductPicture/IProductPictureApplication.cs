using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public interface IProductPictureApplication
    {
        public OperationResult Create(CreateProductPicture command);
        public OperationResult Edit(EditProductPicture command);
        public OperationResult Remove(long id);
        public OperationResult Restore(long id);
        public EditProductPicture GetDetails(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);

    }
}
