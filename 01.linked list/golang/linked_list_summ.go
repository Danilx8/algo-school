package main

func Summ(first, second *LinkedList) *LinkedList {
	if first == nil || second == nil {
		return nil
	}
	if first.Count() != second.Count() {
		return nil
	}

	result := &LinkedList{}
	n1 := first.head
	n2 := second.head
	for n1 != nil {
		v1 := n1.value
		v2 := n2.value
		result.AddInTail(Node{value: v1 + v2})
		n1 = n1.next
		n2 = n2.next
	}
	return result
}
