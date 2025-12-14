// Добавьте метод, который "переворачивает" порядок элементов в связном списке, меняя его на противоположный.
package main

func (l *LinkedList2) Flip() {
	var tmp *Node
	current := l.head

	for current != nil {
		tmp = current.next
		current.next = current.prev
		current.prev = tmp
		current = current.next
	}

	if tmp == nil {
		l.head, l.tail = l.tail, l.head
	}
}
