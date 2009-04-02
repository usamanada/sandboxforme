using System;
using System.ComponentModel;

namespace OrderedPropertyGrid.Examples
{
    //
    // The class definition must include the TypeConverter 
    // attribute to allow it to be ordered
    //
    [TypeConverter(typeof(PropertySorter))]
    [DefaultProperty("B_base")]
    public class SimpleOrdered
    {
        protected const string FIRST_CATEGORY = "First";
        protected const string SECOND_CATEGORY = "Second";

        [Category(FIRST_CATEGORY), PropertyOrder(20)]
        public int B
        {
            get { return 20; }
        }

        [Category(FIRST_CATEGORY), PropertyOrder(30)]
        public int A
        {
            get { return 30; }
        }
    
        [Category(SECOND_CATEGORY), PropertyOrder(10)]
        public int D
        {
            get { return 10; }
        }

        [Category(SECOND_CATEGORY), PropertyOrder(40)]
        public int C
        {
            get { return 40; }
        }
    }
}
