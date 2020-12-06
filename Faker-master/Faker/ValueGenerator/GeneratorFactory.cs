using System;
using System.Collections.Generic;

using Faker.ValueGenerator.BaseTypesGenerators;
using Faker.ValueGenerator.GenericGenerators;

namespace Faker.ValueGenerator
{
    public class GeneratorFactory
    {
        public static List<IBaseGenerator> CraeteBaseTypesGenerators()
        {
            List<IBaseGenerator> list = new List<IBaseGenerator>();
            list.Add(new ShortGenerator());
            list.Add(new BoolGenerator());
            list.Add(new FloatGenerator());
            list.Add(new DoubleGenerator());
            list.Add(new LongGenerator());
            list.Add(new ListGenerator());
            return list ;
        }
    }
}
