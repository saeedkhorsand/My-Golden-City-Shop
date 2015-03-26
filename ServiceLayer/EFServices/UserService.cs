using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ServiceLayer.Interfaces;
using DomainClasses.Enums;
using DataLayer.Context;
using System.Data.Entity;
using DomainClasses.Entities;
using Utilities.Security;
using System.Globalization;
using EntityFramework.Extensions;
using ViewModel;
using ViewModel.Admin;
using System.Linq.Expressions;
using ViewModel.Admin.Product;

namespace ServiceLayer.EFServices
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<User> _users;

        #endregion //Fields

        #region Cosntructor
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _users = _unitOfWork.Set<User>();
        }
        #endregion //Constructor

        #region Private Members
        private static VerifyUserStatus Verify(User selectedUser, string password)
        {
            var result = VerifyUserStatus.VerifiedFaild;

            bool verifiedPassword = Encryption.VerifyPassword(password, selectedUser.Password);

            if (verifiedPassword)
            {
                if (selectedUser.IsBaned)
                {
                    result = VerifyUserStatus.UserIsBaned;
                }
                else
                {
                    selectedUser.LastLoginDate = DateTime.Now;
                    result = VerifyUserStatus.VerifiedSuccessfully;
                }
            }

            return result;
        }

        #endregion // Private Members

        #region CRUD
        public EditedUserStatus EditUser(User user)
        {
            if (ExistsByPhoneNumber(user.PhoneNumber, user.Id)) return EditedUserStatus.PhoneNumberExist;
            if (ExistsByUserName(user.UserName, user.Id)) return EditedUserStatus.UserNameExist;
            var selectedUser = GetUserById(user.Id);
            if (user.Password != null) selectedUser.Password = user.Password;
            selectedUser.UserName = user.UserName;
            selectedUser.FirstName = user.FirstName;
            selectedUser.LastName = user.LastName;
            selectedUser.IsBaned = user.IsBaned;
            selectedUser.PhoneNumber = user.PhoneNumber;
            selectedUser.Role = user.Role;
            return EditedUserStatus.UpdatingUserSuccessfully;
        }

        public IList<UserViewModel> GetDataTable(out int total, string term, int page, int count, DomainClasses.Enums.Order order, UserOrderBy orderBy, UserSearchBy searchBy)
        {
            var selectedUsers = _users.AsNoTracking().Include(a => a.Role).AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case UserSearchBy.UserName:
                        selectedUsers = selectedUsers.Where(user => user.UserName.Contains(term)).AsQueryable();
                        break;
                    case UserSearchBy.RoleDescription:
                        selectedUsers = selectedUsers.Where(user => user.Role.Description.Contains(term)).AsQueryable();
                        break;
                    case UserSearchBy.PhoneNumber:
                        selectedUsers =
                            selectedUsers.Where(user => user.PhoneNumber.Contains(term)).AsQueryable();
                        break;
                    case UserSearchBy.Ip:
                        selectedUsers =
                            selectedUsers.Where(user => user.IP.Contains(term)).AsQueryable();
                        break;
                }
            }


            if (order == DomainClasses.Enums.Order.Asscending)
            {
                switch (orderBy)
                {
                    case UserOrderBy.UserName:
                        selectedUsers = selectedUsers.OrderBy(user => user.UserName).AsQueryable();
                        break;
                    case UserOrderBy.OrderCount:
                        selectedUsers = selectedUsers.OrderBy(user => user.Orders.Count).AsQueryable();
                        break;
                    case UserOrderBy.RegisterDate:
                        selectedUsers = selectedUsers.OrderBy(user => user.RegisterDate).AsQueryable();
                        break;
                }
            }
            else
            {
                switch (orderBy)
                {
                    case UserOrderBy.UserName:
                        selectedUsers = selectedUsers.OrderByDescending(user => user.UserName).AsQueryable();
                        break;
                    case UserOrderBy.OrderCount:
                        selectedUsers = selectedUsers.OrderByDescending(user => user.Orders.Count).AsQueryable();
                        break;
                    case UserOrderBy.RegisterDate:
                        selectedUsers = selectedUsers.OrderByDescending(user => user.RegisterDate).AsQueryable();
                        break;
                }
            }
            var totalQuery = selectedUsers.FutureCount();
            var selectQuery = selectedUsers.Skip((page - 1) * count).Take(count)
                .Select(a => new UserViewModel
                {
                    UserName = a.UserName,
                    FullName = a.FirstName + " " + a.LastName,
                    PhoneNumber = a.PhoneNumber,
                    RegisterType = a.RegisterType == UserRegisterType.Active ? "خرید کرده" : "خرید نکرده",
                    Baned = a.IsBaned,
                    CommentCount = a.Comments.Count,
                    Id = a.Id,
                    OrderCount = a.Orders.Count,
                    RoleDescritpion = a.Role.Description
                }).Future();
            total = totalQuery.Value;
            var users = selectQuery.ToList();
            return users;
        }


        public AddUserStatus Add(User user)
        {
            if (ExistsByPhoneNumber(user.PhoneNumber)) return AddUserStatus.PhoneNumberExist;
            if (ExistsByUserName(user.UserName)) return AddUserStatus.UserNameExist;
            _users.Add(user);
            return AddUserStatus.AddingUserSuccessfully;
        }

        public DetailsUserViewModel GetUserDetail(long id)
        {
            return
                _users.Where(x => x.Id == id)
                    .Include(x => x.Role)
                    .Select(
                        x =>
                            new DetailsUserViewModel
                            {
                                CommentsCount = x.Comments.Count,
                                FirstName = x.FirstName,
                                LastName = x.LastName,
                                IP = x.IP,
                                OrdersCount = x.Orders.Count,
                                PhoneNumber = x.PhoneNumber,
                                RegisterType =
                                    x.RegisterType == UserRegisterType.Active
                                        ? "وارد سایت شده است"
                                        : "وارد سایت نشده است",
                                RoleName = x.Role.Description,
                                UserName = x.UserName

                            })
                    .FirstOrDefault();
        }


        #endregion //Mange operation

        #region Authentication
        public bool ExistsByUserName(string userName)
        {
            return
                _users.Any(
                    user => user.UserName == userName);
        }

        public bool ExistsByPhoneNumber(string phoneNumber)
        {
            return
                _users.Any(
                    user => user.PhoneNumber == phoneNumber);
        }

        public bool ExistsByUserName(string userName, long id)
        {
            return
                _users.Any(
                    user => user.Id != id && user.UserName == userName);
        }

        public bool ExistsByPhoneNumber(string phoneNumber, long id)
        {
            return
                _users.Any(
                    user => user.Id != id && user.PhoneNumber == phoneNumber);
        }
        public string GeneratePassword()
        {
            var _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            int _passwordLength = 6;
            char[] chars = new char[_passwordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < _passwordLength; i++)
            {
                chars[i] = _allowedChars[(int)((allowedCharCount) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        public VerifyUserStatus VerifyUserByPhoneNumber(string phoneNumber, string password, ref string correctUserName, ref long userId, string ip)
        {
            User selectedUser = _users.SingleOrDefault(x => x.PhoneNumber == phoneNumber);
            var result = VerifyUserStatus.VerifiedFaild;
            if (selectedUser != null)
            {
                result = Verify(selectedUser, password);
                if (result == VerifyUserStatus.VerifiedSuccessfully)
                {
                    correctUserName = selectedUser.UserName;
                    userId = selectedUser.Id;
                    selectedUser.IP = ip;
                    selectedUser.RegisterType = UserRegisterType.Active;
                }
            }
            return result;
        }
        #endregion //Authentication


        public ChangePasswordResult ChangePasswordByUserName(string userName, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public ChangePasswordResult ChangePasswordByUserId(long Id, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public void DeActiveUser(long id)
        {
            throw new NotImplementedException();
        }

        public void DeActiveUsers(int[] usersId)
        {
            throw new NotImplementedException();
        }

        public void ActiveUser(long id)
        {
            throw new NotImplementedException();
        }

        public void ActiveUsers(int[] usersId)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(long id)
        {
            return _users.Find(id);
        }

        public User GetUserByUserName(string userName)
        {
            return _users.FirstOrDefault(a => a.UserName == userName);
        }

        public User GetUserByPhoneNumber(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public IList<User> GetUser(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }


        public bool IsUserActive(long id)
        {
            throw new NotImplementedException();
        }




        public int GetUsersNumber()
        {
            throw new NotImplementedException();
        }

        public IList<string> SearchUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public IList<string> SearchUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Role GetUserRole(long userId)
        {
            throw new NotImplementedException();
        }

        public EditUserViewModel GetUserDataForEdit(long userId)
        {
            return _users.Include(a => a.Role).Where(a => a.Id == userId)
                .Select(a => new EditUserViewModel
                {
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    PhoneNumber = a.PhoneNumber,
                    UserName = a.UserName,
                    Id = a.Id,
                    RoleId = a.Role.Id,
                    IsBaned = a.IsBaned
                }).FirstOrDefault();
        }




        public User Find(string userName)
        {
            return _users.FirstOrDefault(a => a.UserName == userName);
        }

        //public IList<UserIdAndUserName> SearchUser(string userName)
        //{
        //    throw new NotImplementedException();
        //}

        public IList<string> GetUsersPhoneNumbers()
        {
            throw new NotImplementedException();
        }

        public string GetRoleByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public string GetRoleByPhoneNumber(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public string GetUserNameByPhoneNumber(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public IList<string> SearchByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public IList<string> SearchByRoleDescription(string roleDescription)
        {
            throw new NotImplementedException();
        }

        public IList<string> SearchByFirstName(string firstName)
        {
            throw new NotImplementedException();
        }

        public IList<string> SearchByLastName(string lastName)
        {
            throw new NotImplementedException();
        }

        public IList<string> SearchByPhoneNumber(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public IList<string> SearchByIP(string ip)
        {
            throw new NotImplementedException();
        }

        public EditedUserStatus UpdateProfile(ProfileViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public bool Authenticate(string phoneNumber, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsBaned(string userName)
        {
            throw new NotImplementedException();
        }


        public UserStatus GetStatus(string userName)
        {
            return _users.AsNoTracking()
                .Where(user => user.UserName == userName)
                .Select(user => new UserStatus { IsBaned = user.IsBaned, Role = user.Role.Name })
                .Single();
        }

        public IList<UserIdAndUserName> SearchUser(string userName)
        {
            throw new NotImplementedException();
        }


        public ProfileViewModel GetUserDataForUpdateProfile(long userId)
        {
            throw new NotImplementedException();
        }


        private void RemoveUserComments(int userId)
        {

        }

        public void Remove(long id)
        {
            var user = _users.Include(a => a.Role).Where(a => a.Id == id && a.Role.Name != "admin").Delete();
        }



        public bool LimitAddToWishList(string userName)
        {
            return GetUserByUserName(userName).ProductsFavorite.Count > 50;
        }


        public IEnumerable<ProductSectionViewModel> GetUserWishList(out int total, int page, int count, string userName)
        {
            var user =
                _users.AsNoTracking().Include(a => a.ProductsFavorite).FirstOrDefault(a => a.UserName.Equals(userName));
            total = 0;
            if (user == null) return null;
            total = user.ProductsFavorite.Count;

            var products =
                user.ProductsFavorite.AsQueryable()
                    .OrderByDescending(a => a.Id)
                    .Skip((page - 1)*count)
                    .Take(count)
                    .Select(a => new ProductSectionViewModel
                    {
                        AvrageRate = a.Rate.AverageRating,
                        Id = a.Id,
                        IsFreeShipping = a.IsFreeShipping,
                        IsInStock = a.Stock - a.Reserve >= a.Ratio,
                        Name = a.Name,
                        Price = a.Price,
                        Ratio = a.Ratio,
                        PrincipleImagePath = a.PrincipleImagePath,
                        SellCount = a.SellCount,
                        TotalDiscount = a.DiscountPercent + a.Category.DiscountPercent,
                        ViewCount = a.ViewCount
                    });
            return products.ToList();
        }
    }
}
