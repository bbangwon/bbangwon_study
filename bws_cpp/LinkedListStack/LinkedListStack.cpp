// LinkedListStack.cpp : 콘솔 응용 프로그램에 대한 진입점을 정의합니다.
//

#include "stdafx.h"
#include "LinkedListStack.h"
#include "Calculator.h"

void LLS_CreateStack(LinkedListStack** Stack)
{
	/*스택을 자유 저장소에 생성*/
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
	/*자유 저장소에 노드 할당*/
	Node* NewNode = new Node;
	/*입력받은 문자열의 크기만큼을 자유 저장소에 할당*/
	NewNode->Data = new char[strlen(NewData) + 1];
	/*자유 저장소에 문자열 복사*/
	strcpy_s(NewNode->Data, strlen(NewData) + 1, NewData);	/*데이터를 저장한다.*/
	NewNode->NextNode = nullptr;	/*다음노드에 대한 포인터는 nullptr로 초기화한다.*/
	return NewNode;	/*노드의 주소를 반환한다.*/
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
		/*최상위 노드를 찾아 NewNode를 연결한다(쌓는다).*/
		Node* OldTop = Stack->List;
		while (OldTop->NextNode != nullptr)
		{
			OldTop = OldTop->NextNode;
		}
		OldTop->NextNode = NewNode;
	}
	/*스택의 Top필드에 새 노드의 주소를 등록한다.*/
	Stack->Top = NewNode;
}

Node * LLS_Pop(LinkedListStack * Stack)
{
	/*현재 최상위 노드의 주소를 다른 포인터에 복사해 둔다.*/
	Node* TopNode = Stack->Top;
	if (Stack->List == Stack->Top)
	{
		Stack->List = nullptr;
		Stack->Top = nullptr;
	}
	else
	{
		/*새로운 최상위 노드를 스택의 Top필드에 등록한다.*/
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

