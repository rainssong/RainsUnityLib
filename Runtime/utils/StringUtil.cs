using System.Text;

namespace com.rainssong.utils
{

    public static class StringUtil
    {

        public static string DeleteUTF8BOM(string str)
        {
            string _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (str.StartsWith(_byteOrderMarkUtf8))
            {
                int lastIndexOfUtf8 = _byteOrderMarkUtf8.Length;
                str = str.Remove(0, lastIndexOfUtf8);
            }

            return str;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }

}