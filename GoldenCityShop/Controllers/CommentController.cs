using System.Web.Mvc;
using DataLayer.Context;
using ServiceLayer.Interfaces;

namespace GoldenCityShop.Controllers
{
    public partial class CommentController : Controller
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentService _commentService;
        #endregion

        #region Constructor

        public CommentController(IUnitOfWork unitOfWork,ICommentService commentService)
        {
            _unitOfWork = unitOfWork;
            _commentService = commentService;
        }
        #endregion

        #region GetComments
        public virtual ActionResult ProductComments(long? productId)
        {
            return PartialView(MVC.Comment.Views._CommentsPartial);
        }
        #endregion
        #region Reply

        public virtual ActionResult ReplyComment(long? productId, long? commentId)
        {
            return PartialView(MVC.Comment.Views._ReplyToComment);
        }
        #endregion
        #region Add
        [HttpGet]
        public virtual ActionResult AddComment(long? productId)
        {
            return PartialView(MVC.Comment.Views._AddComment);
        }
        [HttpPost]
        public virtual ActionResult AddComment()
        {
            return PartialView(MVC.Comment.Views._AddComment);
        }
        #endregion
    }
}