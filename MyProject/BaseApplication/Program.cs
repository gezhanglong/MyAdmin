using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApplication
{
    class Program
    {
        public delegate void Ndelegate(int x);

        static void Main(string[] args)
        {
            #region EventTest
            //EventTest e = new BaseApplication.EventTest(); 
            //C c = new C(e);
            //D d = new D(e);
            //e.Raise("left");
            //e.Fall();
            #endregion

            #region interface接口
            //ClassTest classtest = new ClassTest("zhanglong");
            //A a = new A(classtest);
            //Console.Write(classtest[3]);//索引
            //Console.Write(classtest.GetStr);//属性
            //classtest.Print();//事件
            //classtest.PrintStr();//方法 
            #endregion

            #region abstract/virtual
            //ClassTest_Abstract ab = new ClassTest_Abstract("zhanglong");
            //A_a aa = new A_a(ab);
            //Console.Write(ab[3]);//索引
            //Console.Write(ab.strs);//属性
            //ab.PrintEvent();//事件
            //ab.BasePrint();//虚方法 
            //ab.Print();//方法  
            #endregion


            Ndelegate nc = delegate (int x)
            {
                Console.Write("haha"); 
            };

            nc(1);
            nc(2);
            Console.Read();
        }
    }
}
