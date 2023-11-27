using System.Security.Cryptography;
using System.Text;

namespace IrisChallenge.Services
{
    public static class MD5Hasher
    {
        public static string CalculateMD5Hash(string input)
        {
            MD5 md5 = MD5.Create();

            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
