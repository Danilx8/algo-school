package stack

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
