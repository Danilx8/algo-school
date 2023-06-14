namespace AlgorithmsDataStructures
{
    [TestClass]
    public class UnitTest1
    {
        NativeCache<int> cache;
        const int SIZE = 5;

        [TestInitialize]
        public void TestInitialize()
        {
            cache = new NativeCache<int>(SIZE);
        }

        [TestMethod]
        public void AddTest()
        {
            for (int i = 0; i < SIZE; ++i)
            {
                cache.Add(i.ToString(), i);
            }

            for (int i = 0; i < SIZE; ++i)
            {
                Assert.IsTrue(cache.IsKey(i.ToString()));
            }
        }

        [TestMethod]
        public void CollisionTriggerTest()
        {
            cache.Add("John Doe", 18);
            Assert.AreEqual(18, cache.Get("John Doe"));
            cache.Add("John Doe", 36);
            Assert.AreEqual(36, cache.Get("John Doe"));
            Assert.AreEqual(2, cache.hits[cache.CalculateHash("John Doe")]);

            cache.Add("Ivan", 9);
            cache.Get("Ivan");
            cache.Get("Ivan");
            cache.Get("Ivan");
            Assert.AreEqual(3, cache.hits[cache.CalculateHash("Ivan")]);

            cache.Add("Danil", 9);
            cache.Get("Danil");
            cache.Get("Danil");
            cache.Get("Danil");
            Assert.AreEqual(3, cache.hits[cache.CalculateHash("Danil")]);

            cache.Add("Dima", 9);
            cache.Get("Dima");
            cache.Get("Dima");
            cache.Get("Dima");
            Assert.AreEqual(3, cache.hits[cache.CalculateHash("Dima")]);

            cache.Add("Julia", 9);
            cache.Get("Julia");
            cache.Get("Julia");
            cache.Get("Julia");
            Assert.AreEqual(3, cache.hits[cache.CalculateHash("Julia")]);

            cache.Add("Boris", 9);
            cache.Get("Boris");
            cache.Get("Boris");
            cache.Get("Boris");
            Assert.AreEqual(3, cache.hits[cache.CalculateHash("Boris")]);

            cache.Add("Jury", 9);
            cache.Get("Jury");
            cache.Get("Jury");
            cache.Get("Jury");
            Assert.AreEqual(3, cache.hits[cache.CalculateHash("Jury")]);

            Assert.IsFalse(cache.IsKey("John Doe"));
        }
    }
}