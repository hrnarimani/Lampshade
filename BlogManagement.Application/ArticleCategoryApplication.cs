using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication: IArticleCategoryApplication
    {
     

        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var operation = new OperationResult();
            if (_articleCategoryRepository.Exists(x => x.Name == command.Name))
            {
                 operation.Failed(ApplicationMessages.DuplicatedRecord);
                 return operation;
            }

            var slug = command.Slug.Slugify();
            var pictureName = _fileUploader.Upload(command.Picture, slug);
            var articleCategory = new ArticleCategory(command.Name, pictureName, command.PictureAlt,
                command.PictureTitle
                , command.Description, command.ShowOrder, slug, command.Keywords, command.MetaDescription,
                command.CanonicalAddress);
            _articleCategoryRepository.Create(articleCategory);
            _articleCategoryRepository.SaveChanges();
            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;

        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();
            var aticleCategory = _articleCategoryRepository.Get(command.Id);

            if (aticleCategory == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }

            if (_articleCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            {
                operation.Failed(ApplicationMessages.DuplicatedRecord);
                return operation;
            }
                 

            var slug = command.Slug.Slugify();
            var pictureName = _fileUploader.Upload(command.Picture, slug);
            aticleCategory.Edit(command.Name, pictureName, command.PictureAlt,
                command.PictureTitle
                , command.Description, command.ShowOrder, slug, command.Keywords, command.MetaDescription,
                command.CanonicalAddress);

            _articleCategoryRepository.SaveChanges();
            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;

        }

        public EditArticleCategory GetDetails(long id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }

       

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.Search(searchModel);
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _articleCategoryRepository.GetArticleCategories();
        }
    }
}