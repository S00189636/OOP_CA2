﻿using System;
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
    class Activity : IComparable
    {
        string Name;
        DateTime Date;
        public decimal Cost { get; private set; } // get is public but can only be set in the constractor 
        public ActivityType Type { get; private set; }
        public string Description { get; set; } // this is public set and get 

        public Activity(string name, DateTime date, decimal cost, ActivityType type)
        {
            Name = name;
            Date = date;
            Cost = cost;
            Type = type;
            Description =Name+" "+Type+" "+Cost;
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
            return activity.Date.CompareTo(Date);
        }

        // used in listbx
        public override string ToString()
        {
            return string.Format("{0} -{1}", Name, Date.ToShortDateString());
        }

        public string GetDescription()
        {
            return string.Format("{0}, Cost - {1:c}", Description, Cost);
        }

    }
}
