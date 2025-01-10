from stack import Stack


def stacks_calculations(expression):
    first_stack = Stack()
    second_stack = Stack()
    chars = list(expression)

    number_continues = False
    number = ""

    for i in range(len(chars) - 1, -1, -1):
        if number_continues and chars[i].isdigit():
            number = chars[i] + number
        elif number_continues:
            first_stack.push(int(number))
            number = ""
            number_continues = False
        elif chars[i].isdigit():
            number_continues = True
            number += chars[i]
        elif chars[i] != ' ':
            first_stack.push(chars[i])

    if number:
        first_stack.push(int(number))

    while first_stack:
        current_object = first_stack.pop()

        if isinstance(current_object, int):
            second_stack.push(current_object)
        elif current_object == '=':
            return second_stack.pop()
        else:
            second_number = second_stack.pop()
            first_number = second_stack.pop()
            if current_object == '+':
                second_stack.push(first_number + second_number)
            elif current_object == '*':
                second_stack.push(first_number * second_number)
            elif current_object == '-':
                second_stack.push(first_number - second_number)
            elif current_object == '/':
                second_stack.push(first_number / second_number)
            else:
                raise ValueError("Wrong input expression")

    return 0


print(stacks_calculations(input()))
