package stack

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
