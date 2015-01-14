using CookComputing.XmlRpc;
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

    public class SumAndDiff
    {
        public int sum;
        public int difference;
    }
}
