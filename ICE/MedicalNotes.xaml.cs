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

namespace ICE
{
    public partial class MedicalNotes : PhoneApplicationPage
    {
        private bool IsInitialized = false;
        ICESettings settings = new ICESettings();
        KidsCorner kc = new KidsCorner();
        ApplicationBarIconButton appBarButtonEdit;
        ApplicationBarIconButton appBarButtonSave;
        ApplicationBarIconButton appBarButtonCancel;
        public string doctorsName;
        public string doctorsNumber;
        public string dentistName;
        public string dentistNumber;
        public string carrierName;
        public string carrierNumber;
        public bool IsReadOnlyOnMed = false;
        PhoneNumberChooserTask phoneNumberChooserTask;
        public bool IsPhonePickerOnMed = false;
        public bool IsBoxOneTappedMed = false;
        public bool IsBoxTwoTappedMed = false;
        public MedicalNotes()
        {
            InitializeComponent();
            Reset();
            phoneNumberChooserTask = new PhoneNumberChooserTask();
            phoneNumberChooserTask.Completed += new EventHandler<PhoneNumberResult>(phoneNumberChooserTask_Completed);
            IsInitialized = true;
            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = false;

            }
        }

        void Contacts_SearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            //Do something with the results.
            MessageBox.Show(e.Results.Count().ToString());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string thePageNum;
            NavigationContext.QueryString.TryGetValue("goto", out thePageNum);
            myPivot.SelectedIndex = Convert.ToInt32(thePageNum);

            base.OnNavigatedTo(e);
            if (!IsPhonePickerOnMed)
            {
                Reset();
            }

