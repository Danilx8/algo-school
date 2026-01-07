package main

import (
	"testing"
)

func TestQueue_EnqueueAndSize(t *testing.T) {
	q := &Queue[string]{}

	if q.Size() != 0 {
		t.Errorf("Expected initial size 0, got %d", q.Size())
	}

	q.Enqueue("first")
	if q.Size() != 1 {
		t.Errorf("Expected size 1 after one enqueue, got %d", q.Size())
	}

	q.Enqueue("second")
	q.Enqueue("third")
	if q.Size() != 3 {
		t.Errorf("Expected size 3 after three enqueues, got %d", q.Size())
	}
}

func TestQueue_Dequeue_OrderAndValues(t *testing.T) {
	q := &Queue[int]{}
	q.Enqueue(100)
	q.Enqueue(200)
	q.Enqueue(300)

	val1, err := q.Dequeue()
	if err != nil {
		t.Fatalf("Unexpected error on dequeue: %v", err)
	}
	if val1 != 100 {
		t.Errorf("Expected first dequeued value 100, got %d", val1)
	}

	val2, err := q.Dequeue()
	if err != nil {
		t.Fatalf("Unexpected error on second dequeue: %v", err)
	}
	if val2 != 200 {
		t.Errorf("Expected second dequeued value 200, got %d", val2)
	}

	if q.Size() != 1 {
		t.Errorf("Expected size 1 after two dequeues, got %d", q.Size())
	}

	val3, err := q.Dequeue()
	if err != nil {
		t.Fatalf("Unexpected error on last dequeue: %v", err)
	}
	if val3 != 300 {
		t.Errorf("Expected last dequeued value 300, got %d", val3)
	}

	if q.Size() != 0 {
		t.Errorf("Expected size 0 after all dequeues, got %d", q.Size())
	}
}

func TestQueue_Dequeue_EmptyQueue(t *testing.T) {
	q := &Queue[float64]{}

	if q.Size() != 0 {
		t.Errorf("New queue should have size 0")
	}

	val, err := q.Dequeue()
	if err == nil {
		t.Errorf("Expected error when dequeuing from empty queue, got nil")
	}
	if err.Error() != "queue is empty" {
		t.Errorf("Expected error message 'queue is empty', got '%v'", err)
	}

	// zero value должен возвращаться вместе с ошибкой
	var expected float64 = 0
	if val != expected {
		t.Errorf("Expected zero value of type on error, got %v", val)
	}
}

func TestQueue_MixedOperations(t *testing.T) {
	q := &Queue[string]{}

	q.Enqueue("a")
	q.Enqueue("b")
	v, _ := q.Dequeue() // "a"
	if v != "a" {
		t.Errorf("Expected 'a', got %s", v)
	}

	q.Enqueue("c")
	q.Enqueue("d")

	v, _ = q.Dequeue() // "b"
	if v != "b" {
		t.Errorf("Expected 'b', got %s", v)
	}

	if q.Size() != 2 {
		t.Errorf("Expected size 2, got %d", q.Size())
	}

	v, _ = q.Dequeue() // "c"
	if v != "c" {
		t.Errorf("Expected 'c', got %s", v)
	}

	v, _ = q.Dequeue() // "d"
	if v != "d" {
		t.Errorf("Expected 'd', got %s", v)
	}

	_, err := q.Dequeue()
	if err == nil {
		t.Errorf("Expected error on final dequeue from empty queue")
	}
}

func TestQueue_ZeroValues(t *testing.T) {
	q := &Queue[int]{}
	q.Enqueue(0)
	q.Enqueue(0)

	val, _ := q.Dequeue()
	if val != 0 {
		t.Errorf("Expected 0, got %d", val)
	}
}