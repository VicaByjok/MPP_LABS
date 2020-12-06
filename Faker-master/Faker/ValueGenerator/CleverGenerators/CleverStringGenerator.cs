using System;

namespace Faker.ValueGenerator.CleverGenerators
{
    public class CleverStringGenerator : IBaseGenerator
    {
        private Type generatingType = typeof(string);

        private string[] someWords = { "clever", "generator" ,"bruh",
                                        "War","is","peace","Freedom","is","slavery","Ignorance","is","strength",
                                        "The","world's","full","of","lonely","people","afraid","to","make","the","first","move"};

        public object Generate(GeneratorContext context)
        {
            int n = context.Random.Next(someWords.Length + 1);
            return someWords[n];
        }

        public bool CanGenerate(Type t)
        {
            return t.Equals(generatingType);
        }
    }
}
