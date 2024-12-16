class NativeCache:
    def __init__(self, sz):
        self.size = sz
        self.STEP = 3
        self.slots = [None] * self.size
        self.values = [None] * self.size
        self.hits = [0] * self.size

    def calculate_hash(self, value):
        return abs(hash(value)) % self.size

    def is_key(self, key):
        return key in self.slots

    def find_least_wanted(self):
        min_index = 0
        min_value = self.hits[0]

        for value_index in range(self.size):
            if self.hits[value_index] < min_value:
                min_index = value_index
                min_value = self.hits[value_index]

        return min_index

    def remove(self, index):
        self.hits[index] = 0
        self.values[index] = None
        self.slots[index] = None

    def add(self, key, value):
        index = self.calculate_hash(key)
        unable_to_insert = True

        for visited_values in range(self.size):
            if self.slots[index] is None or key == self.slots[index]:
                self.slots[index] = key
                self.values[index] = value
                unable_to_insert = False
                break

            index = (index + self.STEP) % self.size

        if unable_to_insert:
            index = self.find_least_wanted()
            self.remove(index)
            self.add(key, value)

    def get(self, key):
        index = self.calculate_hash(key)

        if index > self.size:
            return None

        for visited_values in range(self.size):
            if key == self.slots[index]:
                self.hits[index] += 1
                return self.values[index]

            index = (index + self.STEP) % self.size

        return None
