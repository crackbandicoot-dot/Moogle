using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine
{
    public class AppConfig
    {
        public string DataBasePath { get; set; } = "../Content";
        public int NumbersOfResultsShowed { get; set; } = 1000;
        public float MinimumScoreLength { get; set; } = 0;
    }
}
