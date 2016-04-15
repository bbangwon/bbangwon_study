// Stack.cpp : �ܼ� ���� ���α׷��� ���� �������� �����մϴ�.
//

#include "stdafx.h"

typedef int ElementType;

typedef struct tagNode
{
	ElementType Data;
} Node;

typedef struct tagArrayStack {
	int Capacity; /* �뷮 */
	int Top;	/*�ֻ��� ����� ��ġ*/
	Node* Nodes; /*���迭*/
} ArrayStack;

void AS_CreateStack(ArrayStack** Stack, int Capacity)
{
	/*������ ���� ����ҿ� ����*/
	(*Stack) = new ArrayStack;

	/*�Էµ� Capacity��ŭ�� ��带 ���� ����ҿ� ����*/
	(*Stack)->Nodes = new Node[Capacity];

	/*Capacity �� Top �ʱ�ȭ*/
	(*Stack)->Capacity = Capacity;
	(*Stack)->Top = 0;
}

void AS_DestroyStack(ArrayStack * Stack)
{
	/*��带 ���� ����ҿ��� ����*/
	delete[] Stack->Nodes;

	/*������ ���� ����ҿ��� ����*/
	delete Stack;
}

void AS_Push(ArrayStack* Stack, ElementType Data)
{
	int Position = Stack->Top;

	Stack->Nodes[Position].Data = Data;
	Stack->Top++;
}

ElementType AS_Pop(ArrayStack* Stack)
{
	int Position = --(Stack->Top);
	return Stack->Nodes[Position].Data;
}

ElementType AS_Top(ArrayStack* Stack)
{
	int Position = Stack->Top - 1;
	return Stack->Nodes[Position].Data;
}

int AS_GetSize(ArrayStack* Stack)
{
	return Stack->Top;
}

int AS_IsEmpty(ArrayStack* Stack)
{
	return (Stack->Top == 0);
}

int main()
{
	int i = 0;
	ArrayStack* Stack = nullptr;

	AS_CreateStack(&Stack, 10);

	AS_Push(Stack, 3);
	AS_Push(Stack, 37);
	AS_Push(Stack, 11);
	AS_Push(Stack, 12);

	printf("Capacity: %d, Size: %d, Top: %d\n\n", Stack->Capacity, AS_GetSize(Stack), AS_Top(Stack));

	for (i = 0; i < 4; i++)
	{
		if (AS_IsEmpty(Stack))
			break;

		printf("Popped: %d, ", AS_Pop(Stack));

		if (!AS_IsEmpty(Stack))
			printf("Current Top: %d\n", AS_Top(Stack));
		else
			printf("Stack Is Empty.\n");
	}
	AS_DestroyStack(Stack);
	return 0;
}

