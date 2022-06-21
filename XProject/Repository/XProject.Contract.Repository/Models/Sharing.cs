using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XProject.Contract.Repository.Models
{
    public class Sharing : Entity
    {
		public string Label { get; set; }
		public string FolderName { get; set; }
		public string RootFolder { get; set; }
		public double? FolderSize { get; set; }
		public string Path { get; set; }
		public string ParentID { get; set; }
		public string GroupId { get; set; }

        public virtual ICollection<LogSharing>  LogSharings { get; set; }
		public virtual ICollection<File> Files { get; set; }

	
	}
}
