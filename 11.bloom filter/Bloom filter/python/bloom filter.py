class ArrayOfBits:
    BitsPerInt = 32

    def __init__(self, size):
        arraySize = (size + ArrayOfBits.BitsPerInt - 1) // ArrayOfBits.BitsPerInt
        self.data = [0 for _ in range(arraySize)]

    def get(self, index):
        intIndex = index // ArrayOfBits.BitsPerInt
        bitOffset = index % ArrayOfBits.BitsPerInt
        return (self.data[intIndex] >> bitOffset) & 1 != 0

    def set(self, index, value):
        intIndex = index // ArrayOfBits.BitsPerInt
        bitOffset = index % ArrayOfBits.BitsPerInt

        if value:
            self.data[intIndex] |= 1 << bitOffset
        else:
            self.data[intIndex] &= ~(1 << bitOffset)


class BloomFilter:
    def __init__(self, f_len):
        self.filter_len = 32
        self.array = ArrayOfBits(self.filter_len)

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
        self.array.set(self.hash1(str1), True)
        self.array.set(self.hash2(str1), True)

    def is_value(self, str1):
        return self.array.get(self.hash1(str1)) and self.array.get(self.hash2(str1))

