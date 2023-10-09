using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;
using System.Collections.Generic;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {

        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IFileUploader fileUploader, IProductRepository productRepository, IProductCategoryRepository productCategoryRepository)
        {
            _productPictureRepository = productPictureRepository;
            _fileUploader = fileUploader;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
        }

       

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();
            //if (_productPictureRepository.Exists(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
            //{
            //    operation.Failed(ApplicationMessages.DuplicatedRecord);
            //    return operation;
            //}

            var product = _productRepository.GetProductWithCategory(command.ProductId);
            var path = $"{product.Category.Slug}//{product.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);


            var productPicture = new ProductPicture(picturePath, command.PictureAlt, command.PictureTitle,
                    command.ProductId);

                _productPictureRepository.Create(productPicture);
                _productPictureRepository.SaveChanges();

                operation.Succedded("عملیات با موفقیت انجام گردید");
                return operation;

            




        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();

            var productPicture = _productPictureRepository.Get(command.Id);

            if (productPicture == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }


            //if (_productPictureRepository.Exists(x =>
            //        x.Picture == command.Picture && x.ProductId == command.ProductId && x.Id != command.Id))
            //{
            //    operation.Failed(ApplicationMessages.DuplicatedRecord);
            //    return operation;
            //}

            var product = _productRepository.GetProductWithCategory(command.ProductId);
            var path = $"{product.Category.Slug}//{product.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            productPicture.Edit(picturePath, command.PictureAlt, command.PictureTitle, command.ProductId);

            _productPictureRepository.SaveChanges();

            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;
        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();

            var productPicture = _productPictureRepository.Get(id);

            if (productPicture == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }

            productPicture.Remove();
            _productPictureRepository.SaveChanges();

            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();

            var productPicture = _productPictureRepository.Get(id);

            if (productPicture == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }

            productPicture.Restore();
            _productPictureRepository.SaveChanges();

            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;
        }




    }
}
