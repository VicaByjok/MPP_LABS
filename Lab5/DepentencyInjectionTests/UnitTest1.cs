using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DependencyInjectionLib;
using System.Collections.Generic;

namespace DepentencyInjectionTests
{
    interface IService1 
    {
        void SomeMethod();
    }
    class Service1: IService1
    {
        public void SomeMethod()
        {
            System.Console.WriteLine("SomeMethod");
        }
    }
    abstract class AbstractService2
    {

    }
    class Service2: AbstractService2
    {

    }
    class Service3
    {

    }
    //
    interface IService
    {
    }
    class ServiceImpl: IService
    {
        public ServiceImpl(IRepository repository) // ServiceImpl зависит от IRepository
        {
        }
    }
    class ServiceImpl2: IService
    {
        public ServiceImpl2()
        {

        }
    }
    interface IRepository { }
    class RepositoryImpl: IRepository
    {
        public RepositoryImpl() { } // может иметь свои зависимости, опустим для простоты
    }
    /// 
    /// ///////////////////////////////////////////
    ///

    [TestClass]
    public class UnitTest1
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public void newTestMethod()
        {
            
            var dependencies = new DependenciesConfiguration();
            dependencies.Register<IService1, Service1>();
            var provider = new DependencyProvider(dependencies);
            logger.Info("Try incorrect data");
            var service1 = provider.Resolve<int>();
        }

