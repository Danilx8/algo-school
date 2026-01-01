package main

// 4. Стек
// 1. Переделайте реализацию стека так, чтобы она работала не с хвостом списка как с верхушкой стека, а с его головой -- на основе подходящей структуры данных, которая бы сохраняла сложность O(1).
// Структура данных - связанный список
// Сложность операций сохранилась на О(1)

import (
	"errors"
	"fmt"
	"strconv"
	"strings"
)

type Node[T any] struct {
	value T
	next  *Node[T]
}

type ReversedStack[T any] struct {
	head *Node[T]
	size int
}

func (s *ReversedStack[T]) Push(item T) {
	s.head = &Node[T]{value: item, next: s.head}
	s.size++
}

func (s *ReversedStack[T]) Pop() (T, error) {
	if s.head == nil {
		var zero T
		return zero, errors.New("ReversedStack is empty")
	}
	item := s.head.value
	s.head = s.head.next
	s.size--
	return item, nil
}

func (s *ReversedStack[T]) Peek() (T, error) {
	if s.head == nil {
		var zero T
		return zero, errors.New("ReversedStack is empty")
	}
	return s.head.value, nil
}

func (s *ReversedStack[T]) Size() int {
	return s.size
}

/* 3. Не запуская программу, скажите, как отработает такой цикл?

while (stack.size() > 0)
stack.pop()
stack.pop()

Если в стеке чётноке количество элементов, то отработает корректно. Если нет - выдаст ошибку на последней итерации
(последний элемент сперва удалится при первом вызове, а вторй вызов приведёт к ошибке)*/

// 4. Напишите функцию, которая получает на вход строку, состоящую из открывающих и закрывающих скобок (например, "(()((())()))" или "(()()(()") и, используя только стек и оператор цикла, определите, сбалансированы ли скобки в этой строке. Сбалансированной считается последовательность, в которой каждой открывающей обязательно соответствует закрывающая, а каждой закрывающей -- открывающая скобки, то есть последовательности "())(" , "))((" или "((())" будут несбалансированы.
// Сложность - O(n)
func (st *Stack[T]) IsBalanced(unbalanced string) bool {
	temp := &Stack[rune]{} // временный стек

	for _, ch := range unbalanced {
		if ch == '(' {
			temp.Push(ch)
		} else if ch == ')' {
			if temp.Size() == 0 {
				return false
			}
			temp.Pop()
		}
	}

	return temp.Size() == 0
}

// 5. Расширьте фукнцию из предыдущего примера, если скобки могут быть трех типов: (), {}, [].
// Сложность - O(n)
func (st *Stack[T]) IsBalancedEnhanced(unbalanced string) bool {
	temp := &Stack[rune]{}

	matching := map[rune]rune{
		')': '(',
		']': '[',
		'}': '{',
	}

	for _, ch := range unbalanced {
		switch ch {
		case '(', '[', '{':
			temp.Push(ch)
		case ')', ']', '}':
			if temp.Size() == 0 {
				return false
			}
			top, _ := temp.Pop()
			if top != matching[ch] {
				return false
			}
		}
	}

	return temp.Size() == 0
}

// 6. Добавьте в стек функцию, возвращающую текущий минимальный элемент в нём за O(1) (подсказка: используйте второй стек).
// Для этого пришлось добавил переменную для хранения сумы элементов (сложность опреаций не изменилась)

type NumericStackWithMin struct {
	items []int // элементы стека
	sum   int   // текущая сумма всех элементов
	size  int   // количество элементов (дублируем для удобства)
}

// Push — добавляет элемент на вершину стека
func (s *NumericStackWithMin) Push(item int) {
	s.items = append(s.items, item)
	s.sum += item
	s.size++
}

// Pop — удаляет и возвращает элемент с вершины
func (s *NumericStackWithMin) Pop() (int, error) {
	if s.size == 0 {
		return 0, errors.New("stack is empty")
	}
	index := len(s.items) - 1
	item := s.items[index]
	s.items = s.items[:index]
	s.sum -= item
	s.size--
	return item, nil
}

// Peek — возвращает элемент с вершины без удаления
func (s *NumericStackWithMin) Peek() (int, error) {
	if s.size == 0 {
		return 0, errors.New("stack is empty")
	}
	return s.items[len(s.items)-1], nil
}

// Size — возвращает количество элементов
func (s *NumericStackWithMin) Size() int {
	return s.size
}

// Average — возвращает среднее арифметическое (float64) за O(1)
func (s *NumericStackWithMin) Average() (float64, error) {
	if s.size == 0 {
		return 0, errors.New("stack is empty")
	}
	return float64(s.sum) / float64(s.size), nil
}

