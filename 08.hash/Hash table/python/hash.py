class HashTable:

    def __init__(self, sz, stp):
        self.count = 0
        self.size = sz
        self.step = stp
        self.slots = [None] * self.size

    def hash_fun(self, value):
        # в качестве value поступают строки!
        # всегда возвращает корректный индекс слота
        return abs(hash(value)) % self.size

    def seek_slot(self, value):
        # находит индекс пустого слота для значения, или None
        index = self.hash_fun(value)
        checked_amount = 0

        while checked_amount < self.size:
            if self.slots[index] is None:
                return index

            checked_amount += 1
            index = (index + self.step) % self.size

        return None

    def put(self, value):
        # записываем значение по хэш-функции

        # возвращается индекс слота или None,
        # если из-за коллизий элемент не удаётся
        # разместить

        if self.find(value) is not None:
            return None

        index = self.seek_slot(value)
        if index is None:
            return None

        self.slots[index] = value
        self.count += 1
        return index

    def find(self, value):
        # находит индекс слота со значением, или None
        if self.count == 0:
            return None

        index = self.hash_fun(value)
        checked_amount = 0

        while checked_amount < self.size:
            if self.slots[index] is not None and self.slots[index] == value:
                return index
            checked_amount += 1
            index = (index + self.step) % self.size

        return None
