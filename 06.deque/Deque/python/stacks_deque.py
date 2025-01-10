class Stack:
    def __init__(self):
        self.stack = []

    def size(self):
        return len(self.stack)

    def pop(self):
        if self.size() > 0:
            return self.stack.pop()
        return None

    def push(self, value):
        self.stack.append(value)

    def peek(self):
        if self.size() > 0:
            return self.stack[self.size() - 1]
        return None # если стек пустой


class DualArrayQueue:
    def __init__(self):
        self.front = Stack()
        self.back = Stack()

    def enqueue(self, item):
        self.front.push(item)

    def dequeue(self):
        if self.front.size() == 0 and self.back.size() == 0: return

        if self.back.size() == 0:
            while self.front:
                self.back.push(self.front.pop())
        return self.back.pop()

    def size(self):
        return self.front.size() + self.back.size()
