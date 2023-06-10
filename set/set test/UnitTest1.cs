using AlgorithmsDataStructures;

namespace set_test
{
    [TestClass]
    public class UnitTest1
    {
        PowerSet<string> set;

        [TestInitialize]
        public void TestInitialize()
        {
            set = new PowerSet<string>();
        }

        [TestMethod]
        public void AddTest()
        {
            int index = set.Put("John Doe");
            Assert.AreNotEqual(-1, index);
            Assert.AreEqual(-1, set.Put("John Doe"));

            //for (int i = 0; i < 5; ++i)
            //{
            //    set.Put(i.ToString());
            //}

            //PowerSet<string> set2 = new PowerSet<string>();
            //for (int j = 0; j < 10; ++j)
            //{
            //    set2.Put(j.ToString());
            //}

            //for (int i = 0; i < 5; ++i)
            //{
            //    Assert.AreNotEqual(-1, set.Find(i.ToString()));
            //}

            //for (int j = 5; j < 10; ++j)
            //{
            //    Assert.AreNotEqual(-1, set.Find(j.ToString()));
            //}
        }

        [TestMethod]
        public void RemoveTest()
        {
            int index = set.Put("John Doe");

            Assert.IsTrue(set.Remove("John Doe"));
        }

        [TestMethod]
        public void IntersectionNonEmptyTest()
        {
            for (int i = 0; i < 5; ++i)
            {
                set.Put(i.ToString());
            }

            PowerSet<string> set2 = new PowerSet<string>();
            for (int j = 0; j < 10; ++j)
            {
                set2.Put(j.ToString());
            }

            PowerSet<string> resultSet = set.Intersection(set2);
            for (int i = 0; i < 5; ++i)
            {
                Assert.AreNotEqual(-1, resultSet.Find(i.ToString()));
            }

            for (int j = 5; j < 10; ++j)
            {
                Assert.AreEqual(-1, resultSet.Find(j.ToString()));
            }
        }

        [TestMethod]
        public void IntersectionEmptyTest()
        {
            for (int i = 0; i < 5; ++i)
            {
                set.Put(i.ToString());
            }

            PowerSet<string> set2 = new PowerSet<string>();
            for (int j = 5; j < 10; ++j)
            {
                set2.Put(j.ToString());
            }

            PowerSet<string> resultSet = set.Intersection(set2);
            Assert.AreEqual(0, resultSet.Size());
        }

        [TestMethod]
        public void GoodUnion()
        {
            for (int i = 0; i < 5; ++i)
            {
                set.Put(i.ToString());
            }

            PowerSet<string> set2 = new PowerSet<string>();
            for (int j = 3; j < 8; ++j)
            {
                set2.Put(j.ToString());
            }

            PowerSet<string> resultSet = set.Union(set2);
            for (int i = 0; i < 8; ++i)
            {
                Assert.AreNotEqual(-1, resultSet.Find(i.ToString()));
            }
        }

        [TestMethod]
        public void LeftBadUnion()
        {
            PowerSet<string> set2 = new PowerSet<string>();
            for (int i = 0; i < 5; ++i)
            {
                set2.Put(i.ToString());
            }

            PowerSet<string> resultSet = set.Union(set2);
            for (int j = 0; j < 5; ++j)
            {
                Assert.AreNotEqual(-1, resultSet.Find(j.ToString()));
            }
        }

        [TestMethod]
        public void RightBadUnion()
        {
            PowerSet<string> set2 = new PowerSet<string>();
            for (int i = 0; i < 5; ++i)
            {
                set.Put(i.ToString());
            }

            PowerSet<string> resultSet = set.Union(set2);
            for (int j = 0; j < 5; ++j)
            {
                Assert.AreNotEqual(-1, resultSet.Find(j.ToString()));
            }
        }

        [TestMethod]
        public void NonEmptyDifference()
        {
            for (int i = 0; i < 5; ++i)
            {
                set.Put(i.ToString());
            }

            PowerSet<string> set2 = new PowerSet<string>();
            for (int j = 3; j < 8; ++j)
            {
                set2.Put(j.ToString());
            }

            PowerSet<string> resultSet = set.Difference(set2);
            for (int i = 0; i < 3; ++i)
            {
                Assert.AreNotEqual(-1, resultSet.Find(i.ToString()));
            }
        }

        [TestMethod]
        public void EmptyDifference()
        {
            for (int i = 0; i < 5; ++i)
            {
                set.Put(i.ToString());
            }

            PowerSet<string> set2 = new PowerSet<string>();
            for (int j = 0; j < 5; ++j)
            {
                set2.Put(j.ToString());
            }

            PowerSet<string> resultSet = set.Difference(set2);
            Assert.AreEqual(0, resultSet.Size());
        }

        [TestMethod]
        public void FullSubSet()
        {
            for (int i = 0; i < 5; ++i)
            {
                set.Put(i.ToString());
            }

            PowerSet<string> set2 = new PowerSet<string>();
            for (int j = 1; j < 4; ++j)
            {
                set2.Put(j.ToString());
            }

            Assert.IsTrue(set.IsSubset(set2));
        }

        [TestMethod]
        public void ReverseSubSet()
        {
            for (int i = 1; i < 4; ++i)
            {
                set.Put(i.ToString());
            }

            PowerSet<string> set2 = new PowerSet<string>();
            for (int j = 0; j < 5; ++j)
            {
                set2.Put(j.ToString());
            }

            Assert.IsFalse(set.IsSubset(set2));
        }

        [TestMethod]
        public void OutOfRangeSubSet()
        {
            for (int i = 0; i < 5; ++i)
            {
                set.Put(i.ToString());
            }

            PowerSet<string> set2 = new PowerSet<string>();
            for (int j = 0; j < 8; ++j)
            {
                set2.Put(j.ToString());
            }

            Assert.IsFalse(set.IsSubset(set2));
        }
    }
}