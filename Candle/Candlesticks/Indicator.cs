using System;
using System.Collections.Generic;
using System.Text;

namespace Candle.Candlesticks
{
    public class Indicator
    {
        public object Result { get; set; }
        public Func<long, long> Format { get; set; }
        public Indicator(IndicatorInput input)
        {
            this.Format = (input) => input;
        }
        public static void ReverseInputs(Candlestick candlestick) {

            throw new NotImplementedException("This method is not implemented");
        }
        public object GetResult()
        {
            return this.Result;
        }
    }
}
