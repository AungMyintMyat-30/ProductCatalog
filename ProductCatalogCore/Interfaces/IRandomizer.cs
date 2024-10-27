using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogCore.Interfaces
{
    public interface IRandomizer
    {
        public string RandomAlphabet(int length);
        public string RandomAlphabet(int length,string dateFormat);
        public string RandomAlphabet(int length, string assignCode,string dateFormat);
        public string RandomAlphanumeric(int length);
        public string RandomAlphanumeric(int length, string dateFormat);
        public string RandomAlphanumeric(int length, string assignCode, string dateFormat);
    }
}
