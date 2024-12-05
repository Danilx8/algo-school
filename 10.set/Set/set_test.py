import time
import unittest
from python import PowerSet

class SetTest(unittest.TestCase):
    def setUp(self):
        self.set = PowerSet()

    def test_add(self):
        self.set.put("John Doe")
        self.set.remove("John Doe")
        self.assertFalse(self.set.get("John Doe"))
        self.set.put("John Doe")
        self.assertTrue(self.set.get("John Doe"))
        self.assertEqual(1, self.set.size())

    def test_remove(self):
        self.set.put("John Doe")
        self.assertTrue(self.set.remove("John Doe"))

    def test_intersection_non_empty(self):
        for i in range(5):
            self.set.put(str(i))

        set2 = PowerSet()
        for j in range(10):
            set2.put(str(j))

        resultSet = self.set.intersection(set2)
        for i in range(5):
            self.assertNotEqual(False, resultSet.get(str(i)))

        for j in range(5, 10):
            self.assertFalse(resultSet.get(str(j)))
        self.assertEqual(5, resultSet.size())

    def test_intersection_empty(self):
        for i in range(5):
            self.set.put(str(i))

        set2 = PowerSet()
        for j in range(5, 10):
            set2.put(str(j))

        resultSet = self.set.intersection(set2)
        self.assertEqual(0, resultSet.size())

    def test_good_union(self):
        for i in range(5):
            self.set.put(str(i))

        set2 = PowerSet()
        for j in range(3, 8):
            set2.put(str(j))

        resultSet = self.set.union(set2)
        for i in range(8):
            self.assertTrue(resultSet.get(str(i)))
        self.assertEqual(8, resultSet.size())

    def test_left_bad_union(self):
        set2 = PowerSet()
        for i in range(5):
            set2.put(str(i))

        resultSet = self.set.union(set2)
        for j in range(5):
            self.assertTrue(resultSet.get(str(j)))
        self.assertEqual(5, resultSet.size())

    def test_right_bad_union(self):
        set2 = PowerSet()
        for i in range(5):
            self.set.put(str(i))

        resultSet = self.set.union(set2)
        for j in range(5):
            self.assertTrue(resultSet.get(str(j)))
        self.assertEqual(5, self.set.size())

    def test_non_empty_difference(self):
        for i in range(5):
            self.set.put(str(i))

        set2 = PowerSet()
        for j in range(3, 8):
            set2.put(str(j))

        resultSet = self.set.difference(set2)
        for i in range(3):
            self.assertTrue(resultSet.get(str(i)))
        self.assertEqual(3, resultSet.size())

    def test_empty_difference(self):
        for i in range(5):
            self.set.put(str(i))

        set2 = PowerSet()
        for j in range(5):
            set2.put(str(j))

        resultSet = self.set.difference(set2)
        self.assertEqual(0, resultSet.size())

    def test_full_subset(self):
        for i in range(5):
            self.set.put(str(i))

        set2 = PowerSet()
        for j in range(1, 4):
            set2.put(str(j))

        self.assertTrue(self.set.issubset(set2))

    def test_reverse_subset(self):
        for i in range(1, 4):
            self.set.put(str(i))

        set2 = PowerSet()
        for j in range(5):
            set2.put(str(j))

        self.assertFalse(self.set.issubset(set2))

    def test_out_of_range_subset(self):
        for i in range(5):
            self.set.put(str(i))

        set2 = PowerSet()
        for j in range(8):
            set2.put(str(j))

        self.assertFalse(self.set.issubset(set2))

    def test_get(self):
        for i in range(5):
            self.set.put(str(i))

        set2 = PowerSet()
        for j in range(10):
            set2.put(str(j))

        for i in range(5):
            self.assertTrue(self.set.get(str(i)))

        for j in range(10):
            self.assertTrue(set2.get(str(j)))

    def test_very_big_get(self):
        SIZE = 2000
        for i in range(SIZE):
            self.set.put(str(i))
            # print(self.set.find(str(i)))

        self.assertEqual(SIZE, self.set.size())

        for i in range(SIZE):
            self.assertTrue(self.set.get(str(i)))

    def test_put_existing_and_non_existing_element(self):
        self.set.put("John Doe")
        self.assertTrue(self.set.put("Jane Smith"))
        self.assertFalse(self.set.put("John Doe"))

    def test_remove_element(self):
        self.set.put("John Doe")
        self.assertTrue(self.set.remove("John Doe"))

    def test_intersection_empty_sets(self):
        resultSet = self.set.intersection(PowerSet())
        self.assertEqual(0, resultSet.size())

    def test_intersection_non_empty_sets(self):
        set2 = PowerSet()
        set2.put("Alice")
        set2.put("Bob")
        self.set.put("Bob")
        resultSet = self.set.intersection(set2)
        self.assertEqual(1, resultSet.size())

    def test_union_both_non_empty_sets(self):
        set2 = PowerSet()
        set2.put("Alice")
        set2.put("Bob")
        self.set.put("John")
        resultSet = self.set.union(set2)
        self.assertEqual(3, resultSet.size())

    def test_union_one_empty_set(self):
        set2 = PowerSet()
        set2.put("Alice")
        set2.put("Bob")
        resultSet = self.set.union(set2)
        self.assertEqual(2, resultSet.size())

    def test_difference_empty_sets(self):
        resultSet = self.set.difference(PowerSet())
        self.assertEqual(0, resultSet.size())

    def test_difference_non_empty_sets(self):
        set2 = PowerSet()
        set2.put("Alice")
        set2.put("Bob")
        self.set.put("Bob")
        resultSet = self.set.difference(set2)
        self.assertEqual(0, resultSet.size())

    def test_issubset_all_elements_in_parameter(self):
        self.set.put("Alice")
        self.set.put("Bob")
        set2 = PowerSet()
        set2.put("Alice")
        set2.put("Bob")
        self.assertTrue(self.set.issubset(set2))

    def test_issubset_all_elements_in_current_set(self):
        self.set.put("Alice")
        self.set.put("Bob")
        set2 = PowerSet()
        set2.put("Bob")
        self.assertFalse(self.set.issubset(set2))

    def test_issubset_not_all_elements_in_current_set(self):
        self.set.put("Alice")
        set2 = PowerSet()
        set2.put("Bob")
        self.assertFalse(self.set.issubset(set2))

    def test_equals(self):
        self.set.put("Alice")
        self.set.put("Bob")
        set2 = PowerSet()
        set2.put("Alice")
        set2.put("Bob")
        self.assertEqual(self.set, set2)

    def test_performance_large_sets(self):
        SIZE = 10000
        for i in range(SIZE):
            self.set.put(str(i))
        set2 = PowerSet()
        for j in range(SIZE//2, SIZE*3//2):
            set2.put(str(j))

        intersection_start_time = time.time()
        self.set.intersection(set2)
        intersection_end_time = time.time()

        union_start_time = time.time()
        self.set.union(set2)
        union_end_time = time.time()

        difference_start_time = time.time()
        self.set.difference(set2)
        difference_end_time = time.time()

        self.assertLess(intersection_end_time - intersection_start_time, 2)
        self.assertLess(union_end_time - union_start_time, 2)
        self.assertLess(difference_end_time - difference_start_time, 2)

    def test_cartesian_product_empty_sets(self):
        resultSet = self.set.cartesian_product(PowerSet())
        self.assertEqual(0, resultSet.size())

    def test_cartesian_product_non_empty_sets(self):
        self.set.put("A")
        self.set.put("B")
        set2 = PowerSet()
        set2.put("1")
        set2.put("2")
        resultSet = self.set.cartesian_product(set2)
        self.assertEqual(4, resultSet.size())
        self.assertTrue(resultSet.get(("A", "1")))
        self.assertTrue(resultSet.get(("A", "2")))
        self.assertTrue(resultSet.get(("B", "1")))
        self.assertTrue(resultSet.get(("B", "2")))

    def test_cartesian_product_large_sets(self):
        SIZE = 100
        set2 = PowerSet()

        for i in range(SIZE):
            self.set.put(str(i))
            set2.put(str(i))

        resultSet = self.set.cartesian_product(set2)
        self.assertEqual(SIZE**2, resultSet.size())


if __name__ == '__main__':
    unittest.main()