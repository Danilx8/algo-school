import unittest
from python.cache import NativeCache


class TestNativeCache(unittest.TestCase):

    def setUp(self):
        self.cache = NativeCache(5)

    def test_add(self):
        for i in range(5):
            self.cache.add(str(i), i)

        for i in range(5):
            self.assertTrue(self.cache.is_key(str(i)))

    def test_collision_trigger(self):
        self.cache.add("John Doe", 18)
        self.assertEqual(18, self.cache.get("John Doe"))
        self.cache.add("John Doe", 36)
        self.assertEqual(36, self.cache.get("John Doe"))
        self.assertEqual(2, self.cache.hits[self.cache.calculate_hash("John Doe")])

        self.cache.add("Ivan", 9)
        self.cache.get("Ivan")
        self.cache.get("Ivan")
        self.cache.get("Ivan")
        self.assertEqual(3, self.cache.hits[self.cache.calculate_hash("Ivan")])

        self.cache.add("Danil", 9)
        self.cache.get("Danil")
        self.cache.get("Danil")
        self.cache.get("Danil")
        self.assertEqual(3, self.cache.hits[self.cache.calculate_hash("Danil")])

        self.cache.add("Dima", 9)
        self.cache.get("Dima")
        self.cache.get("Dima")
        self.cache.get("Dima")
        self.assertEqual(3, self.cache.hits[self.cache.calculate_hash("Dima")])

        self.cache.add("Julia", 9)
        self.cache.get("Julia")
        self.cache.get("Julia")
        self.cache.get("Julia")
        self.assertEqual(3, self.cache.hits[self.cache.calculate_hash("Julia")])

        self.cache.add("Boris", 9)
        self.cache.get("Boris")
        self.cache.get("Boris")
        self.cache.get("Boris")
        self.assertEqual(3, self.cache.hits[self.cache.calculate_hash("Boris")])

        self.cache.add("Jury", 9)
        self.cache.get("Jury")
        self.cache.get("Jury")
        self.cache.get("Jury")
        self.assertEqual(3, self.cache.hits[self.cache.calculate_hash("Jury")])

        self.assertFalse(self.cache.is_key("John Doe"))


if __name__ == '__main__':
    unittest.main()
