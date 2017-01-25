using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace metronomeSimu.SingleCore
{
    public class MetronomeCore
    {
        public int state; // 1在右边，-1在左边
        double theta;
        double thetadot;
        public double A;
        public double B;
        public double getTheta() => theta;
        public MetronomeCore(double theta_)
        {
            //需要指定初始化角度
            theta = theta_;
            //默认静止释放
            thetadot = 0;
            state = -1;
            A = 0;
            B = 0;
        }
        public void afterdeltaT(double deltaT)
        {
            //deltaT越小越精确，综合考虑即可
            double thetaABS = Math.Abs(SinglePara.theta0 + state * theta);
            if (thetaABS < SinglePara.theta1)
            {
                state = -state;
                thetaABS = Math.Abs(SinglePara.theta0 + state * theta);
            }
            A = (- SinglePara.partialE(thetaABS)*SinglePara.getD(state, theta)
                - SinglePara.m * SinglePara.g * SinglePara.lG * Math.Sin(theta)) 
                / SinglePara.I;
            B = SinglePara.m * SinglePara.lG * Math.Cos(theta) / SinglePara.I;
            double thetadotdot = A - B * 0;
            thetadot += thetadotdot * deltaT;
            theta += thetadot * deltaT;
            //MessageBox.Show("theta = " + theta + "\r\nthetadot = " + thetadot +
              //  "\r\nthetadotdot = " + thetadotdot);
        }
    }
}
