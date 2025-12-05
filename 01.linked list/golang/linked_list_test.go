package golang

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

	onesNodes      []*Node
	differentNodes []*Node
	emptyNodes     []*Node
}

func makeFixtures() fixtures {
	f := fixtures{
		emptyList:      &LinkedList{},
		oneElementList: &LinkedList{},
		bigList:        &LinkedList{},
		identicalList:  &LinkedList{},
		onesNodes:      make([]*Node, 0),
		differentNodes: make([]*Node, 0),
		emptyNodes:     make([]*Node, 0),
	}

	one := &Node{Value: 1}
	f.oneElementList.AddInTail(one)

	for i := 0; i < lengthConst; i++ {
		f.bigList.AddInTail(&Node{Value: i})
		// in C# they commented collecting nodes into differentNodes here
	}

	for i := 0; i < lengthConst; i++ {
		n := &Node{Value: 1}
		f.identicalList.AddInTail(n)
		f.onesNodes = append(f.onesNodes, n)
	}

	return f
}

func TestEmptyFind(t *testing.T) {
	f := makeFixtures()
	// DataRow(1) and an empty DataRow() which we mirror as 0
	for _, v := range []int{1, 0} {
		if got := f.emptyList.Find(v); got != nil {
			t.Fatalf("expected nil, got %v", got)
		}
	}
}

func TestSingleFind(t *testing.T) {
	f := makeFixtures()
	if got := f.oneElementList.Find(1); got == nil {
		t.Fatalf("expected to find 1 in oneElementList")
	}
}

func TestBigFind(t *testing.T) {
	f := makeFixtures()
	for _, v := range []int{1, lengthConst - 1, 50} {
		if got := f.bigList.Find(v); got == nil {
			t.Fatalf("expected to find %d in bigList", v)
		}
	}
}

func TestIdenticalFind(t *testing.T) {
	f := makeFixtures()
	if got := f.identicalList.Find(1); got == nil {
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
	for _, v := range []int{0, 50, 99} {
		f.differentNodes = append(f.differentNodes, f.bigList.Find(v))
		got := f.bigList.FindAll(v)
		if !reflect.DeepEqual(got, f.differentNodes) {
			t.Fatalf("expected %v, got %v", f.differentNodes, got)
		}
		// reset for next iteration to mirror per-DataRow behavior
		f.differentNodes = f.differentNodes[:0]
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
	if f.bigList.Len() != lengthConst {
		t.Fatalf("expected length %d, got %d", lengthConst, f.bigList.Len())
	}
}

func TestBigRemove(t *testing.T) {
	f := makeFixtures()
	before := f.bigList.Len()
	f.bigList.Delete(lengthConst-1, false)
	after := f.bigList.Len()
	if !(before == lengthConst && after == lengthConst-1) {
		t.Fatalf("expected removal of one element, before=%d after=%d", before, after)
	}
}

func TestIdenticalRemove(t *testing.T) {
	f := makeFixtures()
	f.identicalList.Delete(1, true)
	if f.identicalList.Len() != 0 {
		t.Fatalf("expected list to be empty after RemoveAll")
	}
}

func TestSingleRemove(t *testing.T) {
	f := makeFixtures()
	f.oneElementList.Delete(1, false)
	if f.oneElementList.Len() != 0 {
		t.Fatalf("expected single element list to be empty after delete")
	}
}

func TestEmptySumm(t *testing.T) {
	f := makeFixtures()
	res := Summ(f.emptyList, f.emptyList)
	if res == nil || res.Head != nil || res.Tail != nil {
		t.Fatalf("expected empty list with nil head and tail, got %+v", res)
	}
}

func TestBigSumm(t *testing.T) {
	f := makeFixtures()

	// build expected sum list (bigList + bigList)
	expected := &LinkedList{}
	for i := 0; i < lengthConst; i++ {
		expected.AddInTail(&Node{Value: i + i})
	}

	got := Summ(f.bigList, f.bigList)
	if got == nil {
		t.Fatalf("expected non-nil result list")
	}

	n1 := expected.Head
	n2 := got.Head
	for n1 != nil && n2 != nil {
		if n1.Value.(int) != n2.Value.(int) {
			t.Fatalf("sum mismatch: expected %d, got %d", n1.Value.(int), n2.Value.(int))
		}
		n1 = n1.Next
		n2 = n2.Next
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
