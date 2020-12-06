using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AssemblyInformation
{
    public class AssemblyService
    {
        private BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public 
            | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;

        private Assembly assembly;
        private Node root = new Node("root", null);

        public AssemblyService(string filename)
        {
            assembly = Assembly.LoadFrom(filename);
        }

        public AssemblyService(Assembly assembly)
        {
            this.assembly = assembly;
        }

        public Node ProcessAssembly()
        {
            Type[] types = assembly.GetTypes();
            HashSet<string> namespaces = new HashSet<string>();

            foreach (Type type in types)
            {
                namespaces.Add(type.Namespace);
            }

            foreach(string @namespace in namespaces)
            {
                string inf = "namespace " + @namespace;
                Node node = new Node(inf, root);
                root.children.Add(node);
            }

            foreach (Type t in types)
            {
                foreach (Node node in root.children)
                {
                    if ("namespace " + t.Namespace == node.inf)
                    {
                        AddClass(t, node);
                    }
                }
            }

            return root;
        }

        private void AddFields(Type type, Node tp)
        {

            FieldInfo[] fields = type.GetFields(flags);
            int i = 0;
            foreach (FieldInfo field in fields)
            {
                string inf = AccessSpecificators.fieldSpecificator(field) + field.FieldType.Name + " " + field.Name;
                Node node = new Node(inf, tp);
                node.children = null;
                tp.children.Add(node);
            }
        }

        private void AddProperties(Type type, Node tp)
        {
            PropertyInfo[] properties = type.GetProperties(flags);
            foreach (PropertyInfo property in properties)
            {
                string value = property.PropertyType.Name + " " + property.Name;
                Node node = new Node(value, tp);
                node.children = null;
                tp.children.Add(node);
            }
        }
        private void AddMethods(Type type, Node tp)
        {
            MethodInfo[] methods = type.GetMethods(flags);
            foreach (MethodInfo method in methods)
            {
                Node node = new Node(GetSignature(method), tp);
                node.children = null;
                tp.children.Add(node);
            }
        }

        private string GetSignature(MethodInfo method)
        {
            string result = AccessSpecificators.MethodSpecificator(method);
            result += method.ReturnType.Name + " ";
            result += method.Name + "(";
            ParameterInfo[] parameters = method.GetParameters();
            if (parameters.Length > 0)
            {
                foreach (ParameterInfo parameter in parameters)
                {
                    result += parameter.ParameterType.Name + " " + parameter.Name + ", ";
                }
                result = result.Remove(result.Length - 2);
            }
            result += ")";
            return result;
        }


        private void AddClass(Type type, Node parent)
        {
            string inf = AccessSpecificators.ClassSpecificator(type);
            inf += "class " + type.Name;
            Node node = new Node(inf, parent);
            parent.children.Add(node);
            AddFields(type, node);
            AddMethods(type, node);
            AddProperties(type, node);

        }
    }
}
