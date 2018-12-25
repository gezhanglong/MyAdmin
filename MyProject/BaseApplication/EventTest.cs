using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApplication
{
    
    public delegate void DelegateRaise(string hand);
    public delegate void DelegateFall();
    public class EventTest
    {
        

        public event DelegateRaise EventRaise;
        public event DelegateFall EventFall;

        public void Raise(string hand)
        {
            if(EventRaise !=null)
            {
                EventRaise(hand);
            }

        }
        public void Fall ()
        {
            if(EventFall !=null)
            {
                EventFall();
            }
        }

    }

    

    public class C
    {
        private EventTest e;
        public C(EventTest e)
        {
            e.EventRaise += new DelegateRaise(C_Raise);
            e.EventFall += new DelegateFall(C_Fall);
        }
        public void C_Raise(string hand)
        {
            if(hand=="right")
            {
                Console.Write("举起右手，攻击");
            }
        }

        public void C_Fall()
        {
            Console.Write("摔杯子，攻击");
        }
    }

    public class D
    {
        private EventTest e;
        public D(EventTest e)
        {
            e.EventRaise += new DelegateRaise(D_Raise);
            e.EventFall += new DelegateFall(D_Fall);
        }
        public void D_Raise(string hand)
        {
            if (hand == "left")
            {
                Console.Write("举起左手，攻击");
            }
        }

        public void D_Fall()
        {
            Console.Write("摔杯子，攻击");
        }
    }
}
