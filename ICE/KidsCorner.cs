using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Phone.ApplicationModel;

namespace ICE
{
    class KidsCorner
    {
        public bool IsRunningInKidsCorner
        {
            get
            {
                if (ApplicationProfile.Modes == ApplicationProfileModes.Alternate)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
