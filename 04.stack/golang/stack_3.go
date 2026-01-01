package main

import (
	"errors"
	"testing"
)

func TestStack_Size(t *testing.T) {
	s := Stack[int]{}

	if s.Size() != 0 {
		t.Errorf("Expected initial size 0, got %d", s.Size())
	}

	s.Push(10)
	s.Push(20)

	if s.Size() != 2 {
		t.Errorf("Expected size 2 after two pushes, got %d", s.Size())
	}

	s.Pop()
	if s.Size() != 1 {
		t.Errorf("Expected size 1 after pop, got %d", s.Size())
	}
}

func TestStack_PushAndPeek(t *testing.T) {
	s := Stack[string]{}

	s.Push("first")
	s.Push("second")

	top, err := s.Peek()
	if err != nil {
		t.Fatalf("Unexpected error on Peek: %v", err)
	}
	if top != "second" {
		t.Errorf("Expected peek to return 'second', got '%s'", top)
	}

	// Размер не должен измениться
	if s.Size() != 2 {
		t.Errorf("Size should remain 2 after Peek, got %d", s.Size())
	}
}

func TestStack_Pop(t *testing.T) {
	s := Stack[int]{}

	s.Push(100)
	s.Push(200)
	s.Push(300)

	val, err := s.Pop()
	if err != nil {
		t.Fatalf("Unexpected error on Pop: %v", err)
	}
	if val != 300 {
		t.Errorf("Expected popped value 300, got %d", val)
	}

	if s.Size() != 2 {
		t.Errorf("Expected size 2 after pop, got %d", s.Size())
	}

	// Проверяем порядок LIFO
	val2, _ := s.Pop()
	if val2 != 200 {
		t.Errorf("Expected second pop to return 200, got %d", val2)
	}
}

func TestStack_PeekOnEmpty(t *testing.T) {
	s := Stack[float64]{}

	_, err := s.Peek()
	if err == nil {
		t.Error("Expected error on Peek from empty stack, got nil")
	}
	if !errors.Is(err, errors.New("stack is empty")) && err.Error() != "stack is empty" {
		t.Errorf("Expected 'stack is empty' error, got: %v", err)
	}
}

func TestStack_PopOnEmpty(t *testing.T) {
	s := Stack[bool]{}

	_, err := s.Pop()
	if err == nil {
		t.Error("Expected error on Pop from empty stack, got nil")
	}
	if err.Error() != "stack is empty" {
		t.Errorf("Expected 'stack is empty' error, got: %v", err)
	}

	// Размер должен остаться 0
	if s.Size() != 0 {
		t.Errorf("Size should be 0 after failed Pop, got %d", s.Size())
	}
}

func TestStack_MultipleTypes(t *testing.T) {
	// Тест с разными типами — просто проверяем компиляцию и базовое поведение
	intStack := Stack[int]{}
	intStack.Push(42)
	if v, _ := intStack.Pop(); v != 42 {
		t.Errorf("Int stack failed: expected 42, got %d", v)
	}

	stringStack := Stack[string]{}
	stringStack.Push("hello")
	if v, _ := stringStack.Pop(); v != "hello" {
		t.Errorf("String stack failed: expected 'hello', got '%s'", v)
	}
}