            if (kc.IsRunningInKidsCorner)
            {
                if (!settings.SettingsDoctorInfo)
                {
                    txbDoctorsInfo.Visibility = System.Windows.Visibility.Collapsed;
                    txbDentistInfo.Visibility = System.Windows.Visibility.Collapsed;
                    txbCarrierInfo.Visibility = System.Windows.Visibility.Collapsed;
                    //txtIDInfo.Visibility = System.Windows.Visibility.Collapsed;
                    txbIDInfo.Visibility = System.Windows.Visibility.Collapsed;
                    //txtNetworkInfo.Visibility = System.Windows.Visibility.Collapsed;
                    txbNetworkInfo.Visibility = System.Windows.Visibility.Collapsed;
                    //txtGroupInfo.Visibility = System.Windows.Visibility.Collapsed;
                    txbGroupInfo.Visibility = System.Windows.Visibility.Collapsed;
                    //txtAdditionalInfo.Visibility = System.Windows.Visibility.Collapsed;
                    txbAdditionalInfo.Visibility = System.Windows.Visibility.Collapsed;
                }
                if (!settings.SettingsAllergy)
                {
                    //txtAllergies.Visibility = System.Windows.Visibility.Collapsed;
                    txbAllergies.Visibility = System.Windows.Visibility.Collapsed;
                }
                if (!settings.SettingsMedical)
                {
                    //txtMedicalConditions.Visibility = System.Windows.Visibility.Collapsed;
                    txbMedicalConditions.Visibility = System.Windows.Visibility.Collapsed;
                }
                if (!settings.SettingsMedicine)
                {
                    //txtMedicines.Visibility = System.Windows.Visibility.Collapsed;
                    txbMedicines.Visibility = System.Windows.Visibility.Collapsed;
                }
                if (!settings.SettingsBloodGroup)
                {
                    rad1.Visibility = System.Windows.Visibility.Collapsed;
                    rad2.Visibility = System.Windows.Visibility.Collapsed;
                    rad3.Visibility = System.Windows.Visibility.Collapsed;
                    rad4.Visibility = System.Windows.Visibility.Collapsed;
                    rad5.Visibility = System.Windows.Visibility.Collapsed;
                    rad6.Visibility = System.Windows.Visibility.Collapsed;
                    rad7.Visibility = System.Windows.Visibility.Collapsed;
                    rad8.Visibility = System.Windows.Visibility.Collapsed;
                    rad9.Visibility = System.Windows.Visibility.Collapsed;
                }
                if (!settings.SettingsAdditionalNotes)
                {
                    //txtAdditionalNotes.Visibility = System.Windows.Visibility.Collapsed;
                    txbAdditionalNotes.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    txbDoctorsInfo.Visibility = System.Windows.Visibility.Visible;
                    txbCarrierInfo.Visibility = System.Windows.Visibility.Visible;
                    txbDentistInfo.Visibility = System.Windows.Visibility.Visible;
                    //txtIDInfo.Visibility = System.Windows.Visibility.Visible;
                    txbIDInfo.Visibility = System.Windows.Visibility.Visible;
                    //txtNetworkInfo.Visibility = System.Windows.Visibility.Visible;
                    txbNetworkInfo.Visibility = System.Windows.Visibility.Visible;
                    //txtGroupInfo.Visibility = System.Windows.Visibility.Visible;
                    txbGroupInfo.Visibility = System.Windows.Visibility.Visible;
                    //txtAdditionalInfo.Visibility = System.Windows.Visibility.Visible;
                    txbAdditionalInfo.Visibility = System.Windows.Visibility.Visible;
                    //txtAllergies.Visibility = System.Windows.Visibility.Visible;
                    txbAllergies.Visibility = System.Windows.Visibility.Visible;
                    //txtMedicalConditions.Visibility = System.Windows.Visibility.Visible;
                    txbMedicalConditions.Visibility = System.Windows.Visibility.Visible;
                    //txtMedicines.Visibility = System.Windows.Visibility.Visible;
                    txbMedicines.Visibility = System.Windows.Visibility.Visible;
                    rad1.Visibility = System.Windows.Visibility.Visible;
                    rad2.Visibility = System.Windows.Visibility.Visible;
                    rad3.Visibility = System.Windows.Visibility.Visible;
                    rad4.Visibility = System.Windows.Visibility.Visible;
                    rad5.Visibility = System.Windows.Visibility.Visible;
                    rad6.Visibility = System.Windows.Visibility.Visible;
                    rad7.Visibility = System.Windows.Visibility.Visible;
                    rad8.Visibility = System.Windows.Visibility.Visible;
                    rad9.Visibility = System.Windows.Visibility.Visible;
                    //txtAdditionalNotes.Visibility = System.Windows.Visibility.Visible;
                    txbAdditionalNotes.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
        private void Reset()
        {
            if (!kc.IsRunningInKidsCorner)
            {
                ApplicationBar = new ApplicationBar();

                //add shared menu item
                BuildLocalizedApplicationBarShared();

                //add default buttons
                BuildLocalizedApplicationBarReadOnly();

                //Fill in some empty space
                txtEmpty.Text = Environment.NewLine + Environment.NewLine + Environment.NewLine;
            }

            //restore saved contents
            txtAllergies.Text = settings.Allergies;
            txbAllergies.Text = settings.Allergies;
            txtMedicalConditions.Text = settings.PhoneOwnerMedicalInfo;
            txbMedicalConditions.Text = settings.PhoneOwnerMedicalInfo;
            txtMedicines.Text = settings.Medicines;
            txbMedicines.Text = settings.Medicines;
            txtAdditionalNotes.Text = settings.AdditionalNotes;
            txbAdditionalNotes.Text = settings.AdditionalNotes;
            rad1.IsChecked = settings.RadioButton1ONeg;
            rad2.IsChecked = settings.RadioButton2OPlus;
            rad3.IsChecked = settings.RadioButton3ANeg;
            rad4.IsChecked = settings.RadioButton4APlus;
            rad5.IsChecked = settings.RadioButton5BNeg;
            rad6.IsChecked = settings.RadioButton6BPlus;
            rad7.IsChecked = settings.RadioButton7ABNeg;
            rad8.IsChecked = settings.RadioButton8ABPlus;
            rad9.IsChecked = settings.RadioButtonDontKnow;
            txbDoctorsInfo.Text = settings.MedDoctorsInfoName + Environment.NewLine +
                                  settings.MedDoctorsInfoPhone;
            txbDentistInfo.Text = settings.MedDentistInfo + Environment.NewLine +
                                  settings.MedDentistInfoPhone;
            txbCarrierInfo.Text = settings.MedCarrierInfo + Environment.NewLine +
                                  settings.MedCarrierInfoPhone;
            txtIDInfo.Text = settings.MedIDInfo;
            txbIDInfo.Text = settings.MedIDInfo;
            txtNetworkInfo.Text = settings.MedNetworkInfo;
            txbNetworkInfo.Text = settings.MedNetworkInfo;
            txtGroupInfo.Text = settings.MedGroupInfo;
            txbGroupInfo.Text = settings.MedGroupInfo;
            txtAdditionalInfo.Text = settings.MedAdditionalInfo;
            txbAdditionalInfo.Text = settings.MedAdditionalInfo;
            ReadOnly();
        }
        //select all text
        void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
        }

        private void phoneNumberChooserTask_Completed(object sender, PhoneNumberResult e)
        {
            if (e.DisplayName != null)
            {
                if (e.TaskResult == TaskResult.OK)
                {
                    MessageBox.Show("The phone number for " + e.DisplayName + " is " + e.PhoneNumber);
                }
                if (IsBoxOneTappedMed)
                {
                    doctorsName = e.DisplayName;
                    doctorsNumber = e.PhoneNumber;
                    txbDoctorsInfo.Text = doctorsName + Environment.NewLine
                                        + doctorsNumber;
                }
                else if (IsBoxTwoTappedMed)
                {
                    dentistName = e.DisplayName;
                    dentistNumber = e.PhoneNumber;
                    txbDentistInfo.Text = dentistName + Environment.NewLine
                                        + dentistNumber;
                }
                else
                {
                    carrierName = e.DisplayName;
                    carrierNumber = e.PhoneNumber;
                    txbCarrierInfo.Text = carrierName + Environment.NewLine
                                        + carrierNumber;
                }
            }
        }

        private void PhoneDoctor_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            IsBoxOneTappedMed = true;
            if (IsReadOnlyOnMed == false)
            {
                IsPhonePickerOnMed = true;
                phoneNumberChooserTask.Show();
            }
            else
            {
                PhoneCallTask phoneCallTask = new PhoneCallTask();
                phoneCallTask.DisplayName = settings.MedDoctorsInfoName;
                phoneCallTask.PhoneNumber = settings.MedDoctorsInfoPhone;

                CompleteThePhoneCall(phoneCallTask);
            }
        }

