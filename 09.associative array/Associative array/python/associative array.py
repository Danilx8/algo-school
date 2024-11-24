class NativeDictionary:
    def __init__(self, sz):
        self.size = sz
        self.slots = [None] * self.size
        self.values = [None] * self.size

    def hash_fun(self, key):
        # в качестве key поступают строки!
        # всегда возвращает корректный индекс слота
        return abs(hash(key)) % self.size

    def is_key(self, key):
        # возвращает True если ключ имеется,
        # иначе False
        if self.slots.__contains__(key):
            return True
        return False

    def put(self, key, value):
        # гарантированно записываем
        # значение value по ключу key
        self.slots[self.hash_fun(key)] = key
        self.values[self.hash_fun(key)] = value

    def get(self, key):
        # возвращает value для key,
        # или None если ключ не найден
        index = self.hash_fun(key)
        if index > self.size:
            return None

        return self.values[index]
