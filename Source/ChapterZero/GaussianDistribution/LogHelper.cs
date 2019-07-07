using System;
using System.Collections.Generic;
using System.Text;

namespace GaussianDistribution
{
    public static class LogHelper
    {
        public static void Log(string logLocation,params string[] args)
        {
            Console.WriteLine(String.Format($"Log on {logLocation},info:{args}"));
        }
    }
}
