namespace AlgorithmsDataStructures
{
    [TestClass]
    public class UnitTest1
    {
        HashTable table;

        [TestInitialize]
        public void TestInitialize()
        {
            table = new HashTable(17, 3);
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreNotEqual(-1, table.SeekSlot("Hello, world"));
        }
    }
}