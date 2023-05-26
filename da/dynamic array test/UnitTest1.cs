using System.Runtime.CompilerServices;

namespace AlgorithmsDataStructures
{
    [TestClass]
    public class UnitTest1
    {
        DynArray<int> bigArray;
        DynArray<int> emptyArray;
        const int LENGTH = 16;

        [TestInitialize]
        public void TestInitialize()
        {
            bigArray    = new DynArray<int>();
            emptyArray  = new DynArray<int>();
            
            for (int i = 0; i < LENGTH; ++i)
            {
                bigArray.Append(i);
            }
        }

        [TestMethod]
        public void EmptyInsert()
        {
            emptyArray.Insert(8, 0);
            CollectionAssert.AreEqual(new int[] { 16, 1, 8 }, 
                new int[] { emptyArray.capacity, emptyArray.count, emptyArray.GetItem(0) });
        }

        [TestMethod]
        public void BigInsertWithoutExtension()
        {
            int newValue = 12;
            bigArray.Remove(3);
            bigArray.Insert(newValue, 3);
            CollectionAssert.AreEqual(new int[] { 16, 16, 12 },
                new int[] { bigArray.capacity, bigArray.count, bigArray.GetItem(3) });
        }

        [TestMethod]
        public void BigInsertWithExtension()
        {
            int newValue = 12;
            bigArray.Insert(newValue, 0);
            CollectionAssert.AreEqual(new int[] { 32, 17, newValue }, 
                new int[] { bigArray.capacity, bigArray.count, bigArray.GetItem(0) });
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException),"Index out of range.")]
        public void WrongInsert()
        {
            int newValue = 12;
            //bigArray.Insert(newValue, -2);
            bigArray.Insert(newValue, 30);
            //EmpyArray.Insert(newValue, 1);
        }

        [TestMethod]
        public void NonShrinkingRemove()
        {
            int deletedValue = 12;
            bigArray.Append(deletedValue);
            int followingValue = 18;
            bigArray.Append(followingValue);
            bigArray.Remove(16);
            CollectionAssert.AreEqual(new int[] { 17, 32, followingValue }, 
                new int[] { bigArray.count, bigArray.capacity, bigArray.GetItem(16)});
        }

        [TestMethod]
        public void ShrinkingRemove()
        {
            int deletedValue = 12;
            bigArray.Append(deletedValue);
            bigArray.Remove(16);
            bigArray.Remove(0);
            CollectionAssert.AreEqual(new int[] { 15, (int)(32 / 3 * 2), LENGTH - 1}, 
                new int[] { bigArray.count, bigArray.capacity, bigArray.GetItem(14) });
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "Index out of range.")]
        public void FailedRemove()
        {
            emptyArray.Remove(9);
        }
    }
}