using System;
using HoldTheAllergen.Backend.Core.Dependency;
using StructureMap;

namespace HoldTheAllergen.Backend.Core
{
    public class IoCHelper
    {
        public static IContainer Container
        {
            get { return ObjectFactory.Container; }
        }

        public static T GetInstance<T>(Type requestedType)
        {
            return (T) ObjectFactory.GetInstance(requestedType);
        }

        public static T GetInstance<T>()
        {
            return GetInstance<T>(typeof (T));
        }

        public static void Initialize()
        {
            DependencyRegistrar.Initialize();
        }
    }
}