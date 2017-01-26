using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace metronomeSimu.SingleCore
{
    public static class SinglePara
    {
        //整个摆锤的质量，单位：kg
        public static double m = 3e-2;
        //g
        public static double g = 9.8;
        //摆锤质心距转动轴距离，单位：m
        public static double lG = 2e-2;
        //摆锤相对轴心的转动惯量，单位：kgm2，显然要满足条件：m*lG*lG < I
        public static double I = 2e-5;
        //齿轮的半张角
        public static double theta0 = 2e-3;
        //开口的半张角，一定小于theta0
        public static double theta1 = 1e-3;
        //偏导数函数，自变量一定大于theta1
        public static double partialE(double theta)
        { 
            //这个曲面是二次曲线，零点在theta0处
            return 0.01 * (theta - theta1);
        }
        //这个是偏导数正负的函数
        public static int getD(int state, double theta)
        {
            switch (state)
            {
                case 1:
                    return (theta + theta0 > 0) ? 1 : -1 ;
                case -1:
                    return (theta - theta0 > 0) ? 1 : -1;
            }
            return 0;
        }
    }
}
