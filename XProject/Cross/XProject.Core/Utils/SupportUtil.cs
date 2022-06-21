using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace XProject.WebApi.Common
{
    public static class SupportUtil
    {
        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
        public static void sendmail()
        {
            var fromAddress = new MailAddress("from@gmail.com", "From Name");
            var toAddress = new MailAddress("to@example.com", "To Name");
            const string fromPassword = "fromPassword";
            const string subject = "Subject";
            const string body = "Body";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

    }
    public class CacheWrapper
    {
        private readonly IMemoryCache _memoryCache;
        private CancellationTokenSource _resetCacheToken = new();

        public CacheWrapper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Add(object key, object value, MemoryCacheEntryOptions memoryCacheEntryOptions)
        {
            using var entry = _memoryCache.CreateEntry(key);
            entry.SetOptions(memoryCacheEntryOptions);
            entry.Value = value;

            // add an expiration token that allows us to clear the entire cache with a single method call
            entry.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
        }

        public void Clear()
        {
            _resetCacheToken.Cancel(); // this triggers the CancellationChangeToken to expire every item from cache

            _resetCacheToken.Dispose(); // dispose the current cancellation token source and create a new one
            _resetCacheToken = new CancellationTokenSource();
        }
    }
}
