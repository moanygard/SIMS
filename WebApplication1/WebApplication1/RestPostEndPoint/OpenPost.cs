using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.RestPostEndPoint
{
    public class OpenPost
    {
        public long idData { get; set; }
        public string dataUrl { get; set; }
        public sbyte dataOpenLicense { get; set; }
        public long dataOwnerId { get; set; }
        public int updateFrequencyId { get; set; }
        public int dataThemeId { get; set; }

        public virtual data_owner dataOwner { get; set; }
        public virtual data_theme dataTheme { get; set; }
        public virtual update_frequency updateFrequency { get; set; }
    }
}