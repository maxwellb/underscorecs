
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnderscoreCs.Tests
{
    [TestClass]
    public class Collections_Map
    {


        #region map

        /// <summary>
        /// map		<c>_.map(list, iteratee, [context])</c>	Alias: collect  
        /// Produces a new array of values by mapping each value in list through a
        /// transformation function (iteratee). The <c>iteratee</c> is passed three arguments: the value,
        /// then the <c>index</c> (or <c>key</c>) of the iteration, and finally a reference to the entire <c>list</c>.
        /// </summary>
        public string MapDoc;


        [TestMethod]
        public void Map_BasicMap()
        {
            var input = new List<int> { 1, 2, 3 };
            var mapper = new Func<int, int>(i => i*3);
            var output = _.Map(input, mapper);
            var expected = new [] { 3, 6, 9 };
            CollectionAssert.AreEqual(expected, output.ToList());
        }

        #endregion
    }
}