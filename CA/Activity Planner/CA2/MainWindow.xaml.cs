using System;
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

        Random random; // will use it to genrate random activities 
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Activities = new ObservableCollection<Activity>();
            SelectedActivities = new ObservableCollection<Activity>();
            AllActivities = new ObservableCollection<Activity>();
            random = new Random();
            // fill the list with random activities 
            for (int i = 0; i < 6; i++)
            {
                Activities.Add(RandomActivity());
            }

            //AllActivities = CopyList(Activities, AllActivities);
            CopyList(Activities, AllActivities);
            lsitBoxAllActivities.ItemsSource = AllActivities;
            lsitBoxSelectedActivities.ItemsSource = SelectedActivities;
            SortAll();
            SetTotalCost();
        }

        // will return a copy of an ObservableCollection list 
        //private ObservableCollection<Activity> CopyList(ObservableCollection<Activity> fromList, ObservableCollection<Activity> toList)
        private void CopyList(ObservableCollection<Activity> fromList, ObservableCollection<Activity> toList)
        {
            // if the source is null do nothing
            if (fromList == null)
                return ;
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

        private Activity RandomActivity()
        {
            // this will creat a new activity with random data 
            Activity activity;
            string[] names = { "Keyaking","Parachuting","Hang Gliding","Sailing","Helicopter Tour"};
            string name = names[random.Next(0, names.Length)] ;
            DateTime Date = new DateTime(random.Next(2019, 2025), random.Next(3, 11), random.Next(1, 31));
            decimal Cost = random.Next(20, 120);
            ActivityType Type = (ActivityType)random.Next(0, 3);
            activity = new Activity(name, Date, Cost, Type);
            activity.Description = string.Format("This is the description of the activity {0} of type {1}" ,name, Type.ToString());
            return activity;
        }

        private void FilterAll(ActivityType type)
        {
            if (AllActivities != null)
                AllActivities.Clear();
            switch (type)
            {
                case ActivityType.Land:
                case ActivityType.Air:
                case ActivityType.Water:
                    foreach (Activity activity in Activities)
                    {
                        if (activity.Type == type)
                            AllActivities.Add(activity);
                    }
                    break;
                case ActivityType.All:
                    //AllActivities = CopyList(Activities, AllActivities);
                    CopyList(Activities, AllActivities);
                    break;
            }
            SortAll();
        }

        private void RadioAll_Checked(object sender, RoutedEventArgs e)
        {
            FilterAll(ActivityType.All);
        }

        private void RadioWater_Checked(object sender, RoutedEventArgs e)
        {
            FilterAll(ActivityType.Water);
        }

        private void RadioAir_Checked(object sender, RoutedEventArgs e)
        {
            FilterAll(ActivityType.Air);
        }

        private void RadioLand_Checked(object sender, RoutedEventArgs e)
        {
            FilterAll(ActivityType.Land);
        }

        private void BtnAddToSelected_Click(object sender, RoutedEventArgs e)
        {
            Activity activity = lsitBoxAllActivities.SelectedItem as Activity;
            if (activity == null)
                return;
            SelectedActivities.Add(activity);
            AllActivities.Remove(activity);
            Activities.Remove(activity);
            SetTotalCost();
            SortAll();
        }

        private void AddToAll_Click(object sender, RoutedEventArgs e)
        {
            Activity activity = lsitBoxSelectedActivities.SelectedItem as Activity;
            radioAll.IsChecked = true;
            if (activity == null)
                return;
            AllActivities.Add(activity);
            Activities.Add(activity);
            SelectedActivities.Remove(activity);
            SetTotalCost();
            SortAll();
        }

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

        private void LsitBoxAllActivities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsitBoxAllActivities.SelectedIndex < 0 || lsitBoxAllActivities.SelectedIndex > AllActivities.Count)
                return;
            Activity activity = AllActivities[lsitBoxAllActivities.SelectedIndex];
            if (activity != null)
                txtblokDescription_.Text = activity.GetDescription();
        }

        private void lsitBoxSelectedActivities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsitBoxSelectedActivities.SelectedIndex < 0 || lsitBoxSelectedActivities.SelectedIndex > SelectedActivities.Count)
                return;
            Activity activity = SelectedActivities[lsitBoxSelectedActivities.SelectedIndex];
            if (activity != null)
                txtblokDescription_.Text = activity.GetDescription();
        }

        private void SortAll()
        {
            Activities = SortList(Activities);
            AllActivities = SortList(AllActivities);
            SelectedActivities = SortList(SelectedActivities);
        }
    }
}
