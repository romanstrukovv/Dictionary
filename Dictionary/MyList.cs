using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    public class NodeList
    {
        public int _inf; //значение в узле
        public NodeList _next; //следующий узел

        public NodeList(int inf, NodeList next)
        {
            this._inf = inf;
            this._next = next;
        }
    }
    public class MyList
    {
        public static NodeList _head;  //узел головы
        public static int _count;  // количество элементов в списке
        public static NodeList p = new NodeList(0, _head);

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public MyList()
        {
            _head = null;
            _count = 0;
        }

        /// <summary>
        /// Добавляет элемент в начало списка
        /// </summary>
        /// <param name="element"></param>
        public void Add(int element)
        {
            NodeList x = new NodeList(element, _head);
            _head = x;

            _count++;
        }

        /// <summary>
        /// Индексация списка
        /// </summary>        
        public int this[int i]
        {
            set
            {
                if (_head != null)
                {
                    if (i == 0)
                    {
                        p = _head;
                        p._inf = value;
                    }
                    else
                    {
                        p = _head;
                        for (int k = 0; k < i; k++)
                            p = p._next;

                        p._inf = value;
                    }
                }
                else throw new ArgumentOutOfRangeException();
            }

            get
            {
                if (_head != null)
                {
                    if (i == 0)
                    {
                        p = _head;
                        return p._inf;
                    }
                    else
                    {
                        p = _head;
                        for (int k = 0; k < i; k++)
                            p = p._next;

                        return p._inf;
                    }
                }
                else throw new ArgumentOutOfRangeException();
            }

        }

        public int Count
        {
            get
            {
                if (_count >= 0)
                    return _count;
                else throw new Exception("The list is empty");
            }
        }
    }
}

