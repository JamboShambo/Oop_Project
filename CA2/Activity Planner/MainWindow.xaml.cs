using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CA2
{
    public partial class MainWindow : Window
    {
        //lists and var created
        List<Activity> AllAct = new List<Activity>();
        List<Activity> SelectAct = new List<Activity>();
        List<Activity> LandAct = new List<Activity>();
        List<Activity> WaterAct = new List<Activity>();
        List<Activity> AirAct = new List<Activity>();
        public decimal TotalCost;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //creating activity objects
            Activity l1 = new Activity("Trekking", "Instructor led group trek through local mountains.", new DateTime(2019, 06, 01), ActivityType.Land, 20m);
            Activity l2 = new Activity("Mountain Biking", "Instructor led half day mountain biking.  All equipment provided.", new DateTime(2019, 06, 02), ActivityType.Land, 30m);
            Activity l3 = new Activity("Abseiling", "Experience the rush of adrenaline as you descend cliff faces from 10-500m.", new DateTime(2019, 06, 03), ActivityType.Land, 40m);
            Activity w4 = new Activity("Kayaking", "Half day lakeland kayak with island picnic.", new DateTime(2019, 06, 01), ActivityType.Water, 40m);
            Activity w5 = new Activity("Surfing", "2 hour surf lesson on the wild atlantic way", new DateTime(2019, 06, 02), ActivityType.Water, 25m);
            Activity w6 = new Activity("Sailing", "Full day lakeland kayak with island picnic.", new DateTime(2019, 06, 03), ActivityType.Water, 50m);
            Activity a7 = new Activity("Parachuting", "Experience the thrill of free fall while you tandem jump from an airplane.", new DateTime(2019, 06, 01), ActivityType.Air, 100m);
            Activity a8 = new Activity("Hang Gliding", "Soar on hot air currents and enjoy spectacular views of the coastal region.", new DateTime(2019, 06, 02), ActivityType.Air, 80m);
            Activity a9 = new Activity("Helicopter Tour", "Experience the ultimate in aerial sight-seeing as you tour the area in our modern helicopters", new DateTime(2019, 06, 03), ActivityType.Air, 200m);

            //adding the objects to a list
            AllAct.Add(l1);
            AllAct.Add(l2);
            AllAct.Add(l3);
            AllAct.Add(w4);
            AllAct.Add(w5);
            AllAct.Add(w6);
            AllAct.Add(a7);
            AllAct.Add(a8);
            AllAct.Add(a9);
            //Sorting all activites
            AllAct.Sort();
        }
        private void BtnMinus_Click(object sender, RoutedEventArgs e)
        {
            //getting what the user has selected and storing as selectedActivity for the selected activity list
            Activity selectedActivity = LbxSelectAct.SelectedItem as Activity;

            //checking if anything was selected by the user in Selected Activites
            if (selectedActivity != null)
            {
                //takes away the user selected activity when the remove button is clicked
                SelectAct.Remove(selectedActivity);
                //adds whatever item was selected back to the all activities listbox
                AllAct.Add(selectedActivity);
                // Below adds it back to the correct type of list 
                if (selectedActivity.TypeOfActivity == ActivityType.Land)
                {
                    LandAct.Add(selectedActivity);
                }
                else if (selectedActivity.TypeOfActivity == ActivityType.Water)
                {
                    WaterAct.Add(selectedActivity);
                }
                else if (selectedActivity.TypeOfActivity == ActivityType.Air)
                {
                    AirAct.Add(selectedActivity);
                }
                //takes away the cost for the removed activity
                TotalCost -= selectedActivity.Cost;
                RefreshAgain();
                Refresh();
            }
            //Error message if nothing is selected and remove is clicked
            else
            {
                MessageBox.Show("Error.... There has not been anything selected");
            }
        }
        private void BtnPlus_Click(object sender, RoutedEventArgs e)
        {
            //getting what the user has selected and storing as selectedActivity for the All Activities list
            Activity selectedActivity = LbxAllAct.SelectedItem as Activity;

            //checking if anything was selected by the user in All Activites
            if (selectedActivity != null)
            {
                //the below checks to see if the dates from selected activitys are the same, if so a error message is displayed
                bool AddOrNot = true;
                for (int i = 0; i < SelectAct.Count; i++)
                {
                    if (selectedActivity.ActivityDate == SelectAct[i].ActivityDate)
                    {
                        MessageBox.Show("Error.... Activity already on this date");
                        SelectAct.Remove(selectedActivity);
                        RefreshAgain();
                        AddOrNot = false;
                    }
                }
                if (AddOrNot == true)
                {
                    //Removes user selected activity when add button is clicked, Then adds it to the Selected Activity box
                    AllAct.Remove(selectedActivity);
                    LandAct.Remove(selectedActivity);
                    WaterAct.Remove(selectedActivity);
                    AirAct.Remove(selectedActivity);
                    SelectAct.Add(selectedActivity);
                    TotalCost += selectedActivity.Cost;
                    Refresh();
                    RefreshAgain();
                }
            }
            //Error message if nothing is selected and add is clicked
            else
            {
                MessageBox.Show("Error.... There has not been anything selected");
            }
        }
        
        private void Refresh()
        {
            // puts itemsource to null, sorts the selected activites and checks what has been changed
            LbxSelectAct.ItemsSource = null;
            SelectAct.Sort();
            LbxSelectAct.ItemsSource = SelectAct;

                //This updates the total cost
                TxtblkTotalCost.Text = null;
                TxtblkTotalCost.Text = string.Format("{0:C}", TotalCost);
            
        }
        private void RefreshAgain()
        {
            //checks if what radio is checked, and displays according to choice 
            if (RadAll.IsChecked == true)
            {
                //without this null the objects would not remove from lists when they are moved over to the other list
                LbxAllAct.ItemsSource = null;
                LbxAllAct.ItemsSource = AllAct;
            }
            else if (RadLand.IsChecked == true)
            {
                LbxAllAct.ItemsSource = null;
                LbxAllAct.ItemsSource = LandAct;
            }
            else if (RadWater.IsChecked == true)
            {
                LbxAllAct.ItemsSource = null;
                LbxAllAct.ItemsSource = WaterAct;
            }
            else
            {
                LbxAllAct.ItemsSource = null;
                LbxAllAct.ItemsSource = AirAct;
            }
        }
        private void LbxAllAct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //The description of the activity is shown in the description text box
            Activity selectedActivity = LbxAllAct.SelectedItem as Activity;
            if (selectedActivity != null)
            {
                TblkDescription.Text = (selectedActivity.Description);
            }
        }
        private void LbxSelectAct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //when event changes call this method
        }
        private void RadAll_Click(object sender, RoutedEventArgs e)
        {
            //when radio button is clicked  call method
            RefreshAgain();
        }
        private void TtblkDescription_Loaded(object sender, RoutedEventArgs e)
        {
            //load description on window load, will be empty
        }
        private void TblkTotalCost_Loaded(object sender, RoutedEventArgs e)
        {
            // on window load display cost, which is zero
            TxtblkTotalCost.Text = string.Format("{0:C}", TotalCost);
        }
    }
}