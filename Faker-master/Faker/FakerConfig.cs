using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Faker.ValueGenerator;

namespace Faker
{
    public class FakerConfig
    {
        private Dictionary<Type, Dictionary<MemberInfo, IBaseGenerator>> customGenerators = new Dictionary<Type, Dictionary<MemberInfo, IBaseGenerator>>();

        public void Add<TClass, TPropertyType, TGenerator>(Expression<Func<TClass, TPropertyType>> expression)
        {
            Expression body = expression.Body;
            if (body.NodeType != ExpressionType.MemberAccess)
                throw new FakerException("Invalid lambda expression");
            IBaseGenerator generator = (IBaseGenerator)Activator.CreateInstance(typeof(TGenerator));
            if (!generator.CanGenerate(typeof(TPropertyType)))
            {
                throw new FakerException("Illegal generator");
            }

            if (customGenerators.ContainsKey(typeof(TClass)))
            {
                Dictionary<MemberInfo, IBaseGenerator> innerDict = customGenerators[typeof(TClass)];
                innerDict.Add(((MemberExpression)body).Member, generator);
            }
            else
            {
                Dictionary<MemberInfo, IBaseGenerator> innerDict = new Dictionary<MemberInfo, IBaseGenerator>();
                innerDict.Add(((MemberExpression)body).Member, generator);
                customGenerators.Add(typeof(TClass), innerDict);
            }
        }

        public bool ClassHasCustomGenerators(Type t)
        {
            return customGenerators.ContainsKey(t);
        }

        public bool FieldOrPropertyHasCustomGenerator(Type classType,string memberName/*MemberInfo member*/,out IBaseGenerator gen)
        {
            Dictionary<MemberInfo, IBaseGenerator> innerDict = customGenerators[classType];
            foreach (KeyValuePair<MemberInfo, IBaseGenerator> pair in innerDict)
                if (pair.Key.Name.Equals(memberName))
                {
                    gen = pair.Value;
                    return true;
                }
            gen = null;
            return false;                
        }
    }
}
