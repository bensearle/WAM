using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductsApp.Controllers
{
    public class MathsController : ApiController
    {

        public IHttpActionResult getMath(int varA, int varB)
        {
            // create the xml proxy to grab stuff
            IMathsStuff mathsProxy = XmlRpcProxyGen.Create<IMathsStuff>();
            
            // this "Ok" business needs looking into.
            return Ok(mathsProxy.RetSum(varA, varB));
        }
    }
}
