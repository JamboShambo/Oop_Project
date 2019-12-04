using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA2
{


    public enum ActivityType
    {
        //Setting up an enum for different types of activity
        Land,
        Air,
        Water
    }


    public class Activity : IComparable<Activity>
    {
        //icomparable is set here with a compare to method below

        //My props
        public string Name { get; set; }
        public DateTime ActivityDate { get; set; }
        public decimal Cost { get; set; }
        public ActivityType TypeOfActivity { get; set; }
        public string _description;
        public string Description
        {
            get
            {
                return $"{_description} ----> { Cost:C} ";
            }
        }

        //my contr
        public Activity(string name, string description, DateTime date, ActivityType category, decimal price)
        {
            Name = name;
            _description = description;
            ActivityDate = date;
            TypeOfActivity = category;
            Cost = price;
        }

        //my method
        public override string ToString()
        {
            return $"{Name} ----> { ActivityDate.ToShortDateString()} ";
        }

        // compare to , comparing the activity objs dates to eachother
        public int CompareTo(Activity date)
        {
            return this.ActivityDate.CompareTo(date.ActivityDate);
        }
    }
}