using System.Globalization;
using System.Text;

namespace XRPL.TrustlineService;

internal static class ExtensionHelpers
{
    internal static string ToHex(this string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        string hex = BitConverter.ToString(bytes);
        var res = hex.Replace("-", string.Empty);
        return res;
        //return Convert.ToHexString(bytes);
    }

    internal static string FromHexString(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;
        var buffer = new byte[input.Length / 2];
        for (var i = 0; i < input.Length; i += 2)
        {
            var hexadecimal = input.Substring(i, 2);
            buffer[i / 2] = byte.Parse(hexadecimal, NumberStyles.HexNumber);
        }
        return Encoding.UTF8.GetString(buffer);
    }
}