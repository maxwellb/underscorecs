using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnderscoreCs.Tests
{
    using Duck = Dictionary<string, object>;

    [TestClass]
    public class Collections
    {
        #region each

        /// <summary>
        /// http://underscorejs.org/#each
        /// each	<c>_.each(list, iteratee, [context])</c>	Alias: forEach 
        /// Iterates over a list of elements, yielding each in turn to an iteratee function.
        /// The iteratee is bound to the context object, if one is passed. Each invocation of iteratee
        /// is called with three arguments: <c>(element, index, list)</c>.
        /// If list is a JavaScript object, iteratee's arguments will be <c>(value, key, list)</c>.
        /// Returns the list for chaining.
        /// </summary>
        public string EachDoc;

        [TestMethod]
        public void Each_GivenIEnumerable_AndIteratee_WhenCalled_ThenEachElementInListIsVisited()
        {
            IEnumerable<string> list = new[] { "A", "B", "C" };
            var mock = new Mock<IObserver<Duck>>(MockBehavior.Loose);
            Action<string> iteratee = element =>
            {
                mock.Object.OnNext(new Duck { { "element", element } });
            };

            _.Each(list, iteratee);

            mock.Verify(m => m.OnNext(It.Is<Duck>(v => (string)v["element"] == "A")));
            mock.Verify(m => m.OnNext(It.Is<Duck>(v => (string)v["element"] == "B")));
            mock.Verify(m => m.OnNext(It.Is<Duck>(v => (string)v["element"] == "C")));
        }

        public class Foo
        {
            public string Bar { get; set; }
        }

        [TestMethod]
        public void Each_GivenArbitraryTypeForList_WhenCalled_ThenEachElementInListIsVisited()
        {
            IEnumerable<Foo> list = new[] { new Foo { Bar = "A" }, new Foo { Bar = "B" }, new Foo { Bar = "C" } };
            var mock = new Mock<IObserver<Duck>>(MockBehavior.Loose);
            Action<Foo> iteratee = element =>
            {
                mock.Object.OnNext(new Duck { { "element", element } });
            };

            _.Each(list, iteratee);

            mock.Verify(m => m.OnNext(It.Is<Duck>(v => ((Foo)v["element"]).Bar == "A")));
            mock.Verify(m => m.OnNext(It.Is<Duck>(v => ((Foo)v["element"]).Bar == "B")));
            mock.Verify(m => m.OnNext(It.Is<Duck>(v => ((Foo)v["element"]).Bar == "C")));
        }

        [TestMethod]
        public void Each_GivenIEnumerable_AndIterateeThatTakesInt_WhenCalled_ThenEachElementInListIsVisited()
        {
            IEnumerable<string> list = new[] { "D", "C", "A", "B" };
            var mock = new Mock<IObserver<Duck>>(MockBehavior.Loose);
            Action<string, int> iteratee = (element, index) =>
            {
                mock.Object.OnNext(new Duck { { "element", element }, { "index", index } });
            };

            _.Each(list, iteratee);

            mock.Verify(m => m.OnNext(It.Is<Duck>(v => (string)v["element"] == "A" && (int)v["index"] == 2)));
            mock.Verify(m => m.OnNext(It.Is<Duck>(v => (string)v["element"] == "B" && (int)v["index"] == 3)));
            mock.Verify(m => m.OnNext(It.Is<Duck>(v => (string)v["element"] == "C" && (int)v["index"] == 1)));
            mock.Verify(m => m.OnNext(It.Is<Duck>(v => (string)v["element"] == "D" && (int)v["index"] == 0)));
        }

        [TestMethod]
        public void Each_GivenArbitraryTypeForList_AndIterateeThatTakesInt_WhenCalled_ThenEachElementInListIsVisited()
        {
            IEnumerable<Foo> list = new[] { new Foo { Bar = "A" }, new Foo { Bar = "B" }, new Foo { Bar = "C" } };
            var mock = new Mock<IObserver<Duck>>(MockBehavior.Loose);
            Action<Foo, int> iteratee = (element, index) =>
            {
                mock.Object.OnNext(new Duck { { "element", element }, { "index", index } });
            };

            _.Each(list, iteratee);

            mock.Verify(m => m.OnNext(It.Is<Duck>(v => ((Foo)v["element"]).Bar == "A" && (int)v["index"] == 0)));
            mock.Verify(m => m.OnNext(It.Is<Duck>(v => ((Foo)v["element"]).Bar == "B" && (int)v["index"] == 1)));
            mock.Verify(m => m.OnNext(It.Is<Duck>(v => ((Foo)v["element"]).Bar == "C" && (int)v["index"] == 2)));
        }

        [TestMethod]
        public void Each_GivenADictionaryForList_WhenCalled_ThenIterateePassesKeyAndValue()
        {
            Dictionary<string, int> list = new Dictionary<string, int> { { "Foo", -1 }, { "Bar", -2 }, { "Baz", -3 } };
            var mock = new Mock<IObserver<Duck>>(MockBehavior.Loose);
            Action<int, string> iteratee = (value, key) =>
            {
                mock.Object.OnNext(new Duck { { "key", key }, { "value", value } });
            };

            _.Each(list, iteratee);

            mock.Verify(m => m.OnNext(It.Is<Duck>(v => (string)v["key"] == "Foo" && (int)v["value"] == -1)));
            mock.Verify(m => m.OnNext(It.Is<Duck>(v => (string)v["key"] == "Bar" && (int)v["value"] == -2)));
            mock.Verify(m => m.OnNext(It.Is<Duck>(v => (string)v["key"] == "Baz" && (int)v["value"] == -3)));
        }

        [TestMethod]
        public void Each_GivenADictionaryForList_AndIterateeHasOneParameter_WhenCalled_ThenIterateePassesValue()
        {
            Dictionary<string, int> list = new Dictionary<string, int> { { "Foo", -1 }, { "Bar", -2 }, { "Baz", -3 } };
            var mock = new Mock<IObserver<Duck>>(MockBehavior.Loose);
            Action<int> iteratee = (value) =>
            {
                mock.Object.OnNext(new Duck { { "value", value } });
            };

            _.Each(list, iteratee);

            mock.Verify(m => m.OnNext(It.Is<Duck>(v => (int)v["value"] == -1)));
            mock.Verify(m => m.OnNext(It.Is<Duck>(v => (int)v["value"] == -2)));
            mock.Verify(m => m.OnNext(It.Is<Duck>(v => (int)v["value"] == -3)));
        }

        #endregion

        #region map

        /// <summary>
        /// map		<c>_.map(list, iteratee, [context])</c>	Alias: collect  
        /// Produces a new array of values by mapping each value in list through a
        /// transformation function (iteratee). The <c>iteratee</c> is passed three arguments: the value,
        /// then the <c>index</c> (or <c>key</c>) of the iteration, and finally a reference to the entire <c>list</c>.
        /// </summary>
        public string MapDoc;

        #endregion
    }
}
