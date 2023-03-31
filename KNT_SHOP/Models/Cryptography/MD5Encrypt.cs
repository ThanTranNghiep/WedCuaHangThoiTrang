using System;
using System.Security.Cryptography;
using System.Text;

namespace KNT_Shop.Models.Cryptography;

public class MD5Encrypt
{
    public string MaHoa = "";
    public MD5Encrypt(string input)
    {
        using (var md5Hash = MD5.Create())
        {
            var sourceBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = md5Hash.ComputeHash(sourceBytes);
            var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            this.MaHoa = hash;
        }
    }
}