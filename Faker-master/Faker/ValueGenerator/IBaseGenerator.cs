using System;

namespace Faker.ValueGenerator
{
    public interface IBaseGenerator
    {
        public object Generate(GeneratorContext generatorContext);

        public bool CanGenerate(Type t);
    }
}
