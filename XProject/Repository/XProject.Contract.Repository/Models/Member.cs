using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XProject.Core.Constants;

namespace XProject.Contract.Repository.Models
{
	public class Member : Entity
	{
		public Member()
		{
			LogMember = new HashSet<LogMember>();
			//SubMembers = new HashSet<Member>();
		}

		public string UserName { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string RootFolder { get; set; }
		public string GroupId { get; set; }
		public MemberPermission Permission { get; set; }
		public string CompanyName { get; set; }
		public int? LimitedDays { get; set; }
		public bool IsAuth { get; set; }
		public decimal Capacity { get; set; }

		public virtual ICollection<LogMember> LogMember { get; set; }
		public virtual Group Group { get; set; }

		public string ParentId { get; set; }
		//public virtual ICollection<Member> SubMembers { get; set; }
	}
}
