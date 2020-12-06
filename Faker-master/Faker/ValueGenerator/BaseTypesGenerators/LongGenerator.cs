using System;

namespace Faker.ValueGenerator.BaseTypesGenerators
{
    class LongGenerator : IBaseGenerator
    {
        private Type generatingType;

        public object Generate(GeneratorContext context)
        {
            var buffer = new byte[8];
            long result;
            do
            {
                context.Random.NextBytes(buffer);
                result = BitConverter.ToInt64(buffer, 0);
            } while (result == 0);

            return result;
        }

        public bool CanGenerate(Type t)
        {
            return t.Equals(generatingType);
        }
    }
}
