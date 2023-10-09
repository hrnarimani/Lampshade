using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BlogManagement.Application
{
    public class ArticleApplication:IArticleApplication 
    {
       
        private readonly IFileUploader _fileUploader;
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleApplication(IFileUploader fileUploader, IArticleRepository articleRepository, IArticleCategoryRepository articleCategoryRepository)
        {
            _fileUploader = fileUploader;
            _articleRepository = articleRepository;
            _articleCategoryRepository = articleCategoryRepository;
        }


        public OperationResult Create(CreateArticle command)
        {
            var operation = new OperationResult();
            if (_articleRepository.Exists(x => x.Title == command.Title))
            {
                operation.Failed(ApplicationMessages.DuplicatedRecord);
                return operation;
            }

            var slug = command.Slug.Slugify();
            var categorySlug = _articleCategoryRepository.GetSlugBy(command.CategoryId);
            var path = $"{categorySlug}/{slug}";
            var pictureName = _fileUploader.Upload(command.Picture, path);
            var publishDate = command.PublishDate.ToGeorgianDateTime();

            var article = new Article(command.Title, command.ShortDescription, command.Description, pictureName,
                command.PictureAlt, command.PictureTitle, publishDate,slug , command.Keywords, command.MetaDescription,
                command.CanonicalAddress, command.CategoryId);
            _articleRepository.Create(article);
            _articleRepository.SaveChanges();
            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation = new OperationResult();
            var article = _articleRepository.Get(command.Id);
            if (article == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }

            if (_articleRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
            {
                operation.Failed(ApplicationMessages.DuplicatedRecord);
                return operation;
            }
            var slug = command.Slug.Slugify();
            var categorySlug = _articleCategoryRepository.GetSlugBy(command.CategoryId);
            var path = $"{categorySlug}/{slug}";
            var pictureName = _fileUploader.Upload(command.Picture, path);
            var publishDate = command.PublishDate.ToGeorgianDateTime();

            article.Edit(command.Title, command.ShortDescription, command.Description, pictureName,
                command.PictureAlt, command.PictureTitle, publishDate, slug, command.Keywords, command.MetaDescription,
                command.CanonicalAddress, command.CategoryId);
            
            _articleRepository.SaveChanges();
            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;

        }

        public EditArticle GetDetails(long id)
        {
            return _articleRepository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }
    }
}
