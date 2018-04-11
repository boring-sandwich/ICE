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

namespace ICE
{
    public partial class settingsPage : PhoneApplicationPage
    {
        private bool IsInitialized = false;
        ICESettings settings = new ICESettings();
        ApplicationBarIconButton appBarButtonEdit;
        ApplicationBarIconButton appBarButtonSave;
        ApplicationBarIconButton appBarButtonCancel;
        public settingsPage()
        {
            InitializeComponent();
            Reset();
            IsInitialized = true;
        }
        private void Reset()
        {
            ApplicationBar = new ApplicationBar();

            //add shared menu item
            BuildLocalizedApplicationBarShared();

            //add default buttons
            BuildLocalizedApplicationBarReadOnly();

            //restore saved contents
            txtEmergencyContactOneName.Text = settings.EmergencyContactNameOne + Environment.NewLine
                                                  + settings.EmergencyContactNumberOne;
            txtEmergencyContactTwoName.Text = settings.EmergencyContactNameTwo + Environment.NewLine
                                                  + settings.EmergencyContactNumberTwo;

            radOption1.IsChecked = settings.RadioButtonOption1;
            radOption2.IsChecked = settings.RadioButtonOption2;

            cbxShowDoctorInfo.IsChecked = settings.SettingsDoctorInfo;
            cbxShowAllergies.IsChecked = settings.SettingsAllergy;
            cbxShowMedical.IsChecked = settings.SettingsMedical;
            cbxShowMedicine.IsChecked = settings.SettingsMedicine;
            cbxShowBloodGroup.IsChecked = settings.SettingsBloodGroup;
            cbxShowAdditional.IsChecked = settings.SettingsAdditionalNotes;

            ReadOnly();

        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
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
            Reset();
        }
        void appBarButtonSave_Click(object sender, EventArgs e)
        {
            settings.RadioButtonOption1 = radOption1.IsChecked.Value;
            settings.RadioButtonOption2 = radOption2.IsChecked.Value;
            settings.SettingsDoctorInfo = cbxShowDoctorInfo.IsChecked.Value;
            settings.SettingsAllergy = cbxShowAllergies.IsChecked.Value;
            settings.SettingsMedical = cbxShowMedical.IsChecked.Value;
            settings.SettingsMedicine = cbxShowMedicine.IsChecked.Value;
            settings.SettingsBloodGroup = cbxShowBloodGroup.IsChecked.Value;
            settings.SettingsAdditionalNotes = cbxShowAdditional.IsChecked.Value;

            settings.UpdateLockScreen();

            ReadOnly();
            ReadOnlyIconView();
        }
        //readonly method
        private void ReadOnly()
        {
            radOption1.IsEnabled = false;
            radOption2.IsEnabled = false;
            cbxShowDoctorInfo.IsEnabled = false;
            cbxShowAllergies.IsEnabled = false;
            cbxShowMedical.IsEnabled = false;
            cbxShowMedicine.IsEnabled = false;
            cbxShowBloodGroup.IsEnabled = false;
            cbxShowAdditional.IsEnabled = false;
        }
        //edit method
        private void EditOnly()
        {
            radOption1.IsEnabled = true;
            radOption2.IsEnabled = true;
            cbxShowDoctorInfo.IsEnabled = true;
            cbxShowAllergies.IsEnabled = true;
            cbxShowMedical.IsEnabled = true;
            cbxShowMedicine.IsEnabled = true;
            cbxShowBloodGroup.IsEnabled = true;
            cbxShowAdditional.IsEnabled = true;
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
        }
        private async void btnGoToLockSettings_Click(object sender, RoutedEventArgs e)
        {
            // Launch URI for the lock screen settings screen.
            var op = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-lock:"));
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