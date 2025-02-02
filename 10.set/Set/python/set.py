from __future__ import annotations

from typing import Any
from functools import reduce

class HashNode:
    def __init__(self, value):
        self.value = value
        self.deleted = False


def multiple_intersection(sets: [PowerSet]) -> PowerSet:
    if len(sets) < 3:
        raise Exception('Not enough sets')

    result = reduce(lambda x,y: x.intersection(y), sets)

    return result


class PowerSet:
    def __init__(self) -> None:
        # ваша реализация хранилища
        self.Size = 20_000
        self.step = 3
        self.slots = [HashNode(None) for _ in range(self.Size)]

    def hash_fun(self, value):
        return abs(hash(value)) % self.Size

    def seek_slot(self, value):
        index = self.hash_fun(value)

        for checked in range(self.Size):
            if self.slots[checked].value == value:
                return -1

            if self.slots[index].value is None:
                return index

            index = (index + self.step) % self.Size

        return -1

    def size(self) -> int:
        size = 0
        for i in self.slots:
            if i.value is not None and i.deleted is False:
                size += 1
        return size

    def put(self, value: Any) -> None:
        # всегда срабатывает
        index = -1
        if not self.get(value):
            index = self.seek_slot(value)

        if index == -1:
            return

        self.slots[index].value = value
        self.slots[index].deleted = False

    def get(self, value: Any) -> bool:
        # возвращает True если value имеется в множестве,
        # иначе False
        index = self.hash_fun(value)

        for checked in range(self.Size):
            if self.slots[index].value == value:
                return True
            if self.slots[index].value is None and not self.slots[index].deleted:
                return False

            index = (index + self.step) % self.Size

        return False

    def remove(self, value: Any) -> bool:
        # возвращает True если value удалено
        # иначе False
        index = self.hash_fun(value)

        for checked in range(self.Size):
            if self.slots[index].value is not None and self.slots[index].value == value:
                self.slots[index].value = None
                self.slots[index].deleted = True
                return True

            index = (index + self.step) % self.Size

        return False

    def intersection(self, set2: PowerSet) -> PowerSet:
        # пересечение текущего множества и set2
        result = PowerSet()

        for checked in range(self.Size):
            current_value = self.slots[checked].value
            if current_value is not None and current_value == set2.slots[checked].value:
                result.put(self.slots[checked].value)

        return result

    def union(self, set2: PowerSet) -> PowerSet:
        # объединение текущего множества и set2
        result = PowerSet()

        for checked in range(self.Size):
            if self.slots[checked].value is not None:
                result.put(self.slots[checked].value)
            if set2.slots[checked].value is not None:
                result.put(set2.slots[checked].value)

        return result

    def difference(self, set2: PowerSet) -> PowerSet:
        # разница текущего множества и set2
        result = PowerSet()
        first_values = []

        for checked in range(self.Size):
            if self.slots[checked].value is not None:
                first_values.append(self.slots[checked].value)

        for checked in range(self.Size):
            if set2.slots[checked].value is not None and set2.slots[checked].value in first_values:
                first_values.remove(set2.slots[checked].value)

        for value in first_values:
            result.put(value)

        return result

    def issubset(self, set2: PowerSet) -> bool:
        # возвращает True, если set2 есть
        # подмножество текущего множества,
        # иначе False
        for node in set2.slots:
            if node.value is not None and not self.get(node.value):
                return False

        return True

    def equals(self, set2: PowerSet) -> bool:
        # возвращает True,
        # если set2 равно текущему множеству,
        # иначе False
        for index in range(self.Size):
            if self.slots[index].value != set2.slots[index].value:
                return False
        return True

    def cartesian_product(self, set2: PowerSet) -> PowerSet:
        result = PowerSet()

        for first_checked in self.slots:
            if first_checked.value is None:
                continue

            for second_checked in set2.slots:
                if second_checked.value is None:
                    continue

                result.put((first_checked.value, second_checked.value))

        return result
