using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diaygeia
{
   public static class HelpUtils
    {
       public static string returnTESTDiaygeiaAPIUrl()
        {
            return ("https://test3.diavgeia.gov.gr/luminapi/opendata/");
        }


        public static string return___PRODUCTION___DiaygeiaAPIUrl()
        {
            return ("https://diavgeia.gov.gr/opendata/");
        }



        public static string return___PRODUCTION___Authorization()
        {
            String username = "100015990_131";
            String password = "Pr0t1234$#@!";
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            return encoded;
        }

        public static string returnTESTAuthorization()
        {
            String username = "10599_api";
            String password = "User@10599";
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            return encoded;
        }


        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }


        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

    }
}
