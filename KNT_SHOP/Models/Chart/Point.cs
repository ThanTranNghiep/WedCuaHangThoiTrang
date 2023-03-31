using System;
using System.Runtime.Serialization;

namespace KNT_SHOP.Models.Chart
{
    [DataContract]
    public class Point
    {
        public Point(string label, decimal y)
        {
            this.Label = label;
            this.Y = y;
        }
 
        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "label")]
        public string Label = "";
 
        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<decimal> Y = null;
    }
}

