using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XProject.Core.Constants;

namespace XProject.Contract.Repository.Models
{
    public class LogSharing :Entity
    {
        public string SharingId { get; set; }
        public string Name { get; set; }
        public string IsDowload { get; set; }
        public LogSharingType Type { get; set; }
        public virtual Sharing Sharing { get; set; }
    }
}
