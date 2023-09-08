using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2001207328_NguyenDuyThinh.Models
{
    public class New_Hot
    {
        public IEnumerable<_2001207328_NguyenDuyThinh.Models.CT_DonHang> banChay { get; set; }
        public IEnumerable<_2001207328_NguyenDuyThinh.Models.Device> moi { get; set; }
    }
}