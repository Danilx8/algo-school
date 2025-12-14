// Добавьте булев метод, который сообщает, имеются ли циклы (замкнутые на себя по кругу) внутри списка.
package main

func (l *LinkedList2) Sort() {
	if l.head == nil {
		return
	}

	current := l.head
	for current != nil {
		next := current.next
		for next != nil {
			if current.value > next.value {
				current.value, next.value = next.value, current.value
			}
			next = next.next
		}
		current = current.next
	}

	current = l.head
	for current.next != nil {
		current = current.next
	}
	l.tail = current
}
