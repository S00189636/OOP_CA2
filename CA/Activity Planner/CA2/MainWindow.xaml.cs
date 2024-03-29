﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        ObservableCollection<Activity> Activities; // this will hold all the activitis 
        ObservableCollection<Activity> SelectedActivities; // this will hold the activities 
        ObservableCollection<Activity> AllActivities; // this will hold filtterd activities 

        //Random random; // will use it to genrate random activities 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Activities = new ObservableCollection<Activity>();
            SelectedActivities = new ObservableCollection<Activity>();
            AllActivities = new ObservableCollection<Activity>();
            //random = new Random();
            // fill the list with random activities 
            //for (int i = 0; i < 6; i++)
            //{
            //    Activities.Add(RandomActivity());
            //}

            // using the code from the moodle
            CreatActivities();
            CopyList(Activities, AllActivities);
            lsitBoxAllActivities.ItemsSource = AllActivities;
            lsitBoxSelectedActivities.ItemsSource = SelectedActivities;
            SortAll();
            SetTotalCost();
        }

        #region Radio buttons Checked
        // when checked fillter the list 
        private void BtnRadio_Checked(object sender, RoutedEventArgs e)
        {
            ActivityType Type;
            Enum.TryParse((sender as RadioButton).Content.ToString(), out Type);
            Filter(Type);
            Highlight(sender);
        }

        #endregion
        #region Buttons methods 
        // when add button is clicked remove the selected item from all lists and add it to selected list 
        // when remove button is clicked add the item back to all list and activities list and remove it from selected list
        private void BtnAddToSelected_Click(object sender, RoutedEventArgs e)
        {
            Activity activity = lsitBoxAllActivities.SelectedItem as Activity;
            if (activity == null)
            {
                MessageBox.Show($"Nothing is selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            foreach (Activity item in SelectedActivities)
            {
                if(activity.Date == item.Date)
                {
                    // show something 
                    //MessageBox.Show($"'{item.Name}' is in the same date \n try removing it first");
                    MessageBox.Show($"'{item.Name}' is on the same date \n try removing it first","Date Conflict",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                    return;
                }
            }

            SelectedActivities.Add(activity);
            AllActivities.Remove(activity);
            Activities.Remove(activity);
            SetTotalCost();
            SortAll();
        }
        // handels the add button click event 
        private void AddToAll_Click(object sender, RoutedEventArgs e)
        {
            Activity activity = lsitBoxSelectedActivities.SelectedItem as Activity;
            radioAll.IsChecked = true;
            if (activity == null)
            {
                MessageBox.Show($"Nothing is selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AllActivities.Add(activity);
            Activities.Add(activity);
            SelectedActivities.Remove(activity);
            SetTotalCost();
            SortAll();
        }
        #endregion
        #region listBox methods
        // when the user change the selection of an item from the list box it will show the item description 
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (listBox.SelectedIndex < 0 || listBox.SelectedIndex > AllActivities.Count)
                return;
            Activity activity = (sender as ListBox).SelectedItem as Activity;
            //if (activity != null)
                txtblokDescription_.Text = activity.GetDescription();
        }
        #endregion
        #region all other methods (sorting and copying)
        // this will set the total cost for all the items added to the selected list
        private void SetTotalCost()
        {
            if (SelectedActivities == null || SelectedActivities.Count < 0)
            {
                txtblokTotalCost.Text = string.Format("{0:c}", 0);
                return;
            }

            decimal total = 0;
            foreach (Activity item in SelectedActivities)
            {
                if (item != null)
                    total += item.Cost;
            }
            txtblokTotalCost.Text = string.Format("{0:c}", total);
        }
        // will return a copy of an ObservableCollection list 
        private void CopyList(ObservableCollection<Activity> fromList, ObservableCollection<Activity> toList)
        {
            // if the source is null do nothing
            if (fromList == null)
                return;
            foreach (Activity item in fromList)
            {
                //fill the list from the source list
                toList.Add(item);
            }
            //return toList;
        }

        // will return a sorted copy of an ObservableCollection list 
        private ObservableCollection<Activity> SortList(ObservableCollection<Activity> listToSort)
        {
            if (listToSort == null)
                return listToSort;
            // if the source is null do nothing
            List<Activity> activityList = listToSort.ToList();
            activityList.Sort();
            listToSort.Clear();
            foreach (Activity item in activityList)
            {
                listToSort.Add(item);
            }
            return listToSort;
        }



        //will return an auto generated 'Activity' object
        //private Activity RandomActivity()
        //{
        //    // this will creat a new activity with random data 
        //    Activity activity;
        //    string[] names = { "Keyaking", "Parachuting", "Hang Gliding", "Sailing", "Helicopter Tour" };
        //    string name = names[random.Next(0, names.Length)];
        //    DateTime Date = new DateTime(random.Next(2019, 2025), random.Next(3, 11), random.Next(1, 31));
        //    decimal Cost = random.Next(20, 120);
        //    ActivityType Type = (ActivityType)random.Next(0, 3);
        //    string Description = string.Format("This is the description of the activity {0} of type {1}", name, Type.ToString());
        //    activity = new Activity(name, Date, Cost, Type, Description);
        //    return activity;
        //}

        private void CreatActivities()
        {
            Activity l1 = new Activity()
            {
                Name = "Treking",
                Description = "Instructor led group trek through local mountains.",
                Date = new DateTime(2019, 06, 01),
                Type = ActivityType.Land,
                Cost = 20m
            };

            Activity l2 = new Activity()
            {
                Name = "Mountain Biking",
                Description = "Instructor led half day mountain biking.  All equipment provided.",
                Date = new DateTime(2019, 06, 02),
                Type = ActivityType.Land,
                Cost = 30m
            };

            Activity l3 = new Activity()
            {
                Name = "Abseiling",
                Description = "Experience the rush of adrenaline as you descend cliff faces from 10-500m.",
                Date = new DateTime(2019, 06, 03),
                Type = ActivityType.Land,
                Cost = 40m
            };

            Activity w1 = new Activity()
            {
                Name = "Kayaking",
                Description = "Half day lakeland kayak with island picnic.",
                Date = new DateTime(2019, 06, 01),
                Type = ActivityType.Water,
                Cost = 40m
            };

            Activity w2 = new Activity()
            {
                Name = "Surfing",
                Description = "2 hour surf lesson on the wild atlantic way",
                Date = new DateTime(2019, 06, 02),
                Type = ActivityType.Water,
                Cost = 25m
            };

            Activity w3 = new Activity()
            {
                Name = "Sailing",
                Description = "Full day lakeland kayak with island picnic.",
                Date = new DateTime(2019, 06, 03),
                Type = ActivityType.Water,
                Cost = 50m
            };

            Activity a1 = new Activity()
            {
                Name = "Parachuting",
                Description = "Experience the thrill of free fall while you tandem jump from an airplane.",
                Date = new DateTime(2019, 06, 01),
                Type = ActivityType.Air,
                Cost = 100m
            };

            Activity a2 = new Activity()
            {
                Name = "Hang Gliding",
                Description = "Soar on hot air currents and enjoy spectacular views of the coastal region.",
                Date = new DateTime(2019, 06, 02),
                Type = ActivityType.Air,
                Cost = 80m
            };

            Activity a3 = new Activity()
            {
                Name = "Helicopter Tour",
                Description = "Experience the ultimate in aerial sight-seeing as you tour the area in our modern helicopters",
                Date = new DateTime(2019, 06, 03),
                Type = ActivityType.Air,
                Cost = 200m
            };

            Activities.Add(l1);
            Activities.Add(l2);
            Activities.Add(l3);
            Activities.Add(w1);
            Activities.Add(w2);
            Activities.Add(w3);
            Activities.Add(a1);
            Activities.Add(a2);
            Activities.Add(a3);
            SortList(Activities);
        }

        //this will highlight the selected Radio button for 6  seconds
        async private Task Highlight(object obj)
        {
            RadioButton radio = obj as RadioButton;
            radio.BorderBrush = Brushes.Red;
            radio.Background = Brushes.Blue;
            await Task.Delay(600);
            radio.BorderBrush = Brushes.Gray;
            radio.Background = Brushes.White;
        }

        // fillters the left hand side listBox 
        private void Filter(ActivityType type)
        {
            if (AllActivities != null)
                AllActivities.Clear();
            switch (type)
            {
                case ActivityType.All:
                    CopyList(Activities, AllActivities);
                    break;
                default:
                    foreach (Activity activity in Activities)
                    {
                        if (activity.Type == type)
                            AllActivities.Add(activity);
                    }
                    break;
            }
            SortAll();
        }

        // when called, it will sort all the lists by using CompareTo
        private void SortAll()
        {
            Activities = SortList(Activities);
            AllActivities = SortList(AllActivities);
            SelectedActivities = SortList(SelectedActivities);
        }
        #endregion
    }
}
