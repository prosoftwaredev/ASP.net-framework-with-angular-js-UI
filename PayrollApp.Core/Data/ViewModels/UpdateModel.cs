using PayrollApp.Core.Data.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Core.Data.ViewModels
{
    public class UpdateModel<T1, T2, T3>
    {
        public T1 AssignNewValue(T1 t1, T2 t2, T3 t3)
        {
            object obj = t3;
            List<string> propList = obj as List<string>;

            if(t1.GetType() == t2.GetType())
            {
                foreach (var prop in propList)
                {
                    switch (prop)
                    {
                        case "LastUpdated":
                            t1.GetType().GetProperty(prop).SetValue(t1, DateTime.Now);
                            break;

                        case "LastUpdatedBy":
                            t1.GetType().GetProperty(prop).SetValue(t1, RoleHelper.GetCurrentUserID);
                            break;

                        default:
                                var newValue = t2.GetType().GetProperty(prop).GetValue(t2, null);
                                t1.GetType().GetProperty(prop).SetValue(t1, newValue);
                            break;
                    }
                }
            }
            return t1;            
        }
    }
}
