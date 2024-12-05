import unittest
from python import Bag


class TestBag(unittest.TestCase):
    def setUp(self):
        self.bag = Bag()

    def test_put(self):
        self.bag.put(1)
        self.assertEqual(self.bag.size(), 1)

        self.bag.put(1)
        self.assertEqual(self.bag.size(), 2)

    def test_remove(self):
        self.bag.put(1)
        self.bag.put(1)

        self.bag.remove(1)
        self.assertEqual(self.bag.size(), 1)

        self.bag.remove(1)
        self.assertEqual(self.bag.size(), 0)

    def test_size(self):
        self.assertEqual(self.bag.size(), 0)

        self.bag.put(1)
        self.assertEqual(self.bag.size(), 1)

    def test_hash_fun(self):
        value = "test"
        hash_value = self.bag.hash_fun(value)
        self.assertIsInstance(hash_value, int)

    def test_seek_slot(self):
        self.bag.put(1)
        index = self.bag.seek_slot(1)
        self.assertGreaterEqual(index, 0)
        self.assertLess(index, 20000)

if __name__ == '__main__':
    unittest.main()
