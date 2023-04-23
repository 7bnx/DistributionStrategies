using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Strategy.Tests
{
  [TestClass]
  public class FirstStrategyTests : DistributionStrategyBaseTests
  {
    protected override IDistributionStrategy ConcreteStrategy => new First();

    [TestMethod]
    [DynamicData(nameof(GetValidInputExpectedOutput), DynamicDataSourceType.Method)]
    public override void Calculation_with_non_zero_sum_and_non_empty_array_return_calculated_array(TestData testData)
    {
      var result = new First() { FractionalDigits = testData.FractionalDigits}.Calculate(testData.Sum, testData.Sums);
      CollectionAssert.AreEqual(
        testData.Expected, result.Dist, 
        $"{TestNumFaildText(testData.TestNum)}. {TestActualArrayNotMatchExpectedText(testData.Expected, result.Dist)}"
      );
    }
    protected static IEnumerable<object[]> GetValidInputExpectedOutput()
    {
      yield return new object[] { new TestData(0, 0, 100d, new double[] { 10d, 20d, 30d, 50d, 70d, 40d }, new double[] { 10d, 20d, 30d, 40d, 0d,  0d }) };
      yield return new object[] { new TestData(1, 0, 150d, new double[] { 10d, 20d, 30d, 50d, 70d, 40d }, new double[] { 10d, 20d, 30d, 50d, 40d, 0d }) };
      yield return new object[] { new TestData(2, 0, 220d, new double[] { 10d, 20d, 30d, 50d, 70d, 40d }, new double[] { 10d, 20d, 30d, 50d, 70d, 40d }) };
      yield return new object[] { new TestData(3, 0, 1000d, new double[] { 10d, 20d, 30d, 50d, 70d, 40d }, new double[] { 10d, 20d, 30d, 50d, 70d, 40d }) };
      yield return new object[] { new TestData(4, 0, double.MaxValue, new double[] { 10d, 20d, 30d, 50d, 70d, 40d }, new double[] { 10d, 20d, 30d, 50d, 70d, 40d }) };
      yield return new object[] { new TestData(5, 1, 500d, new double[] { 4.6534643, 465.543325 }, new double[] { 4.7, 465.5 }) };
      yield return new object[] { new TestData(6, 1, 500d, new double[] { 4.6434643, 465.543325 }, new double[] { 4.6, 465.5 }) };
      yield return new object[] { new TestData(7, 1, 400d, new double[] { 4.6434643, 465.543325 }, new double[] { 4.6, 395.4 }) };
      yield return new object[] { new TestData(8, 1, 500d, new double[] { 4.99, 465.543325 }, new double[] { 5, 465.5 }) };
      yield return new object[] { new TestData(9, 1, 400d, new double[] { 4.99, 465.543325 }, new double[] { 5, 395 }) };
      yield return new object[] { new TestData(10, 1, 500d, new double[] { 4.99, 0, 465.543325 }, new double[] { 5, 0, 465.5 }) };
      yield return new object[] { new TestData(11, 1, 400d, new double[] { 4.99, 0, 465.543325 }, new double[] { 5, 0, 395 }) };
      yield return new object[] { new TestData(12, 2, 500d, new double[] { 8.545, double.MaxValue }, new double[] { 8.54, 491.46 }) };
      yield return new object[] { new TestData(13, 2, 400d, new double[] { 4.99, 465.543325 }, new double[] { 4.99, 395.01 }) };
      yield return new object[] { new TestData(14, 2, 400d, new double[] { 4.999, 465.543325 }, new double[] { 5d, 395d }) };
      yield return new object[] { new TestData(15, 5, 500d, new double[] { 8.545, double.MaxValue, 100d }, new double[] { 8.545, 491.455, 0 }) };
      yield return new object[] { new TestData(16, 5, 400d, new double[] { 1.999, 2d, 3.43 }, new double[] { 1.999, 2d, 3.43 }) };
      yield return new object[] { new TestData(17, 5, double.MaxValue, new double[] { 1.999, 2d, 3.43 }, new double[] { 1.999, 2d, 3.43 }) };
      yield return new object[] { new TestData(18, 5, double.MaxValue, new double[] { double.MaxValue, 0, 0 }, new double[] { double.MaxValue, 0, 0 }) };
      yield return new object[] { new TestData(19, 15, 400d, new double[] { 4.7612345672456125, 500d }, new double[] { 4.761234567245612, 395.2387654327544 }) };
    }
  }
}
