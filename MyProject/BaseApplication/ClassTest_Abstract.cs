using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApplication
{
    public delegate void YouDelegate();
   

    public abstract class AbstractClass
    {
        private string str;
        public AbstractClass(string _string)
        {
            str = _string;
        }
       
        public abstract  string strs { get; set; }
       
        public abstract void Print();

        public abstract char this[int index] { get; }

        public abstract event MyDelegate MyEvent;
        public virtual void BasePrint()
        {
            Console.Write("父类打印");
        }
    }
    public class ClassTest_Abstract : AbstractClass
    {
        private string _string;
        public ClassTest_Abstract(string str):base(str)
        {
            _string = str;
        }
        public override char this[int index]
        {
            get
            {
                return _string[index];
            }
        }

        public override string strs
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

        public override event MyDelegate MyEvent;

        public void PrintEvent()
        {
            if(MyEvent !=null)
            {
                MyEvent();
            }
        }

        public override void Print()
        {
            Console.Write(_string);
        }

        public override void BasePrint()
        {
            Console.Write("子类打印");
        }
    }

    public class A_a
    {
        ClassTest_Abstract c;
        public A_a(ClassTest_Abstract c)
        {
            c.MyEvent += new MyDelegate(Print);
        }
        public void Print()
        {
            Console.Write("event print");
        }
    }
}
