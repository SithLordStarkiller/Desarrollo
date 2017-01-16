using System.Diagnostics;
using System.Reflection;

namespace PubliPayments
{
    public static class Versionado
    {
        private static string _versionActualPrivate = "";
        private static void ObtenerVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            _versionActualPrivate = fvi.FileVersion;
        }

        public static string VersionActual
        {
            get
            {
                if (_versionActualPrivate == "")
                    ObtenerVersion();
                return _versionActualPrivate;
            }
        }
    }
}
