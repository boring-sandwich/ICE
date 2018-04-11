using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;
using System.Diagnostics;
using Microsoft.Phone.Shell;

namespace ICE
{
    class ICESettings
    {
        // Our settings
        IsolatedStorageSettings settings;

        // The key names of our settings
        const string PhoneOwnersNameKeyName = "PhoneOwnersName";
        const string EmergencyContactNameOneKeyName = "EmergencyContactNameOne";
        const string EmergencyContactNumberOneKeyName = "EmergencyContactNumberOne";
        const string EmergencyContactNameTwoKeyName = "EmergencyContactNameTwo";
        const string EmergencyContactNumberTwoKeyName = "EmergencyContactNumberTwo";
        const string PhoneOwnerAllergiesKeyName = "Allergies";
        const string PhoneOwnerMedicalInfoKeyName = "PhoneOwnerMedicalInfo";
        const string PhoneOwnersMedicinesKeyname = "Medicines";
        const string PhoneOwnersAdditionalNotesKeyName = "AdditionalNotes";
        const string RadioButton1SettingKeyName = "RadioButton1ONeg";
        const string RadioButton2SettingKeyName = "RadioButton2OPlus";
        const string RadioButton3SettingKeyName = "RadioButton3ANeg";
        const string RadioButton4SettingKeyName = "RadioButton4APlus";
        const string RadioButton5SettingKeyName = "RadioButton5BNeg";
        const string RadioButton6SettingKeyName = "RadioButton6BPlus";
        const string RadioButton7SettingKeyName = "RadioButton7ABNeg";
        const string RadioButton8SettingKeyName = "RadioButton8ABPlus";
        const string RadioButton9SettingKeyName = "RadioButtonDontKnow";
        const string RadioButtonOption1KeyName = "RadioButtonOption1";
        const string RadioButtonOption2KeyName = "RadioButtonOption2";
        const string MedDoctorsInfoNameKeyName = "MedDoctorsInfoName";
        const string MedDoctorsInfoPhoneKeyName = "MedDoctorsInfoPhone";
        const string MedDentistInfoNameKeyName = "MedDentistInfo";
        const string MedDentistInfoPhoneKeyName = "MedDentistInfoPhone";
        const string MedCarrierInfoKeyName = "MedCarrierInfo";
        const string MedCarrierInfoPhoneKeyName = "MedCarrierInfoPhone";
        const string MedIDInfoKeyName = "MedIDInfo";
        const string MedNetworkInfoKeyName = "MedNetworkInfo";
        const string MedGroupInfoKeyName = "MedGroupInfo";
        const string MedAdditionalInfoKeyName = "MedAdditionalInfo";
        const string SettingsDoctorKeyName = "SettingsDoctorInfo";
        const string SettingsAllergyKeyName = "SettingsAllergy";
        const string SettingsMedicalKeyName = "SettingsMedical";
        const string SettingsMedicineKeyName = "SettingsMedicine";
        const string SettingsBloodGroupKeyName = "SettingsBloodGroup";
        const string SettingsAdditionalNotesKeyName = "SettingsAdditionalNotes";

