using Microsoft.EntityFrameworkCore;
using XProject.Contract.Repository.Models;

namespace XProject.Repository.Infrastructure
{
    public sealed partial class AppDbContext
    {
        public DbSet<Member> members { get; set; }
        public DbSet<Sharing> sharings { get; set; }
        public DbSet<MappingSharingMember> mappingSharingMembers { get; set; }
        public DbSet<LogSharing> logSharings { get; set; }
        public DbSet<LogMember> logMembers { get; set; }
        public DbSet<Group> groups { get; set; }
        public DbSet<File> files { get; set; }
        public DbSet<Company> companies { get; set; }
    }
}