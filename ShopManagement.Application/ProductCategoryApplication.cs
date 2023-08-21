using _0_Framework.Application;
using Azure;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }


        public OperationResult  Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
               return operation.Failed("duplicate record");
           

            var slug = command.Slug.Slugify();

            var productCategory = new ProductCategory(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);

            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
          
            return operation.Succedded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();

            var productcategory = _productCategoryRepository.Get(command.Id );

            if (productcategory == null)
                return operation.Failed("رکوردی با اطلاعات درخواست شده یافت نگردید لطفا دوباره تلاش فرمایید");

            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id ))
            return operation.Failed("امکان ثبت رکورد تکراری وجود ندارد. لطفا دوباره تلاش فرمایید");

            var slug = command.Slug.Slugify();
            productcategory.Edit(command.Name, command.Description,command.Picture,
                command.PictureAlt,command.PictureTitle , command.Keywords,command.MetaDescription,slug);

            _productCategoryRepository.SaveChanges();

            return operation.Succedded();

            
        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }
    }
}