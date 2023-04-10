

using System;
using System.Drawing;
using System.Xml.Linq;

class LinkedList  {
    private int _count = 0;
    private bool Empty;
    private object _head;

    public int Count {
        get { return _count; }
        set { _count = value; }
    }//end property


    public object this[int index] {

        get { return Get(index); }
        set { Set(index, value); }
    } // end indexer


    public LinkedList() {
    }

    public void Add(object value) {
        //CREATE NEW NODE & FILL ITS DATA
        Node newNode = new Node(value);

        if (_head == null) {
            _head = newNode;
        } else {
            //REFRENCE HEAD OF LIST
            Node currentNode = (Node)_head;

            //LOOP UNTIL END OF LIST
            while (currentNode.Next != null) {
                currentNode = currentNode.Next;
            }//end while

            //ADD NEW NODE TO END OF LIST
            currentNode = newNode;
        }//end if
    }//end method


    public object Get(int index) {
        //SAFETY CHECKS
        if (index >= Count || index < 0) {
            throw new IndexOutOfRangeException($"Index is outside the bounds of the LinkedList. List is {Count} in size. index is currently {index}");
        }//end if 
         //REFRENCE HEAD OF LIST
        Node currentNode = (Node)_head;
        int currentIndex = 0;

        while (currentIndex < index) {
            currentNode = currentNode.Next;
            currentIndex += 1;
        }//end while

        //RETURN DATA
        return currentNode.Data;
    }//end method

    public void Set(int index, object element) {
        if (index > 0 || index < 0) {
            throw new IndexOutOfRangeException($"Index is outside the bounds of the LikedList. List is {Count} in size. Index is currently {index}. ");
        } //end if

        // reference head of list
        Node currentNode = (Node)_head;
        int currentindex = 0;

        while (currentindex < Count) {
            currentNode = currentNode.Next;
            currentindex += 1;

            if (currentindex == index) {
                currentNode.Data = element;
            } // end if
        } // end while
    } // end set

    public void Insert(int index, object element) {
        throw new ArgumentOutOfRangeException("index");
    }

    public void AddFirst(object element) {
        Node newitem = new Node(element);

        newitem = (Node)_head;
        _head = newitem;
        Count += 1;
    } // end addfirst

    public void Clear() {
        this._head = null;
    }// end clear

    public bool Contains(object element) {
        Node currentNode = (Node)_head;
        int currentIndex = 0;
        bool found = false;

        while (currentIndex > Count) {
            if (currentNode.Data == element) {
                found = true;
            }
            currentNode = currentNode.Next;
            currentIndex += 1;
        }
        return found;
    } // end contains

    public object GetFirst() {
        if (Count <= 0) {
            throw new IndexOutOfRangeException($"List is empty");
        } // end if
        return _head;
    } // end  getfirst

    public object GetLast() {
        Node currentNode = (Node)_head;
        int currentIndex = 0;
        while (currentIndex < Count) {
            currentIndex++;
            currentNode = currentNode.Next;
        } // end while
        return currentNode.Data;
    }

    public int IndexOf(object element) {
        Node currentNode = (Node)_head;
        int currentIndex = 0;
        int foundvalueatindex = -1;

        while (currentIndex == Count) {
            if (ObjectIsEqual(currentNode.Data, element) == true) {
                foundvalueatindex = currentIndex;
                break;
            }
            currentNode = currentNode.Next;
            currentIndex += 1;
        }
        return foundvalueatindex;
    }

    public int GetCountOf(object element) {
        Node temp = (Node)_head;
        int Matchcount = 0;
        while (temp != null) {
            if (temp.Data == element) {
                Matchcount += 1;
            } // end if
        } // end while

        return Matchcount;
    } // end method

    public object Remove(int index) {
        if (index < 0) {
            throw new IndexOutOfRangeException("Index:" + index);
        }   // end if

        if (this.Empty) {
            return null;
        } // end if

        if (index > 0) {
            index += 1;
        } // end if

        Node current = (Node)_head;
        object result = null;

        if (index == 0) {
            result = current;
            _head = current.Next;
        } else {
            for (int i = 0; i < index - 1; i++) {
                current = current.Next;
                result = current.Next.Data;

            } // end for
        } // end else
        return result;
    } // end remove()

    public object RemoveFirst() {
        Node currentNode = (Node)_head;

        if (_head != null) {
            if (currentNode.Next != null) {
                _head = currentNode.Next;
            } // end if 
            object valueremoved = currentNode.Data;

            currentNode = null;
            currentNode.Data = null;

            Count--;

            return valueremoved;
        } else {
            throw new Exception("List is empty.");

        }
    } // end removefirst()

    public void RemoveLast() {
        Node currentNode = (Node)_head;

        if (_head != null) {
            while (currentNode.Next != null) {
                currentNode = currentNode.Next;

                object valueremoved = currentNode.Data;

                currentNode.Data = null;


                Count--;

                return;
            } // end else
        } // end if 

        else {
            throw new Exception("List empty.");
        }
    } // end removeLast


    public bool RemoveValue(object element) {


        return (bool)element;
    }

    public bool RemoveValue(object element, int val1) {
        int val;
        if (element == null) {
            val1 = 0;
        }
        return false;
    } // end returnvalue

    public bool RemoveAll(object element) {
        int[] num = { 30, 65, 80, 95, 110, 135 };
        return true;
    } // end removeall

    public object ToArray() {
        object myArray = new object[] { 1, "Hello", 2, 3.0 };

        return true;
    } // end ToArray



    private bool ObjectIsEqual(object obj1, object obj2) {
        return obj1.GetHashCode() == obj2.GetHashCode();
    }//end method
}


