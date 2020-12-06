using System;

namespace Faker.ValueGenerator.BaseTypesGenerators
{
    class DoubleGenerator : IBaseGenerator
    {
        private Type generatingType = typeof(double);

        public object Generate(GeneratorContext context)
        {
            return context.Random.NextDouble();
        }

        public bool CanGenerate(Type t)
        {
            return t.Equals(generatingType);
        }
    }
}
