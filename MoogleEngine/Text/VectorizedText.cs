using MoogleEngine.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.Text
{
    public class VectorizedText
    {
        private SimpleDictionary<string, double> _base;
        private readonly int _dimension;
        private readonly double _module;

        public SimpleDictionary<string, double> Base=>_base;
        public double this[string s]
        {
            get => _base[s];
            set => _base[s] = value;
        }
        public VectorizedText(SimpleDictionary<string,double> _base)
        {
            this._base = _base;
            this._module = Math.Sqrt(_base.Values.Sum(x => x * x));
            this._dimension = _base.Count;
        }
       
        
        private double DotProduct(VectorizedText other)
        {
            double res = 0;
            VectorizedText v1 = this;
            VectorizedText v2 = other;
            if (v1._dimension>v2._dimension)
            {
                (v2, v1) = (v1, v2);
            }
            var components = v1._base.Keys;
            foreach (var word in components) 
            {
                res += v1[word] * v2[word];
            }
            return res;
        }
        public double Cos(VectorizedText other)
        {
            double dotProduct = this.DotProduct(other);
            if (this._module != 0 && other._module != 0 && dotProduct > 0)
            {
                return dotProduct / (this._module * other._module);
            }
            return 0;
            
        }

    }
}
