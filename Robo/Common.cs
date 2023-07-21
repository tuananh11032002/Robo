using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Robo
{
    public class Common
    {
        public static string DocThongTin(string duongdan)
        {
            string strNoiDungFile = "";
            //Khai báo 1 luồng file
            FileStream fs = new FileStream(duongdan, FileMode.Open, FileAccess.Read);
            //Đưa vào luồng đủ đọc thông tin
            StreamReader reader = new StreamReader(fs);
            //Duyệt tất cả thông tin có trong file để đưa ra
            string dong = "";
            while (reader.Peek() >= 0)
            {
                dong = reader.ReadLine()+"\r\n";
                //Cộng gộp thông tin
                strNoiDungFile += dong;
            }
            reader.Close();
            fs.Close();
            return strNoiDungFile;
        }
        public static void GhiThongTin(string duongdan, string noidung)
        {
            FileInfo fi = new FileInfo(duongdan);
            FileStream fs = null;
            if (fi.Exists)
            {
                fs = new FileStream(fi.FullName, FileMode.Truncate);
            }
            else
            {
                fs = new FileStream(fi.FullName, FileMode.OpenOrCreate);
            }
            //Tạo 1 đối tượng để ghi file
            StreamWriter w = new StreamWriter(fs);
            w.Write(noidung);
            w.Flush();
            
            w.Close();
            fs.Close();
        }


    }
}
