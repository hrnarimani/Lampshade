using _0_Framework.Application;
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


        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
            { 
                operation.Failed(ApplicationMessages.RecordNotFound );
            return operation;
            }
            else
            {
                var slug = command.Slug.Slugify();

                var productCategory = new ProductCategory(command.Name, command.Description, command.Picture,
                    command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);

                _productCategoryRepository.Create(productCategory);
                _productCategoryRepository.SaveChanges();

                operation.Succedded(ApplicationMessages.SuccessMessage );
                return operation;
               
            }
            
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();

            var productcategory = _productCategoryRepository.Get(command.Id);

            if (productcategory == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }
            

            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            {
                operation.Failed(ApplicationMessages.DuplicatedRecord );
                return operation;
            }

            var slug = command.Slug.Slugify();
            productcategory.Edit(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);

            _productCategoryRepository.SaveChanges();

            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;

        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetProductCategories();
        }
    }
}