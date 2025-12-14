// Добавьте булев метод, который сообщает, имеются ли циклы (замкнутые на себя по кругу) внутри списка.
package main

func (l *LinkedList2) CheckCycles() bool {
	node := l.head
	for range l.Count() {
		node = node.next
	}

	return node != l.tail
}
