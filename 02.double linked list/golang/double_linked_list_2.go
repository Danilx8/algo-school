// Тесты
package main

import (
	"errors"
	"testing"
)

func createList(values ...int) *LinkedList2 {
	l := &LinkedList2{}
	for _, v := range values {
		node := &Node{value: v}
		l.AddInTail(node)
	}
	return l
}

func listToValues(l *LinkedList2) []int {
	var vals []int
	node := l.head
	for node != nil {
		vals = append(vals, node.value)
		node = node.next
	}
	return vals
}

func checkLinks(t *testing.T, l *LinkedList2) {
	if l.head == nil {
		if l.tail != nil {
			t.Error("head nil but tail not")
		}
		return
	}
	if l.head.prev != nil {
		t.Error("head prev not nil")
	}
	node := l.head
	prev := (*Node)(nil)
	for node != nil {
		if node.prev != prev {
			t.Errorf("wrong prev for %d", node.value)
		}
		prev = node
		node = node.next
	}
	if l.tail != prev {
		t.Error("tail not last")
	}
	if l.tail != nil && l.tail.next != nil {
		t.Error("tail next not nil")
	}
}

func TestAddInTail(t *testing.T) {
	l := &LinkedList2{}
	if l.head != nil || l.tail != nil {
		t.Error("initial not empty")
	}

	node1 := &Node{value: 1}
	l.AddInTail(node1)
	if l.head != node1 || l.tail != node1 {
		t.Error("after first add")
	}
	if node1.next != nil || node1.prev != nil {
		t.Error("first node links")
	}
	checkLinks(t, l)

	node2 := &Node{value: 2}
	l.AddInTail(node2)
	if l.head != node1 || l.tail != node2 {
		t.Error("after second add")
	}
	if node1.next != node2 || node2.prev != node1 {
		t.Error("links after second")
	}
	if node2.next != nil {
		t.Error("tail next")
	}
	checkLinks(t, l)
}

func TestCount(t *testing.T) {
	l := createList()
	if l.Count() != 0 {
		t.Error("empty count")
	}

	l = createList(1)
	if l.Count() != 1 {
		t.Error("one count")
	}

	l = createList(1, 2, 3)
	if l.Count() != 3 {
		t.Error("three count")
	}
}

func TestFind(t *testing.T) {
	l := createList(1, 2, 3, 2)

	node, err := l.Find(2)
	if err != nil || node.value != 2 {
		t.Error("find first 2")
	}

	node, err = l.Find(3)
	if err != nil || node.value != 3 {
		t.Error("find 3")
	}

	node, err = l.Find(4)
	if err == nil || node != nil {
		t.Error("find missing")
	}
	if !errors.Is(err, errors.New("node not found")) {
		t.Error("error message")
	}

	l = &LinkedList2{}
	node, err = l.Find(1)
	if err == nil {
		t.Error("find in empty")
	}
}

func TestFindAll(t *testing.T) {
	l := createList(1, 2, 3, 2, 4, 2)

	nodes := l.FindAll(2)
	if len(nodes) != 3 {
		t.Error("findall count")
	}
	if nodes[0].value != 2 || nodes[1].value != 2 || nodes[2].value != 2 {
		t.Error("findall values")
	}

	nodes = l.FindAll(5)
	if len(nodes) != 0 {
		t.Error("findall missing")
	}

	nodes = l.FindAll(1)
	if len(nodes) != 1 {
		t.Error("findall single")
	}

	l = &LinkedList2{}
	nodes = l.FindAll(1)
	if len(nodes) != 0 {
		t.Error("findall empty")
	}
}

