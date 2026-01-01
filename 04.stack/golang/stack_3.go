package stack

// Переделайте реализацию стека так, чтобы она работала не с хвостом списка как с верхушкой стека, а с его головой -- на основе подходящей структуры данных, которая бы сохраняла сложность O(1).
// Структура данных - связанный список

import "errors"

type Node[T any] struct {
	value T
	next  *Node[T]
}

type ReversedStack[T any] struct {
	head *Node[T]
	size int
}

func (s *ReversedStack[T]) Push(item T) {
	s.head = &Node[T]{value: item, next: s.head}
	s.size++
}

func (s *ReversedStack[T]) Pop() (T, error) {
	if s.head == nil {
		var zero T
		return zero, errors.New("ReversedStack is empty")
	}
	item := s.head.value
	s.head = s.head.next
	s.size--
	return item, nil
}

func (s *ReversedStack[T]) Peek() (T, error) {
	if s.head == nil {
		var zero T
		return zero, errors.New("ReversedStack is empty")
	}
	return s.head.value, nil
}

func (s *ReversedStack[T]) Size() int {
	return s.size
}
