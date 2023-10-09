using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public  class ProductApplication:IProductApplication
    {


        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
        }

     
        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
            {
                operation.Failed(ApplicationMessages.DuplicatedRecord); 
                return operation;
            }
            else
            {
                var slug = command.Slug.Slugify();
                var categorySlug = _productCategoryRepository.GetSlugById(command.CategoryId);
                var path = $"{categorySlug}//{slug}";
                var picturePath = _fileUploader.Upload(command.Picture, path);

                var product = new Product(command.Name, command.Code, command.ShortDescription,
                    command.Description,
                    picturePath, command.PictureAlt, command.PictureTitle,
                    command.CategoryId, command.KeyWords, command.MetaDescription, slug);

                _productRepository.Create(product);
                _productRepository.SaveChanges();

                operation.Succedded("عملیات با موفقیت انجام گردید");
                return operation;
            }
        }

        
      public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();

            var product = _productRepository.Get(command.Id);

            if (product == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }


            if (_productRepository .Exists(x => x.Name == command.Name && x.Id != command.Id))
            {
                operation.Failed(ApplicationMessages.DuplicatedRecord);
                return operation;
            }

            var slug = command.Slug.Slugify();
            var categorySlug = _productCategoryRepository.GetSlugById(command.CategoryId);
            var path = $"{categorySlug}//{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            product.Edit(command.Name, command.Code, command.ShortDescription,
                command.Description,
                picturePath, command.PictureAlt, command.PictureTitle,
                command.CategoryId, command.KeyWords, command.MetaDescription, slug);

            _productRepository .SaveChanges();

            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;
        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }

        public List<ProductViewModel> Serach(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }

       

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }
    }
}
