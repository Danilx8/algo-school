from __future__ import annotations

from typing import Any


class HashNode:
    def __init__(self, value):
        self.value = value
        self.deleted = False
        self.repetitions = 0


class Bag:
    def __init__(self):
        self.Size = 20_000
        self.step = 3
        self.slots = [HashNode(None) for _ in range(self.Size)]

    def hash_fun(self, value):
        return abs(hash(value)) % self.Size

    def seek_slot(self, value):
        index = self.hash_fun(value)

        for checked in range(self.Size):
            if self.slots[index].value is None or self.slots[index].value == value:
                return index

            index = (index + self.step) % self.Size

        return -1

    def size(self) -> int:
        size = 0
        for i in self.slots:
            if i.value is not None and not i.deleted:
                size += i.repetitions
        return size

    def put(self, value: Any) -> None:
        # всегда срабатывает
        index = self.seek_slot(value)

        if index == -1:
            return

        if self.slots[index].value is not None:
            self.slots[index].repetitions += 1
        else:
            self.slots[index].value = value
            self.slots[index].deleted = False
            self.slots[index].repetitions = 1

    def remove(self, value: Any) -> None:
        index = self.seek_slot(value)

        if index == -1:
            return

        if self.slots[index].repetitions > 1:
            self.slots[index].repetitions -= 1
        else:
            self.slots[index].value = None
            self.slots[index].deleted = True
            self.slots[index].repetitions = 0

    def print(self) -> None:
        for i in self.slots:
            if i.value is not None:
                print(i.value + " встречается " + str(i.repetitions) + " раз")
