class AverageStack:
    def __init__(self):
        self.stack = []
        self.total = 0
        self.count = 0

    def size(self):
        return len(self.stack)

    def pop(self):
        if self.size() > 0:
            value = self.stack.pop()
            self.total -= value
            self.count -= 1
            return value
        return None

    def push(self, value):
        self.stack.append(value)
        self.total += value
        self.count += 1

    def peek(self):
        if self.size() > 0:
            return self.stack[self.size() - 1]
        return None

    def average(self):
        if self.count > 0:
            return self.total / self.count
        return None
