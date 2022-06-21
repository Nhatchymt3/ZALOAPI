using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XProject.Core.Constants;

namespace XProject.Contract.Repository.Models
{
    public class LogMember:Entity
    {
        public string MemberId { get; set; }
        public string Name { get; set; }
        public string login { get; set; }
        public DateTime DateTime { get; set; }
        public LogMemberType Type { get; set; }

        public virtual Member Member { get; set; }

     }
}
