using System.Security.Cryptography;
using System.Text;

namespace SFA.DAS.ASK.Application.Utils
{
    public static class SchoolHasher
    {
        public static string GetSchoolHash(string name, string postcode)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                var nameHash = GetMd5Hash(md5Hash, name);
                var postcodeHash = GetMd5Hash(md5Hash, postcode);
                var combinedHash = GetMd5Hash(md5Hash, nameHash + postcodeHash);

                return combinedHash;
            }
        }
        
        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}