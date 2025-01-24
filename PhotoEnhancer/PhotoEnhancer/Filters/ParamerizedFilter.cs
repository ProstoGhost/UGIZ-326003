using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer.Filters
{
    public abstract class ParamerizedFilter : IFilter
    {
        IParameters parameters;

        public ParamerizedFilter(IParameters parameters) =>
            this.parameters = parameters;

        public ParameterInfo[] GetParametersInfo() => 
            parameters.GetDescription();

        abstract public Photo Process(Photo original, IParameters parameters);

        public Photo Process(Photo original, double[] values)
        {
            parameters.SetValues(values);
            return Process(original, parameters);
        }
    }
}
