using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;



namespace Dictionary
{

    public class NodeDic<T>
    {
        public NodeDic<T> parent;
        public NodeDic<T> left;
        public NodeDic<T> right;
        public T val;
        public int key;

        public NodeDic(int key, T val)
        {
            this.val = val;
            this.key = key;
        }

        public NodeDic()
        {
            this.val = default(T);
            this.left = null;
            this.right = null;
            this.parent = null;
            this.key = int.MaxValue;
        }
    }

    public class MyDictionary<Tval> : NodeDic<Tval>
    {

        public NodeDic<Tval> top;
        int count = 0;
        int capacity = int.MaxValue;
        public static StreamWriter sw = File.CreateText("dicGraphvis.gv");


        /// <summary>
        /// Дефолт конструктор
        /// </summary>
        public MyDictionary()
        {
            top = null;
            this.count = 0;
            this.capacity = 10;
        }

        /// <summary>
        /// (ключ головы, значение головы, вместительность словаря)
        /// </summary>
        /// <param name="capacity"></param>
        public MyDictionary(int key, Tval val, int capacity)
        {
            this.top = null;
            this.capacity = capacity;
            this.count = 1;
        }

        /// <summary>
        /// Количество элементов в словаре
        /// </summary>
        public int Count
        {
            get
            {
                if (count < 0)
                    throw new Exception("The dictionary is empty");
                if (count > capacity)
                    throw new ArgumentOutOfRangeException();
                else return count;
            }
        }

        /// <summary>
        /// Емкость словаря
        /// </summary>
        public int Capacity
        {
            get
            {
                return capacity;
            }
        }

        /// <summary>
        /// Добавление элемента в словарь по ключу
        /// </summary>
        /// <param name="val"></param>
        /* public void Add(int key, Tval val)
         {
             bool added;

             if (top == null)
             {
                 NodeDic<Tval> NewNode = new NodeDic<Tval>(val);
                 top.key = key;
                 top = NewNode;
                 count++;
                 return;
             }

             NodeDic<Tval> currentNode = top;
             added = false;

             do
             {
                 if ((currentNode.left == null) || (currentNode.right == null))
                 {
                     if ((currentNode.left == null) && (currentNode.right != null))
                     {
                         if (key == currentNode.key) throw new Exception("The key already exists");

                         if (key < currentNode.key)
                         {
                             NodeDic<Tval> NewNode = new NodeDic<Tval>(val);
                             currentNode.left = NewNode;
                             currentNode.left.key = key;
                             sw.WriteLine(currentNode.val + "->" + currentNode.left.val + "[color=red]" + ";");
                             count++;
                             added = true;
                             return;
                         }

                         if (key > currentNode.key) currentNode = currentNode.right;
                     }

                     if ((currentNode.right == null) && (currentNode.left != null))
                     {
                         if (key == currentNode.key) throw new Exception("The key already exists");

                         if (key > currentNode.key)
                         {
                             NodeDic<Tval> NewNode = new NodeDic<Tval>(val);
                             currentNode.right = NewNode;
                             currentNode.right.key = key;
                             sw.WriteLine(currentNode.val + "->" + currentNode.right.val + "[color=red]" + ";");
                             count++;
                             added = true;
                             return;
                         }
                         if (key < currentNode.key) currentNode = currentNode.left;
                     }

                     if ((currentNode.left == null) && (currentNode.right == null))
                     {
                         if (key == currentNode.key) throw new Exception("The key already exists");

                         if (key < currentNode.key)
                         {
                             NodeDic<Tval> NewNode = new NodeDic<Tval>(val);
                             currentNode.left = NewNode;
                             currentNode.left.key = key;
                             sw.WriteLine(currentNode.val + "->" + currentNode.left.val + "[color=red]" + ";");
                             count++;
                             added = true;
                             return;
                         }
                         if (key > currentNode.key)
                         {
                             NodeDic<Tval> NewNode = new NodeDic<Tval>(val);
                             currentNode.right = NewNode;
                             currentNode.right.key = key;
                             sw.WriteLine(currentNode.val + "->" + currentNode.right.val + "[color=red]" + ";");
                             count++;
                             added = true;
                             return;
                         }
                     }
                 }
                 else
                 {
                     if (key < currentNode.key)
                         currentNode = currentNode.left;
                     if (key < currentNode.key)
                         currentNode = currentNode.right;
                 }
             } while (!added);

         }*/

        /// <summary>
        /// Добавление элемента по ключу
        /// </summary>
        /// <param name="nextNode"></param>
        public void Add(NodeDic<Tval> nextNode)
        {

            if (top == null)
            {
                top = nextNode;
                top.key = nextNode.key;
                top.val = nextNode.val;
                nextNode.parent = null;
                count++;
                return;
            }

            NodeDic<Tval> currentNode = top;
            NodeDic<Tval> previousNode = null;

            while (currentNode != null)
            {
                previousNode = currentNode;

                if (nextNode.key > currentNode.key)
                    currentNode = currentNode.right;

                else currentNode = currentNode.left;
            }

            if (nextNode.key < previousNode.key)
            {
                previousNode.left = nextNode;
                sw.WriteLine(previousNode.key + "->" + nextNode.key + "[color=red]" + ";");
            }

            else
            {
                previousNode.right = nextNode;
                sw.WriteLine(previousNode.key + "->" + nextNode.key + "[color=red]" + ";");
            }

            nextNode.parent = previousNode;
            count++;
            return;

        }

