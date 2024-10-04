from python.linked_list import LinkedList


def main(first: LinkedList, second: LinkedList):
    if first.len() != second.len():
        return

    result = []
    node1 = first.head
    node2 = second.head
    for i in range(first.len()):
        result += [node1.val + node2.val]
        node1 = node1.next
        node2 = node2.next
    return result
