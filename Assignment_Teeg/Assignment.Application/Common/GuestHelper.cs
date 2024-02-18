using Assignment.Application.Features.Guest.Commands;
using Assignment.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assignment.Application.Common
{
    public static class GuestHelper
    {
        public static ErrorModel ValidateGuest(this AddGuestCommand guest)
        {
            ErrorModel errorModel = new ErrorModel();

            if (!Regex.IsMatch(guest.FirstName, "^[a-zA-Z0-9_@(+).,-]+$") || !Regex.IsMatch(guest.LastName, "^[a-zA-Z0-9_@(+).,-]+$"))
            {
                errorModel.ErrorMessage = "Please enter a valid firstname or lastname.";
                return errorModel;
            }
            if (!IsValidDateFormat(guest, "MM/dd/yyyy"))
            {
                errorModel.ErrorMessage = "Please enter a valid birthdate.";
                return errorModel;
            }
            if (!Regex.IsMatch(guest.Email, "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$"))
            {
                errorModel.ErrorMessage = "Please enter a valid email.";
                return errorModel;
            }
            if (!IsValidPhoneNumber(guest.PhoneNumbers))
            {
                errorModel.ErrorMessage = "Please enter a valid phone number.";
                return errorModel;
            }

            return errorModel;
        }

        private static bool IsValidDateFormat(AddGuestCommand guest, string dateFormat)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(guest.BirthDate);
                string s = dt.ToString(dateFormat, CultureInfo.InvariantCulture);
                DateTime.Parse(s, CultureInfo.InvariantCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPhoneNumber(this List<string> phoneNumbers)
        {
            ErrorModel errorModel = new ErrorModel();
            foreach (var phone in phoneNumbers)
            {
                if (!Regex.IsMatch(phone, "^(?:(?:\\+?1\\s*(?:[.-]\\s*)?)?(?:\\(\\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\\s*\\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\\s*(?:[.-]\\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\\s*(?:[.-]\\s*)?([0-9]{4})(?:\\s*(?:#|x\\.?|ext\\.?|extension)\\s*(\\d+))?$"))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
