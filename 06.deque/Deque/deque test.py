import unittest
from python.deque import Deque

class MyTestCase(unittest.TestCase):
    def setUp(self):
        self.empty_deque = Deque()
        self.single_deque = Deque()
        self.big_deque = Deque()

        self.single_deque.addFront(1)

        for i in range(10):
            if i % 2 == 0:
                self.big_deque.addFront(i)
            else:
                self.big_deque.addTail(i)

    def test_empty_deque_size(self):
        self.assertEqual(0, self.empty_deque.size())

    def test_single_add_difference(self):
        self.assertEqual((self.single_deque.size(), self.single_deque.removeTail()), (1, 1))

    def test_different_additions(self):
        self.empty_deque.addFront(12)
        new_empty_deque = Deque()

        new_empty_deque.addTail(12)
        self.assertEqual(self.empty_deque.removeTail(), new_empty_deque.removeFront())


if __name__ == '__main__':
    unittest.main()
