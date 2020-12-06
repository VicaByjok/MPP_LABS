using System;

namespace Faker.ValueGenerator.BaseTypesGenerators
{
    class BoolGenerator : IBaseGenerator
    {
        private Type generatingType =  typeof(bool);

        public object Generate(GeneratorContext context)
        {
            if (context.Random.Next(2) == 1)
                return true;
            else
                return false;
        }

        public bool CanGenerate(Type t)
        {
            return t.Equals(generatingType);
        }
    }
}
