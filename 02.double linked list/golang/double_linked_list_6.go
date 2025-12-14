// Добавьте метод, объединяющий два списка в третий. Эти списки предварительно надо отсортировать, и выдать результирующий список, в котором все элементы также будут упорядочены (для результирующего списка использовать метод сортировки не разрешается).
package main

func (l *LinkedList2) Combine(l2 *LinkedList2) *LinkedList2 {
	result := new(LinkedList2)

	l.Sort()
	l2.Sort()

	node1 := l.head
	node2 := l2.head

	for node1 != nil || node2 != nil {
		var current *Node
		if node2 == nil || (node1 != nil && node1.value <= node2.value) {
			current = node1
			node1 = node1.next
		} else {
			current = node2
			node2 = node2.next
		}

		result.AddInTail(*current)
	}

	return result
}
