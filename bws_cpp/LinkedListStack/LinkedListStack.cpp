// LinkedListStack.cpp : �ܼ� ���� ���α׷��� ���� �������� �����մϴ�.
//

#include "stdafx.h"
#include "LinkedListStack.h"
#include "Calculator.h"

void LLS_CreateStack(LinkedListStack** Stack)
{
	/*������ ���� ����ҿ� ����*/
	(*Stack) = new LinkedListStack;
	(*Stack)->List = nullptr;
	(*Stack)->Top = nullptr;
}

void LLS_DestroyStack(LinkedListStack * Stack)
{
	while (!LLS_IsEmpty(Stack))
	{
		Node* Popped = LLS_Pop(Stack);
		LLS_DestroyNode(Popped);
	}
	delete Stack;
}

Node * LLS_CreateNode(char * NewData)
{
	/*���� ����ҿ� ��� �Ҵ�*/
	Node* NewNode = new Node;
	/*�Է¹��� ���ڿ��� ũ�⸸ŭ�� ���� ����ҿ� �Ҵ�*/
	NewNode->Data = new char[strlen(NewData) + 1];
	/*���� ����ҿ� ���ڿ� ����*/
	strcpy_s(NewNode->Data, strlen(NewData) + 1, NewData);	/*�����͸� �����Ѵ�.*/
	NewNode->NextNode = nullptr;	/*������忡 ���� �����ʹ� nullptr�� �ʱ�ȭ�Ѵ�.*/
	return NewNode;	/*����� �ּҸ� ��ȯ�Ѵ�.*/
}

void LLS_DestroyNode(Node* _Node)
{
	delete _Node->Data;
	delete _Node;
}

void LLS_Push(LinkedListStack * Stack, Node * NewNode)
{
	if (Stack->List == nullptr)
	{
		Stack->List = NewNode;
	}
	else
	{
		/*�ֻ��� ��带 ã�� NewNode�� �����Ѵ�(�״´�).*/
		Node* OldTop = Stack->List;
		while (OldTop->NextNode != nullptr)
		{
			OldTop = OldTop->NextNode;
		}
		OldTop->NextNode = NewNode;
	}
	/*������ Top�ʵ忡 �� ����� �ּҸ� ����Ѵ�.*/
	Stack->Top = NewNode;
}

Node * LLS_Pop(LinkedListStack * Stack)
{
	/*���� �ֻ��� ����� �ּҸ� �ٸ� �����Ϳ� ������ �д�.*/
	Node* TopNode = Stack->Top;
	if (Stack->List == Stack->Top)
	{
		Stack->List = nullptr;
		Stack->Top = nullptr;
	}
	else
	{
		/*���ο� �ֻ��� ��带 ������ Top�ʵ忡 ����Ѵ�.*/
		Node* CurrentTop = Stack->List;
		while (CurrentTop != nullptr && CurrentTop->NextNode != Stack->Top)
		{
			CurrentTop = CurrentTop->NextNode;
		}
		Stack->Top = CurrentTop;
		CurrentTop->NextNode = nullptr;
	}
	return TopNode;
}

Node * LLS_Top(LinkedListStack * Stack)
{
	return Stack->Top;
}

int LLS_GetSize(LinkedListStack * Stack)
{
	int Count = 0;
	Node* Current = Stack->List;
	while (Current!=nullptr)
	{
		Current = Current->NextNode;
		Count++;
	}
	return Count;
}

int LLS_IsEmpty(LinkedListStack * Stack)
{
	return (Stack->List == nullptr);
}

int main()
{
	char InfixExpression[100];
	char PostfixExpression[100];

	double Result = 0.0;
	memset(InfixExpression, 0, sizeof(InfixExpression));
	memset(PostfixExpression, 0, sizeof(PostfixExpression));

	printf("Enter Infix Expression:");
	scanf_s("%s", InfixExpression, sizeof(InfixExpression));

	GetPostfix(InfixExpression, PostfixExpression);

	printf("Infix:%s\nPostfix:%s\n", InfixExpression, PostfixExpression);
	Result = Calculate(PostfixExpression);
	printf("Calculation Result : %f\n", Result);
    return 0;
}

