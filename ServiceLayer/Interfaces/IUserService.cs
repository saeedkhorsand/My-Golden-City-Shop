using DomainClasses.Entities;
using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.Admin;
using ViewModel.Admin.Product;

namespace ServiceLayer.Interfaces
{
    public class UserIdAndUserName
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }

    public class UserStatus
    {
        public bool IsBaned { get; set; }
        public string Role { get; set; }
    }
    public interface IUserService
    {
        string GeneratePassword();
        AddUserStatus Add(User user);
        void Remove(long id);
        VerifyUserStatus VerifyUserByPhoneNumber(string phoneNumber, string password, ref string userName,
            ref long userId, string ip);
        ChangePasswordResult ChangePasswordByUserName(string userName, string oldPassword, string newPassword);
        ChangePasswordResult ChangePasswordByUserId(long Id, string oldPassword, string newPassword);
        void DeActiveUser(long id);
        void ActiveUser(long id);
        User GetUserById(long id);
        User GetUserByUserName(string userName);
        User GetUserByPhoneNumber(string phoneNumber);
        IList<User> GetAllUsers();
        IList<User> GetUser(Expression<Func<User, bool>> expression);
        bool ExistsByUserName(string userName);
        bool ExistsByPhoneNumber(string phoneNumber);
        bool ExistsByUserName(string userName, long id);
        bool ExistsByPhoneNumber(string phoneNumber, long id);
        bool IsUserActive(long id);

        DetailsUserViewModel GetUserDetail(long id);
        int GetUsersNumber();
        IList<string> SearchUserName(string userName);
        IList<string> SearchUserId(string userId);
        Role GetUserRole(long userId);
        EditUserViewModel GetUserDataForEdit(long userId);
        ProfileViewModel GetUserDataForUpdateProfile(long userId);
        EditedUserStatus EditUser(User user);

        IList<UserViewModel> GetDataTable(out int total, string term, int page, int count,
            DomainClasses.Enums.Order order, UserOrderBy orderBy, UserSearchBy searchBy);

        User Find(string userName);
        // IList<UserIdAndUserName> SearchUser(string userName);
        IList<string> GetUsersPhoneNumbers();

        /// <summary>
        ///     Get role name by username
        /// </summary>
        /// <param name="userName">Username of selected user</param>
        /// <returns></returns>
        string GetRoleByUserName(string userName);

        string GetRoleByPhoneNumber(string phoneNumber);
        string GetUserNameByPhoneNumber(string phoneNumber);

        IList<string> SearchByUserName(string userName);
        IList<string> SearchByRoleDescription(string roleDescription);
        IList<string> SearchByFirstName(string firstName);
        IList<string> SearchByLastName(string lastName);
        IList<string> SearchByPhoneNumber(string phoneNumber);
        IList<string> SearchByIP(string ip);

        IEnumerable<ProductSectionViewModel> GetUserWishList(out int total, int page, int count, string userName);
        EditedUserStatus UpdateProfile(ProfileViewModel viewModel);
        bool Authenticate(string phoneNumber, string password);
        bool IsBaned(string userName);
        UserStatus GetStatus(string userName);
        IList<UserIdAndUserName> SearchUser(string userName);
        bool LimitAddToWishList(string userName);
    }
}
