using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebMarket.DomainClasses.Enums;

namespace WebMarket.Utilities
{
    public class DropDown
    {
        public static SelectList GetUserSearchByList(UserSearchBy userSearchBy)
        {
            var selectedUserSearchBy = new List<SelectListItem>
            {
                new SelectListItem {Text = "نام کاربری", Value = "UserName"},
                new SelectListItem {Text = "شماره همراه", Value = "PhoneNumber"},
                new SelectListItem {Text = "نقش", Value = "RoleDescription"}
            };

            return new SelectList(selectedUserSearchBy, "Value", "Text", userSearchBy);
        }

        public static SelectList GetUserOrderByList(UserOrderBy usersOrderBy)
        {
            var selectedOrder = new List<SelectListItem>
            {
                new SelectListItem {Text = "نام کاربری", Value = "UserName"},
                new SelectListItem {Text = "شماره همراه", Value = "PhoneNumber"},
                new SelectListItem {Text = "نقش", Value = "RoleDescription"}
            };
            return new SelectList(selectedOrder, "Value", "Text", usersOrderBy);
        }
        public static SelectList GetCountList(int selected)
        {
            var selectedCount = new List<SelectListItem>
            {
                new SelectListItem {Text = "10", Value = "10"},
                new SelectListItem {Text = "30", Value = "30"},
                new SelectListItem {Text = "50", Value = "50"}
            };
            return new SelectList(selectedCount, "Value", "Text", selected);
        }
        public static SelectList GetOrderList(Order order)
        {
            var selectedUserOrderBy = new List<SelectListItem>
            {
                new SelectListItem {Text = "نزولی", Value = "Descendign"},
                new SelectListItem {Text = "صعودی", Value = "Asscending"}
            };
            return new SelectList(selectedUserOrderBy, "Value", "Text", order);
        }

        public static SelectList GetPaymentList(Payments payment)
        {
            var selectedPayment = new List<SelectListItem>
            {
                new SelectListItem {Text = "پرداخت شده", Value = "Paid"},
                new SelectListItem {Text = "پرداخت نشده", Value = "UnPaid"}
            };
            return new SelectList(selectedPayment, "Value", "Text", payment);
        }

        public static SelectList GetProductStatus(ProductStatus productStatus)
        {
            var selectedStatus = new List<SelectListItem>
            {
                new SelectListItem {Text = "موجود", Value = "Available"},
                new SelectListItem {Text = "اتمام موجود", Value = "UnAvailable"},
                new SelectListItem {Text = "به زودی", Value = "Soon"}
            };
            return new SelectList(selectedStatus, "Value", "Text", productStatus);
        }

        
       
    }
}
