using ProductsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using CookComputing.XmlRpc;
using System.Web.Script.Serialization;
using ProductsApp.Helpers;
using WirelessAssetMonitoring;
using System.Web.Services;
using System.IO;

namespace ProductsApp.Controllers
{
    public class WirelessAssetController : ApiController
    {
        // initialise some products
        WamDp[] WamDps = new WamDp[]
        {
            new WamDp{AssetGuid=Guid.NewGuid(), Name = "Ben", Time = UnixTime.Now(), Digit=true, Analogue = 3.98, LocLat = 51.096, LocLong = 1.07  },
            new WamDp{AssetGuid=Guid.NewGuid(), Name = "Will", Time = UnixTime.Now(), Digit=true, Analogue = 4.98, LocLat = 51.048, LocLong = 3.07  },
            new WamDp{AssetGuid=Guid.NewGuid(), Name = "Laz", Time = UnixTime.Now(), Digit=true, Analogue = 598.1, LocLat = 51.238, LocLong = 0.37  },
            new WamDp{AssetGuid=Guid.NewGuid(), Name = "Mike", Time = UnixTime.Now(), Digit=true, Analogue = 400.5, LocLat = 5.98, LocLong = 0.67  }
        };

        [HttpGet]
        public IEnumerable<WamDp> getAllWas()
        {
            return WamDps;
        }

        /// <summary>
        /// returns an asset with matching GUID
        /// </summary>
        /// <param name="AssetGuid">GUID</param>
        /// <returns>mnathed asset</returns>
        [HttpGet]
        public IHttpActionResult getWAsset(Guid AssetGuid)
        {
            WamDp retWd = new WamDp();
            foreach (WamDp wd in WamDps)
            {
                if (wd.AssetGuid == AssetGuid)
                {
                    retWd = wd;
                    break;
                }
            }

            return Ok(retWd);
        }

        /// <summary>
        /// Post a wireless asset object
        /// </summary>
        /// <param name="wd"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult postWAsset(WamDp wd)
        {
            if (wd != null)
            {
                IWoa woaEndPoint = XmlRpcProxyGen.Create<IWoa>();
                try
                {
                    var serializer = new JavaScriptSerializer();
                    Debug.WriteLine(woaEndPoint.SetGpsDp(serializer.Serialize(wd)));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            else
            {
                wd = new WamDp();
            }
            return Ok(wd);
        }

        [WebMethod]
        void webpageTest()
        {
            postWAsset(WamDps[0]);
        }
    }
}
