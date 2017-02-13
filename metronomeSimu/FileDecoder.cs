using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace metronomeSimu
{
    class FileDecoder
    {
        public static String DefaultPath = "data/";
        //文件头部由两个int，第一个是有几组数，另一个是每组数的长度
        public static double showDeltaT;
        public static List<double[]> tryDecode(String fileName)
        {
            if (!File.Exists(DefaultPath + fileName)) return null;
            FileStream fs = new FileStream(DefaultPath + fileName, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            int Count = br.ReadInt32();
            int Length = br.ReadInt32();
            showDeltaT = br.ReadDouble();
            List<double[]> data = new List<double[]>();
            for (int i=0; i<Count; i++)
            {
                double[] x = new double[Length];
                for (int j=0; j<Length; j++)
                {
                    x[j] = br.ReadDouble();
                }
                data.Add(x);
            }
            br.Close();
            fs.Close();
            return data;
        }
    }
}
