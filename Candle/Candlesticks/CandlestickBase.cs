using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Candle.Candlesticks
{
    public abstract class CandlestickBase
    {
        public int RequiredCount { get; set; }
        public string Name { get; set; }

        protected CandlestickBase()
        {

        }

        public abstract bool Logic(StockData stockData);
        public bool ApproximateEqual(int a, int b)
        {
            var left = float.Parse(Math.Abs(a - b).ToString()) * 1;
            var right = float.Parse((a * 0.001).ToString()) * 1;
            return left <= right;
        }

        public IEnumerable<StockData> GetAllPatternIndex(StockData stockData)
        {
            if (stockData.CloseList.Count < this.RequiredCount)
            {
               throw new Exception("Data count less than data required for the strategy ', this.name");
               
            }
            if (stockData.ReversedInput)
            {
                stockData.OpenList.Reverse();
                stockData.HighList.Reverse();
                stockData.LowList.Reverse();
                stockData.CloseList.Reverse();
            }

            var calculate = GenerateDataForCandleStick(stockData).Where(x=> x != null && Logic(x)!=null);
            return calculate;
        }

        public bool HasPattern(StockData stockData)
        {
            if (stockData.CloseList.Count < this.RequiredCount)
            {
                return false;
            }

            if (!stockData.ReversedInput) return Logic(GetLastDataForCandleStick(stockData));
            stockData.OpenList.Reverse();
            stockData.HighList.Reverse();
            stockData.LowList.Reverse();
            stockData.CloseList.Reverse();
            return Logic(GetLastDataForCandleStick(stockData));
        }

        protected List<StockData> GenerateDataForCandleStick(StockData stockData)
        {
            var requiredCount = this.RequiredCount;
            var closeList = stockData.CloseList;
            var list=new List<StockData>();

            for (var i = 0; i <= stockData.CloseList.Count - 1; i++)
            {
                var j = 0;
                var currentData = stockData.CloseList[i];
                var _stockData = new StockData();
                while (j < requiredCount)
                {
                    _stockData.OpenList.Add(stockData.CloseList[i-j]);
                    _stockData.CloseList.Add(stockData.CloseList[i - j]);
                    _stockData.HighList.Add(stockData.HighList[i - j]);
                    _stockData.LowList.Add(stockData.LowList[i - j]);
                }
                list.Add(_stockData);
            }

            for (var i = 0; i <= list.Count; i++)
            {
                if (i > stockData.CloseList.Count - requiredCount)
                    list.Remove(list[i]);
            }

            return list;
        }

        protected StockData GetLastDataForCandleStick(StockData stockData)
        {
            var requiredCount = this.RequiredCount;
            if (stockData.CloseList.Count == requiredCount)
            {
                return stockData;
            }
            else
            {
                var returnVal=new StockData();
                var i = 0;
                var index = stockData.CloseList.Count - requiredCount;
                while (i < requiredCount)
                {
                    returnVal.OpenList.Add(stockData.OpenList[index + i]);
                    returnVal.HighList.Add(stockData.HighList[index + i]);
                    returnVal.LowList.Add(stockData.LowList[index + i]);
                    returnVal.CloseList.Add(stockData.CloseList[index + i]);
                    i++;
                }
                return returnVal;
            }
        }
}
}
