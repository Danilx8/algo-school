package golang

import (
	"fmt"
	"reflect"
)

type Node struct {
	Value interface{}
	Next  *Node
}

type LinkedList struct {
	Head *Node
	Tail *Node
}

func (ll *LinkedList) AddInTail(item *Node) {
	if ll.Head == nil {
		ll.Head = item
	} else {
		ll.Tail.Next = item
	}
	ll.Tail = item
}

func (ll *LinkedList) PrintAllNodes() {
	node := ll.Head
	for node != nil {
		fmt.Println(node.Value)
		node = node.Next
	}
}

func (ll *LinkedList) Find(val interface{}) *Node {
	node := ll.Head
	for node != nil {
		if reflect.DeepEqual(node.Value, val) {
			return node
		}
		node = node.Next
	}
	return nil
}

func (ll *LinkedList) FindAll(val interface{}) []*Node {
	var nodes []*Node
	node := ll.Head
	for node != nil {
		if reflect.DeepEqual(node.Value, val) {
			nodes = append(nodes, node)
		}
		node = node.Next
	}
	return nodes
}

func (ll *LinkedList) Delete(val interface{}, all bool) {
	if ll.Head == nil {
		return
	}

	node := ll.Head

	// Delete from the beginning while head matches
	for node != nil && reflect.DeepEqual(node.Value, val) {
		ll.Head = ll.Head.Next
		node = ll.Head
		if ll.Head == nil {
			ll.Tail = nil
		}
		if !all {
			return
		}
	}

	var prev *Node
	for node != nil {
		for node != nil && !reflect.DeepEqual(node.Value, val) {
			prev = node
			node = node.Next
		}

		if node == nil {
			return
		}

		// remove node
		prev.Next = node.Next
		node = prev.Next
		if prev.Next == nil {
			ll.Tail = prev
		}

		if !all {
			break
		}
	}
}

// Clean removes all nodes from the list
func (ll *LinkedList) Clean() {
	ll.Head = nil
	ll.Tail = nil
}

func (ll *LinkedList) Len() int {
	count := 0
	node := ll.Head
	for node != nil {
		count++
		node = node.Next
	}
	return count
}

func (ll *LinkedList) Insert(afterNode *Node, newNode *Node) {
	if afterNode == ll.Tail {
		ll.AddInTail(newNode)
		return
	}

	if afterNode == nil {
		newNode.Next = ll.Head
		ll.Head = newNode
		if ll.Tail == nil {
			ll.Tail = newNode
		}
		return
	}

	node := ll.Head
	for node != nil {
		if node == afterNode {
			newNode.Next = node.Next
			node.Next = newNode
			if node.Next != nil && node.Next.Next == nil {
				ll.Tail = node.Next
			}
			break
		}
		node = node.Next
	}
}

// Summ returns a new list where each node value is the sum of corresponding
// nodes of the input lists. If the lists have different lengths, it returns nil.
// Assumes node values are integers (int); behavior is undefined for other types.
func Summ(first, second *LinkedList) *LinkedList {
	if first == nil || second == nil {
		return nil
	}
	if first.Len() != second.Len() {
		return nil
	}

	result := &LinkedList{}
	n1 := first.Head
	n2 := second.Head
	for n1 != nil {
		v1, _ := n1.Value.(int)
		v2, _ := n2.Value.(int)
		result.AddInTail(&Node{Value: v1 + v2})
		n1 = n1.Next
		n2 = n2.Next
	}
	return result
}
