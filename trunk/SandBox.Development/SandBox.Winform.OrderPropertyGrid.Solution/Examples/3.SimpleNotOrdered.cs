using System;
using System.ComponentModel;

namespace OrderedPropertyGrid.Examples
{
    [DefaultProperty("B_base")]
    public class SimpleNotOrdered
    {
        protected const string FIRST_CATEGORY = "First";
        protected const string SECOND_CATEGORY = "Second";

        [Category(FIRST_CATEGORY)]
        public int B
        {
            get { return 0; }
        }

        [Category(FIRST_CATEGORY)]
        public int A
        {
            get { return 0; }
        }
    
        [Category(SECOND_CATEGORY)]
        public int D
        {
            get { return 0; }
        }
    
        [Category(SECOND_CATEGORY)]
        public int C
        {
            get { return 0; }
        }
    }
}
