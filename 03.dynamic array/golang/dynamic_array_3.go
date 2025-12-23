package dynarray

import (
	"fmt"
)

type BankDynArray[T any] struct {
	count    int
	capacity int
	array    []T
}

func (da *BankDynArray[T]) Init() {
	da.count = 0
	da.MakeArray(16)
}

func (da *BankDynArray[T]) MakeArray(newCapacity int) {
	if newCapacity < 16 {
		newCapacity = 16
	}

	newArray := make([]T, newCapacity)

	// Копируем все существующие элементы (но не больше, чем есть)
	copyCount := da.count
	if copyCount > newCapacity {
		copyCount = newCapacity // На случай уменьшения, но обычно count <= newCapacity
	}
	copy(newArray[:copyCount], da.array[:copyCount])

	da.array = newArray
	da.capacity = newCapacity
}

// Insert Сложность функции - O(n) поскольку мы проходим по всем элементам массива в худшем случае
func (da *BankDynArray[T]) Insert(itm T, index int) error {
	if index < 0 || index > da.count {
		return fmt.Errorf("bad index '%d'", index)
	}

	if index == da.count {
		da.Append(itm)
		return nil
	}

	// Проверяем и расширяем, если массив полон
	if da.count == da.capacity {
		da.MakeArray(da.capacity * 2)
	}

	// Сдвигаем элементы вправо
	for i := da.count; i > index; i-- {
		da.array[i] = da.array[i-1]
	}
	da.array[index] = itm
	da.count++

	return nil
}

// Remove Сложность функции - O(n) поскольку мы проходим по всем элементам массива в худшем случае
func (da *BankDynArray[T]) Remove(index int) error {
	if index < 0 || index >= da.count {
		return fmt.Errorf("bad index '%d'", index)
	}

	// Сдвигаем элементы влево, перезаписывая удаляемый
	for i := index; i < da.count-1; i++ {
		da.array[i] = da.array[i+1]
	}
	// Обнуляем последний элемент (опционально, для GC)
	var zero T
	da.array[da.count-1] = zero
	da.count--

	// Уменьшаем capacity, если нужно (чтобы избежать колебаний, обычно уменьшаем при count < capacity/4, но здесь по вашему примеру < capacity/2)
	if da.count < da.capacity/2 && da.capacity > 16 {
		newCap := da.capacity / 2
		if newCap >= 16 {
			da.MakeArray(newCap)
		} else {
			da.MakeArray(16)
		}
	}

	return nil
}

func (da *BankDynArray[T]) Append(itm T) {
	if da.count == da.capacity {
		da.MakeArray(da.capacity * 2)
	}
	da.array[da.count] = itm
	da.count++
}

func (da *BankDynArray[T]) GetItem(index int) (T, error) {
	var result T

	if index < 0 || index >= da.count {
		return result, fmt.Errorf("bad index '%d'", index)
	}

	return da.array[index], nil
}
