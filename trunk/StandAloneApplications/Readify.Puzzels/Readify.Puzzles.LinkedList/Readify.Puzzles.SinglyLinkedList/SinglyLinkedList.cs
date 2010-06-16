using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Readify.Puzzles.SinglyLinkedList
{
    public class SinglyLinkedList <T>
    {
        #region private members
        private int length;
        private Node<T> head;
        private Node<T> tail;
        #endregion

        #region public properties
        public int Length { get { return length; } }
        public Node<T> Head { get { return head; } }
        public Node<T> Tail { get { return tail; } }
        #endregion

        #region constructor
        public SinglyLinkedList()
        { 
            length = 0;
            head = null;
            tail = null;
        }
        #endregion

        #region public methods

        public void AddNode(T nodeValue)
        { 
            Node<T> n = new Node<T>(nodeValue);
            
            if (length < 1)
            {
                head = n;
            }
            else
            {
                tail.NextNode = n;
            }

            tail = n;
            ++length;
        }

        public Node<T> FindNodeFromtTail (int index)
        {
            validateArgument(index);

            int actualIndex = length - (index + 1);
            return FindNode(actualIndex);
        }

        public Node<T> FindNode(int index)
        {
            if(validateArgument(index))
            {
                Node<T> node;
                for (node = head; index > 0; index--)
                {
                    if (node.NextNode != null)
                    {
                        node = node.NextNode;
                    }
                }
                return node;
            }
            return null;
            
        }

        #endregion

        #region private methods

        private bool validateArgument(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("Index", index, "Index must be greater than or equal to 0");
            }
            else if(index >= length)
            {
                throw new ArgumentOutOfRangeException("Index", index, "Index was out of range. Must be non-negative and less than the size of the List.");
            }
            return true;

        }

        #endregion
    }
}
