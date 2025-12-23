package dynarray

import (
	"fmt"
	"math"
)

type MultiDynArray[T any] struct {
	numDims    int   // Количество измерений
	counts     []int // Текущие размеры по измерениям (len)
	capacities []int // Емкости по измерениям (cap)
	strides    []int // Strides для вычисления плоского индекса: strides[i] = product of counts[i+1:]
	data       []T   // Плоский слайс данных, len(data) == product of capacities (для резерва)
}

func (mda *MultiDynArray[T]) Init(sizes ...int) {
	mda.numDims = len(sizes)
	mda.counts = make([]int, mda.numDims)
	mda.capacities = make([]int, mda.numDims)
	mda.strides = make([]int, mda.numDims)

	totalCap := 1
	for i := mda.numDims - 1; i >= 0; i-- {
		size := 0
		if i < len(sizes) {
			size = sizes[i]
		}
		mda.counts[i] = size
		mda.capacities[i] = max(16, nextPowerOfTwo(size)) // Начальная capacity >= size, power of 2
		totalCap *= mda.capacities[i]
		if i == mda.numDims-1 {
			mda.strides[i] = 1
		} else {
			mda.strides[i] = mda.strides[i+1] * mda.counts[i+1]
		}
	}
	mda.data = make([]T, totalCap)
}

func (mda *MultiDynArray[T]) MakeArray(newCaps ...int) error {
	if len(newCaps) != mda.numDims {
		return fmt.Errorf("expected %d new capacities, got %d", mda.numDims, len(newCaps))
	}

	oldCounts := make([]int, mda.numDims)
	copy(oldCounts, mda.counts)
	oldStrides := make([]int, mda.numDims)
	copy(oldStrides, mda.strides)

	newCapacities := make([]int, mda.numDims)
	newTotalCap := 1
	for i := 0; i < mda.numDims; i++ {
		if newCaps[i] < 16 {
			newCaps[i] = 16
		}
		newCapacities[i] = newCaps[i]
		newTotalCap *= newCapacities[i]
		if mda.counts[i] > newCapacities[i] {
			mda.counts[i] = newCapacities[i] // Обрезаем размер, если новая capacity меньше
		}
	}

	// Создаем новый data
	newData := make([]T, newTotalCap)

	// Обновляем strides для новых counts
	mda.updateStrides()

	// Копируем старые данные
	copyMulti(mda.data, oldStrides, newData, mda.strides, oldCounts, 0, 0, 0, mda.numDims)

	// Обновляем
	mda.capacities = newCapacities
	mda.data = newData

	return nil
}

// Вспомогательная рекурсивная функция для копирования данных между flattened arrays с разными strides.
func copyMulti[T any](oldData []T, oldStrides []int, newData []T, newStrides []int, counts []int, oldOffset int, newOffset int, dim int, numDims int) {
	if dim == numDims {
		newData[newOffset] = oldData[oldOffset]
		return
	}

	count := counts[dim]
	oldStride := oldStrides[dim]
	newStride := newStrides[dim]
	for i := 0; i < count; i++ {
		copyMulti[T](oldData, oldStrides, newData, newStrides, counts, oldOffset+i*oldStride, newOffset+i*newStride, dim+1, numDims)
	}
}

// updateStrides обновляет strides на основе текущих counts.
func (mda *MultiDynArray[T]) updateStrides() {
	if mda.numDims == 0 {
		return
	}
	mda.strides[mda.numDims-1] = 1
	for i := mda.numDims - 2; i >= 0; i-- {
		mda.strides[i] = mda.strides[i+1] * mda.counts[i+1]
	}
}

// GetItem возвращает элемент по многомерным индексам.
// Если любой индекс >= count для измерения или <0, ошибка.
func (mda *MultiDynArray[T]) GetItem(indices ...int) (T, error) {
	var zero T
	if len(indices) != mda.numDims {
		return zero, fmt.Errorf("expected %d indices, got %d", mda.numDims, len(indices))
	}
	offset, err := mda.computeOffset(indices)
	if err != nil {
		return zero, err
	}
	return mda.data[offset], nil
}

