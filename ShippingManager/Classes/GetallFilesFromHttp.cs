using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShippingManager.Classes
{
    public static class GetallFilesFromHttp
    {
        public static string Url = "";

        public static string GetDirectoryListingRegexForUrl(string url)
        {
            if (url.Equals(Url))
            {
                return "\\\"([^\"]*)\\\"";
            }
        }
        public static List<string> ListDiractory()
        {
            List<string> lsFiles = new List<string>();
            string url = Url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string html = reader.ReadToEnd();

                    Regex regex = new Regex(GetDirectoryListingRegexForUrl(url));
                    MatchCollection matches = regex.Matches(html);
                    if (matches.Count > 0)
                    {
                        foreach (Match match in matches)
                        {
                            if (match.Success && match.ToString().Contains('.'))
                            {

                                String s = match.ToString();
                                s = s.Replace('"', ' ').Trim(' ');
                                s = s.Replace('/', ' ').Trim(' ');
                                s = Removelast(s);
                                lsFiles.Add(s);
                            }
                        }
                    }
                }
            }
            return lsFiles;
        }

        /// <summary>
        /// Best Of My Logic.
        /// Recursive Fucntion call .
        /// </summary>
        /// <param name="Filename">
        /// File Name Count
        /// </param>
        /// <returns></returns>
        public static String Removelast(String Filename)
        {
            String _return = Filename;
            int i = Filename.IndexOf(' ');
            _return = Filename.Remove(0, i + 1);
            if (Filename.Contains(' '))
                _return = Removelast(_return);
            return _return;
        }
    }
}
