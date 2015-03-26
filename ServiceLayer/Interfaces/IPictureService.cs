using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace ServiceLayer.Interfaces
{
    public interface IPictureService
    {
        void Add(Picture picture);
        void Delete(long id);
        DomainClasses.Entities.Picture GetById(long id);
        IEnumerable<Picture> GetAll(int page, int count,out int total,long folderId);
        string[] GetPicturesOfFolder(long id);


        IEnumerable<Picture> GetpictureByFolderId(long folderId);
    }
}