// SetItem устанавливает элемент по многомерным индексам.
// Если любой индекс >= count, автоматически масштабирует это измерение (увеличивает count до index+1, удваивает capacity если нужно, копирует данные).
func (mda *MultiDynArray[T]) SetItem(value T, indices ...int) error {
	if len(indices) != mda.numDims {
		return fmt.Errorf("expected %d indices, got %d", mda.numDims, len(indices))
	}

	oldCounts := make([]int, mda.numDims)
	copy(oldCounts, mda.counts)
	oldStrides := make([]int, mda.numDims)
	copy(oldStrides, mda.strides)

	needResize := false
	for d := 0; d < mda.numDims; d++ {
		if indices[d] < 0 {
			return fmt.Errorf("negative index %d for dim %d", indices[d], d)
		}
		if indices[d] >= mda.counts[d] {
			mda.counts[d] = indices[d] + 1
			if mda.counts[d] > mda.capacities[d] {
				mda.capacities[d] = max(mda.capacities[d]*2, mda.counts[d])
			}
			needResize = true
		}
	}

	if needResize {
		// Вычисляем новый total cap
		newTotalCap := 1
		for _, cap := range mda.capacities {
			newTotalCap *= cap
		}

		newData := make([]T, newTotalCap)

		mda.updateStrides() // Обновляем strides на новые counts

		oldData := mda.data
		copyMulti(oldData, oldStrides, newData, mda.strides, oldCounts, 0, 0, 0, mda.numDims) // Копируем только старую часть

		mda.data = newData
	}

	offset, _ := mda.computeOffset(indices) // Теперь offset в пределах
	mda.data[offset] = value
	return nil
}

// computeOffset вычисляет плоский offset по индексам.
func (mda *MultiDynArray[T]) computeOffset(indices []int) (int, error) {
	offset := 0
	for d := 0; d < mda.numDims; d++ {
		if indices[d] >= mda.counts[d] || indices[d] < 0 {
			return 0, fmt.Errorf("index %d out of bounds for dim %d (size %d)", indices[d], d, mda.counts[d])
		}
		offset += indices[d] * mda.strides[d]
	}
	if offset >= len(mda.data) {
		return 0, fmt.Errorf("computed offset %d exceeds data len %d", offset, len(mda.data))
	}
	return offset, nil
}

// Insert адаптирован как SetItem, поскольку вставка со сдвигом в многомерном массиве неоднозначна.
func (mda *MultiDynArray[T]) Insert(itm T, indices ...int) error {
	return mda.SetItem(itm, indices...)
}

// Remove устанавливает элемент в zero и сжимает counts только если удаляемый - последний во всех измерениях.
func (mda *MultiDynArray[T]) Remove(indices ...int) error {
	if len(indices) != mda.numDims {
		return fmt.Errorf("expected %d indices, got %d", mda.numDims, len(indices))
	}
	offset, err := mda.computeOffset(indices)
	if err != nil {
		return err
	}
	var zero T
	mda.data[offset] = zero

	// Сжимаем counts, если индекс == count[d]-1 для всех d
	shrink := true
	for d := 0; d < mda.numDims; d++ {
		if indices[d] != mda.counts[d]-1 {
			shrink = false
			break
		}
	}
	if shrink {
		for d := 0; d < mda.numDims; d++ {
			mda.counts[d]--
			if mda.counts[d] < mda.capacities[d]/2 && mda.capacities[d] > 16 {
				mda.capacities[d] /= 2
			}
		}
		mda.updateStrides()
		// Для сжатия capacities нужно вызвать MakeArray с новыми cap, но для простоты опустим полный realloc здесь.
	}
	return nil
}

// Append добавляет в конец последнего измерения (увеличивает count[last]).
func (mda *MultiDynArray[T]) Append(itm T) {
	if mda.numDims == 0 {
		mda.SetItem(itm)
		return
	}
	indices := make([]int, mda.numDims)
	indices[mda.numDims-1] = mda.counts[mda.numDims-1]
	mda.SetItem(itm, indices...)
}

// max helper
func max(a, b int) int {
	if a > b {
		return a
	}
	return b
}

// nextPowerOfTwo for initial capacity
func nextPowerOfTwo(n int) int {
	if n == 0 {
		return 1
	}
	return int(math.Pow(2, math.Ceil(math.Log2(float64(n)))))
}
