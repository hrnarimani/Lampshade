using _0_Framework.Application;
using CommentManagement.Domain.CommentAgg;
using CommentManagementApplication.Contracts.Comment;

namespace CommentManagement.Application
{
    public class CommentApplication:ICommentApplication
    {
        private readonly IcommentRepository _commentRepository;

        public CommentApplication(IcommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Add(AddComment command)
        {
            var operation = new OperationResult();
            var comment = new Comment(command.Name, command.Email,command.Website, command.Message, command.OwnerRecordId ,command.Type,command.ParentId);
            _commentRepository.Create(comment);
            _commentRepository.SaveChanges();
            operation.Succedded("عملیات با موفقیت انجام گردید");
            return operation;

        }


        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
           return _commentRepository.Search(searchModel);
        }

        public OperationResult Confirm(long id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);

            if (comment == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }
           
            comment.Confirm();
            _commentRepository.SaveChanges();
            operation.Succedded("عملیات با موفقیت انجام گردید");
            return operation;
        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);

            if (comment == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }

            comment.Cancel();
            _commentRepository.SaveChanges();
            operation.Succedded("عملیات با موفقیت انجام گردید");
            return operation;
        }
    }
}
