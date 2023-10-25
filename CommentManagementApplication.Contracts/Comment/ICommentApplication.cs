using _0_Framework.Application;

namespace CommentManagementApplication.Contracts.Comment
{
    public  interface ICommentApplication
    {
        OperationResult Add(AddComment command);
        List<CommentViewModel> Search(CommentSearchModel searchModel);
        OperationResult Confirm(long id);
        OperationResult Cancel(long id);

    }
}
