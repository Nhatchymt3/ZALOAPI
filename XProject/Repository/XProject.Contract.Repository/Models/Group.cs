using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XProject.Contract.Repository.Models
{
    public class Group:Entity
    {
        public string MemberId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}
