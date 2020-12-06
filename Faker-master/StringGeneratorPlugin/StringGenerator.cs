using System;

using Faker.ValueGenerator;

namespace StringGeneratorPlugin
{
    public class StringGenerator : IBaseGenerator
    {
        private Type generatingType = typeof(string);

            public object Generate(Faker.GeneratorContext context)
            {
                byte[] arr = new byte[context.Random.Next(2, 255)];
                context.Random.NextBytes(arr);
                return Convert.ToBase64String(arr);
            }

            public bool CanGenerate(Type t)
            {
                return t.Equals(generatingType);
            }
    }
}
