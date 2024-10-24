import unittest
from python.dynamic_array import DynArray
from random import randint


class MyTestCase(unittest.TestCase):
    def test_insert_simplt(self):
        array = DynArray()
        for i in range(16):
            array.insert(i, i * 2)

        for i in range(16):
            self.assertEqual(array[i], i * 2)

        self.assertEqual(array.capacity, 16)


if __name__ == '__main__':
    unittest.main()
