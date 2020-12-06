using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.ValueGenerator.CleverGenerators
{
    public class CleverIntGenerator : IBaseGenerator
    {
        private Type generatingType;

        public object Generate(GeneratorContext context)
        {
            return 42;
        }

        public bool CanGenerate(Type t)
        {
            return t.Equals(generatingType);
        }
    }
}
