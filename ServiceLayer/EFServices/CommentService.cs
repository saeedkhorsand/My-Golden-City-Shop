using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using EntityFramework.Extensions;
using ServiceLayer.Interfaces;
using DataLayer.Context;

namespace ServiceLayer.EFServices
{
    public class CommentService : ICommentService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Comment> _comments;
        #endregion

        #region Constructor
        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _comments = _unitOfWork.Set<Comment>();
        }
        #endregion

        #region Private Methods

        private void RecursiveRemove(Comment comment)
        {
            if (comment == null)
                return;
            if (!comment.Children.Any())
                return;
            foreach (var item in _comments)
            {
                RecursiveRemove(item);
                _comments.Where(a => a.Id == item.Id).Delete();
            }
        }
        #endregion
        public void Remove(long id)
        {
            _comments.Where(a => a.Id == id).Delete();
        }

        public async Task RemoveUserComments(long userId)
        {
            await _comments.Where(a => a.Author.Id == userId).DeleteAsync();
        }

        public void AddCommnet(Comment comment)
        {
            _comments.Add(comment);
        }

        public IEnumerable<Comment> GetProductComments(long productId)
        {
            return
                _comments.Include(a => a.Author)
                    .Include(a => a.Children).Where(a => a.ParentId == productId).ToList();
        }
    }
}
