from functools import reduce


class BitArray:
    def __init__(self, size):
        self.size = size
        self.arr = [0] * ((size + 31) // 32)  # Use integer division to determine the number of integers needed

    def set(self, index):
        if index < 0 or index >= self.size:
            raise IndexError("Index out of bounds")
        array_index = index // 32
        bit_offset = index % 32
        self.arr[array_index] |= 1 << bit_offset

    def clear(self, index):
        if index < 0 or index >= self.size:
            raise IndexError("Index out of bounds")
        array_index = index // 32
        bit_offset = index % 32
        self.arr[array_index] &= ~(1 << bit_offset)

    def get(self, index):
        if index < 0 or index >= self.size:
            raise IndexError("Index out of bounds")
        array_index = index // 32
        bit_offset = index % 32
        return (self.arr[array_index] >> bit_offset) & 1


class BloomFilter:
    def __init__(self, f_len):
        self.filter_len = 32
        self.array = BitArray(f_len)

    def hash1(self, str1):
        MULTIPLIER = 17
        sum_val = 0

        for i in range(len(str1)):
            code = ord(str1[i])
            sum_val *= MULTIPLIER
            sum_val += code

        return int(sum_val % self.filter_len)

    def hash2(self, str1):
        MULTIPLIER = 223
        sum_val = 0

        for i in range(len(str1)):
            code = ord(str1[i])
            sum_val *= MULTIPLIER
            sum_val += code

        return int(sum_val % self.filter_len)

    def add(self, str1):
        self.array.set(self.hash1(str1))
        self.array.set(self.hash2(str1))

    def is_value(self, str1):
        return self.array.get(self.hash1(str1)) and self.array.get(self.hash2(str1))


# склейка фильтров блюма значительно увеличит ложные срабатывания
# не проверяю здесь совпадают ли длины фильтров
def filters_mash(filters: [BloomFilter]) -> [BloomFilter]:
    result = BloomFilter(32)

    indices_to_set = [i for i in range(32) for f in filters if f.array.arr[i]]

    for idx in indices_to_set:
        result.array.arr[idx] = 1
