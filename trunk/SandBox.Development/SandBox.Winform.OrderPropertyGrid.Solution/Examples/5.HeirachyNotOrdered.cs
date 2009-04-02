using System;
using System.ComponentModel;

namespace OrderedPropertyGrid.Examples
{
    public class InheritedNotOrdered : BaseNotOrdered
    {
        [Category(FIRST_CATEGORY)]
        public int B_inherited
        {
            get { return 0; }
        }
       
        [Category(FIRST_CATEGORY)]
        public int A_inherited
        {
            get { return 0; }
        }
    
        [Category(SECOND_CATEGORY)]
        public int D_inherited
        {
            get { return 0; }
        }

        [Category(SECOND_CATEGORY)]
        public int C_inherited
        {
            get { return 0; }
        }
   }

    [DefaultProperty("B_base")]
    public class BaseNotOrdered
    {
        protected const string FIRST_CATEGORY = "First";
        protected const string SECOND_CATEGORY = "Second";

        [Category(FIRST_CATEGORY)]
        public int B_base
        {
            get { return 0; }
        }

        [Category(FIRST_CATEGORY)]
        public int A_base
        {
            get { return 0; }
        }
    
        [Category(SECOND_CATEGORY)]
        public int D_base
        {
            get { return 0; }
        }
    
        [Category(SECOND_CATEGORY)]
        public int C_base
        {
            get { return 0; }
        }
    }
}