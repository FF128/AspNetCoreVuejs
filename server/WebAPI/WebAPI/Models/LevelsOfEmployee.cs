using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Description("Levels of Employee")]
    public class LevelsOfEmployee : BaseModel
    {
        public int Id { get; set; }
        public string LOECode { get; set; }
        public string LOEDesc { get; set; }
    }
}