// 8. Постфиксная запись выражения -- это запись, в которой порядок вычислений определяется не скобками и приоритетами, а только позицией элемента в выражении. Например, в выражениях разрешено использовать целые числа и операции + и * . Тогда выражение
func StacksCalculations(expression string) (int, error) {
	// Разбиваем строку на токены (числа и операторы)
	tokens := strings.Fields(expression) // разделяем по пробелам

	// Переворачиваем порядок токенов, т.к. в C# код шёл с конца строки
	for i, j := 0, len(tokens)-1; i < j; i, j = i+1, j-1 {
		tokens[i], tokens[j] = tokens[j], tokens[i]
	}

	firstStack := &Stack[int]{} // для исходных токенов (смешанный стек)

	// Обрабатываем токены в обратном порядке (как в оригинальном коде)
	for _, token := range tokens {
		if token == "" {
			continue
		}

		// Если это число — пушим как int
		if num, err := strconv.Atoi(token); err == nil {
			firstStack.Push(num)
		} else {
			// Иначе — это оператор или '='
			// Преобразуем строку в руну (первый символ)
			r := []rune(token)[0]
			firstStack.Push(int(r)) // сохраняем как код символа
		}
	}

	secondStack := &Stack[int]{} // для вычислений

	for firstStack.Size() > 0 {
		item, _ := firstStack.Pop()

		// Проверяем, число ли это: если код > 127 или не оператор — вероятно число
		// Лучше: попробовать определить, был ли это изначально числом
		// Но проще: всё, что не оператор и не '=', считаем числом (но мы уже разделили)

		// В нашем случае: если item — это код символа (например, '+', '=', и т.д.)
		ch := rune(item)

		if ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '=' {
			if ch == '=' {
				if secondStack.Size() == 0 {
					return 0, fmt.Errorf("nothing to return after '='")
				}
				result, _ := secondStack.Pop()
				return result, nil
			}

			if secondStack.Size() < 2 {
				return 0, fmt.Errorf("not enough operands for operator '%c'", ch)
			}

			// Важно: в постфиксной нотации второй операнд — тот, что был позже
			second, _ := secondStack.Pop()
			first, _ := secondStack.Pop()

			switch ch {
			case '+':
				secondStack.Push(first + second)
			case '-':
				secondStack.Push(first - second)
			case '*':
				secondStack.Push(first * second)
			case '/':
				if second == 0 {
					return 0, fmt.Errorf("division by zero")
				}
				secondStack.Push(first / second)
			}
		} else {
			// Это число
			secondStack.Push(item)
		}
	}

	return 0, fmt.Errorf("no '=' found in expression")
}

// Пример использования
func main() {
	tests := []string{
		"8 2 + 5 * 9 + =",
	}

	for _, expr := range tests {
		result, err := StacksCalculations(expr)
		if err != nil {
			fmt.Printf("Выражение: %q → Ошибка: %v\n", expr, err)
		} else {
			fmt.Printf("Выражение: %q → Результат: %d\n", expr, result)
		}
	}
}

type BiDiNode struct {
	prev  *BiDiNode
	next  *BiDiNode
	value int
}

type LinkedList2 struct {
	head *BiDiNode
	tail *BiDiNode
}

func (l *LinkedList2) AddInTail(item BiDiNode) {
	if l.head == nil {
		l.head = &item
		l.head.next = nil
		l.head.prev = nil
	} else {
		l.tail.next = &item
		item.prev = l.tail
		item.next = nil
	}
	l.tail = &item
}

func (l *LinkedList2) Count() int {
	count := 0
	node := l.head
	for node != nil {
		count++
		node = node.next
	}
	return count
}

// 9. Метод, который "переворачивает" связный список.
// Нужно изменить направление ссылок между узлами так, чтобы каждый предыдущий узел стал следующим для текущего. Пробегаемся по всему списку, аккуратно меняем местами указатели для каждого узла, и в заключение не забываем поменять head и tail.
// Так и сделал. При переборе списка понадобилась временная переменная
func (l *LinkedList2) Flip() {
	var tmp *BiDiNode
	current := l.head

	for current != nil {
		tmp = current.next
		current.next = current.prev
		current.prev = tmp
		current = current.next
	}

	if tmp == nil {
		l.head, l.tail = l.tail, l.head
	}
}

// 10. Проверка, имеются ли циклы внутри списка.
// Цикл for по элементам до длины списка, и если конечным узлом не будет хвост, значит в списке есть цикл.
// Реализовано именно так. Главное не реализовывать ч/з while
func (l *LinkedList2) CheckCycles() bool {
	node := l.head
	for range l.Count() {
		node = node.next
	}

	return node != l.tail
}

// 11. Сортировка списка.
// Сортировка пузырьком лучше всего подходит для связного списка.
// (Проходили его на курсе для начинающих с полного нуля :) Погуглите что это такое).
// Так + в конце приходится пробегаться по всем элементам ещё раз, чтобы правильно определить хвост получившегося списка
func (l *LinkedList2) Sort() {
	if l.head == nil {
		return
	}

	current := l.head
	for current != nil {
		next := current.next
		for next != nil {
			if current.value > next.value {
				current.value, next.value = next.value, current.value
			}
			next = next.next
		}
		current = current.next
	}

	current = l.head
	for current.next != nil {
		current = current.next
	}
	l.tail = current
}

