using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace MinimalServer.Config
{
    public static class Certificate
    {
        public static X509Certificate2 Load()
        {
            var assembly = typeof(Certificate).Assembly;
            using (var stream = assembly.GetManifestResourceStream("MinimalServer.Config.idsrv3test.pfx"))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return new X509Certificate2(memoryStream.ToArray(), "idsrv3test");
                }
            }
        }
    }
}