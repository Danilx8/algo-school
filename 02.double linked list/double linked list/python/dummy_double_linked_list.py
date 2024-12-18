class Node:
    def __init__(self, value=None):
        self.value = value
        self.prev = None
        self.next = None


class DummyNode(Node):
    def __init__(self):
        super().__init__()
        self.next = None
        self.prev = None


class DummyLinkedList:
    def __init__(self):
        self.head = DummyNode()
        self.tail = DummyNode()
        self.head.next = self.tail
        self.tail.prev = self.head

    def add_in_tail(self, item):
        item.prev = self.tail.prev
        self.tail.prev.next = item
        self.tail.prev = item
        item.next = self.tail

    def find(self, value):
        node = self.head.next
        while not isinstance(node, DummyNode):
            if node.value == value:
                return node
            node = node.next
        return None

    def find_all(self, value):
        nodes = []
        node = self.head.next
        while not isinstance(node, DummyNode):
            if node.value == value:
                nodes.append(node)
            node = node.next
        return nodes

    def remove(self, value):
        first = self.head
        second = self.head.next
        while not isinstance(second, DummyNode) and second.value != value:
            second = second.next
            first = first.next
        if isinstance(second, DummyNode):
            return False
        first.next = second.next
        second.next.prev = first
        return True

    def remove_all(self, value):
        node = self.head.next

        while not isinstance(node, DummyNode) and node.value == value:
            self.head.next = self.head.next.next
            if isinstance(self.head.next, DummyNode):
                self.tail.prev = self.head
            else:
                self.head.next.prev = None
            node = self.head.next

        before = None

        while not isinstance(node, DummyNode):
            while not isinstance(node, DummyNode) and node.value != value:
                before = node
                node = node.next

            if isinstance(node, DummyNode):
                break

            before.next = node.next
            node = node.next
            if isinstance(node, DummyNode):
                self.tail.prev = before
            else:
                node.prev = before

    def clear(self):
        self.head.next = self.tail
        self.tail.prev = self.head

    def count(self):
        node = self.head.next
        count = 0

        while not isinstance(node, DummyNode):
            count += 1
            node = node.next
        return count

    def insert(self, node_after, node_to_insert):
        node = self.head.next

        if node_after is None:
            self.head.next.prev = node_to_insert
            node_to_insert.prev = self.head
            node_to_insert.next = self.head.next
            self.head.next = node_to_insert
        elif node_after == self.tail.next:
            self.add_in_tail(node_to_insert)
        elif not isinstance(node, DummyNode):
            while not isinstance(node.next, DummyNode):
                if node == node_after:
                    node_to_insert.next = node.next
                    node_to_insert.prev = node
                    node.next.prev = node_to_insert
                    node.next = node_to_insert
                    break
                node = node.next
