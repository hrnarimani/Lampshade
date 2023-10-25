using CommentManagement.Application;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EF.Core;
using CommentManagement.Infrastructure.EF.Core.Repository;
using CommentManagementApplication.Contracts.Comment;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommentManagement.Configuration
{
    public class CommentManagementBootstarpper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            

            services.AddTransient<ICommentApplication, CommentApplication>();
            services.AddTransient<IcommentRepository, CommentRepository>();

            services.AddDbContext<CommentContext>(x => x.UseSqlServer(connectionString));

        }
    }
}