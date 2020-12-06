using System;

using Faker.ValueGenerator;

namespace IntGenerator
{
    public class IntGenerator : IBaseGenerator
    {
        private Type generatingType = typeof(int);

        public object Generate(Faker.GeneratorContext context)
        {
            return context.Random.Next(1);
        }

        public bool CanGenerate(Type t)
        {
            return t.Equals(generatingType);
        }
    }
}
