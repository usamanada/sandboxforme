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
    public class BaseOrdered
    {
        protected const string FIRST_CATEGORY = "First";
        protected const string SECOND_CATEGORY = "Second";

        [Category(FIRST_CATEGORY), PropertyOrder(20)]
        public int B_base
        {
            get { return 20; }
        }

        [Category(FIRST_CATEGORY), PropertyOrder(30)]
        public int A_base
        {
            get { return 30; }
        }
    
        [Category(SECOND_CATEGORY), PropertyOrder(10)]
        public int D_base
        {
            get { return 10; }
        }

        [Category(SECOND_CATEGORY), PropertyOrder(40)]
        public int C_base
        {
            get { return 40; }
        }
    }

    //
    // The class definition must include the TypeConverter 
    // attribute to allow it to be ordered
    //
    [TypeConverter(typeof(PropertySorter))]
    public class InheritedOrdered : BaseOrdered
    {
        [Category(FIRST_CATEGORY), PropertyOrder(21)]
        public int B_inherited
        {
            get { return 21; }
        }
    
        [Category(FIRST_CATEGORY), PropertyOrder(22)]
        public int A_inherited
        {
            get { return 22; }
        }

        [Category(SECOND_CATEGORY), PropertyOrder(42)]
        public int D_inherited
        {
            get { return 42; }
        }

        [Category(SECOND_CATEGORY), PropertyOrder(41)]
        public int C_inherited
        {
            get { return 41; }
        }
    }
}
