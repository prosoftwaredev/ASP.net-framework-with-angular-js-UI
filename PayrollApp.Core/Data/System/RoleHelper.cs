using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Core.Data.System
{
    public class RoleHelper
    {
        public static long GetCurrentUserID { get; set; }

        public enum RoleName
        {
            Admin, User, Customer, Owner, GeneraralManager, AccountManager, Accountant, Dispatcher, ATMAddendant, Employer, Clerk
        }

        public static int GetRoleID(Enum RoleName)
        {
            int RoleID = 0;

            switch (Convert.ToInt32(RoleName))
            {
                case 0:
                    RoleID = 1;  //Admin
                    break;

                case 1:
                    RoleID = 2;  //User
                    break;

                case 2:
                    RoleID = 3;  //Customer
                    break;

                case 3:
                    RoleID = 4; //Owner
                    break;

                case 4:
                    RoleID = 5; //GeneraralManager
                    break;

                case 5:
                    RoleID = 6; //AccountManager
                    break;

                case 6:
                    RoleID = 7; //Accountant
                    break;

                case 7:
                    RoleID = 8; //Dispatcher
                    break;

                case 8:
                    RoleID = 9; //ATMAddendant
                    break;

                case 9:
                    RoleID = 10; //Employer
                    break;

                case 10:
                    RoleID = 11; //Clerk
                    break;

                default:
                    break;
            }
            return RoleID;
        }
    }
}
