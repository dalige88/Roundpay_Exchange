using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoundpayFinTech.AppCode.Model
{
    public class WebsiteMaster
    {
        [Key]
        public int _ID { get; set; }
        public int _UserId { get; set; }
        public string _ReferralContent { get; set; }
    }
}
