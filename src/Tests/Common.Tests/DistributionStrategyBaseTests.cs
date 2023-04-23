using Common;

namespace Strategy.Tests
{
  [TestClass]
  public abstract class DistributionStrategyBaseTests
  {
    protected abstract IDistributionStrategy ConcreteStrategy { get; }
    protected static string TestNumFaildText(int num) 
      => $"Test {num} failed";
    protected static string TestActualArrayNotMatchExpectedText(double[] expected, double[] actual)
      => $"Actual array({string.Join("; ", actual)}) not match expected({string.Join("; ", expected)})";

    [DataTestMethod]
    [DataRow(0, 100d)]
    [DataRow(1, 1000d)]
    [DataRow(2, 75543d)]
    [DataRow(3, 32525.7346)]
    [DataRow(4, double.MaxValue)]
    public void Calculation_with_empty_sums_array_return_empty_array(int testNum, double sum)
    {
      var result = ConcreteStrategy.Calculate(sum, Array.Empty<double>());
      CollectionAssert.AreEqual(
        Array.Empty<double>(), result.Dist, 
        $"{TestNumFaildText(testNum)}. Actual array({string.Join("; ", result.Dist)}) not empty. "
      );
    }

    [DataTestMethod]
    [DataRow(0, new double[] { -0.0000000000000001})]
    [DataRow(1, new double[] { -1d })]
    [DataRow(2, new double[] {double.MinValue })]
    [DataRow(3, new double[] { 1d, 9d, -9d })]
    [DataRow(4, new double[] { 0d, 0d, 1d, -5d, 8d })]
    public void Calculation_with_negative_sums_array_return_exception_out_of_range(int testNum, double[] sums)
    {
      Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        ConcreteStrategy.Calculate(1d, sums),
        $"{TestNumFaildText(testNum)}. Values of {nameof(sums)} = {string.Join(", ", sums)}. "
      );
    }

    [DataTestMethod]
    [DynamicData(nameof(GetTestDataZeroSumAndNonEmptyArray), DynamicDataSourceType.Method)]
    public void Calculation_with_zero_sum_and_non_empty_array_return_array_with_all_zeroes(TestData testData)
    {
      var result = ConcreteStrategy.Calculate(testData.Sum, testData.Sums);
      CollectionAssert.AreEqual(
        testData.Expected, result.Dist, 
        $"{TestNumFaildText(testData.TestNum)}. {TestActualArrayNotMatchExpectedText(testData.Expected, result.Dist)}"
      );
    }

    [DataTestMethod]
    [DataRow(0, -1d)]
    [DataRow(1, -1.12532)]
    [DataRow(2, -66332423)]
    [DataRow(3, double.MinValue)]
    public void Calculation_with_negative_sum_exception_out_of_range(int testNum, double sum)
    {
      Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        ConcreteStrategy.Calculate(sum, new double[] { 1d }),
        $"{TestNumFaildText(testNum)}. Value of {nameof(sum)} = {sum}. "
      );
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Calculation_with_positive_infifnity_sum_exception()
      => ConcreteStrategy.Calculate(double.PositiveInfinity, new double[] { 1d });

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Calculation_with_negative_infifnity_sum_exception()
      => ConcreteStrategy.Calculate(double.NegativeInfinity, new double[] { 1d });

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Calculation_with_nan_sum_exception()
      => ConcreteStrategy.Calculate(double.NaN, new double[] { 1d });

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Calculation_with_null_sums_exception()
      => ConcreteStrategy.Calculate(0d, null!);

    [DataTestMethod]
    [DataRow(0, -1)]
    [DataRow(1, 16)]
    [DataRow(2, int.MinValue)]
    [DataRow(3, int.MaxValue)]
    public void Set_fractional_digits_exception_out_of_range(int testNum, int fractionalDigits)
    {
      Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        ConcreteStrategy.FractionalDigits = fractionalDigits,
        $"{TestNumFaildText(testNum)}. Value of {nameof(fractionalDigits)} = {fractionalDigits}. "
      );
    }

    [DataTestMethod]
    public abstract void Calculation_with_non_zero_sum_and_non_empty_array_return_calculated_array(TestData testData);

    private static IEnumerable<object[]> GetTestDataZeroSumAndNonEmptyArray()
    {
      var fractionalDigitsArr = new int[] { 0, 1, 5, 7, 10, 15};
      for(int i = 0; i < fractionalDigitsArr.Length; i++)
      {
        var sumsArray = new double[] { 10.5436234, 20d, 3032452.23523, 502323523325325325, 0.464757235232352, 0d };
        var length = sumsArray.Length;
        var testData = new TestData(i * 2, fractionalDigitsArr[i], 0d, sumsArray, new double[length]);
        yield return new object[] { testData };
        yield return new object[] { testData with { TestNum = i * 2 + 1, Sums = new double[length], Expected = new double[length] } };
      }
    }

    public readonly record struct TestData(
      int TestNum,
      int FractionalDigits,
      double Sum,
      double[] Sums,
      double[] Expected
    );
  }
}