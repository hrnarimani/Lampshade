using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application
{
    public  class ProductApplication:IProductApplication
    {
        private readonly IProductRepository _productRepository ;

        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }
            else
            {
                var slug = command.Slug.Slugify();

                var product = new Product(command.Name, command.Code, command.UnitPrice, command.ShortDescription,
                    command.Description,
                    command.Picture, command.PictureAlt, command.PictureTitle,
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
            product.Edit(command.Name, command.Code, command.UnitPrice, command.ShortDescription,
                command.Description,
                command.Picture, command.PictureAlt, command.PictureTitle,
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

        public OperationResult IsStock(long id)
        {
            var operation = new OperationResult();

            var product = _productRepository.Get(id);

            if (product == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }

            product.InStock();
            _productRepository.SaveChanges();

            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;
        }

        public OperationResult NotInStock(long id)
        {
            var operation = new OperationResult();

            var product = _productRepository.Get(id);

            if (product == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }

            product.NotInStock();
            _productRepository.SaveChanges();

            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;
        }
    }
}
