using System.ComponentModel;

namespace XProject.Core.Constants
{
    public class ApplicationConstants
    {
        public const string SYSTEM = "system";
        public const string MASTER = "master";

        public const string TOKEN2FA = "2fa";
    }

    public enum NotificationCategory
    {
        [Description("NONE")]
        None = 0,

        [Description("GENERAL")]
        General = 1,

        [Description("UPLOAD")]
        Upload = 2,

        [Description("DOWNLOAD")]
        Download = 3,
    }

    public enum ReadStatus
    {
        [Description("UNREAD")] UnRead = 0,
        [Description("READ")] Read = 1
    }

    public enum LogMemberType
    {
        [Description("LOGIN")]
        Login = 0,
        [Description("CREATE SUB USER")]
        CREATE_SUB_USER = 1,
        [Description("UPLOAD FILE")]
        UPLOAD_FILE = 2,
    }

    public enum LogSharingType
    {
        [Description("UPLOAD")]
        UPLOAD = 0,
        [Description("DOWNLOAD")]
        DOWNLOAD = 1,
        [Description("MODIFY")]
        MODIFY = 2,
        [Description("DELETE")]
        DELETE = 3,
    }

    public enum MemberPermission
    {
        [Description("User")]
        User = 0,
        [Description("Admin")]
        Admin = 1,
    }

    public class ClaimConstant
    {
        public const string CLAIM_USERNAME = "Username";
        public const string USER_ANONYMOUS = "Anonymous";
    }

    public class AuthenticationScheme
    {
        public const string BEARER = "Bearer";
        public const string BASIC = "Basic";
    }
}