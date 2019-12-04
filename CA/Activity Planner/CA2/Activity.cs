using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA2
{
    public enum ActivityType
    {
        Land,//0
        Air,//1
        Water,//2
        All//3
    }
    public class Activity : IComparable
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal Cost { get;  set; } 
        public ActivityType Type { get;  set; }
        public string Description { get; set; } 

        public Activity() { }
        // used to creat random activity
        public Activity(string name, DateTime date, decimal cost, ActivityType type)
        {
            Name = name;
            Date = date;
            Cost = cost;
            Type = type;
            Description = Name + " " + Type + " " + Cost;
        }

        // this is used to creat an object with description 
        public Activity(string name, DateTime date, decimal cost, ActivityType type, string description)
            : this(name, date, cost, type)
        {
            Description = description;
        }

        // used for sorting 
        public int CompareTo(object obj)
        {
            Activity activity = obj as Activity;
            
            return this.Date.CompareTo(activity.Date);
            
        }

        // used in listbx item string 
        public override string ToString()
        {
            return string.Format("{0} -{1}", Name, Date.ToShortDateString());
        }

        // will return a formatted string 
        public string GetDescription()
        {
            return string.Format("{0}, Cost - {1:c}", Description, Cost);
        }

        

    }
}
