using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer.Filters
{
    public abstract class ParamerizedFilter<TParameters> : IFilter
        where TParameters : IParameters, new()
    {
        public string name;
        public ParameterInfo[] GetParametersInfo() => 
            new TParameters().GetDescription();

        abstract public Photo Process(Photo original, TParameters parameters);

        public Photo Process(Photo original, double[] values)
        {
            var parameters = new TParameters();
            parameters.SetValues(values);
            return Process(original, parameters);
        }

        public override string ToString() => name;
    }
}
