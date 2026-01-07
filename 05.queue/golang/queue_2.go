package main

import "errors"

// 3. Напишите функцию, которая "вращает" очередь по кругу на N элементов.

func (q *Queue[T]) Rotate(n int) {
    if q.Size() == 0 || n == 0 {
        return
    }
    
    q.items = append(q.items[n:], q.items[:n]...)
}

// 4. Реализуйте очередь с помощью двух стеков
type TwoStackQueue[T any] struct {
    input  Stack[T]
    output Stack[T]
}

func (q *TwoStackQueue[T]) Enqueue(item T) {
    q.input.Push(item)
}

func (q *TwoStackQueue[T]) Dequeue() (T, error) {
    if q.output.Size() == 0 {
        for q.input.Size() != 0 {
            val, err := q.input.Pop()
            if err != nil {
                return val, err
            }
            q.output.Push(val)
        }
    }
    return q.output.Pop()
}

func (q *TwoStackQueue[T]) Size() int {
    return len(q.input.items) + len(q.output.items)
}

// 5. Добавьте функцию, которая обращает все элементы в очереди в обратном порядке.
func (q *Queue[T]) Reverse() {
    if q.Size() == 0 {
        return
    }
    stack := Stack[T]{}
    for q.Size() != 0 {
        val, _ := q.Dequeue()
        stack.Push(val)
    }
    for stack.Size() != 0 {
        val, _ := stack.Pop()
        q.Enqueue(val)
    }
}

// 6. Реализуйте круговую (циклическую буферную) очередь статическим массивом фиксированного размера. Добавьте ей метод проверки, полна ли она (при этом добавление новых элементов невозможно).
// Обеспечьте эффективное управление указателями начала и конца очереди в рамках массива, чтобы избежать неоправданных сдвигов данных.
type CircularQueue[T any] struct {
    items    []T // Используем слайс фиксированного размера
    capacity int
    head     int // Индекс начала
    tail     int // Индекс следующего добавления
    size     int // Текущий размер
}

func NewCircularQueue[T any](capacity int) *CircularQueue[T] {
    if capacity <= 0 {
        panic("capacity must be positive")
    }
    return &CircularQueue[T]{
        items:    make([]T, capacity),
        capacity: capacity,
        head:     0,
        tail:     0,
        size:     0,
    }
}

func (q *CircularQueue[T]) Enqueue(item T) error {
    if q.IsFull() {
        return errors.New("queue is full")
    }
    q.items[q.tail] = item
    q.tail = (q.tail + 1) % q.capacity
    q.size++
    return nil
}

func (q *CircularQueue[T]) Dequeue() (T, error) {
    var zero T
    if q.IsEmpty() {
        return zero, errors.New("queue is empty")
    }
    item := q.items[q.head]
    q.head = (q.head + 1) % q.capacity
    q.size--
    return item, nil
}

func (q *CircularQueue[T]) Size() int {
    return q.size
}

func (q *CircularQueue[T]) IsEmpty() bool {
    return q.size == 0
}

func (q *CircularQueue[T]) IsFull() bool {
    return q.size == q.capacity
}