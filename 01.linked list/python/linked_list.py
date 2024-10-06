class Node:
    def __init__(self, v):
        self.value = v
        self.next = None


class LinkedList:

    def __init__(self):
        self.head = None
        self.tail = None

    def add_in_tail(self, item):
        if self.head is None:
            self.head = item
        else:
            self.tail.next = item
        self.tail = item

    def print_all_nodes(self):
        node = self.head
        while node != None:
            print(node.value)
            node = node.next

    def find(self, val):
        node = self.head
        while node is not None:
            if node.value == val:
                return node
            node = node.next
        return None

    def find_all(self, val):
        nodes = []
        node = self.head
        while node is not None:
            if node.value == val:
                nodes += [node]
            node = node.next
        return nodes

    def delete(self, val, all=False):
        if self.head is None:  # Check if list is not empty
            return

        node = self.head
        while node is not None and node.value == val:  # Delete first node(s) that are equal(s) given value
            self.head = self.head.next
            node = self.head
            if self.head is None:
                self.tail = None
            if not all:
                return

        previous_node = None
        while node is not None:  # Delete node(s) in middle
            while node is not None and node.value != val:
                previous_node = node
                node = node.next

            if node is None:
                return

            previous_node.next = node.next
            node = previous_node.next
            if previous_node.next is None:
                self.tail = previous_node

            if not all:
                break

    def clean(self):
        node = self.head
        pointer = node.next
        while pointer is not None:
            node.next = pointer.next
            node = pointer
            pointer = pointer.next
        self.head = None
        self.tail = None

    def len(self):
        node = self.head
        count = 0
        while node is not None:
            count += 1
            node = node.next
        return count

    def insert(self, afterNode, newNode):
        if self.head is None:
            self.add_in_tail(newNode)
        node = self.head
        while node is not None:
            if node == afterNode:
                newNode.next = node.next
                node.next = newNode
                if node.next.next is None:
                    self.tail = node.next
                break
            node = node.next
        return

