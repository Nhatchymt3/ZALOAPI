using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XProject.Contract.Repository.Models
{
    public class File:Entity
    {
        public string Name { get; set; }
        public string SharingID { get; set; }
        public string Path { get; set; }
        public double Size { get; set; }
        public string Type { get; set; }
        public string Label { get; set; }
        public virtual Sharing Sharing { get; set; }

    }
}
