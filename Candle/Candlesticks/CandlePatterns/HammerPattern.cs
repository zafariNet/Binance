using System;
using System.Collections.Generic;
using System.Text;

namespace Candle.Candlesticks.CandlePatterns
{
    public class HammerPattern: CandlestickBase
    {
        public HammerPattern():base()
        {
            this.Name = "HammerPattern";
            this.RequiredCount = 5;
        }

        public override bool Logic(StockData stockData)
        {
            throw new NotImplementedException();
        }
        public bool DownwardTrend(StockData stock, bool confirm = true)
        {
            var end = confirm ? 3 : 4;
            // Analyze trends in closing prices of the first three or four candlesticks
            let gains = averagegain({ values: data.close.slice(0, end), period: end - 1 });
            let losses = averageloss({ values: data.close.slice(0, end), period: end - 1 });
            // Downward trend, so more losses than gains
            return losses > gains;
        }

    }
}
