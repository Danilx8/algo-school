package main

import (
	"fmt"
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

	// Копируем все существующие элементы (но не больше, чем есть)
	copy(newArray, da.array)

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

	tmpArray := make([]T, da.capacity)
	for j := 0; j < index; j++ {
		tmpArray[j] = da.array[j]
	}
	for k := index + 1; k < da.count; k++ {
		tmpArray[k-1] = da.array[k]
	}
	da.array = tmpArray
	da.count--

	if da.count < da.capacity/2 && da.capacity > 16 {
		if float64(da.capacity)/1.5 >= 16 {
			da.MakeArray(da.capacity / 2)
		} else {
			da.MakeArray(16)
		}
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
