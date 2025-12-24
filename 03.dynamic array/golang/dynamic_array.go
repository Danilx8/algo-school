package main

import (
	"fmt"
	"math"
	"os"
)

type DynArray[T any] struct {
	count    int
	capacity int
	array    []T
}

func (da *DynArray[T]) Init() {
	da.count = 0
	da.MakeArray(16)
}

func (da *DynArray[T]) MakeArray(newCapacity int) {
	if newCapacity < 16 {
		newCapacity = 16
	}
	newArray := make([]T, newCapacity)
	copy(newArray, da.array[:da.count]) // Копируем только реальные элементы до count, остальное будет zero value
	da.array = newArray
	da.capacity = newCapacity
}

// Insert Сложность функции - O(n) поскольку мы проходим по всем элементам массива
func (da *DynArray[T]) Insert(itm T, index int) error {
	if index < 0 || index > da.count {
		return fmt.Errorf("bad index '%d'", index)
	}

	if index == da.count {
		da.Append(itm)
		return nil
	}

	if index >= da.capacity {
		da.capacity *= 2
	}

	for i := da.count; i > index; i-- {
		da.array[i] = da.array[i-1]
	}
	da.array[index] = itm
	da.count++

	return nil
}

// Remove Сложность функции - O(n) поскольку мы проходим по всем элементам массива
func (da *DynArray[T]) Remove(index int) error {
	if index < 0 || index >= da.count {
		return fmt.Errorf("bad index '%d'", index)
	}

	// Сдвигаем элементы влево
	for i := index; i < da.count-1; i++ {
		da.array[i] = da.array[i+1]
	}

	da.count--
	for j := da.count; j < da.capacity; j++ {
		da.array[j] = *new(T)
	}

	if da.count < da.capacity/2 && da.capacity > 16 {
		newCap := int(float64(da.capacity) / 1.5)
		if newCap < 16 {
			newCap = 16
		}
		da.MakeArray(newCap)
	}

	return nil
}

func (da *DynArray[T]) Append(itm T) {
	if da.count == da.capacity {
		da.MakeArray(da.capacity * 2)
	}
	da.array[da.count] = itm
	da.count++
}

func (da *DynArray[T]) GetItem(index int) (T, error) {
	var result T

	if index < 0 || index >= da.count {
		return result, fmt.Errorf("bad index '%d'", index)
	}

	return da.array[index], nil
}
