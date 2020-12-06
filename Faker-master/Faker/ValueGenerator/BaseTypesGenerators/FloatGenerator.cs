using System;

namespace Faker.ValueGenerator.BaseTypesGenerators
{
    class FloatGenerator : IBaseGenerator
    {
        private Type generatingType;

        public object Generate(GeneratorContext context)
        {
            return (float)context.Random.NextDouble();
        }

        public bool CanGenerate(Type t)
        {
            return t.Equals(generatingType);
        }
    }
}
