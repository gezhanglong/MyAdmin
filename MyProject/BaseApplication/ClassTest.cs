using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApplication
{
    public delegate void MyDelegate();
    public interface IMy
    {
        //索引
        char this[int index]
        {
            get;
        }
        //属性
        string GetStr { get; set; }

        //方法
        void PrintStr();

        //事件
        event MyDelegate MyEvent;
    }

    public abstract class absClass
    {
        public  string str;
       
        public abstract  string strs { get; set; }
       
        public abstract void Print();

        public abstract char this[int index] { get; }

        abstract public  event MyDelegate MyEvent;
    }
    public class ClassTest : IMy
    {
        private string _string;
        public ClassTest(string str)
        {
            _string = str;
        }
        public char this[int index]
        {
            get
            {
                return _string[index];
            }
        }

        public string GetStr
        {
            get
            {
                return _string;
            }

            set
            {
                value = _string;
            }
        }

        public event MyDelegate MyEvent;

        public void Print()
        {
            if(MyEvent !=null)
            {
                MyEvent();
            }
        }

        public void PrintStr()
        {
            Console.Write(_string);
        }
    }

    public class A
    {
        ClassTest c;
        public A(ClassTest c)
        {
            c.MyEvent += new MyDelegate(Print);
        }
        public void Print()
        {
            Console.Write("event print");
        }
    }
}
