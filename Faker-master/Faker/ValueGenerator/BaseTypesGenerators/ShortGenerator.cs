using System;

namespace Faker.ValueGenerator.BaseTypesGenerators
{
    class ShortGenerator : IBaseGenerator
    {
        private Type generatingType = typeof(short);
        
        public object Generate(GeneratorContext context)
        {
            return (short)context.Random.Next(1);
        }

        public bool CanGenerate(Type t)
        {
            return t.Equals(generatingType);
        }
    }
}
