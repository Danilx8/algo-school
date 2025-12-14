package main

import (
	"errors"
	"os"
	"reflect"
)

type Node struct {
	prev  *Node
	next  *Node
	value int
}

type LinkedList2 struct {
	head *Node
	tail *Node
}

func (l *LinkedList2) AddInTail(item Node) {
	if l.head == nil {
		l.head = &item
		l.head.next = nil
		l.head.prev = nil
	} else {
		l.tail.next = &item
		item.prev = l.tail
		item.next = nil
	}
	l.tail = &item
}

func (l *LinkedList2) Count() int {
	count := 0
	node := l.head
	for node != nil {
		count++
		node = node.next
	}
	return count
}

// error не nil, если узел не найден
func (l *LinkedList2) Find(n int) (Node, error) {
	node := l.head
	for node != nil {
		if node.value == n {
			return *node, nil
		}
		node = node.next
	}
	return Node{value: -1, next: nil}, errors.New("node not found")
}

func (l *LinkedList2) FindAll(n int) []Node {
	var nodes []Node
	node := l.head
	for node != nil {
		if node.value == n {
			nodes = append(nodes, *node)
		}
		node = node.next
	}
	return nodes
}

func (l *LinkedList2) Delete(n int, all bool) {
	if all {
		for l.head != nil && l.head.value == n {
			l.head = l.head.next
			if l.head == nil {
				l.tail = nil
			} else {
				l.head.prev = nil
			}
		}
		if l.head == nil {
			return
		}
		node := l.head
		for node != nil {
			if node.next != nil && node.next.value == n {
				toRemove := node.next
				node.next = toRemove.next
				if node.next == nil {
					l.tail = node
				} else {
					node.next.prev = node
				}
			} else {
				node = node.next
			}
		}
	} else {
		if l.head == nil {
			return
		}
		if l.head.value == n {
			l.head = l.head.next
			if l.head == nil {
				l.tail = nil
			} else {
				l.head.prev = nil
			}
			return
		}
		node := l.head
		for node.next != nil {
			if node.next.value == n {
				toRemove := node.next
				node.next = toRemove.next
				if node.next == nil {
					l.tail = node
				} else {
					node.next.prev = node
				}
				return
			}
			node = node.next
		}
	}
}

func (l *LinkedList2) Insert(after *Node, add Node) {
	if after == nil {
		add.next = l.head
		add.prev = nil
		if l.head != nil {
			l.head.prev = &add
		} else {
			l.tail = &add
		}
		l.head = &add
		return
	}
	node := l.head
	for node != nil {
		if node == after {
			add.next = node.next
			add.prev = node
			if node.next != nil {
				node.next.prev = &add
			} else {
				l.tail = &add
			}
			node.next = &add
			return
		}
		node = node.next
	}
}

func (l *LinkedList2) InsertFirst(first Node) {
	l.Insert(nil, first)
}

func (l *LinkedList2) Clean() {
	current := l.head
	for current != nil {
		next := current.next
		current.next = nil
		current.prev = nil
		current = next
	}
	l.head = nil
	l.tail = nil
}
