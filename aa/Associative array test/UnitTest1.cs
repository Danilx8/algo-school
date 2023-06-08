namespace AlgorithmsDataStructures
{
    [TestClass]
    public class UnitTest1
    {
        NativeDictionary<int> dict;

        [TestInitialize]
        public void TestInitialize()
        {
            dict = new NativeDictionary<int>(19);
        }

        [TestMethod]
        public void AddTest()
        {
            dict.Put("first", 1);
            Assert.AreEqual(1, dict.Get("first"));
        }
    }
}