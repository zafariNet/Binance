using System;
using System.Collections.Generic;
using System.Text;

namespace Candle.Candlesticks
{
    public class AverageGain:Indicator
    {


        public AverageGain(AvgGainInput input) : base(input)
        {
            var values = input.Values;
            var period = input.Period;
            var format = this.Format;
        }

        public List<long> Averagegain()
        {

        }

        private IEnumerable<long> Iterate(int period)
        {
            var counter = 1;
            var gainSum = 0;
            var avgGain=0;
            var gain=0;
            while (true)
            {
                
            }
        }
    }

    public class AvgGainInput : IndicatorInput
    {
        public int Period { get; set; }

        public List<int> Values { get; set; }
    }
}
