using StructureMap;

namespace HoldTheAllergen.API.Core.Dependency
{
    public class IoCBootstrapper
    {
        private static readonly object Lock = new object();
        private static bool _initialized;

        public static IContainer Initialize()
        {
            if (_initialized) return IoCHelper.Container;

            lock (Lock)
            {
                if (_initialized) return IoCHelper.Container;

                _initialized = true;

                IoCHelper.Initialize();
            }

            return IoCHelper.Container;
        }
    }
}