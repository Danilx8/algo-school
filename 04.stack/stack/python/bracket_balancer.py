from stack import Stack


def balanced_brackets(brackets):
    stack = Stack()

    for char in brackets:
        if char in "({[":
            stack.push(char)
        elif char in ")}]":
            if len(stack.stack) == 0 or not is_matching(stack.pop(), char):
                return False

    return len(stack.stack) == 0


def is_matching(opening, closing):
    matching_pairs = {
        '(': ')',
        '{': '}',
        '[': ']'
    }
    return matching_pairs.get(opening) == closing
