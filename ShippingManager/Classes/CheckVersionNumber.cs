using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingManager.Classes
{
    public static class CheckVersionNumber
    {

        public static string ReadString(String WhtReturn)
        {
            String _return = null;
            try
            {
                string Location = "";
                String Timestring = "";
                String Language = "English";
                String IsBarcodeShow = "";
                String VersionNo = "1.01.01";
                String[] Lines = File.ReadAllLines(Environment.CurrentDirectory + "\\ShippingApp\\LocalSetting.txt");
                foreach (String line in Lines)
                {
                    var word = line.Split(new char[] { '#' });
                    Location = word[0].ToString();
                    Timestring = word[1].ToString();
                    Language = word[2].ToString();
                    IsBarcodeShow = word[3].ToString();
                    VersionNo = word[4].ToString();
                }
                switch (WhtReturn)
                {
                    case "Location":
                        _return = Location.ToString();
                        break;
                    case "LogoutTime":
                        _return = Timestring.ToString();
                        break;
                    case "Language":
                        _return = Language;
                        break;
                    case "ISBarcodeShow":
                        _return = IsBarcodeShow;
                        break;
                    case "VRN":
                        _return = VersionNo;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            { }
            return _return;
        }

    }
}
