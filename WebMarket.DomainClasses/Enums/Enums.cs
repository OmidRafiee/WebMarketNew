using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.DomainClasses.Enums
{
    #region Factors
    public enum Payments
    {
        [Display(Name = "پرداخت شده")]
        Paid,
        [Display(Name = "پرداخت نشده")]
        UnPaid
        
    }
    #endregion
    #region Users

    public enum Roles
    {
        [Display(Name = "تغییر نقش")]
        NoRole,
        [Display(Name = "مدیر سایت")]
        Admin,
        [Display(Name = "مشتری")]
        Customer
    }
    public enum UserOrderBy
    {

        [Display(Name = "نام کاربری")]
        UserName,
        [Display(Name = "نقش")]
        RoleDescription,
        [Display(Name = "شماره همراه")]
        PhoneNumber
    }

    public enum UserSearchBy
    {
        [Display(Name = "نام کاربری")]
        UserName,
        [Display(Name = "شماره همراه")]
        PhoneNumber,
        [Display(Name = "نقش")]
        RoleDescription
    }
    public enum VerifyUserStatus
    {
        VerifiedSuccessfully,
        UserIsBaned,
        VerifiedFaild
    }

    public enum AddUserStatus
    {
        UserNameExist,
        PhoneNumberExist,
        AddingUserSuccessfully
    }
    public enum EditedUserStatus
    {
        UserNameExist,
        PhoneNumberExist,
        UpdatingUserSuccessfully
    }

    public enum ChangePasswordResult
    {
        ChangedSuccessfully,
        ChangedFaild
    }

    #endregion //Users

    #region public
    public enum Order
    {
        [Display(Name = "صعودی")]
        Asscending,
        [Display(Name = "نزولی")]
        Descending
    }

    public enum PageCount
    {
        [Display(Name = "10")]
        Count10 = 10,
        [Display(Name = "30")]
        Count30 = 30,
        [Display(Name = "50")]
        Count50 = 50
    }

    #endregion //Public

    public enum AddGroupStatus
    {
        AddSuccessfully,
        GroupNameExist
    }

    public enum EditGroupStatus
    {
        EditSuccessfully,
        GroupNameExist
    }

}
