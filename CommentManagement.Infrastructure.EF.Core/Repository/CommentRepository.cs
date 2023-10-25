using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CommentManagement.Domain.CommentAgg;
using CommentManagementApplication.Contracts.Comment;

namespace CommentManagement.Infrastructure.EF.Core.Repository
{
    public  class CommentRepository:RepositoryBase<long,Comment> , IcommentRepository
    {
        private readonly CommentContext  _context;

        public CommentRepository(CommentContext context) : base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments
               
                .Select(x => new CommentViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Website = x.Website,
                    Message = x.Message,
                    OwnerRecordId = x.OwnerRecordId,
                    IsCanceled =x.IsCanceled,
                    IsConfirmed = x.IsConfirmed,
                    
                    
                    CommentDate = x.CreationDate.ToFarsi(),
                   

                    //bar asase ProductViewModel meghdar dehi shodan
                });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));


            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email.Contains(searchModel.Email));

           

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
