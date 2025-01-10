from stack import Stack


class MinStack:
    def __init__(self):
        self.main_stack = Stack()
        self.min_stack = Stack()

    def size(self):
        return self.main_stack.size()

    def push(self, item):
        self.main_stack.push(item)
        if len(self.min_stack.stack) == 0 or item <= self.min_stack.peek():
            self.min_stack.push(item)

    def pop(self):
        if len(self.main_stack.stack) != 0:
            item = self.main_stack.pop()
            if item == self.min_stack.peek():
                self.min_stack.pop()
            return item
        return None

    def peek(self):
        return self.main_stack.peek()

    def get_min(self):
        return self.min_stack.peek()
