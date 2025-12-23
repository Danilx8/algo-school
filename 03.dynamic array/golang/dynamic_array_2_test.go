package dynarray

import (
	"testing"
)

func TestDynArray(t *testing.T) {
	// 1. Вставка элемента, когда в итоге размер буфера не превышен
	t.Run("Insert without exceeding capacity (and capacity doesn't change)", func(t *testing.T) {
		var da DynArray[int]
		da.Init() // capacity = 16, count = 0

		// Добавим 5 элементов через Append (capacity не меняется)
		for i := 0; i < 5; i++ {
			da.Append(i)
		}

		if da.count != 5 || da.capacity != 16 {
			t.Fatalf("precondition failed: count=%d, capacity=%d", da.count, da.capacity)
		}

		// Вставляем в середину — capacity НЕ должен измениться (по текущей логике index < capacity)
		err := da.Insert(999, 2)
		if err != nil {
			t.Fatalf("unexpected error on insert: %v", err)
		}

		if da.count != 6 {
			t.Errorf("expected count = 6, got %d", da.count)
		}
		if da.capacity != 16 {
			t.Errorf("expected capacity remains 16, got %d", da.capacity)
		}

		// Проверяем порядок
		expected := []int{0, 1, 999, 2, 3, 4}
		for i, exp := range expected {
			item, _ := da.GetItem(i)
			if item != exp {
				t.Errorf("at index %d expected %d, got %d", i, exp, item)
			}
		}
	})

	// 2. Вставка элемента, когда в результате превышен размер буфера
	t.Run("Insert causing capacity increase (only via append at end)", func(t *testing.T) {
		var da DynArray[int]
		da.Init()

		// Заполняем ровно до capacity
		for i := 0; i < 16; i++ {
			da.Append(i)
		}

		if da.count != 16 || da.capacity != 16 {
			t.Fatalf("precondition failed")
		}

		// Вставка в конец (index == count) → вызывает Append → capacity удваивается
		err := da.Insert(999, 16)
		if err != nil {
			t.Fatalf("unexpected error: %v", err)
		}

		if da.count != 17 {
			t.Errorf("expected count = 17, got %d", da.count)
		}
		if da.capacity != 32 {
			t.Errorf("expected capacity doubled to 32, got %d", da.capacity)
		}

		item, _ := da.GetItem(16)
		if item != 999 {
			t.Errorf("expected inserted item 999, got %d", item)
		}
	})

	// 3. Попытка вставки в недопустимую позицию
	t.Run("Insert at invalid position returns error", func(t *testing.T) {
		var da DynArray[int]
		da.Init()
		da.Append(1)
		da.Append(2)

		invalidIndices := []int{-1, 3, 10, -100}
		for _, idx := range invalidIndices {
			err := da.Insert(0, idx)
			if err == nil {
				t.Errorf("expected error for index %d, but got nil", idx)
			}
		}
	})

	// 4. Удаление элемента, когда размер буфера остаётся прежним
	t.Run("Remove without shrinking capacity", func(t *testing.T) {
		var da DynArray[int]
		da.Init()

		// Доведём до capacity 32
		for i := 0; i < 20; i++ {
			da.Append(i)
		}

		if da.capacity != 32 || da.count != 20 {
			t.Fatalf("precondition failed")
		}

		err := da.Remove(10)
		if err != nil {
			t.Fatalf("unexpected error: %v", err)
		}

		if da.count != 19 {
			t.Errorf("expected count = 19, got %d", da.count)
		}
		if da.capacity != 32 {
			t.Errorf("expected capacity remains 32, got %d (shrink condition not met)", da.capacity)
		}

		// Проверяем, что элементы сдвинулись
		item, _ := da.GetItem(10)
		if item != 11 {
			t.Errorf("expected item at 10 to be 11 after remove(10), got %d", item)
		}
	})

	// 5. Удаление элемента, когда в результате понижается размер буфера
	t.Run("Remove triggering capacity shrink", func(t *testing.T) {
		var da DynArray[int]
		da.Init()

		// Доведём до capacity 32, count 32
		for i := 0; i < 32; i++ {
			da.Append(i)
		}

		if da.capacity != 32 || da.count != 32 {
			t.Fatalf("precondition failed")
		}

		// Удаляем элементы, пока count не станет < 16 (capacity/2)
		for i := 0; i < 17; i++ {
			da.Remove(0)
		}

		// Теперь count = 15, capacity всё ещё 32
		// Удаляем ещё один → count = 14
		// Условие: 14 < 32/2 (16) && capacity > 16 → должно сработать уменьшение
		err := da.Remove(0)
		if err != nil {
			t.Fatalf("unexpected error: %v", err)
		}

		if da.count != 14 {
			t.Errorf("expected count = 14, got %d", da.count)
		}
		// По текущей реализации: capacity /= 2 → 16
		if da.capacity != 16 {
			t.Errorf("expected capacity shrunk to 16, got %d", da.capacity)
		}
	})

	// 6. Попытка удаления элемента в недопустимой позиции
	t.Run("Remove at invalid position returns error", func(t *testing.T) {
		var da DynArray[int]
		da.Init()
		da.Append(1)
		da.Append(2)
		da.Append(3)

		invalidIndices := []int{-1, 3, 4, 100}
		for _, idx := range invalidIndices {
			err := da.Remove(idx)
			if err == nil {
				t.Errorf("expected error for index %d, but got nil", idx)
			}
		}
	})
}
