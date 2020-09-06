using System;
using System.Collections.Generic;
using System.Text;

namespace Candle.Candlesticks
{
    public class IndicatorInput
    {
        public bool ReversedInput { get; set; }
        public Func<long,long> Format { get; set; }

    }
}
