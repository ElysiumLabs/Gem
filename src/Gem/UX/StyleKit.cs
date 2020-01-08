using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Gem.UX
{
    public class StyleKit : StyleKitBase
    {
        public void Apply(Application application)
        {
            var r = application?.Resources;

            if (r == null)
            {
                return;
            }

            foreach (var item in this)
            {
                r[item.Key] = item.Value;
            }
        }
    }


    public class StyleKitBase : Dictionary<string, object>
    {
        public T GetIfExistsValue<T>([CallerMemberName]string key = null)
        {
            try
            {
                {
                    return (T)this[key];
                }
            }
            catch (System.Exception)
            {
                return default(T);
            }
        }

        public void SetValue<T>(T value, [CallerMemberName]string key = null)
        {
            this[key] = value;
        }

    }
}
