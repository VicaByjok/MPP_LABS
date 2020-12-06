using System;

namespace Faker.ValueGenerator.SystemClassGenerator
{
    class DateGenerator : IBaseGenerator
    {
        private Type generatingType = typeof(DateTime);

        public object Generate(GeneratorContext context)
        {
            var buf = new byte[8];
            context.Random.NextBytes(buf);
            var ticks = BitConverter.ToInt64(buf, 0);
            return new DateTime(Math.Abs(ticks % (DateTime.MaxValue.Ticks - DateTime.MinValue.Ticks)) + DateTime.MinValue.Ticks);
        }

        public bool CanGenerate(Type t)
        {
            return t.Equals(generatingType);
        }
    }
}
