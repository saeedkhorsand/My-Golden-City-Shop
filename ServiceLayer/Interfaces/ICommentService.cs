using System.Collections.Generic;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace ServiceLayer.Interfaces
{
    public interface ICommentService
    {

        void Remove(long id);
        void AddCommnet(Comment comment);
        IEnumerable<Comment> GetProductComments(long productId);
        Task RemoveUserComments(long userId);

    }
}
