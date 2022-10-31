using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.RestPostEndPoint
{
    public class UsagePost
    {
        public long idDataUsage { get; set; }
        public long openDataId { get; set; }
        public DateTime dateOfUsage { get; set; }

        public int dataFormatId { get; set; }
        public int languageId { get; set; }
        public sbyte isDownloaded { get; set; }

        public virtual dataFormat dataFormat { get; set; }
        public virtual dataLanguage language { get; set; }
         public virtual OpenPost openData { get; set; }
    }
}