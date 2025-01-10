class Queue:
    def __init__(self):
        # инициализация хранилища данных
        self.headIndex = 0
        self.values = []

    def enqueue(self, item):
        # вставка в хвост
        self.values.insert(0, item)
        self.headIndex += 1

    def dequeue(self):
        # выдача из головы
        if self.headIndex == 0:
            return None

        required = self.values[self.headIndex - 1]
        self.values.remove(self.values[self.headIndex - 1])
        self.headIndex -= 1
        return required

    def size(self):
        return self.headIndex  # размер очереди

    def rotate_queue(self, n):
        if n > self.size() or n < 0:
            return

        for _ in range(n):
            item = self.dequeue()
            self.enqueue(item)

    def reverse(self):
        tmp = []
        while self.size() > 0:
            item = self.dequeue()
            tmp.append(item)

        while len(tmp) > 0:
            item = tmp.pop()
            self.enqueue(item)