        private static void CompleteThePhoneCall(PhoneCallTask phoneCallTask)
        {
            if (phoneCallTask.PhoneNumber.Length == 0)
            {
                MessageBox.Show("Please enter an emergency phone contact number.", "Phone", MessageBoxButton.OK);
            }
            else
            {
                phoneCallTask.Show();
            }
        }
        private void PhoneDentist_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            IsBoxOneTappedMed = false;
            IsBoxTwoTappedMed = true;
            if (IsReadOnlyOnMed == false)
            {
                IsPhonePickerOnMed = true;
                phoneNumberChooserTask.Show();
            }
            else
            {
                PhoneCallTask phoneCallTask = new PhoneCallTask();
                phoneCallTask.DisplayName = settings.MedDentistInfo;
                phoneCallTask.PhoneNumber = settings.MedDentistInfoPhone;

                CompleteThePhoneCall(phoneCallTask);
            }
        }
        private void PhoneCarrier_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            IsBoxOneTappedMed = false;
            IsBoxTwoTappedMed = false;

            if (IsReadOnlyOnMed == false)
            {
                IsPhonePickerOnMed = true;
                phoneNumberChooserTask.Show();
            }
            else
            {
                PhoneCallTask phoneCallTask = new PhoneCallTask();
                phoneCallTask.DisplayName = settings.MedCarrierInfo;
                phoneCallTask.PhoneNumber = settings.MedCarrierInfoPhone;

                CompleteThePhoneCall(phoneCallTask);
            }
        }

        private void BuildLocalizedApplicationBarReadOnly()
        {
            // Remove buttons
            ApplicationBar.Buttons.Remove(appBarButtonSave);
            ApplicationBar.Buttons.Remove(appBarButtonCancel);
            // Create a new button and set the text value to the localized string from AppResources. Edit
            appBarButtonEdit = new ApplicationBarIconButton(new Uri("/Assets/AppBar/edit.png", UriKind.Relative));
            appBarButtonEdit.Text = AppResources.AppBarEditButtonText;
            ApplicationBar.Buttons.Add(appBarButtonEdit);
            appBarButtonEdit.Click += appBarButtonEdit_Click;
        }
        private void BuildLocalizedApplicationBarShared()
        {
            ApplicationBarMenuItem appBarSettingsMenuItem = new ApplicationBarMenuItem(AppResources.AppBarSettingsMenuItemText);
            ApplicationBar.MenuItems.Add(appBarSettingsMenuItem);
            appBarSettingsMenuItem.Click += appBarSettingsMenuItem_Click;

            // Create a new menu item with the localized string from AppResources. About
            ApplicationBarMenuItem appBarAboutMenuItem = new ApplicationBarMenuItem(AppResources.AppBarAboutMenuItemText);
            ApplicationBar.MenuItems.Add(appBarAboutMenuItem);
            appBarAboutMenuItem.Click += appBarAboutMenuItem_Click;
        }

        private void BuildLocalizedApplicationBarEdit()
        {
            // Remove buttons
            ApplicationBar.Buttons.Remove(appBarButtonEdit);

            //Create a new button. Save
            appBarButtonSave = new ApplicationBarIconButton(new Uri("/Assets/AppBar/save.png", UriKind.Relative));
            appBarButtonSave.Text = "save";
            ApplicationBar.Buttons.Add(appBarButtonSave);
            appBarButtonSave.Click += appBarButtonSave_Click;

            //Create a new button. Cancel
            appBarButtonCancel = new ApplicationBarIconButton(new Uri("/Assets/AppBar/cancel.png", UriKind.Relative));
            appBarButtonCancel.Text = "cancel";
            ApplicationBar.Buttons.Add(appBarButtonCancel);
            appBarButtonCancel.Click += appBarButtonCancel_Click;
        }

        private void appBarButtonCancel_Click(object sender, EventArgs e)
        {
            IsPhonePickerOnMed = false;
            Reset();
        }
        void appBarButtonSave_Click(object sender, EventArgs e)
        {
            settings.Allergies = txtAllergies.Text;
            txbAllergies.Text = settings.Allergies;
            settings.PhoneOwnerMedicalInfo = txtMedicalConditions.Text;
            txbMedicalConditions.Text = settings.PhoneOwnerMedicalInfo;
            settings.Medicines = txtMedicines.Text;
            txbMedicines.Text = settings.Medicines;
            settings.AdditionalNotes = txtAdditionalNotes.Text;
            txbAdditionalNotes.Text = settings.AdditionalNotes;
            settings.RadioButton1ONeg = rad1.IsChecked.Value;
            settings.RadioButton2OPlus = rad2.IsChecked.Value;
            settings.RadioButton3ANeg = rad3.IsChecked.Value;
            settings.RadioButton4APlus = rad4.IsChecked.Value;
            settings.RadioButton5BNeg = rad5.IsChecked.Value;
            settings.RadioButton6BPlus = rad6.IsChecked.Value;
            settings.RadioButton7ABNeg = rad7.IsChecked.Value;
            settings.RadioButton8ABPlus = rad8.IsChecked.Value;
            settings.RadioButtonDontKnow = rad9.IsChecked.Value;
            settings.MedDoctorsInfoName = doctorsName;
            settings.MedDoctorsInfoPhone = doctorsNumber;
            settings.MedDentistInfo = dentistName;
            settings.MedDentistInfoPhone = dentistNumber;
            settings.MedCarrierInfo = carrierName;
            settings.MedCarrierInfoPhone = carrierNumber;
            settings.MedIDInfo = txtIDInfo.Text;
            txbIDInfo.Text = settings.MedIDInfo;
            settings.MedNetworkInfo = txtNetworkInfo.Text;
            txbNetworkInfo.Text = settings.MedNetworkInfo;
            settings.MedGroupInfo = txtGroupInfo.Text;
            txbGroupInfo.Text = settings.MedGroupInfo;
            settings.MedAdditionalInfo = txtAdditionalInfo.Text;
            txbAdditionalInfo.Text = settings.MedAdditionalInfo;

            IsPhonePickerOnMed = false;

            ReadOnly();
            ReadOnlyIconView();
        }
        //readonly method
        private void ReadOnly()
        {
            txtAllergies.IsReadOnly = true;
            txbAllergies.Visibility = System.Windows.Visibility.Visible;
            txtAllergies.Visibility = System.Windows.Visibility.Collapsed;
            txtMedicalConditions.IsReadOnly = true;
            txbMedicalConditions.Visibility = System.Windows.Visibility.Visible;
            txtMedicalConditions.Visibility = System.Windows.Visibility.Collapsed;
            txtMedicines.IsReadOnly = true;
            txbMedicines.Visibility = System.Windows.Visibility.Visible;
            txtMedicines.Visibility = System.Windows.Visibility.Collapsed;
            txtAdditionalNotes.IsReadOnly = true;
            txbAdditionalNotes.Visibility = System.Windows.Visibility.Visible;
            txtAdditionalNotes.Visibility = System.Windows.Visibility.Collapsed;
            rad1.IsEnabled = false;
            rad2.IsEnabled = false;
            rad3.IsEnabled = false;
            rad4.IsEnabled = false;
            rad5.IsEnabled = false;
            rad6.IsEnabled = false;
            rad7.IsEnabled = false;
            rad8.IsEnabled = false;
            rad9.IsEnabled = false;
            txtIDInfo.IsReadOnly = true;
            txbIDInfo.Visibility = System.Windows.Visibility.Visible;
            txtIDInfo.Visibility = System.Windows.Visibility.Collapsed;
            txtNetworkInfo.IsReadOnly = true;
            txbNetworkInfo.Visibility = System.Windows.Visibility.Visible;
            txtNetworkInfo.Visibility = System.Windows.Visibility.Collapsed;
            txtGroupInfo.IsReadOnly = true;
            txbGroupInfo.Visibility = System.Windows.Visibility.Visible;
            txtGroupInfo.Visibility = System.Windows.Visibility.Collapsed;
            txtAdditionalInfo.IsReadOnly = true;
            txbAdditionalInfo.Visibility = System.Windows.Visibility.Visible;
            txtAdditionalInfo.Visibility = System.Windows.Visibility.Collapsed;

            IsReadOnlyOnMed = true;

        }
        //edit method
        private void EditOnly()
        {
            txtAllergies.IsReadOnly = false;
            txbAllergies.Visibility = System.Windows.Visibility.Collapsed;
            txtAllergies.Visibility = System.Windows.Visibility.Visible;
            txtMedicalConditions.IsReadOnly = false;
            txbMedicalConditions.Visibility = System.Windows.Visibility.Collapsed;
            txtMedicalConditions.Visibility = System.Windows.Visibility.Visible;
            txtMedicines.IsReadOnly = false;
            txbMedicines.Visibility = System.Windows.Visibility.Collapsed;
            txtMedicines.Visibility = System.Windows.Visibility.Visible;
            txtAdditionalNotes.IsReadOnly = false;
            txbAdditionalNotes.Visibility = System.Windows.Visibility.Collapsed;
            txtAdditionalNotes.Visibility = System.Windows.Visibility.Visible;
            rad1.IsEnabled = true;
            rad2.IsEnabled = true;
            rad3.IsEnabled = true;
            rad4.IsEnabled = true;
            rad5.IsEnabled = true;
            rad6.IsEnabled = true;
            rad7.IsEnabled = true;
            rad8.IsEnabled = true;
            rad9.IsEnabled = true;
            txtIDInfo.IsReadOnly = false;
            txbIDInfo.Visibility = System.Windows.Visibility.Collapsed;
            txtIDInfo.Visibility = System.Windows.Visibility.Visible;
            txtNetworkInfo.IsReadOnly = false;
            txbNetworkInfo.Visibility = System.Windows.Visibility.Collapsed;
            txtNetworkInfo.Visibility = System.Windows.Visibility.Visible;
            txtGroupInfo.IsReadOnly = false;
            txbGroupInfo.Visibility = System.Windows.Visibility.Collapsed;
            txtGroupInfo.Visibility = System.Windows.Visibility.Visible;
            txtAdditionalInfo.IsReadOnly = false;
            txbAdditionalInfo.Visibility = System.Windows.Visibility.Collapsed;
            txtAdditionalInfo.Visibility = System.Windows.Visibility.Visible;

            IsReadOnlyOnMed = false;
        }
        //hide icons
        private void ReadOnlyIconView()
        {
            BuildLocalizedApplicationBarReadOnly();

        }
        //show icons
        private void EditOnlyIconView()
        {
            BuildLocalizedApplicationBarEdit();
        }

        private void appBarButtonEdit_Click(object sender, EventArgs e)
        {
            EditOnly();
            EditOnlyIconView();
            doctorsName = settings.MedDoctorsInfoName;
            doctorsNumber = settings.MedDoctorsInfoPhone;
            dentistName = settings.MedDentistInfo;
            dentistNumber = settings.MedDentistInfoPhone;
            carrierName = settings.MedCarrierInfo;
            carrierNumber = settings.MedCarrierInfoPhone;
        }
        private void MoveDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (sender == txtIDInfo)
                {
                    txtNetworkInfo.Focus();
                }
                else if (sender == txtNetworkInfo)
                {
                    txtGroupInfo.Focus();
                }
                else if (sender == txtGroupInfo)
                {
                    txtAdditionalInfo.Focus();
                }

                e.Handled = true;
            }
        }

        void appBarAboutMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }
        void appBarSettingsMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/settingsPage.xaml", UriKind.Relative));
        }
    }
}