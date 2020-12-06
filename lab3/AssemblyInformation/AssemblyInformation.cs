using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AssemblyInformation
{
    public class AssemblyInformation
    {
        private Assembly assembly;
        private Node root = new Node("root", null);

        public AssemblyInformation(string fileName)
        {
            assembly = Assembly.LoadFrom(fileName);
        }

        public Node ProcessAssembly()
        {
            Type[] types = assembly.GetTypes();
            HashSet<string> namespaces = new HashSet<string>();
            List<string> fullnames = new List<string>();
            Dictionary<Type, List<string>> classes;

            foreach (Type type in types)
            {
                fullnames.Add(type.FullName);
                namespaces.Add(type.Namespace);
            }

            foreach (string ns in namespaces)
            {
                Node node = new Node("namespace " + ns, root);
                root.children.Add(node);
            }

            classes = ProcessFullName(types);

            foreach (Type t in classes.Keys)
            {
                foreach (Node node in root.children)
                {
                    if ("namespace " + t.Namespace == node.value)
                    {
                        AddClass(t, classes[t], node);
                    }
                }
            }
            int i = 0;
            return root;
        }

        private void AddFields(Type type, Node tp)
        {

            FieldInfo[] fields = type.GetFields();
            int i = 0;
            foreach (FieldInfo field in fields)
            {
                Node node = new Node(field.FieldType.Name + " " + field.Name, tp);
                node.children = null;
                tp.children.Add(node);
            }
        }

        private void AddProperties(Type type, Node tp)
        {
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Node node = new Node(property.PropertyType.Name + " " + property.Name, tp);
                node.children = null;
                tp.children.Add(node);
            }
        }
        private void AddMethods(Type type, Node tp)
        {
            MethodInfo[] methods = type.GetMethods(BindingFlags.NonPublic);
            foreach (MethodInfo method in methods)
            {
                Node node = new Node(GetSignature(method), tp);
                node.children = null;
                tp.children.Add(node);
            }
        }

        private string GetSignature(MethodInfo method)
        {
            string result = "";
            result += method.DeclaringType.Name + " ";
            result += method.Name + "(";
            ParameterInfo[] parameters = method.GetParameters();
            if (parameters.Length > 0)
            {
                int i = 0;
                foreach (ParameterInfo parameter in parameters)
                {
                    result += parameter.ParameterType.Name + " " + parameter.Name + ", ";
                }
                result = result.Remove(result.Length - 2);
            }
            result += ")";
            return result;
        }

        private Dictionary<Type, List<string>> ProcessFullName(Type[] types)
        {
            Dictionary<Type, List<string>> dict = new Dictionary<Type, List<string>>();

            foreach (Type type in types)
            {
                string fullname = type.FullName;
                List<string> classes = new List<string>();
                string tp = "";
                for (int i = 1; i < fullname.Length; i++)
                {
                    if ((fullname[i - 1] == '.') || (fullname[i - 1] == '+'))
                    {
                        for (int j = i; (j < fullname.Length) && (fullname[j] != '+'); j++)
                        {
                            tp += fullname[j];
                        }
                        classes.Add(tp);
                        tp = "";
                    }
                }
                dict.Add(type, classes);
            }
            return dict;
        }

        private void AddClass(Type type, List<string> values, Node parent)
        {

            foreach (Node n in parent.children)
            {
                if (n.value == "class " + values[0])
                {

                    values.Remove(values[0]);
                    AddClass(type, values, n);
                    return;
                }
            }
            Node node = new Node("class " + values[0], parent);
            parent.children.Add(node);
            AddFields(type, node);
            AddMethods(type, node);
            AddProperties(type, node);

        }
    }
}
