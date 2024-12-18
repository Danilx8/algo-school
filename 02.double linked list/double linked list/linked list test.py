import unittest
from python import LinkedList2, Node, combine


class TestDoublyLinkedList(unittest.TestCase):
    def setUp(self):
        # Test 1: No cycle in doubly linked list
        self.dll_no_cycle = LinkedList2()
        for i in range(1, 6):
            self.dll_no_cycle.add_in_tail(Node(i))

        # Test 2: Cycle in doubly linked list
        self.dll_cycle = LinkedList2()
        for char in ['A', 'B', 'C', 'D', 'E']:
            self.dll_cycle.add_in_tail(Node(char))
        self.dll_cycle.tail.next = self.dll_cycle.head.next  # Introduce cycle

        # Test 3: Reverse a doubly linked list
        self.dll_reverse = LinkedList2()
        for i in range(1, 6):
            self.dll_reverse.add_in_tail(Node(i))

        # Test 4: Reverse a doubly linked list with characters
        self.dll_reverse_chars = LinkedList2()
        for char in ['a', 'b', 'c', 'd', 'e']:
            self.dll_reverse_chars.add_in_tail(Node(char))

        self.dll = LinkedList2()

        self.list1 = LinkedList2()
        self.list2 = LinkedList2()

    def test_has_cycle(self):
        self.assertFalse(self.dll_no_cycle.contains_cycles())  # Test 1: Expect no cycle
        self.assertTrue(self.dll_cycle.contains_cycles())      # Test 2: Expect a cycle

    def test_reverse_list(self):
        expected_reversed_list = [5, 4, 3, 2, 1]
        self.dll_reverse.traverse()
        reversed_list = []
        current = self.dll_reverse.head
        while current:
            reversed_list.append(current.value)
            current = current.next
        self.assertEqual(reversed_list, expected_reversed_list)  # Test 3: Expect reversed list to match

        expected_reversed_list_chars = ['e', 'd', 'c', 'b', 'a']
        self.dll_reverse_chars.traverse()
        reversed_list_chars = []
        current = self.dll_reverse_chars.head
        while current:
            reversed_list_chars.append(current.value)
            current = current.next
        self.assertEqual(reversed_list_chars, expected_reversed_list_chars)  # Test 4: Expect reversed list to match

    def test_sort_method(self):
        # Test sorting an unsorted list
        unsorted_values = [3, 1, 2]
        sorted_values = sorted(unsorted_values)

        for value in unsorted_values:
            self.dll.add_in_tail(Node(value))

        self.dll.sort()

        sorted_list_values = []
        current = self.dll.head
        while current:
            sorted_list_values.append(current.value)
            current = current.next

        # Check if the list is sorted correctly
        self.assertEqual(sorted_list_values, sorted_values)

        # Check if the head and tail are correct after sorting
        self.assertEqual(self.dll.head.value, sorted_values[0])
        self.assertEqual(self.dll.tail.value, sorted_values[-1])

    def test_combine_sorted_lists(self):
        list1_values = [1, 3, 5]
        list2_values = [2, 4, 6]

        for value in list1_values:
            self.list1.add_in_tail(Node(value))

        for value in list2_values:
            self.list2.add_in_tail(Node(value))

        result = combine(self.list1, self.list2)

        expected_sorted_values = [1, 2, 3, 4, 5, 6]
        sorted_values = []
        current = result.head
        while current:
            sorted_values.append(current.value)
            current = current.next

        self.assertEqual(sorted_values, expected_sorted_values)

    def test_combine_with_empty_lists(self):
        result = combine(self.list1, self.list2)
        self.assertIsNone(result.head)
        self.assertIsNone(result.tail)


if __name__ == '__main__':
    unittest.main()
