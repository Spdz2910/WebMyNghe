using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace _2001207328_NguyenDuyThinh.Models
{
    public class device_head
    {
        public List<Device> device { get; set; }
        public List<ICON> icon { get; set; }
        public Sheet1 details { get; set; }
    }
}