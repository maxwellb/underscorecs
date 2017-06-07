using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnderscoreCs.Tests
{
    [TestClass]
    public class Underscore
    {

        [TestMethod]
        public void Underscore_exists()
        {
            Assembly.Load(new AssemblyName { Name = "UnderscoreCs" }).GetType("UnderscoreCs._", throwOnError: true, ignoreCase: false);
        }
    }
}
