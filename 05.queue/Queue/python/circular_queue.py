class CircularQueue:
    def __init__(self, size):
        self.size = size
        self.queue = [None] * size
        self.front = 0
        self.back = 0
        self.count = 0

    def enqueue(self, item):
        if self.is_full():
            return False
        self.queue[self.back] = item
        self.back = (self.back + 1) % self.size
        self.count += 1
        return True

    def dequeue(self):
        if self.count == 0:
            return None
        item = self.queue[self.front]
        self.front = (self.front + 1) % self.size
        self.count -= 1
        return item

    def is_full(self):
        return self.count == self.size

    def size(self):
        return self.count
