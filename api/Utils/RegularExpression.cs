using System.Text;
using System.Text.RegularExpressions;
namespace AxonsMoveTMSService.Utils
{
    public class RegularExpression
    {
        public bool IsDigits2Decimail(string str)
        {
            string reg = @"^[0-9]*(\.[0-9]{1,2})?$";
            return (Regex.IsMatch(str, reg, RegexOptions.Singleline)) ? true : false;
        }

        public bool IsTextEng(string str)
        {
            string reg = @"^[a-zA-Z0-9 _.,-]*$";
            return (Regex.IsMatch(str, reg, RegexOptions.Singleline)) ? true : false;
        }

        public bool IsTextEngOrNumber(string str)
        {
            string reg = @"^[a-zA-Z0-9]*$";
            return (Regex.IsMatch(str, reg, RegexOptions.Singleline)) ? true : false;
        }

        public bool IsNumber(string str)
        {
            string reg = @"^[0-9]*$";
            return (Regex.IsMatch(str, reg, RegexOptions.Singleline)) ? true : false;
        }

        public bool IsTime24(string str)
        {
            string reg = @"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$";
            return (Regex.IsMatch(str, reg, RegexOptions.Singleline)) ? true : false;
        }
        public bool IsMaximumLengh(string str, int maxLengh)
        {
            if (string.IsNullOrEmpty(str)) return false;
            byte[] bytes = Encoding.Default.GetBytes(str);
            return (bytes.Length > maxLengh) ? true : false;
        }
        public bool IsPatternEmail(string str)
        {
            var hasThai = new Regex(@"[ก-๙]+");
            var hasemail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return (!hasemail.IsMatch(str) || hasThai.IsMatch(str)) ? false : true;
        }
        public bool IsValidTimeFormat(string input)
        {
            TimeSpan dummyOutput;
            return TimeSpan.TryParse(input, out dummyOutput);
        }
    }
}