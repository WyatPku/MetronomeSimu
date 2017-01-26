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
        //文件头部由两个int，第一个是有几组数，另一个是每组数的长度
        public static void EncodeTo(String fileName, List<double[]> data)
        {
            if (!Directory.Exists(DefaultPath))
            {
                Directory.CreateDirectory(DefaultPath);
            }
            FileStream fs = new FileStream(DefaultPath + fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(data.Count);
            //选择最大的长度保存
            int Length = -1;
            foreach (double[] x in data)
            {
                if (Length == -1) Length = x.Length;
                if (x.Length > Length) Length = x.Length;
            }
            bw.Write(Length);
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
