using System.Text.RegularExpressions;

namespace TranMinhDucBTH2.Models.Process
{
    public class StringProcess
    {
        public string AutoGenerateCode(string strInput)
        {
            string strResult ="", numPart = "",strPart= "";
            numPart = Regex.Match(strInput,@"\d+").Value;
            strPart = Regex.Match(strInput,@"\D+").Value;
            int inPart = (Convert.ToInt32(numPart)+1);
            for (int i = 0;i <numPart.Length - inPart.ToString().Length;i++)
            {
                strPart +="0";
            }
            strResult = strPart + intPart;
            return strResult
        }
    }
}