using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarbledCheck.Domain
{
    [Serializable]
    public class ConversionRecord
    {
        public string SourceEncodingInfo { get; set; }
        public string TargetEncodingInfo { get; set; }
        public string ConversionString { get; set; }
    }
}
