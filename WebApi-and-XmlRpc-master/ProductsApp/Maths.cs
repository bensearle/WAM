using CookComputing.XmlRpc;
using ProductsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsApp
{
    [XmlRpcUrl("http://www.cookcomputing.com/xmlrpcsamples/math.rem")]
    public interface IMathsStuff : IXmlRpcProxy
    {
        [XmlRpcMethod("math.Add")]
        int RetSum(int a, int b);

        [XmlRpcMethod("math.Divide")]
        int RetDivision(int a, int b);

        [XmlRpcMethod("math.Multiply")]
        int RetProduct(int a, int b);

        [XmlRpcMethod("math.Subtract")]
        int RetDifference(int a, int b);

        [XmlRpcMethod("math.SumAndDifference")]
        SumAndDiff RetSumAndDifference(int a, int b);
    }

    [XmlRpcUrl("http://192.168.6.11:3870/RPC2")]
    public interface IWoa : IXmlRpcProxy
    {
        [XmlRpcMethod("woa.poke")]
        int SetGpsDp(string s);


        
    }

    public class SumAndDiff
    {
        public int sum;
        public int difference;
    }
}
