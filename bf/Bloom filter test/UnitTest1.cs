namespace AlgorithmsDataStructures
{
    [TestClass]
    public class UnitTest1
    {
        BloomFilter filter;
        const int AMOUNT = 10;
        
        [TestInitialize]
        public void TestInitialize()
        {
            filter = new BloomFilter(32);
        }

        [TestMethod]
        [DataRow("0123456789")]
        [DataRow("1234567890")]
        [DataRow("2345678901")]
        [DataRow("3456789012")]
        [DataRow("4567890123")]
        [DataRow("5678901234")]
        [DataRow("6789012345")]
        [DataRow("7890123456")]
        [DataRow("8901234567")]
        [DataRow("9012345678")]
        public void SeparateTest(string value)
        {
            filter.Add(value);
            Assert.IsTrue(filter.IsValue(value));
        }

        [TestMethod]
        [DataRow(new string[] {"0123456789", "1234567890", "2345678901", "3456789012", "4567890123",
            "5678901234", "6789012345", "7890123456", "8901234567", "9012345678"})]
        public void CombinedTest(string[] values)
        {
            for (int i = 0; i < AMOUNT; ++i)
            {
                filter.Add(values[i]);
            }

            for (int i = 0; i < AMOUNT; ++i)
            {
                Assert.IsTrue(filter.IsValue(values[i]));
            }
        }
    }
}