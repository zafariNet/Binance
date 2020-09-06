
using System;
using System.Collections.Generic;

namespace Candle
{
    public class Candlestick
    {
        public long Id { get; set; }
        public double HighPrice { get; set; }
        public double OpenPrice { get; set; }
        public double ClosePrice { get; set; }
        public double LowPrice { get; set; }
    }
    public class CandleDetector
    {
        private static double _bodyLen(Candlestick candle)
        {
            return Math.Abs(candle.OpenPrice - candle.ClosePrice);
        }
        private static double _wickLen(Candlestick candle)
        {
            return candle.HighPrice - Math.Max(candle.OpenPrice, candle.ClosePrice);
        }
        private static double _tailLen(Candlestick candle)
        {
            return Math.Min(candle.OpenPrice, candle.ClosePrice) - candle.LowPrice;
        }
        private static bool _isBullish(Candlestick candle)
        {
            return candle.OpenPrice < candle.ClosePrice;
        }

        private static bool _isBearish(Candlestick candle)
        {
            return candle.OpenPrice > candle.ClosePrice;
        }

        private static bool _isHammerLike(Candlestick candle)
        {

            return (((candle.HighPrice - candle.LowPrice) > 3 * (candle.OpenPrice - candle.ClosePrice)) &&
                    ((candle.ClosePrice - candle.LowPrice) / (.001 + candle.HighPrice - candle.LowPrice) > 0.6) &&
                    ((candle.OpenPrice - candle.LowPrice) / (.001 + candle.HighPrice - candle.LowPrice) > 0.6)) && ((_bodyLen(candle)*100/_tailLen(candle)) >=25 && (_bodyLen(candle) * 100 / _tailLen(candle)) >= 50);

            //return _tailLen(candle) > _bodyLen(candle) * 2 &&
            //       _wickLen(candle) < _bodyLen(candle);
        }

        private static bool _isInvertedHammerLike(Candlestick candle)
        {
            return _wickLen(candle) > (_bodyLen(candle) * 2) &&
                   _tailLen(candle) < _bodyLen(candle);
        }

        private static bool _isEngulfed(Candlestick shortest, Candlestick longest)
        {
            return _bodyLen(shortest) < _bodyLen(longest);
        }

        private static bool _isGap(Candlestick lowest, Candlestick upmost)
        {
            return Math.Max(lowest.OpenPrice, lowest.ClosePrice) < Math.Min(upmost.OpenPrice, upmost.ClosePrice);
        }

        private static bool _isGapUp(Candlestick previous, Candlestick current)
        {
            return _isGap(previous, current);
        }

        private static bool _isGapDown(Candlestick previous, Candlestick current)
        {
            return _isGap(current, previous);
        }

        //List<Candlestick> findPattern(List<Candlestick> dataArray, Func<List<Candlestick>, bool> callback)
        //{
        //    var upperBound = (dataArray.Count - callback.length) + 1;
        //    var matches = new List<Candlestick>();

        //    for (var i = 0; i < upperBound; i++)
        //    {
        //        var args = new List<Candlestick>();

        //        // Read the leftmost j values at position i in array.
        //        // The j values are callback arguments.
        //        for (var j = 0; j < callback.; j++)
        //        {
        //            args.Add(dataArray[i + j]);
        //        }

        //        // Destructure args and find matches.
        //        if (callback(args))
        //        {
        //            matches.Add(args[1]);
        //        }
        //    }

        //    return matches;
        //}

        // Boolean pattern detection.
        // @public

        public static bool IsHammer(Candlestick candle)
        {
            return //_isBullish(candle) &&
                   _isHammerLike(candle);

        }
        public static bool IsInvertedHammer(Candlestick candle)
        {
            return _isBearish(candle) &&
                   _isInvertedHammerLike(candle);
        }

        public static bool IsHangingMan(Candlestick previous, Candlestick current)
        {
            return _isBullish(previous) &&
                   _isBearish(current) &&
                   _isGapUp(previous, current) &&
                   _isHammerLike(current);
        }
        public static bool IsShootingStar(Candlestick previous, Candlestick current)
        {
            return _isBullish(previous) &&
                   _isBearish(current) &&
                   _isGapUp(previous, current) &&
                   _isInvertedHammerLike(current);
        }

        public static bool IsBullishEngulfing(Candlestick previous, Candlestick current)
        {
            return _isBearish(previous) &&
                   _isBullish(current) &&
                   _isEngulfed(previous, current);
        }

        public static bool IsBearishEngulfing(Candlestick previous, Candlestick current)
        {
            return _isBullish(previous) &&
                   _isBearish(current) &&
                   _isEngulfed(previous, current);
        }

        public static bool IsBullishHarami(Candlestick previous, Candlestick current)
        {
            return _isBearish(previous) &&
                   _isBullish(current) &&
                   _isEngulfed(current, previous);
        }

        public static bool IsBearishHarami(Candlestick previous, Candlestick current)
        {
            return _isBullish(previous) &&
                   _isBearish(current) &&
                   _isEngulfed(current, previous);
        }
        public static bool IsBullishKicker(Candlestick previous, Candlestick current)
        {
            return _isBearish(previous) &&
                   _isBullish(current) &&
                   _isGapUp(previous, current);
        }

        public static bool IsBearishKicker(Candlestick previous, Candlestick current)
        {
            return _isBullish(previous) &&
                   _isBearish(current) &&
                   _isGapDown(previous, current);
        }

    }
}
