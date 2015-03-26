using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataLayer.Context;
using DomainClasses.Entities;
using EntityFramework.Extensions;
using ServiceLayer.Interfaces;

namespace ServiceLayer.EFServices
{
    public class PictureService : IPictureService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Picture> _pictures;
        #endregion

        #region Constructor

        public PictureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _pictures = _unitOfWork.Set<Picture>();
        }
        #endregion

        public void Add(Picture picture)
        {
            _pictures.Add(picture);
        }

        public void Delete(long id)
        {
            _pictures.Where(a => a.Id.Equals(id)).Delete();
        }

        public IEnumerable<Picture> GetAll(int page, int count, out int total, long folderId)
        {
            var pircuters =
                _pictures.Include(a => a.Folder)
                    .AsNoTracking()
                    .Where(a => a.FolderId.Equals(folderId))
                    .OrderByDescending(a => a.Id);
            var totalQuery = pircuters.FutureCount();
            var query = pircuters.Skip((page - 1) * count).Take(count).Future();
            total = totalQuery.Value;
            return query.ToList();
        }


        public Picture GetById(long id)
        {
            return _pictures.Find(id);
        }


        public string[] GetPicturesOfFolder(long id)
        {
            return
                _pictures.AsNoTracking()
                    .Include(a => a.Folder)
                    .Where(a => a.FolderId == id)
                    .Select(a => a.Path)
                    .ToArray();
        }

        public IEnumerable<Picture> GetpictureByFolderId(long folderId)
        {
            return
                _pictures.AsNoTracking()
                    .Include(a => a.Folder)
                    .Where(a => a.FolderId == folderId)
                    .OrderByDescending(a => a.Id)
                    .ToList();
        }
    }
}
