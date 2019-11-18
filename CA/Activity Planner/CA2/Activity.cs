using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA2
{
    public enum ActivityType
    {
        Land,
        Air,
        Water,
        All
    }
    class Activity : IComparable
    {
        string Name;
        DateTime Date;
        public decimal Cost { get; private set; }
        public ActivityType Type { get; private set; }
        public string Description { get; set; }

        public Activity(string name, DateTime date, decimal cost, ActivityType type)
        {
            Name = name;
            Date = date;
            Cost = cost;
            Type = type;
        }

        public int CompareTo(object obj)
        {
            Activity activity = obj as Activity;
            return activity.Date.CompareTo(Date);
        }

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
