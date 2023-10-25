using _0_Framework.Domain;
using CommentManagementApplication.Contracts.Comment;

namespace CommentManagement.Domain.CommentAgg
{
    public interface  IcommentRepository:IRepository<long , Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
