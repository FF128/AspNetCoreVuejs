using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Description("Language")]
    public class Language : BaseModel
    {
        public int Id { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageDesc { get; set; }
    }
}