        // The default value of our settings
        const string PhoneOwnerNameDefault = "type in your name";
        const string EmergencyContactNameOneDefault = "tap edit then choose emergency contact";
        const string EmergencyContactNumberOneDefault = "";
        const string EmergencyContactNameTwoDefault = "tap edit then choose emergency contact";
        const string EmergencyContactNumberTwoDefault = "";
        const string PhoneOwnerAllergiesDefault = "type in any allergies you have";
        const string PhoneOwnerMedicalInfoDefault = "type in any medical conditions that you have";
        const string PhoneOwnerMedicinesDefault = "type in any medicines you take including the frequency and dosage";
        const string PhoneOwnerAdditionalNotesDefault = "type in any additional information e.g. operations, pregnancy, family history etc";
        const bool RadioButton1SettingDefault = false;
        const bool RadioButton2SettingDefault = false;
        const bool RadioButton3SettingDefault = false;
        const bool RadioButton4SettingDefault = false;
        const bool RadioButton5SettingDefault = false;
        const bool RadioButton6SettingDefault = false;
        const bool RadioButton7SettingDefault = false;
        const bool RadioButton8SettingDefault = false;
        const bool RadioButton9SettingDefault = true;
        const bool RadioButtonOption1Default = true;
        const bool RadioButtonOption2Default = false;
        const string MedDoctorsInfoNameDefault = "tap edit then choose doctor";
        const string MedDoctorsInfoPhoneDefault = "";
        const string MedDentistInfoNameDefault = "tap edit then choose dentist";
        const string MedDentistInfoPhoneDefault = "";
        const string MedCarrierInfoDefault = "tap edit then choose carrier";
        const string MedCarrierInfoPhoneDefault = "";
        const string MedIDInfoDefault = "type in ID e.g.123456";
        const string MedNetworkInfoDefault = "e.g. provider name";
        const string MedGroupInfoDefault = "e.g. pink level";
        const string MedAdditionalInfoDefault = "e.g. european health number or NHS ID etc.";
        const bool CheckBoxSettingsDoctor = false;
        const bool CheckBoxSettingsAllergy = false;
        const bool CheckBoxSettingsMedical = false;
        const bool CheckBoxSettingsMedicine = false;
        const bool CheckBoxSettingsBloodGroup = false;
        const bool CheckBoxSettingsAdditionalNotes = false;

        /// <summary>
        /// Constructor that gets the application settings.
        /// </summary>
        public ICESettings()
        {
            // Get the settings for this application.
            settings = IsolatedStorageSettings.ApplicationSettings;
        }
        public void UpdateLockScreen()
        {
            string strMessage;
            string strName;
            string strNumber;
            if (RadioButtonOption2)
            {
                strMessage = "In case of emergency";
                strName = "ICE call: " + EmergencyContactNameTwo;
                strNumber = EmergencyContactNumberTwo;
            }
            else
            {
                strMessage = "In case of emergency";
                strName = "ICE call: " + EmergencyContactNameOne;
                strNumber = EmergencyContactNumberOne;
            }

            UpdatePrimaryTile(strMessage, strName, strNumber);
        }
        private static void UpdatePrimaryTile(string content, string name, string number)
        {
            IconicTileData primaryTileData = new IconicTileData();
            primaryTileData.Title = content;
            primaryTileData.WideContent1 = name;
            primaryTileData.WideContent2 = number;

            ShellTile primaryTile = ShellTile.ActiveTiles.First();
            primaryTile.Update(primaryTileData);
        }


        /// <summary>
        /// Update a setting value for our application. If the setting does not
        /// exist, then add the setting.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddOrUpdateValue(string Key, Object value)
        {
            bool valueChanged = false;

            // If the key exists
            if (settings.Contains(Key))
            {
                // If the value has changed
                if (settings[Key] != value)
                {
                    // Store the new value
                    settings[Key] = value;
                    valueChanged = true;
                }
            }
            // Otherwise create the key.
            else
            {
                settings.Add(Key, value);
                valueChanged = true;
            }
            return valueChanged;
        }

        /// <summary>
        /// Get the current value of the setting, or if it is not found, set the 
        /// setting to the default setting.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetValueOrDefault<T>(string Key, T defaultValue)
        {
            T value;

            // If the key exists, retrieve the value.
            if (settings.Contains(Key))
            {
                value = (T)settings[Key];
            }
            // Otherwise, use the default value.
            else
            {
                value = defaultValue;
            }
            return value;
        }

        /// <summary>
        /// Save the settings.
        /// </summary>
        public void Save()
        {
            settings.Save();
        }

        /// <summary>
        /// Property to get and set a emergency contact name #1.
        /// </summary>
        public string EmergencyContactNameOne
        {
            get
            {
                return GetValueOrDefault<string>(EmergencyContactNameOneKeyName, EmergencyContactNameOneDefault);
            }
            set
            {
                if (AddOrUpdateValue(EmergencyContactNameOneKeyName, value))
                {
                    Save();
                }
            }
        }