        /// <summary>
        /// Поиск элемента по ключу
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Tval Find(int key)
        {
            NodeDic<Tval> currentNode = top;

            while (currentNode != null)
            {
                if (currentNode.key == key)
                    return currentNode.val;
                if (key < currentNode.key)
                    currentNode = currentNode.left;
                if (key > currentNode.key)
                    currentNode = currentNode.right;
            }

            throw new Exception("The dictionary is empty");
        }

        /// <summary>
        /// Удаление элемента
        /// </summary>
        /// <param name="key"></param>
        public void Remove(int key)
        {
            NodeDic<Tval> currentNode = top;
            NodeDic<Tval> parent = null;

            while (currentNode != null && currentNode.key != key)
            {
                parent = currentNode;
                if (key < currentNode.key)
                    currentNode = currentNode.left;
                else
                    currentNode = currentNode.right;
            }

            if (currentNode != null)
            {
                NodeDic<Tval> removed = null;

                if (currentNode.left == null || currentNode.right == null)
                {
                    NodeDic<Tval> child = null;
                    removed = currentNode;

                    if (currentNode.left != null)
                        child = currentNode.left;
                    else if (currentNode.right != null)
                        child = currentNode.right;

                    if (parent == null)
                        top = child;
                    else
                    {
                        if (parent.left == currentNode)
                            parent.left = child;
                        else
                            parent.right = child;
                    }
                }
                else
                {
                    NodeDic<Tval> mostLeft = currentNode.right;
                    NodeDic<Tval> mostLeftParent = currentNode;

                    while (mostLeft.left != null)
                    {
                        mostLeftParent = mostLeft;
                        mostLeft = mostLeft.left;
                    }

                    currentNode.key = mostLeft.key;
                    removed = mostLeft;

                    if (mostLeftParent.left == mostLeft)
                        mostLeftParent.left = null;
                    else
                        mostLeftParent.right = null;
                }
            }
        }

        public void beginOfFile()
        {
            sw.WriteLine("digraph {");
        }

        public void endOfFile()
        {
            sw.WriteLine("}");
            sw.Close();
        }
    }
}


/*   public Tval Find(int key)
           {
               if (top != null)
               {
                   if (key == 0)
                   {
                       return top.val;
                   }
                   else
                   {
                       NodeDic<Tval> currentNode = top;
                       if ((key % 2 == 0) && (key > 0))
                       {
                           while (currentNode.key < key)
                           {
                               currentNode = currentNode.right;
                           }
                           return currentNode.val;
                       }

                       if ((key % 2 != 0) && (key > 0))
                       {
                           while (currentNode.key < key)
                           {
                               currentNode = currentNode.left;
                           }
                           return currentNode.val;
                       }
                       else throw new Exception("The key is below zero");
                   }
               }
               else throw new Exception("The dictionary is empty");
           }*/


/*    public Tval this[int key]
    {
        set
        {
            if (top != null)
            {
                if (key == top.key)
                {
                    top.val = value;
                }
                else
                {
                    NodeDic<Tval> currentNode = top;

                    while ((currentNode.left != null) && (currentNode.right != null))
                    {
                        if (key > currentNode.key)
                            currentNode = currentNode.right;

                        if (key < currentNode.key)
                            currentNode = currentNode.left;

                        if (key == currentNode.key)
                        {
                            currentNode.val = value;
                        }
                    }
                }
            }
            else throw new Exception("The dictionary is empty");
        }
        get
        {
            if (top != null)
            {
                if (key == top.key)
                {
                    return top.val;
                }

                NodeDic<Tval> currentNode = top;

                while ((currentNode.left != null) || (currentNode.right != null))
                {
                    if (key > currentNode.key)
                        currentNode = currentNode.right;

                    if (key < currentNode.key)
                        currentNode = currentNode.left;

                    if (key == currentNode.key)
                    {
                        return currentNode.val;
                    }
                }
                if ((currentNode.left == null) && (currentNode.right == null))
                {
                    if (key == currentNode.key)
                        return currentNode.val;

                    else throw new Exception("No such key");
                }
            }

            else throw new Exception("The dictionary is empty");
        }
    }*/

/* public Tval Find(NodeDic<Tval> currentNode, int key)
 {
     if (key != currentNode.key)
     {
         if ((currentNode.left != null) || (currentNode.right != null))
         {
             if ((currentNode.left != null) && (key < currentNode.key))
                 Find(currentNode.left, key);

             if ((currentNode.left == null) && (key < currentNode.key))
                 throw new Exception("No such key");

             if ((currentNode.right != null) && (key > currentNode.key))
                 Find(currentNode.right, key);

             if ((currentNode.right == null) && (key > currentNode.key))
                 throw new Exception("No such key");
         }

         if ((currentNode.left == null) && (currentNode.right == null))
             throw new Exception("No such key");
     }
     return currentNode.val;
 }*/

/* public Tval GoFind(int key)
 {
     if (top != null)
     {
         NodeDic<Tval> resultNode = new NodeDic<Tval>();

         resultNode.val = Find(top, key);

         return resultNode.val;
     }
     else throw new Exception("The dictionary is empty");
 }*/