// 12. Слияние списков
// Обобщим сразу на произвольное количество входных списков (на входе - список связных списков). Эти списки предварительно надо отсортировать, и поочерёдно сканируя текущие элементы списков, выдаём в результирующий список тот элемент,который наиболее подходит по свойству упорядоченности (например, минимальный из всех текущих, если сортируем по возрастанию).
// Есть и более эффективные подходы, в частности использовать т.н. "кучу" (изучаем далее), из которой текущий минимальный элемент извлекается "автоматически".
// Частично приходится перезаписать реализацию связанного списка
// Сложность алгоритма - O(n log k) (если взять за n число списков, а за k - длину списка на выходе)
// Плюс такой реализации заключается в том, что для сортировки списков не приходится итерировать каждый, а можно проходиться по их головам, постепенно заполняя результирующий массив

func Combine(lists []*LinkedList2) *LinkedList2 {
	result := new(LinkedList2)

	// Если нет списков — возвращаем пустой результат
	if len(lists) == 0 {
		return result
	}

	// Индекс списка с минимальным текущим значением
	for {
		var minNode *BiDiNode = nil
		var minListIdx int = -1

		// Находим список с минимальной головой среди непустых
		for i, list := range lists {
			if list == nil || list.head == nil {
				continue
			}
			if minNode == nil || list.head.value < minNode.value {
				minNode = list.head
				minListIdx = i
			}
		}

		// Если все списки закончились — выходим
		if minNode == nil {
			break
		}

		// Добавляем минимальный узел в результат
		result.AddInTail(*minNode)

		// Переходим к следующему узлу в выбранном списке
		lists[minListIdx].head = lists[minListIdx].head.next
	}

	return result
}

// 13. Dummy
// Идея, что создаём отдельный класс Dummy (наследник основного узла), и в циклах не узлы сравниваем, а проверяем тип узла -- он Dummy или нет.
// Нередкая ошибка - добавлять в дамми отдельный флажок (или вообще не делать Dummy, а впихивать флажок в основной класс Node). Наоборот, мы стараемся избежать ненужных дополнительных состояний узла и сделать проверки в коде более выразительными.
//
// В дополнение к dummy-узлам может создаваться также круговой связанный список, когда последний узел соединяется с первым узлом, создавая цикл. В таком случае можно обойтись вообще одним dummy-узлом, который будет одновременно и head, и tail [6-006].
// К сожалению, в голанге нет наследования, поэтому приходится впихивать флажок в основной класс

type DummyNode struct {
	prev    *DummyNode
	next    *DummyNode
	value   int
	isDummy bool
}

type DummyLinkedList struct {
	head *DummyNode
	tail *DummyNode
}

func (l *DummyLinkedList) Init() *DummyLinkedList {
	l.head.next = l.tail
	l.tail.prev = l.head
	l.head.isDummy = true
	l.tail.isDummy = true

	return l
}

func (l *DummyLinkedList) AddInTail(item DummyNode) {
	item.prev = l.tail.prev
	l.tail.prev.next = &item
	l.tail.prev = &item
	item.next = l.tail
}

func (l *DummyLinkedList) Find(value int) (DummyNode, error) {
	node := l.head.next
	for !node.isDummy {
		if node.value == value {
			return *node, nil
		}
		node = node.next
	}
	return DummyNode{value: -1, next: nil}, errors.New("node not found")
}

func (l *DummyLinkedList) FindAll(value int) []DummyNode {
	var nodes []DummyNode
	node := l.head.next
	for !node.isDummy {
		if node.value == value {
			nodes = append(nodes, *node)
		}
		node = node.next
	}
	return nodes
}

func (l *DummyLinkedList) Delete(value int, all bool) {
	if l.head == nil || l.head.next == nil {
		return // пустой список
	}

	deleted := false
	first := l.head       // предыдущий узел (изначально dummy)
	second := l.head.next // текущий проверяемый узел

	for !second.isDummy {
		if second.value == value {
			// Удаляем second
			first.next = second.next
			second.next.prev = first

			deleted = true

			if !all {
				return // удалили одно — выходим
			}

			// Продолжаем с следующего узла
			second = first.next
		} else {
			// Переходим дальше
			first = second
			second = second.next
		}
	}

	// Корректируем tail, если удалили последний элемент
	if l.tail.value == value && deleted {
		// Находим новый tail — последний не-dummy узел
		node := l.head
		for !node.next.isDummy {
			node = node.next
		}
		l.tail = node
	}
}

func (l *DummyLinkedList) Insert(after *DummyNode, add DummyNode) {
	node := l.head.next

	if after == nil {
		l.head.next.prev = &add
		add.prev = l.head
		add.next = l.head.next
		l.head.next = &add
	} else if after == l.tail.next {
		l.AddInTail(add)
	} else if !node.isDummy {
		for !node.next.isDummy {
			if node == after {
				add.next = node.next
				add.prev = node
				node.next.prev = &add
				node.next = &add
				break
			}
			node = node.next
		}
	}
}

func (l *DummyLinkedList) InsertFirst(first DummyNode) {
	l.Insert(nil, first)
}

func (l *DummyLinkedList) Count() int {
	count := 0
	node := l.head.next
	for !node.isDummy {
		count++
		node = node.next
	}
	return count
}

func (l *DummyLinkedList) Clean() {
	l.head.next = l.tail
	l.tail.prev = l.head
}