        /// <summary>
        /// Property to get and set a emergency contact number #1.
        /// </summary>
        public string EmergencyContactNumberOne
        {
            get
            {
                return GetValueOrDefault<string>(EmergencyContactNumberOneKeyName, EmergencyContactNumberOneDefault);
            }
            set
            {
                if (AddOrUpdateValue(EmergencyContactNumberOneKeyName, value))
                {
                    Save();
                }
            }
        }
        /// <summary>
        /// Property to get and set a emergency contact name #2.
        /// </summary>
        public string EmergencyContactNameTwo
        {
            get
            {
                return GetValueOrDefault<string>(EmergencyContactNameTwoKeyName, EmergencyContactNameTwoDefault);
            }
            set
            {
                if (AddOrUpdateValue(EmergencyContactNameTwoKeyName, value))
                {
                    Save();
                }
            }
        }

        /// <summary>
        /// Property to get and set a emergency contact number #2.
        /// </summary>
        public string EmergencyContactNumberTwo
        {
            get
            {
                return GetValueOrDefault<string>(EmergencyContactNumberTwoKeyName, EmergencyContactNumberTwoDefault);
            }
            set
            {
                if (AddOrUpdateValue(EmergencyContactNumberTwoKeyName, value))
                {
                    Save();
                }
            }
        }

        /// <summary>
        /// Property to get and set the Phone Owners Name
        /// </summary>

        public string PhoneOwnersName
        {
            get
            {
                return GetValueOrDefault<string>(PhoneOwnersNameKeyName, PhoneOwnerNameDefault);
            }
            set
            {
                if (AddOrUpdateValue(PhoneOwnersNameKeyName, value))
                {
                    Save();
                }
            }
        }

        /// <summary>
        /// Property to get and set the phone owners medical info
        /// </summary>
        /// 

        ///Property to get and set Allergies info
        public string Allergies
        {
            get
            {
                return GetValueOrDefault<string>(PhoneOwnerAllergiesKeyName, PhoneOwnerAllergiesDefault);
            }
            set
            {
                if (AddOrUpdateValue(PhoneOwnerAllergiesKeyName, value))
                {
                    Save();
                }
            }
        }


        public string PhoneOwnerMedicalInfo
        {
            get
            {
                return GetValueOrDefault<string>(PhoneOwnerMedicalInfoKeyName, PhoneOwnerMedicalInfoDefault);
            }
            set
            {
                if (AddOrUpdateValue(PhoneOwnerMedicalInfoKeyName, value))
                {
                    Save();
                }
            }
        }
        /// <summary>
        /// Property to get and set medicines info
        /// </summary>
        public string Medicines
        {
            get
            {
                return GetValueOrDefault<string>(PhoneOwnersMedicinesKeyname, PhoneOwnerMedicinesDefault);
            }
            set
            {
                if (AddOrUpdateValue(PhoneOwnersMedicinesKeyname, value))
                {
                    Save();
                }
            }
        }

        ///<summary>
        ///Property to get and set Additional Notes
        /// </summary>
        public string AdditionalNotes
        {
            get
            {
                return GetValueOrDefault<string>(PhoneOwnersAdditionalNotesKeyName, PhoneOwnerAdditionalNotesDefault);
            }
            set
            {
                if (AddOrUpdateValue(PhoneOwnersAdditionalNotesKeyName, value))
                {
                    Save();
                }
            }
        }

        /// <summary>
        /// Property to get and set a RadioButton Setting Key.
        /// </summary>
        public bool RadioButton1ONeg
        {
            get
            {
                return GetValueOrDefault<bool>(RadioButton1SettingKeyName, RadioButton1SettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(RadioButton1SettingKeyName, value))
                {
                    Save();
                }
            }
        }


        /// <summary>
        /// Property to get and set a RadioButton Setting Key.
        /// </summary>
        public bool RadioButton2OPlus
        {
            get
            {
                return GetValueOrDefault<bool>(RadioButton2SettingKeyName, RadioButton2SettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(RadioButton2SettingKeyName, value))
                {
                    Save();
                }
            }
        }

