package main

import (
	"errors"
)

type Node struct {
	next  *Node
	value int
}

type LinkedList struct {
	head *Node
	tail *Node
}

func (l *LinkedList) AddInTail(item Node) {
	if l.head == nil {
		l.head = &item
	} else {
		l.tail.next = &item
	}

	l.tail = &item
}

func (l *LinkedList) Find(n int) (Node, error) {
	node := l.head
	for node != nil {
		if node.value == n {
			return *node, nil
		}
		node = node.next
	}
	return Node{value: -1, next: nil}, errors.New("couldn't find node with such value")
}

func (l *LinkedList) FindAll(n int) []Node {
	nodes := make([]Node, 0)
	node := l.head
	for node != nil {
		if node.value == n {
			// append a detached copy (next = nil) for predictable equality in tests
			nodes = append(nodes, Node{value: node.value})
		}
		node = node.next
	}
	return nodes
}

func (l *LinkedList) Delete(n int, all bool) {
	if l.head == nil {
		return
	}

	node := l.head

	// Delete from the beginning while head matches
	for node != nil && node.value == n {
		l.head = l.head.next
		node = l.head
		if l.head == nil {
			l.tail = nil
		}
		if !all {
			return
		}
	}

	var prev *Node
	for node != nil {
		for node != nil && node.value != n {
			prev = node
			node = node.next
		}

		if node == nil {
			return
		}

		// remove node
		prev.next = node.next
		node = prev.next
		if prev.next == nil {
			l.tail = prev
		}

		if !all {
			break
		}
	}
}

func (l *LinkedList) Count() int {
	count := 0
	node := l.head
	for node != nil {
		count++
		node = node.next
	}
	return count
}

func (l *LinkedList) Insert(after *Node, add Node) {
	if after == l.tail {
		l.AddInTail(add)
		return
	}

	if after == nil {
		add.next = l.head
		l.head = &add
		if l.tail == nil {
			l.tail = &add
		}
		return
	}

	node := l.head
	for node != nil {
		if node == after {
			add.next = node.next
			node.next = &add
			if node.next != nil && node.next.next == nil {
				l.tail = node.next
			}
			break
		}
		node = node.next
	}
}

func (l *LinkedList) InsertFirst(first Node) {
	if l.head == nil {
		l.head = &first
		l.tail = &first
		return
	}
	first.next = l.head
	l.head = &first
}

func (l *LinkedList) Clean() {
	l.head = nil
	l.tail = nil
}
