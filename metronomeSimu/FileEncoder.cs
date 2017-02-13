using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace metronomeSimu
{
    public static class FileEncoder
    {
        public static String DefaultPath = "data/";
        //文件头部有两个int，第一个是有几组数，另一个是每组数的长度，还有一个double是没一个数据的时间间隔
        public static void EncodeTo(String fileName, List<double[]> data, double showDeltaT)
        {
            if (!Directory.Exists(DefaultPath))
            {
                Directory.CreateDirectory(DefaultPath);
            }
            FileStream fs = new FileStream(DefaultPath + fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(data.Count);
            //选择最小的长度保存
            int Length = data[0].Length;
            foreach (double[] x in data)
            {
                if (x.Length < Length) Length = x.Length;
            }
            bw.Write(Length);
            bw.Write(showDeltaT);
            foreach (double[] x in data)
            {
                for (int i=0; i<Length; i++)
                {
                    if (i < x.Length)
                    {
                        bw.Write(x[i]);
                    }
                }
                bw.Flush();
            }
            bw.Close();
            fs.Close();
        }
    }
}
