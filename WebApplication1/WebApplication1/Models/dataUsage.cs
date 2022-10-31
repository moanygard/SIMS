using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class dataUsage
    {
        public long id_data_usage { get; set; }
        public long open_data_id { get; set; }
        public DateTime date_of_usage { get; set; }
        public int data_format_id { get; set; }
        public bool is_downloaded { get; set; }

        public openData OpenD { get; set; }
    }
}