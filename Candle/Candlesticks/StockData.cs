using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Candle.Candlesticks
{
    public class StockData
    {
        public bool ReversedInput { get; set; }
        public List<long> OpenList { get; set; }
        public List<long> HighList { get; set; }
        public List<long> LowList { get; set; }
        public List<long> CloseList { get; set; }
        public StockData(List<long> openList, List<long> highList, List<long> lowList, List<long> closeList, bool reversedInput)
        {
            OpenList = openList;
            HighList = highList;
            LowList = lowList;
            CloseList = closeList;
            ReversedInput = reversedInput;
        }

        public StockData()
        {
            
        }
    }
}
