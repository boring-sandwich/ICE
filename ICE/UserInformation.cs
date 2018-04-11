using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.UserData;
using ICE.Resources;
using System.Windows.Media.Imaging;
using Windows.Phone.ApplicationModel;

namespace ICE
{
    class UserInformation
    {
        ICESettings settings = new ICESettings();
        public void ExportAppUserInformation()
        {
            string exportInformation = "Personal Information" + Environment.NewLine
                                     + "Name:" + " " + settings.PhoneOwnersName + Environment.NewLine
                                     + "Emergency Contact One: " + settings.EmergencyContactNameOne + " " + settings.EmergencyContactNumberOne + Environment.NewLine
                                     + "Emergency Contact Two: " + settings.EmergencyContactNameTwo + " " + settings.EmergencyContactNumberTwo + Environment.NewLine
                                     + Environment.NewLine
                                     + "Doctor: " + settings.MedDoctorsInfoName + " " + settings.MedDoctorsInfoPhone + Environment.NewLine
                                     + "Dentist: " + settings.MedDentistInfo + " " + settings.MedDentistInfoPhone + Environment.NewLine
                                     + Environment.NewLine
                                     + "Insurance Information" + Environment.NewLine
                                     + "Carrier: " + settings.MedCarrierInfo + " " + settings.MedCarrierInfoPhone + Environment.NewLine
                                     + "ID: " + settings.MedIDInfo + Environment.NewLine
                                     + "Network: " + settings.MedNetworkInfo + Environment.NewLine
                                     + "Group: " + settings.MedGroupInfo + Environment.NewLine
                                     + "Additional Information: " + Environment.NewLine + settings.MedAdditionalInfo + Environment.NewLine
                                     + Environment.NewLine
                                     + "General Information and Conditions" + Environment.NewLine
                                     + "Allergies: " + Environment.NewLine + settings.Allergies + Environment.NewLine
                                     + "Medical Information: " + Environment.NewLine + settings.PhoneOwnerMedicalInfo + Environment.NewLine
                                     + "Medicines: " + Environment.NewLine + settings.PhoneOwnerMedicalInfo + Environment.NewLine
                                     + "Additional Notes: " + Environment.NewLine + settings.AdditionalNotes;

            ShareStatusTask shareStatusTask = new ShareStatusTask();
            shareStatusTask.Status = exportInformation;

            shareStatusTask.Show();
        }
    }
}
