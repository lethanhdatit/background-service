using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace background_service.Models
{
    public class JobConfig
    {
        public bool IsRefireWhenFail { get; set; }
        public int RefireLimit { get; set; }

    }
}
