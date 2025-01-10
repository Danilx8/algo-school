class Deque:
    def __init__(self):
        # инициализация внутреннего хранилища
        self.elements = []

    def addFront(self, item):
        # добавление в голову
        self.elements.append(item)

    def addTail(self, item):
        # добавление в хвост
        self.elements.insert(0, item)

    def removeFront(self):
        # удаление из головы
        if len(self.elements) == 0: return None  # если очередь пуста

        result = self.elements[len(self.elements) - 1]
        self.elements.remove(result)
        return result

    def removeTail(self):
        # удаление из хвоста
        if len(self.elements) == 0: return None  # если очередь пуста

        result = self.elements[0]
        self.elements.remove(result)
        return result

    def size(self):
        return len(self.elements)  # размер очереди

    def rotate(self, n):
        if self.size() == 0:
            return

        n = n % self.size()
        if n > 0:
            for _ in range(n):
                item = self.removeTail()
                self.addFront(item)
        elif n < 0:
            for _ in range(-n):
                item = self.removeFront()
                self.addTail(item)

