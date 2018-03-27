using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASFront.Classes;

namespace ASFront.Classes
{
    public static class Methods
    {
        static public readonly Dictionary<string, Func<string, int, Int64, string>> MethodsDict = new Dictionary<string, Func<string,int, Int64, string>>();

        static Methods()
        {
            MethodsDict.Add("#Year", Year);
          
        }

      static  public string Execute(string methodName, string MetPar, int indicatorID, Int64 clientId)
        {
            Func<string, int, Int64, string> method;

            if (!MethodsDict.TryGetValue(methodName, out method))
            {
                // Not found;
                throw new Exception();
            }

            string result = method(MetPar,  indicatorID, clientId);

            

            return result;
        }

        public static string Year(string Par,  int indicatorID, Int64 clientId)
        {
            int returnValue = 0;

            if (Par.StartsWith("$"))
                Par = Par.Remove(0);

            KeyValuePair<string, string> val = CommonFunction.FindParametr(Par, indicatorID);

            

            DateTime brt = CommonFunction.getFieldValueDateTime(val.Key, val.Value, clientId);
            returnValue = (DateTime.Now.Year - brt.Year);

            return returnValue.ToString();
        }
    }
}