using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XProject.Contract.Repository.Models
{
    public class MappingSharingMember:Entity
    {
        public string MemberId { get; set; }
        public string SharingId { get; set; }
        public int RW { get; set; }
        public bool IsOwner { get; set; }
        public DateTimeOffset? Expiration_date { get; set; }
        public virtual Member  Member { get; set; }
        public virtual Sharing Sharing { get; set; }

    }
}
