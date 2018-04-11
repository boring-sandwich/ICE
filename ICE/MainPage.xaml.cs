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
    public partial class MainPage : PhoneApplicationPage
    {

        private bool IsInitialized = false;
        ICESettings settings = new ICESettings();
        ApplicationBarIconButton appBarButtonEdit;
        ApplicationBarIconButton appBarButtonSave;
        ApplicationBarIconButton appBarButtonCancel;
        ApplicationBarIconButton appBarButtonShare;
        PhoneNumberChooserTask phoneNumberChooserTask;
        public bool IsReadOnlyOn = true;
        public bool IsPhonePickerOn = false;
        public bool IsBoxOneTapped = false;
        public string contactNameOne;
        public string contactNameTwo;
        public string contactPhoneOne;
        public string contactPhoneTwo;

        public MainPage()
        {
            InitializeComponent();
            Reset();
            phoneNumberChooserTask = new PhoneNumberChooserTask();
            phoneNumberChooserTask.Completed += new EventHandler<PhoneNumberResult>(phoneNumberChooserTask_Completed);
            IsInitialized = true;
        }
        void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }
        void Contacts_SearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            //Do something with the results.
            MessageBox.Show(e.Results.Count().ToString());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (!IsPhonePickerOn)
            {
                Reset();
            }
        }

        private void Reset()
        {
            KidsCorner kc = new KidsCorner();
            if (!kc.IsRunningInKidsCorner)
            {
                ApplicationBar = new ApplicationBar();
                //add shared menu item
                BuildLocalizedApplicationBarShared();

                //add default buttons
                BuildLocalizedApplicationBarReadOnly();
            }
            //restore saved contents
            txtEmergencyContactOneName.Text = settings.EmergencyContactNameOne + Environment.NewLine
                                                  + settings.EmergencyContactNumberOne;
            txtEmergencyContactTwoName.Text = settings.EmergencyContactNameTwo + Environment.NewLine
                                                  + settings.EmergencyContactNumberTwo;
            txtPhoneOwnersName.Text = settings.PhoneOwnersName;
            txbPhoneOwnersName.Text = settings.PhoneOwnersName;

            ReadOnly();

        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
        }

        void phoneNumberChooserTask_Completed(object sender, PhoneNumberResult e)
        {
            if (e.DisplayName != null)
            {
                if (e.TaskResult == TaskResult.OK)
                {
                    MessageBox.Show("The phone number for " + e.DisplayName + " is " + e.PhoneNumber);
                }
                if (IsBoxOneTapped)
                {
                    contactNameOne = e.DisplayName;
                    contactPhoneOne = e.PhoneNumber;
                    txtEmergencyContactOneName.Text = contactNameOne + Environment.NewLine
                                                    + contactPhoneOne;
                }
                else
                {
                    contactNameTwo = e.DisplayName;
                    contactPhoneTwo = e.PhoneNumber;
                    txtEmergencyContactTwoName.Text = contactNameTwo + Environment.NewLine
                                                    + contactPhoneTwo;
                }
            }

        }

        private void PhoneOne_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            IsBoxOneTapped = true;
            //Launch the Phone App with the emergency name and contact number
            if (IsReadOnlyOn == false)
            {
                IsPhonePickerOn = true;
                phoneNumberChooserTask.Show();
            }
            else
            {
                PhoneCallTask phoneCallTask = new PhoneCallTask();
                phoneCallTask.DisplayName = settings.EmergencyContactNameOne;
                phoneCallTask.PhoneNumber = settings.EmergencyContactNumberOne;

                if (phoneCallTask.PhoneNumber.Length == 0)
                {
                    MessageBox.Show("Please enter an emergency phone contact number.", "Phone", MessageBoxButton.OK);
                }
                else
                {
                    phoneCallTask.Show();
                }
            }
        }
        private void PhoneTwo_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            IsBoxOneTapped = false;
            if (IsReadOnlyOn == false)
            {
                IsPhonePickerOn = true;
                phoneNumberChooserTask.Show();
            }
            else
            {
                PhoneCallTask phoneCallTask = new PhoneCallTask();
                phoneCallTask.DisplayName = settings.EmergencyContactNameTwo;
                phoneCallTask.PhoneNumber = settings.EmergencyContactNumberTwo;

                if (phoneCallTask.PhoneNumber.Length == 0)
                {
                    MessageBox.Show("Please enter an emergency phone contact number.", "Phone", MessageBoxButton.OK);
                }
                else
                {
                    phoneCallTask.Show();
                }
            }
        }

        private void BuildLocalizedApplicationBarReadOnly()
        {
            // Remove buttons
            ApplicationBar.Buttons.Remove(appBarButtonSave);
            ApplicationBar.Buttons.Remove(appBarButtonCancel);
            // Create a new button. Share
            appBarButtonShare = new ApplicationBarIconButton(new Uri("/Assets/AppBar/share.png", UriKind.Relative));
            appBarButtonShare.Text = AppResources.AppBarShareButtonText;
            ApplicationBar.Buttons.Add(appBarButtonShare);
            appBarButtonShare.Click += appBarButtonShare_Click;

            // Create a new button and set the text value to the localized string from AppResources. Edit
            appBarButtonEdit = new ApplicationBarIconButton(new Uri("/Assets/AppBar/edit.png", UriKind.Relative));
            appBarButtonEdit.Text = AppResources.AppBarEditButtonText;
            ApplicationBar.Buttons.Add(appBarButtonEdit);
            appBarButtonEdit.Click += appBarButtonEdit_Click;
        }

        void appBarButtonShare_Click(object sender, EventArgs e)
        {
            //create a massive text box that almalgmates all the information in the app.
            MessageBoxResult result = MessageBox.Show("Please note this will share all the information in this app. Do you wish to continue?", "Advisory Notice", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel)
            {
                return;
            }
            UserInformation us = new UserInformation();
            us.ExportAppUserInformation();
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
            ApplicationBar.Buttons.Remove(appBarButtonShare);
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
            IsPhonePickerOn = false;
            Reset();
        }

        void appBarButtonSave_Click(object sender, EventArgs e)
        {
            settings.EmergencyContactNameOne = contactNameOne;
            settings.EmergencyContactNumberOne = contactPhoneOne;
            settings.EmergencyContactNameTwo = contactNameTwo;
            settings.EmergencyContactNumberTwo = contactPhoneTwo;
            settings.PhoneOwnersName = txtPhoneOwnersName.Text;
            txbPhoneOwnersName.Text = settings.PhoneOwnersName;
            IsPhonePickerOn = false;

            settings.UpdateLockScreen();

            ReadOnly();
            ReadOnlyIconView();
        }

        //readonly method
        private void ReadOnly()
        {
            txtPhoneOwnersName.IsReadOnly = true;
            txbPhoneOwnersName.Visibility = System.Windows.Visibility.Visible;
            txtPhoneOwnersName.Visibility = System.Windows.Visibility.Collapsed;
            IsReadOnlyOn = true;
        }
        //edit method
        private void EditOnly()
        {
            txtPhoneOwnersName.IsReadOnly = false;
            txbPhoneOwnersName.Visibility = System.Windows.Visibility.Collapsed;
            txtPhoneOwnersName.Visibility = System.Windows.Visibility.Visible;
            IsReadOnlyOn = false;
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
            contactNameOne = settings.EmergencyContactNameOne;
            contactPhoneOne = settings.EmergencyContactNumberOne;
            contactNameTwo = settings.EmergencyContactNameTwo;
            contactPhoneTwo = settings.EmergencyContactNumberTwo;
        }

        void appBarAboutMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }
        void appBarSettingsMenuItem_Click(object sender, EventArgs e)
        {

            NavigationService.Navigate(new Uri("/settingsPage.xaml", UriKind.Relative));
        }

        private void txbAllergies_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MedicalNotes.xaml?goto=0", UriKind.Relative));
        }

        private void txbMedicalConditions_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MedicalNotes.xaml?goto=1", UriKind.Relative));
        }

        private void txbMedicines_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MedicalNotes.xaml?goto=2", UriKind.Relative));
        }

        private void txbBlood_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MedicalNotes.xaml?goto=3", UriKind.Relative));
        }

        private void txbAdditional_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MedicalNotes.xaml?goto=4", UriKind.Relative));
        }

        private void txbDoctorInfo_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MedicalNotes.xaml?goto=5", UriKind.Relative));
        }

        private void txtPhoneOwnersName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Focus();
                e.Handled = true;
            }
        }
    }
}