package main

import (
	"errors"
)

// Stack представляет generic стек.
type Stack[T any] struct {
	items []T
}

// Size возвращает текущий размер стека.
func (st *Stack[T]) Size() int {
	return len(st.items)
}

// Peek возвращает элемент на вершине стека без его удаления.
// Если стек пуст — возвращает ошибку.
func (st *Stack[T]) Peek() (T, error) {
	if len(st.items) == 0 {
		var zero T
		return zero, errors.New("stack is empty")
	}
	return st.items[len(st.items)-1], nil
}

// Pop удаляет и возвращает элемент с вершины стека.
// Если стек пуст — возвращает ошибку.
func (st *Stack[T]) Pop() (T, error) {
	if len(st.items) == 0 {
		var zero T
		return zero, errors.New("stack is empty")
	}
	index := len(st.items) - 1
	item := st.items[index]
	st.items = st.items[:index]
	return item, nil
}

// Push добавляет элемент на вершину стека.
func (st *Stack[T]) Push(item T) {
	st.items = append(st.items, item)
}
