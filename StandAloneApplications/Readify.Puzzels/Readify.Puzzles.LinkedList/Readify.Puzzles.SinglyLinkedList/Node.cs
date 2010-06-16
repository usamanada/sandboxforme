using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Readify.Puzzles.SinglyLinkedList
{
    public class Node<T>
    {
        #region properties

        public T NodeValue { get; set; }
        public Node<T> NextNode { get; set; }

        #endregion

        #region Constructor

        public Node()
        {  
            NodeValue = default(T);
            NextNode = null;  
        }  
        
        public Node(T nodeValue)
        {  
            this.NodeValue = nodeValue;
            NextNode = null;
        }
        
        #endregion
    }
}
