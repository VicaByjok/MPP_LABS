using AssemblyInformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace Tests
{
    [TestClass]
    public class AssemblyInformationTests
    {
        private AssemblyService assInf;
        private string filename = "C:\\Users\\laptop\\Desktop\\SPP\\lab3\\TestLib\\bin\\Debug\\netstandard2.0\\TestLib.dll";
        private Type[] types;
        private Assembly assembly;
        private Node root;

        [TestInitialize]
        public void Init()
        {
            assInf = new AssemblyInformation.AssemblyService(filename);
            assembly = Assembly.LoadFrom(filename);
            root = assInf.ProcessAssembly();
        }
        [TestMethod]
        public void NamespacesCountTest()
        {
            int actual = root.children.Count;
            int expected = 2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TypesCountTest()
        {
            types = assembly.GetTypes();
            int actual = 0;
            foreach(Node node in root.children){
                actual += node.children.Count;
            }
            int expected = 5;
            Assert.AreEqual(expected, actual);
        }

    }
}