        [TestMethod]
        public void ExceptionTest()
        {
            logger.Info("Start exception test");
            Action action​ = newTestMethod;
            Assert.ThrowsException<System.InvalidCastException>(action);
            logger.Info("End exception test");
        }
        [TestMethod]
        public void InterfaceTest()
        {
            logger.Info("Start Interface test");
            var dependencies = new DependenciesConfiguration();
            logger.Info("Start register <IService1,Service1>");
            dependencies.Register<IService1, Service1>();
            var provider = new DependencyProvider(dependencies);
            var service1 = provider.Resolve<IService1>();
            Assert.IsNotNull(service1);
            logger.Info("End Interface test");
        }
        [TestMethod]
        public void AbstractTest()
        {
            logger.Info("Start Abstract test");
            var dependencies = new DependenciesConfiguration();
            logger.Info("Start register <AbstractService2,Service2>");
            dependencies.Register<AbstractService2, Service2>();
            var provider = new DependencyProvider(dependencies);
            var service2 = provider.Resolve<AbstractService2>();
            Assert.IsNotNull(service2);
            logger.Info("End Abstract test");
        }
        [TestMethod]
        public void AsSelfTest()
        {
            logger.Info("Start AsSelf test");
            var dependencies = new DependenciesConfiguration();
            logger.Info("Start register <Service3,Service3>");
            dependencies.Register<Service3, Service3>();
            var provider = new DependencyProvider(dependencies);
            var service3 = provider.Resolve<Service3>();
            Assert.IsNotNull(service3);
            logger.Info("End AsSelf test");
        }
        // должен быть создан ServiceImpl (реализация IService), в конструктор которому передана
        // RepositoryImpl (реализация IRepository)
        [TestMethod]
        public void RecursiveTest()
        {
            logger.Info("Start Recursive test");
            var dependencies = new DependenciesConfiguration();
            logger.Info("Start register <IService, ServiceImpl>");
            dependencies.Register<IService, ServiceImpl>();
            logger.Info("Start register <IRepository, RepositoryImpl>");
            dependencies.Register<IRepository, RepositoryImpl>();
            var provider = new DependencyProvider(dependencies);
            var service1 = provider.Resolve<IService>();
            Assert.IsNotNull(service1);
            logger.Info("End Recursive test");
        }
        [TestMethod] 
        public void NumerousImplsTest()
        {
            logger.Info("Start NumerousImpls test");
            IList<Type> implTypes = new List<Type>();
            implTypes.Add(typeof(ServiceImpl));
            implTypes.Add(typeof(ServiceImpl2));
            var dependencies = new DependenciesConfiguration();
            logger.Info("Start register <IService, ServiceImpl1>");
            dependencies.Register<IService, ServiceImpl>();
            logger.Info("Start register <IService, ServiceImpl2>");
            dependencies.Register<IService, ServiceImpl2>();
            var provider = new DependencyProvider(dependencies);
            // должен быть создан ServiceImpl (реализация IService), в конструктор которому передана
            // RepositoryImpl (реализация IRepository)
            var services =  provider.Resolve<IEnumerable<IService>>();
            Assert.IsNotNull(services);

            foreach (var implType in implTypes)
            {
                bool exists = false;
                foreach (var service in services)
                {
                    if (service.GetType() == implType)
                    {
                        exists = true;
                        break;
                    }
                }
                Assert.IsTrue(exists);
                logger.Info("End NumerousImpls test");
            }
        }
        [TestMethod] 
        public void SingletonTest()
        {
            logger.Info("Start Singleton test");
            var dependencies = new DependenciesConfiguration();
            logger.Info("Start register <IService, ServiceImpl>(DependencyTTL.SINGLETON)");
            dependencies.Register<IService, ServiceImpl>(DependencyTTL.SINGLETON);

            var provider = new DependencyProvider(dependencies);
            var inst1 = provider.Resolve<IService>();
            var inst2 = provider.Resolve<IService>();
            Assert.AreSame(inst1, inst2);
            logger.Info("End Singleton test");
        }
        interface IGeneric<T>
        {
        }
        class GenericClass<T>: IGeneric<T>
        {

        }
        class GenericClass2<T> : IGeneric<T>
        {

        }
        [TestMethod]
        public void GenericDependencyTest()
        {
            logger.Info("Start GenericDependency test");
            var dependencies = new DependenciesConfiguration();
            logger.Info("Start Register<IGeneric<Service3>, GenericClass<Service3>>()");
            dependencies.Register<IGeneric<Service3>, GenericClass<Service3>>();

            var provider = new DependencyProvider(dependencies);
            var inst1 = provider.Resolve<IGeneric<Service3>>();

            Assert.AreEqual(typeof(GenericClass<Service3>), inst1.GetType());
            logger.Info("End GenericDependency test");
        }
        [TestMethod]
        public void OpenGenericDependencyTest()
        {
            logger.Info("Start OpenGenericDependency test");
            var dependencies = new DependenciesConfiguration();
            logger.Info("Start Register(typeof(IGeneric<>), typeof(GenericClass<>))");
            dependencies.Register(typeof(IGeneric<>), typeof(GenericClass<>));

            var provider = new DependencyProvider(dependencies);
            var inst1 = provider.Resolve<IGeneric<Service3>>();

            Assert.AreEqual(typeof(GenericClass<Service3>), inst1.GetType());
            logger.Info("End OpenGenericDependency test");
        }
        [TestMethod]
        public void MultipleGenericDependencyTest()
        {
            logger.Info("Start MultipleGenericDependency test");
            var dependencies = new DependenciesConfiguration();
            logger.Info("Start Register(typeof(IGeneric<>), typeof(GenericClass<>))");
            dependencies.Register(typeof(IGeneric<>), typeof(GenericClass<>));
            logger.Info("Start Register(typeof(IGeneric<>), typeof(GenericClass2<>))");
            dependencies.Register(typeof(IGeneric<>), typeof(GenericClass2<>));
            logger.Info("Start Register<IRepository, RepositoryImpl>()");
            dependencies.Register<IRepository, RepositoryImpl>();
            var provider = new DependencyProvider(dependencies);
            var inst1 = provider.Resolve<IGeneric<Service3>>();

            Assert.AreEqual(typeof(GenericClass<Service3>), inst1.GetType());
            logger.Info("End MultipleGenericDependency test");
        }
    }
}
