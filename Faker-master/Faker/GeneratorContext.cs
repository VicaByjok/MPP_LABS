using System;

namespace Faker
{
    public class GeneratorContext
    {

        public Random Random { get; }

        public Type TargetType { get; }

        public Faker Faker { get; }

        public GeneratorContext(Random random, Type targetType, Faker faker)
        {
            Random = random;
            TargetType = targetType;
            Faker = faker;
        }
    }
}
