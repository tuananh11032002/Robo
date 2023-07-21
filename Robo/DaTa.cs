using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robo
{
    public class DaTa
    {
        public string KichThuoc { get; set; }
        public long ThoiGian { get; set; }
        public DaTa(string s, long tg)
        {
            this.KichThuoc = s;
            this.ThoiGian = tg;
        }
    }
}
