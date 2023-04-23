using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
  public interface IDistributionStrategy
  {
    int FractionalDigits { get; set; }
    DistributionResult Calculate(double sum, IEnumerable<double> sums);

  }
}
