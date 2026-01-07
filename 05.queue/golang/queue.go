package main

import (
	"errors"
	"os"
)

type Queue[T any] struct {
	items []T
}

func (q *Queue[T]) Size() int {
	return len(q.items)
}


// Сложность - O(n)
func (q *Queue[T]) Dequeue() (T, error) {
	var zero T
	if len(q.items) == 0 {
		return zero, errors.New("queue is empty")
	}
	item := q.items[0]
	// Здесь список постоянно пересоздаётся
	q.items = q.items[1:]
	return item, nil
}

// Сложность - Q(1)
func (q *Queue[T]) Enqueue(item T) {
	q.items = append(q.items, item)
}