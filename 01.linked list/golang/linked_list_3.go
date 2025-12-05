package main

import (
	"reflect"
	"testing"
)

const lengthConst = 10

type fixtures struct {
	emptyList      *LinkedList
	oneElementList *LinkedList
	bigList        *LinkedList
	identicalList  *LinkedList

	onesNodes      []Node
	differentNodes []Node
	emptyNodes     []Node
}

func makeFixtures() fixtures {
	f := fixtures{
		emptyList:      &LinkedList{},
		oneElementList: &LinkedList{},
		bigList:        &LinkedList{},
		identicalList:  &LinkedList{},
		onesNodes:      make([]Node, 0),
		differentNodes: make([]Node, 0),
		emptyNodes:     make([]Node, 0),
	}

	one := Node{value: 1}
	f.oneElementList.AddInTail(one)

	for i := 0; i < lengthConst; i++ {
		f.bigList.AddInTail(Node{value: i})
	}

	// build identical list and expected nodes slice (copies)
	for i := 0; i < lengthConst; i++ {
		n := Node{value: 1}
		f.identicalList.AddInTail(n)
		f.onesNodes = append(f.onesNodes, n)
	}

	return f
}

func TestEmptyFind(t *testing.T) {
	f := makeFixtures()
	for _, v := range []int{1, 0} {
		_, err := f.emptyList.Find(v)
		if err == nil {
			t.Fatalf("expected not found error, got nil for %d", v)
		}
	}
}

func TestSingleFind(t *testing.T) {
	f := makeFixtures()
	_, err := f.oneElementList.Find(1)
	if err != nil {
		t.Fatalf("expected to find 1 in oneElementList, got err: %v", err)
	}
}

func TestBigFind(t *testing.T) {
	f := makeFixtures()
	for _, v := range []int{1, lengthConst - 1} {
		if _, err := f.bigList.Find(v); err != nil {
			t.Fatalf("expected to find %d in bigList", v)
		}
	}
}

func TestIdenticalFind(t *testing.T) {
	f := makeFixtures()
	if _, err := f.identicalList.Find(1); err != nil {
		t.Fatalf("expected to find 1 in identicalList")
	}
}

func TestEmptyFindAll(t *testing.T) {
	f := makeFixtures()
	for _, v := range []int{1, 12, 0} { // includes empty DataRow as 0
		got := f.emptyList.FindAll(v)
		if !reflect.DeepEqual(got, f.emptyNodes) {
			t.Fatalf("expected empty slice, got %v", got)
		}
	}
}

func TestBigFindAll(t *testing.T) {
	f := makeFixtures()
	// value present once
	v := 0
	got := f.bigList.FindAll(v)
	// expected slice with single node (copy)
	expected := []Node{{value: 0}}
	if !reflect.DeepEqual(got, expected) {
		t.Fatalf("expected %v, got %v", expected, got)
	}
}

func TestIdenticalFindAll(t *testing.T) {
	f := makeFixtures()
	got := f.identicalList.FindAll(1)
	if !reflect.DeepEqual(got, f.onesNodes) {
		t.Fatalf("expected onesNodes slice, got %v", got)
	}
}

func TestBigCount(t *testing.T) {
	f := makeFixtures()
	if f.bigList.Count() != lengthConst {
		t.Fatalf("expected length %d, got %d", lengthConst, f.bigList.Count())
	}
}

func TestBigRemove(t *testing.T) {
	f := makeFixtures()
	before := f.bigList.Count()
	f.bigList.Delete(lengthConst-1, false)
	after := f.bigList.Count()
	if !(before == lengthConst && after == lengthConst-1) {
		t.Fatalf("expected removal of one element, before=%d after=%d", before, after)
	}
}

func TestIdenticalRemove(t *testing.T) {
	f := makeFixtures()
	f.identicalList.Delete(1, true)
	if f.identicalList.Count() != 0 {
		t.Fatalf("expected list to be empty after RemoveAll")
	}
}

func TestSingleRemove(t *testing.T) {
	f := makeFixtures()
	f.oneElementList.Delete(1, false)
	if f.oneElementList.Count() != 0 {
		t.Fatalf("expected single element list to be empty after delete")
	}
}

func TestEmptySumm(t *testing.T) {
	f := makeFixtures()
	res := Summ(f.emptyList, f.emptyList)
	if res == nil {
		t.Fatalf("expected non-nil empty result list")
	}
	if res.head != nil || res.tail != nil {
		t.Fatalf("expected empty list with nil head and tail, got %+v", res)
	}
}

func TestBigSumm(t *testing.T) {
	f := makeFixtures()

	// build expected sum list (bigList + bigList)
	expected := &LinkedList{}
	for i := 0; i < lengthConst; i++ {
		expected.AddInTail(Node{value: i + i})
	}

	got := Summ(f.bigList, f.bigList)
	if got == nil {
		t.Fatalf("expected non-nil result list")
	}

	n1 := expected.head
	n2 := got.head
	for n1 != nil && n2 != nil {
		if n1.value != n2.value {
			t.Fatalf("sum mismatch: expected %d, got %d", n1.value, n2.value)
		}
		n1 = n1.next
		n2 = n2.next
	}
	if n1 != nil || n2 != nil {
		t.Fatalf("lists have different lengths")
	}
}

func TestWrongLengthSumm(t *testing.T) {
	f := makeFixtures()
	if Summ(f.emptyList, f.oneElementList) != nil {
		t.Fatalf("expected nil for lists of different lengths")
	}
}
