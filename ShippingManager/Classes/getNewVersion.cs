using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingManager.Classes
{
    public class getNewVersion
    {
        /// <summary>
        /// Retruns new version information from the database
        /// </summary>
        /// <returns>String Version Number released </returns>
        public static string get()
        {
            Shipping_ManagerEntities ent = new Shipping_ManagerEntities();
            string _return = "1.01.01";
            try
            {
                int id = ent.VersionReleaseds.Max(i => i.VersionID);
                _return = ent.VersionReleaseds.FirstOrDefault(i => i.VersionID == id).VersionNumber;
            }
            catch (Exception)
            { }
            return _return;
        }
    }
}
