class Node:
    def __init__(self, v):
        self.value = v
        self.prev = None
        self.next = None


class LinkedList2:
    def __init__(self):
        self.head = None
        self.tail = None

    def add_in_tail(self, item):
        if self.head is None:
            self.head = item
            item.prev = None
            item.next = None
        else:
            self.tail.next = item
            item.prev = self.tail
        self.tail = item

    def find(self, val):
        node = self.head
        while node is not None:
            if node.value == val:
                return node
            node = node.next
        return None

    def find_all(self, val):
        node = self.head
        nodes = []
        while node is not None:
            if node.value == val:
                nodes.append(node)
            node = node.next
        return nodes

    def delete(self, val, all=False):
        node = self.head

        while node is not None and node.value == val:  # Удаление если это первый(е) элемент(ы)
            self.head = self.head.next
            if self.head is None:
                self.tail = None
            else:
                self.head.prev = None
            node = self.head
            if not all:
                return

        before = None

        while node is not None:
            while node is not None and node.value != val:
                before = node
                node = node.next

            if node is None:
                return

            before.next = node.next
            node = node.next
            if node is None:
                self.tail = before
            else:
                node.prev = before

            if not all:
                return

    def clean(self):
        current = self.head
        pointer = self.head.next
        while pointer:
            current.next = None
            current.prev = None
            current = pointer
            pointer = pointer.next
        self.head = None
        self.tail = None

    def len(self):
        counter = 0
        node = self.head
        while node:
            counter += 1
            node = node.next
        return counter

    def insert(self, afterNode, newNode):
        node = self.head

        if afterNode is None:  # Вставка в начало
            self.add_in_tail(newNode)
            return

        if self.tail == afterNode:  # Вставка в конец
            self.add_in_tail(newNode)
            return

        if node is None:
            return

        while node.next is not None:  # Вставка в середину
            if node == afterNode:
                newNode.next = node.next
                newNode.prev = node
                node.next.prev = newNode
                node.next = newNode
                return
            node = node.next

    def add_in_head(self, newNode):
        newNode.prev = None
        newNode.next = self.head
        if self.head:
            self.head.prev = newNode
        else:
            self.tail = newNode
        self.head = newNode
        return

    def traverse(self):
        tmp = None
        node = self.tail

        while node is not None:
            tmp = node.next
            node.next = node.prev
            node.prev = tmp
            node = node.next

        if tmp is not None:
            self.head, self.tail = self.tail, self.head

