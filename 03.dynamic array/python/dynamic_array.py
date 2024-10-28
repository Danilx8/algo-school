import ctypes


class DynArray:

    def __init__(self):
        self.count = 0
        self.capacity = 16
        self.array = self.make_array(self.capacity)

    def __len__(self):
        return self.count

    def make_array(self, new_capacity):
        return (new_capacity * ctypes.py_object)()

    def __getitem__(self, i):
        if i < 0 or i >= self.count:
            raise IndexError('Index is out of bounds')
        return self.array[i]

    def resize(self, new_capacity):
        new_array = self.make_array(new_capacity)
        for i in range(self.count):
            new_array[i] = self.array[i]
        self.array = new_array
        self.capacity = new_capacity

    def append(self, itm):
        if self.count == self.capacity:
            self.resize(2 * self.capacity)
        self.array[self.count] = itm
        self.count += 1

    def insert(self, i, itm):
        # добавляем объект itm в позицию i, начиная с 0
        if i < 0 or i > self.count:
            raise IndexError('Index out of range')

        if i == self.count:
            self.append(itm)
            return

        if self.count >= self.capacity:
            self.resize(2 * self.capacity)

        for i in range(self.count - 1, i - 1, -1):
            self.array[i + 1] = self.array[i]
        self.array[i] = itm
        self.count += 1

    def delete(self, i):
        if i < 0 or i >= self.count:
            raise IndexError('Index is out of bounds')
        else:
            new_array = self.make_array(self.capacity)
            for j in range(i):
                new_array[j] = self.array[j]
            for j in range(i, self.count - 1):
                new_array[j] = self.array[j + 1]
            self.array = new_array
            self.count -= 1

            if self.count < self.capacity / 2 and self.capacity > 16:
                if int(self.capacity / 1.5) >= 16:
                    self.resize(int(self.capacity / 1.5))
                else:
                    self.resize(16)