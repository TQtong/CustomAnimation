using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Wpfnet5
{
    public  class AntiShake
    {
        public  void StartTest()
        {
            int i= 1000;
            while (i-->0)
            {
                AntiShakeFun2(TestFun, 1000);
                Thread.Sleep(10);
            }
        }


        //希望当频繁调用时该函数100ms执行一次
        public  void TestFun()
        {
            System.Diagnostics.Debug.WriteLine("aaa");
        }

        bool worked = false;
        public void AntiShakeFun(Action fun, int delay)
        {
            if(!worked)
            {
                fun.Invoke();
                worked = true;
                Task.Run(() =>
                {
                    Thread.Sleep(delay);
                    worked = false;
                });
            }
        }

        void ElapsedEventHandler(object sender, ElapsedEventArgs e)
        {
            fun?.Invoke();
        }
        System.Timers.Timer  timer = new System.Timers.Timer();
        Action fun;
        public AntiShake()
        {
            timer.AutoReset = false;
            timer.Elapsed += ElapsedEventHandler;
        }
        public  void AntiShakeFun2(Action pfun, int delay)
        {

            //判断当前是否在等待
            if (timer.Enabled)
            {
                //timer.Stop();
                //timer.Start();
            }else
            {
                fun = pfun;
                timer.Interval = delay;
                timer.Start();
            }
            //在等待，则重置等待时间
            //不在等待，则开始等待
            
                
        }


    }
}
