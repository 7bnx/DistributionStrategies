using Common;

namespace Strategy
{
  public class Last : First
  {
    protected override IEnumerable<double> ConcreteCalculationHook(double sum, IEnumerable<double> sums)
      => base.ConcreteCalculationHook(sum, sums.Reverse()).Reverse();
  }
}