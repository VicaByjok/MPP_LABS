using System;
using System.Collections.Generic;
using System.Reflection;

namespace Faker
{
    class CircularReferencesResolver
    {
        private Dictionary<MemberInfo, int> circularReferences = new Dictionary< MemberInfo, int>();
        private int maxRecursionLevel;

        
        public CircularReferencesResolver(int maxLevel)
        {
            maxRecursionLevel = maxLevel;
        }

        public void AddReference(Type t, MemberInfo member)
        {
            if (!circularReferences.ContainsKey(member))
                circularReferences.Add( member, 0);
            else
                circularReferences[member] += 1;
        }

        public void RemoveReference(Type t, MemberInfo member)
        {
            if (circularReferences.ContainsKey(member) && circularReferences[member] != 0)
                circularReferences[member] -= 1;
            if (circularReferences[ member] == -1)
                circularReferences.Remove( member);
        }

        public bool CanCreateAnObject(Type t, MemberInfo member)
        {
            return circularReferences.ContainsKey( member) && circularReferences[ member] < maxRecursionLevel;
        }
    }
}
