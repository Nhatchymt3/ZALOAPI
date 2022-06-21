using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XProject.Contract.Repository.Models
{
    public class Company : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmailPrefix { get; set; }
        public string Address { get; set; }
        public int TotalStaff { get; set; }
    }
}