        /// <summary>
        /// Property to get and set a RadioButton Setting Key.
        /// </summary>
        public bool RadioButton3ANeg
        {
            get
            {
                return GetValueOrDefault<bool>(RadioButton3SettingKeyName, RadioButton3SettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(RadioButton3SettingKeyName, value))
                {
                    Save();
                }
            }
        }
        /// <summary>
        /// Property to get and set a RadioButton Setting Key.
        /// </summary>
        public bool RadioButton4APlus
        {
            get
            {
                return GetValueOrDefault<bool>(RadioButton4SettingKeyName, RadioButton4SettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(RadioButton4SettingKeyName, value))
                {
                    Save();
                }
            }
        }
        /// <summary>
        /// Property to get and set a RadioButton Setting Key.
        /// </summary>
        public bool RadioButton5BNeg
        {
            get
            {
                return GetValueOrDefault<bool>(RadioButton5SettingKeyName, RadioButton5SettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(RadioButton5SettingKeyName, value))
                {
                    Save();
                }
            }
        }
        /// <summary>
        /// Property to get and set a RadioButton Setting Key.
        /// </summary>
        public bool RadioButton6BPlus
        {
            get
            {
                return GetValueOrDefault<bool>(RadioButton6SettingKeyName, RadioButton6SettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(RadioButton6SettingKeyName, value))
                {
                    Save();
                }
            }
        }
        /// <summary>
        /// Property to get and set a RadioButton Setting Key.
        /// </summary>
        public bool RadioButton7ABNeg
        {
            get
            {
                return GetValueOrDefault<bool>(RadioButton7SettingKeyName, RadioButton7SettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(RadioButton7SettingKeyName, value))
                {
                    Save();
                }
            }
        }
        /// <summary>
        /// Property to get and set a RadioButton Setting Key.
        /// </summary>
        public bool RadioButton8ABPlus
        {
            get
            {
                return GetValueOrDefault<bool>(RadioButton8SettingKeyName, RadioButton8SettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(RadioButton8SettingKeyName, value))
                {
                    Save();
                }
            }
        }
        /// <summary>
        /// Property to get and set a RadioButton Setting Key.
        /// </summary>
        public bool RadioButtonDontKnow
        {
            get
            {
                return GetValueOrDefault<bool>(RadioButton9SettingKeyName, RadioButton9SettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(RadioButton9SettingKeyName, value))
                {
                    Save();
                }
            }
        }
        public bool RadioButtonOption1
        {
            get
            {
                return GetValueOrDefault<bool>(RadioButtonOption1KeyName, RadioButtonOption1Default);
            }
            set
            {
                if (AddOrUpdateValue(RadioButtonOption1KeyName, value))
                {
                    Save();
                }
            }
        }
        public bool RadioButtonOption2
        {
            get
            {
                return GetValueOrDefault<bool>(RadioButtonOption2KeyName, RadioButtonOption2Default);
            }
            set
            {
                if (AddOrUpdateValue(RadioButtonOption2KeyName, value))
                {
                    Save();
                }
            }
        }
        public string MedDoctorsInfoName
        {
            get
            {
                return GetValueOrDefault<string>(MedDoctorsInfoNameKeyName, MedDoctorsInfoNameDefault);
            }
            set
            {
                if (AddOrUpdateValue(MedDoctorsInfoNameKeyName, value))
                {
                    Save();
                }
            }
        }
        public string MedDoctorsInfoPhone
        {
            get
            {
                return GetValueOrDefault<string>(MedDoctorsInfoPhoneKeyName, MedDoctorsInfoPhoneDefault);
            }
            set
            {
                if (AddOrUpdateValue(MedDoctorsInfoPhoneKeyName, value))
                {
                    Save();
                }
            }
        }
        public string MedDentistInfo
        {
            get
            {
                return GetValueOrDefault<string>(MedDentistInfoNameKeyName, MedDentistInfoNameDefault);
            }
            set
            {
                if (AddOrUpdateValue(MedDentistInfoNameKeyName, value))
                {
                    Save();
                }
            }
        }
        public string MedDentistInfoPhone
        {
            get
            {
                return GetValueOrDefault<string>(MedDentistInfoPhoneKeyName, MedDentistInfoPhoneDefault);
            }
            set
            {
                if (AddOrUpdateValue(MedDentistInfoPhoneKeyName, value))
                {
                    Save();
                }
            }
        }
        public string MedCarrierInfo
        {
            get
            {
                return GetValueOrDefault<string>(MedCarrierInfoKeyName, MedCarrierInfoDefault);
            }
            set
            {
                if (AddOrUpdateValue(MedCarrierInfoKeyName, value))
                {
                    Save();
                }
            }
        }
        public string MedCarrierInfoPhone
        {
            get
            {
                return GetValueOrDefault<string>(MedCarrierInfoPhoneKeyName, MedCarrierInfoPhoneDefault);
            }
            set
            {
                if (AddOrUpdateValue(MedCarrierInfoPhoneKeyName, value))
                {
                    Save();
                }
            }
        }
        public string MedIDInfo
        {
            get
            {
                return GetValueOrDefault<string>(MedIDInfoKeyName, MedIDInfoDefault);
            }
            set
            {
                if (AddOrUpdateValue(MedIDInfoKeyName, value))
                {
                    Save();
                }
            }
        }
        public string MedNetworkInfo
        {
            get
            {
                return GetValueOrDefault<string>(MedNetworkInfoKeyName, MedNetworkInfoDefault);
            }
            set
            {
                if (AddOrUpdateValue(MedNetworkInfoKeyName, value))
                {
                    Save();
                }
            }
        }
        public string MedGroupInfo
        {
            get
            {
                return GetValueOrDefault<string>(MedGroupInfoKeyName, MedGroupInfoDefault);
            }
            set
            {
                if (AddOrUpdateValue(MedGroupInfoKeyName, value))
                {
                    Save();
                }
            }
        }
        public string MedAdditionalInfo
        {
            get
            {
                return GetValueOrDefault<string>(MedAdditionalInfoKeyName, MedAdditionalInfoDefault);
            }
            set
            {
                if (AddOrUpdateValue(MedAdditionalInfoKeyName, value))
                {
                    Save();
                }
            }
        }
        public bool SettingsDoctorInfo
        {
            get
            {
                return GetValueOrDefault<bool>(SettingsDoctorKeyName, CheckBoxSettingsDoctor);
            }
            set
            {
                if (AddOrUpdateValue(SettingsDoctorKeyName, value))
                {
                    Save();
                }
            }
        }
        public bool SettingsAllergy
        {
            get
            {
                return GetValueOrDefault<bool>(SettingsAllergyKeyName, CheckBoxSettingsAllergy);
            }
            set
            {
                if (AddOrUpdateValue(SettingsAllergyKeyName, value))
                {
                    Save();
                }
            }
        }
        public bool SettingsMedical
        {
            get
            {
                return GetValueOrDefault<bool>(SettingsMedicalKeyName, CheckBoxSettingsMedical);
            }
            set
            {
                if (AddOrUpdateValue(SettingsMedicalKeyName, value))
                {
                    Save();
                }
            }
        }
        public bool SettingsMedicine
        {
            get
            {
                return GetValueOrDefault<bool>(SettingsMedicineKeyName, CheckBoxSettingsMedicine);
            }
            set
            {
                if (AddOrUpdateValue(SettingsMedicineKeyName, value))
                {
                    Save();
                }
            }
        }
        public bool SettingsBloodGroup
        {
            get
            {
                return GetValueOrDefault<bool>(SettingsBloodGroupKeyName, CheckBoxSettingsBloodGroup);
            }
            set
            {
                if (AddOrUpdateValue(SettingsBloodGroupKeyName, value))
                {
                    Save();
                }
            }
        }
        public bool SettingsAdditionalNotes
        {
            get
            {
                return GetValueOrDefault<bool>(SettingsAdditionalNotesKeyName, CheckBoxSettingsAdditionalNotes);
            }
            set
            {
                if (AddOrUpdateValue(SettingsAdditionalNotesKeyName, value))
                {
                    Save();
                }
            }
        }
    }
}