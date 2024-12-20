from python.linked_list import LinkedList
from functools import reduce


def main(lists: [LinkedList]) -> [int]:
    length = lists[0].len()
    if not map(lambda x: x.len() == length, lists):
        raise Exception('lists must have same length')

    result = []

    nodes = [linked_list.head for linked_list in lists]
    for _ in range(len(lists)):
        result.append(reduce(lambda x, y: x.value + y.value, nodes))
        map(lambda x: x.next(), nodes)

    return result
