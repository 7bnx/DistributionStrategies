using Common;

namespace Strategy
{
  public class First : DistributionStrategyBase
  {
    protected override IEnumerable<double> ConcreteCalculationHook(double sum, IEnumerable<double> sums)
    {
      var result = new double[sums.Count()];
      for (int i = 0; i < result.Length && sum > 0; i++)
      {
        var n = sums.ElementAt(i);
        var prevSum = sum;
        sum -= n;
        result[i] = sum <= 0 ? prevSum : n;
      }
      return result;
    }
  }
}