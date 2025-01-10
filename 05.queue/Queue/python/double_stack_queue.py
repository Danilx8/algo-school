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


class DoubleStackQueue:
    def __init__(self):
        self.back = Stack()
        self.front = Stack()

    def enqueue(self, value):
        self.back.push(value)

    def dequeue(self):
        if self.back.size() == 0 and self.front.size() == 0:
            return None

        if self.front.size() == 0 and self.back.size() != 0:
            while self.back.size() > 0:
                self.front.push(self.back.pop())
            return self.front.pop()
        else:
            return self.front.pop()

    def size(self):
        return self.front.size() + self.back.size()
