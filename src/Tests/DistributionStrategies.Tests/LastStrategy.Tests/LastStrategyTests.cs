using Common;

namespace Strategy.Tests
{
  [TestClass]
  public class LastStrategyTests : DistributionStrategyBaseTests
  {
    protected override IDistributionStrategy ConcreteStrategy => new Last();

    [TestMethod]
    [DynamicData(nameof(GetValidInputExpectedOutput), DynamicDataSourceType.Method)]
    public override void Calculation_with_non_zero_sum_and_non_empty_array_return_calculated_array(TestData testData)
    {
      var result = new Last() { FractionalDigits = testData.FractionalDigits }.Calculate(testData.Sum, testData.Sums);
      CollectionAssert.AreEqual(
        testData.Expected, result.Dist,
        $"{TestNumFaildText(testData.TestNum)}. {TestActualArrayNotMatchExpectedText(testData.Expected, result.Dist)}"
      );
    }
    protected static IEnumerable<object[]> GetValidInputExpectedOutput()
    {
      yield return new object[] { new TestData(0, 0, 100d, new double[] { 10d, 20d, 30d, 50d, 70d, 40d }, new double[] { 0d, 0d, 0d, 0d, 60d, 40d }) };
      yield return new object[] { new TestData(1, 0, 150d, new double[] { 10d, 20d, 30d, 50d, 70d, 40d }, new double[] { 0d, 0d, 0d, 40d, 70d, 40d }) };
      yield return new object[] { new TestData(2, 0, 220d, new double[] { 10d, 20d, 30d, 50d, 70d, 40d }, new double[] { 10d, 20d, 30d, 50d, 70d, 40d }) };
      yield return new object[] { new TestData(3, 0, 1000d, new double[] { 10d, 20d, 30d, 50d, 70d, 40d }, new double[] { 10d, 20d, 30d, 50d, 70d, 40d }) };
      yield return new object[] { new TestData(4, 0, double.MaxValue, new double[] { 10d, 20d, 30d, 50d, 70d, 40d }, new double[] { 10d, 20d, 30d, 50d, 70d, 40d }) };
      yield return new object[] { new TestData(5, 1, 500d, new double[] { 4.6534643, 465.543325 }, new double[] { 4.7, 465.5 }) };
      yield return new object[] { new TestData(6, 1, 500d, new double[] { 4.6434643, 465.543325 }, new double[] { 4.6, 465.5 }) };
      yield return new object[] { new TestData(7, 1, 400d, new double[] { 465.543325, 4.6434643 }, new double[] { 395.4, 4.6 }) };
      yield return new object[] { new TestData(8, 1, 500d, new double[] { 4.99, 465.543325 }, new double[] { 5d, 465.5 }) };
      yield return new object[] { new TestData(9, 1, 400d, new double[] { 465.543325, 4.99 }, new double[] { 395d, 5d }) };
      yield return new object[] { new TestData(10, 1, 500d, new double[] { 4.99, 0d, 465.543325 }, new double[] { 5d, 0d, 465.5 }) };
      yield return new object[] { new TestData(11, 1, 400d, new double[] { 4.99, 0d, 465.543325 }, new double[] { 0d, 0d, 400d }) };
      yield return new object[] { new TestData(12, 2, 500d, new double[] { 8.545, double.MaxValue }, new double[] { 0d, 500d }) };
      yield return new object[] { new TestData(13, 2, 400d, new double[] { 465.543325, 4.99 }, new double[] {395.01, 4.99 }) };
      yield return new object[] { new TestData(14, 2, 400d, new double[] { 465.543325, 4.999 }, new double[] { 395d, 5d }) };
      yield return new object[] { new TestData(15, 5, 500d, new double[] { 100d, double.MaxValue, 8.545 }, new double[] { 0d, 491.455, 8.545 }) };
      yield return new object[] { new TestData(16, 5, 400d, new double[] { 1.999, 2d, 3.43 }, new double[] { 1.999, 2d, 3.43 }) };
      yield return new object[] { new TestData(17, 5, double.MaxValue, new double[] { 1.999, 2d, 3.43 }, new double[] { 1.999, 2d, 3.43 }) };
      yield return new object[] { new TestData(18, 5, double.MaxValue, new double[] { double.MaxValue, 0d, 0d }, new double[] { double.MaxValue, 0d, 0d }) };
      yield return new object[] { new TestData(19, 15, 400d, new double[] { 500d, 4.7612345672456125 }, new double[] { 395.2387654327544, 4.761234567245612 }) };
    }
  }
}