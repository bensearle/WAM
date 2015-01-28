using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WirelessAssetMonitoring
{
    [XmlRpcUrl("http://192.168.6.11:3870/RPC2")]
    public interface IWoa : IXmlRpcProxy
    {
        [XmlRpcMethod("woa.poke")]
        int SetGpsDp(string s);
    }
}