func TestDelete(t *testing.T) {
	// Delete single, not all
	l := createList(1, 2, 3, 4)
	l.Delete(2, false)
	checkLinks(t, l)
	if !equal(listToValues(l), []int{1, 3, 4}) {
		t.Error("delete middle single")
	}

	l = createList(1, 2, 3)
	l.Delete(1, false)
	checkLinks(t, l)
	if !equal(listToValues(l), []int{2, 3}) {
		t.Error("delete head single")
	}

	l = createList(1, 2, 3)
	l.Delete(3, false)
	checkLinks(t, l)
	if !equal(listToValues(l), []int{1, 2}) {
		t.Error("delete tail single")
	}

	l = createList(1)
	l.Delete(1, false)
	checkLinks(t, l)
	if !equal(listToValues(l), []int{}) {
		t.Error("delete only single")
	}

	l = createList(1, 2, 2, 3)
	l.Delete(2, false)
	checkLinks(t, l)
	if !equal(listToValues(l), []int{1, 2, 3}) {
		t.Error("delete first dup single")
	}

	// Delete all
	l = createList(1, 2, 2, 3, 2)
	l.Delete(2, true)
	checkLinks(t, l)
	if !equal(listToValues(l), []int{1, 3}) {
		t.Error("delete all dups")
	}

	l = createList(2, 2, 2)
	l.Delete(2, true)
	checkLinks(t, l)
	if !equal(listToValues(l), []int{}) {
		t.Error("delete all all")
	}

	l = createList(1, 2, 3)
	l.Delete(4, true)
	checkLinks(t, l)
	if !equal(listToValues(l), []int{1, 2, 3}) {
		t.Error("delete missing all")
	}

	l = createList(2, 1, 2)
	l.Delete(2, true)
	checkLinks(t, l)
	if !equal(listToValues(l), []int{1}) {
		t.Error("delete head and tail all")
	}
}

func TestInsert(t *testing.T) {
	l := createList(1, 3, 4)
	node2 := &Node{value: 2}
	after, _ := l.Find(1)
	l.Insert(after, node2)
	checkLinks(t, l)
	if !equal(listToValues(l), []int{1, 2, 3, 4}) {
		t.Error("insert middle")
	}

	node5 := &Node{value: 5}
	after, _ = l.Find(4)
	l.Insert(after, node5)
	checkLinks(t, l)
	if !equal(listToValues(l), []int{1, 2, 3, 4, 5}) {
		t.Error("insert tail")
	}

	node0 := &Node{value: 0}
	l.Insert(nil, node0) // like InsertFirst
	checkLinks(t, l)
	if !equal(listToValues(l), []int{0, 1, 2, 3, 4, 5}) {
		t.Error("insert head")
	}

	l = &LinkedList2{}
	node1 := &Node{value: 1}
	l.Insert(nil, node1)
	checkLinks(t, l)
	if !equal(listToValues(l), []int{1}) {
		t.Error("insert into empty")
	}

	// Insert after non-existing
	l = createList(1, 2)
	fake := &Node{value: 100}
	l.Insert(fake, &Node{value: 3}) // should do nothing
	if !equal(listToValues(l), []int{1, 2}) {
		t.Error("insert after missing")
	}
}

func TestInsertFirst(t *testing.T) {
	l := createList(2, 3)
	node1 := &Node{value: 1}
	l.InsertFirst(node1)
	checkLinks(t, l)
	if !equal(listToValues(l), []int{1, 2, 3}) {
		t.Error("insert first")
	}

	l = &LinkedList2{}
	node1 = &Node{value: 1}
	l.InsertFirst(node1)
	checkLinks(t, l)
	if !equal(listToValues(l), []int{1}) {
		t.Error("insert first empty")
	}
}

func TestClean(t *testing.T) {
	l := createList(1, 2, 3)
	l.Clean()
	if l.head != nil || l.tail != nil {
		t.Error("after clean")
	}
	if l.Count() != 0 {
		t.Error("count after clean")
	}
}

func equal(a, b []int) bool {
	if len(a) != len(b) {
		return false
	}
	for i := range a {
		if a[i] != b[i] {
			return false
		}
	}
	return true
}

// To run tests, but since it's code, main not needed for tests.
