namespace Common
{
  public abstract class DistributionStrategyBase : IDistributionStrategy
  {
    const int DefaultNumberOfFractionalDigits = 2;
    public int FractionalDigits 
    { 
      get => _numberofFractionalDigits; 
      set
      {
        if(value < 0 || value > 15)
          throw new ArgumentOutOfRangeException(nameof(value), value, "Rounding digits must be between 0 and 15, inclusive");
        _numberofFractionalDigits = value;
      }
    }
    private int _numberofFractionalDigits = DefaultNumberOfFractionalDigits;
    public DistributionResult Calculate(double sum, IEnumerable<double> sums)
    {
      ValidateCalculationParameters(sum, sums);
      var dist = ConcreteCalculationHook(sum, sums).Select(Round).ToArray();
      return new DistributionResult 
      { 
        Dist = dist, 
        Error = Round(sum - dist.Sum())
      };
    }

    private static void ValidateCalculationParameters(double sum, IEnumerable<double> sums)
    {
      ValidationSum(sum);
      ValidationSums(sums);
    }

    private static void ValidationSum(double sum)
    {
      if (!double.IsFinite(sum))
        throw new ArgumentOutOfRangeException(nameof(sum), $"Parameter has non-finite value({sum})");
      if (sum < 0) 
        throw new ArgumentOutOfRangeException(nameof(sum), $"Value({sum} < 0)");
    }

    private static void ValidationSums(IEnumerable<double> sums)
    {
      if (sums is null)
        throw new ArgumentNullException(nameof(sums));
      if (sums.Any(n => n < 0))
        throw new ArgumentOutOfRangeException(nameof(sums), $"Parameter {nameof(sums)} has value < 0({string.Join("; ", sums)})");
    }

    private double Round(double value)
      => Math.Round(value, FractionalDigits);

    protected abstract IEnumerable<double> ConcreteCalculationHook(double sum, IEnumerable<double> sums);
  }
}