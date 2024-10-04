import unittest
from python.linked_list import Node, LinkedList


class LinkedListTestCase(unittest.TestCase):
    def setUp(self):
        self.linked_list = LinkedList()

    def test_add_in_tail(self):
        node1 = Node(1)
        self.linked_list.add_in_tail(node1)
        self.assertEqual(self.linked_list.head, node1)

    def test_find(self):
        node1 = Node(1)
        node2 = Node(2)
        self.linked_list.add_in_tail(node1)
        self.linked_list.add_in_tail(node2)
        self.assertEqual(self.linked_list.find(2), node2)
        self.assertIsNone(self.linked_list.find(3))

    def test_find_all(self):
        node1 = Node(1)
        node2 = Node(2)
        node3 = Node(1)
        self.linked_list.add_in_tail(node1)
        self.linked_list.add_in_tail(node2)
        self.linked_list.add_in_tail(node3)
        self.assertEqual(self.linked_list.find_all(1), [node1, node3])
        self.assertEqual(self.linked_list.find_all(3), [])

    def test_delete(self):
        node1 = Node(1)
        node2 = Node(2)
        self.linked_list.add_in_tail(node1)
        self.linked_list.add_in_tail(node2)
        self.linked_list.delete(1)
        self.assertEqual(self.linked_list.head, node2)
        self.linked_list.delete(2)
        self.assertIsNone(self.linked_list.head)

    def test_delete_all_occurences(self):
        node1 = Node(1)
        node2 = Node(1)
        node3 = Node(1)
        self.linked_list.add_in_tail(node1)
        self.linked_list.add_in_tail(node2)
        self.linked_list.add_in_tail(node3)
        self.linked_list.delete(1, all=True)
        self.assertIsNone(self.linked_list.head)

    def test_clean(self):
        node1 = Node(1)
        self.linked_list.add_in_tail(node1)
        self.linked_list.clean()
        self.assertIsNone(self.linked_list.head)

    def test_insert(self):
        node1 = Node(1)
        node2 = Node(2)
        node3 = Node(3)
        self.linked_list.add_in_tail(node1)
        self.linked_list.add_in_tail(node2)
        self.linked_list.insert(node1, node3)
        self.assertEqual(node1.next, node3)
        self.assertEqual(node3.next, node2)


if __name__ == '__main__':
    unittest.main()

