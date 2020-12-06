using System;
using System.Collections;
using System.Collections.Generic;


namespace Faker.ValueGenerator.GenericGenerators
{
    public class ListGenerator : IBaseGenerator
    {
        private Type generatingType  = typeof(List<>);

        public object Generate(GeneratorContext context)
        {
            if (!context.TargetType.IsGenericType || !context.TargetType.GetGenericTypeDefinition().Equals(typeof(List<>)))
                return null;
            Type innerType = context.TargetType.GetGenericArguments()[0];
 /*           if (innerType.IsGenericType)
                return null;*/
            IList obj = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(innerType));
            int n = context.Random.Next(1,11);
            for (int i = 0; i < n; i++)
            {
                obj.Add(context.Faker.Create(innerType));
            }
            return obj;
        }

        public bool CanGenerate(Type t)
        {
            return t.Equals(generatingType);
        }
    }
}
