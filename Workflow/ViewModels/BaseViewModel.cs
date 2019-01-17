using System;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace CWG.API.Workflow.ViewModels
{
    public class  NotPatchableAttribute : Attribute {}
    public class BaseViewModel
    {
        public void Patch(object u)
        {     
            var props = from p in this.GetType().GetProperties()
                        let attr  = p.GetCustomAttribute(typeof(NotPatchableAttribute))
                        where attr == null
                        select p;

            foreach(var prop in props)
            {
                var val = prop.GetValue(this, null);
                if(val != null)
                    prop.SetValue(u,val);
            }
        }
    }
}