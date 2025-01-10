from stack import Stack


def balanced_brackets(brackets):
    stack = Stack()

    for char in brackets:
        stack.push(char)

    if stack.size() % 2 != 0:
        return False

    return brackets_balancer(stack)


def brackets_balancer(stack):
    while stack.size() > 0:
        if stack.pop() == '(':
            return False
        else:
            if not brackets_balancer(stack) and stack.size() == 0:
                return True
    return False
