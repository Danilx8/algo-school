// фиктивный/пустой (dummy) узел
package main

import "errors"

type DummyNode struct {
	prev    *DummyNode
	next    *DummyNode
	value   int
	isDummy bool
}

type DummyLinkedList struct {
	head *DummyNode
	tail *DummyNode
}

func (l *DummyLinkedList) Init() *DummyLinkedList {
	l.head.next = l.tail
	l.tail.prev = l.head
	l.head.isDummy = true
	l.tail.isDummy = true

	return l
}

func (l *DummyLinkedList) AddInTail(item DummyNode) {
	item.prev = l.tail.prev
	l.tail.prev.next = &item
	l.tail.prev = &item
	item.next = l.tail
}

func (l *DummyLinkedList) Find(value int) (DummyNode, error) {
	node := l.head.next
	for !node.isDummy {
		if node.value == value {
			return *node, nil
		}
		node = node.next
	}
	return DummyNode{value: -1, next: nil}, errors.New("node not found")
}

func (l *DummyLinkedList) FindAll(value int) []DummyNode {
	var nodes []DummyNode
	node := l.head.next
	for !node.isDummy {
		if node.value == value {
			nodes = append(nodes, *node)
		}
		node = node.next
	}
	return nodes
}

func (l *DummyLinkedList) Delete(value int, all bool) {
	if l.head == nil || l.head.next == nil {
		return // пустой список
	}

	deleted := false
	first := l.head       // предыдущий узел (изначально dummy)
	second := l.head.next // текущий проверяемый узел

	for !second.isDummy {
		if second.value == value {
			// Удаляем second
			first.next = second.next
			second.next.prev = first

			deleted = true

			if !all {
				return // удалили одно — выходим
			}

			// Продолжаем с следующего узла
			second = first.next
		} else {
			// Переходим дальше
			first = second
			second = second.next
		}
	}

	// Корректируем tail, если удалили последний элемент
	if l.tail.value == value && deleted {
		// Находим новый tail — последний не-dummy узел
		node := l.head
		for !node.next.isDummy {
			node = node.next
		}
		l.tail = node
	}
}

func (l *DummyLinkedList) Insert(after *DummyNode, add DummyNode) {
	node := l.head.next

	if after == nil {
		l.head.next.prev = &add
		add.prev = l.head
		add.next = l.head.next
		l.head.next = &add
	} else if after == l.tail.next {
		l.AddInTail(add)
	} else if !node.isDummy {
		for !node.next.isDummy {
			if node == after {
				add.next = node.next
				add.prev = node
				node.next.prev = &add
				node.next = &add
				break
			}
			node = node.next
		}
	}
}

func (l *DummyLinkedList) InsertFirst(first DummyNode) {
	l.Insert(nil, first)
}

func (l *DummyLinkedList) Count() int {
	count := 0
	node := l.head.next
	for !node.isDummy {
		count++
		node = node.next
	}
	return count
}

func (l *DummyLinkedList) Clean() {
	l.head.next = l.tail
	l.tail.prev = l.head
}
