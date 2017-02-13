using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace metronomeSimu.SingleCore
{
    public static class SinglePara
    {
        //调节滑块重量，单位：kg
        public const double mADJ = 0.006933;
        //调节滑块相对自己质心的转动惯量，单位：kgm2
        public const double IADJ = 3.70e-7;
        //复摆的重量，不算滑块，单位：kg
        public const double mBAI = 0.03617;
        //复摆自身的转动惯量，不算滑块，单位：kgm2
        public const double IBAI = 6.17e-5;
        //复摆自身的质心距转动轴的距离，单位：m
        public const double lcBAI = 0.026;
        //根据节拍器上的速度标号，生成动态的质心距转轴距离、转动惯量
        public static double getLfromV(double v)
        {
            return -0.6182e-3 * v + 147.43e-3; //返回拟合的直线，为滑块质心到转轴距离
            //输入是表盘上的速度
        }
        public static double getlG(double v = 60)
        {
            double l = getLfromV(v);
            return (mBAI * lcBAI - mADJ * l) / (mBAI + mADJ);
        }
        public static double getI(double v = 60)
        {
            double l = getLfromV(v);
            return IBAI + IADJ + mADJ * l * l;
        }


        //整个摆锤的质量，单位：kg
        public static double m = mBAI + 0;
        //g
        public static double g = 9.8;
        //摆锤质心距转动轴距离，单位：m
        //public static double lG = lcBAI;
        //摆锤相对轴心的转动惯量，单位：kgm2，显然要满足条件：m*lG*lG < I
        //public static double I = IBAI;
        //齿轮的半张角
        public static double theta0 = 2e-3;
        //开口的半张角，一定小于theta0
        public static double theta1 = 1e-3;
        //偏导数函数，自变量一定大于theta1
        public static double partialE(double theta)
        {
            //这个曲面是二次曲线，零点在theta0处
            //return 0.01 * (theta - theta1);
            return 0; //不给动力
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
