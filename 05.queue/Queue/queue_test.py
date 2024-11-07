import unittest
from python.queue import Queue


class TestQueue(unittest.TestCase):
    LENGTH = 10

    def setUp(self):
        self.empty_queue = Queue()
        self.big_queue = Queue()
        for i in range(self.LENGTH):
            self.big_queue.enqueue(i)

    def test_empty_enqueue(self):
        self.empty_queue.enqueue(10)
        self.assertEqual(1, self.empty_queue.size())

    def test_clear_queue(self):
        self.assertEqual([0, 1, 2, self.big_queue.size() - 3],
                         [self.big_queue.dequeue(),
                          self.big_queue.dequeue(),
                          self.big_queue.dequeue(),
                          self.big_queue.size()])


if __name__ == '__main__':
    unittest.main()
