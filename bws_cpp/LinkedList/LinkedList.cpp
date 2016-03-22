// LinkedList.cpp : 콘솔 응용 프로그램에 대한 진입점을 정의합니다.
//

#include "stdafx.h"

typedef int ElementType;

struct Node
{
	ElementType Data; /* 데이터 필드 */
	Node* NextNode;	/* 다음 노드를 가리키는 포인터 */
};

//노드 생성
Node* SLL_CreateNode(ElementType NewData)
{
	Node* NewNode = new Node;

	NewNode->Data = NewData;/*데이터를 저장한다.*/
	NewNode->NextNode = nullptr;/*다음 노드에 대한 포인터는 nullptr로 초기화한다.*/

	return NewNode;/*노드의 주소를 반환한다.*/
}

//노드 소멸
void SLL_DestroyNode(Node* Node)
{
	delete Node;
}

/*노드 추가*/
void SSL_AppendNode(Node** Head, Node* NewNode)
{
	/*헤드 노드가 NULL이라면 새로운 노드가 Head*/
	if ((*Head) == nullptr)
	{
		*Head = NewNode;
	}
	else
	{
		/*테일을 찾아 NewNode를 연결한다. */
		Node* Tail = (*Head);
		while (Tail->NextNode != nullptr)
			Tail = Tail->NextNode;

		Tail->NextNode = NewNode;
	}
}

/*노드 탐색*/
Node* SSL_GetNodeAt(Node* Head, int Location)
{
	Node* Current = Head;
	while (Current != nullptr && (--Location) >= 0)
		Current = Current->NextNode;

	return Current;
}

/*노드 제거*/
void SSL_RemoveNode(Node** Head, Node* Remove)
{
	if (*Head == Remove)
	{
		*Head = Remove->NextNode;
	}
	else
	{
		Node* Current = *Head;
		while (Current != nullptr && Current->NextNode != Remove)
			Current = Current->NextNode;

		if (Current != nullptr)
			Current->NextNode = Remove->NextNode;
	}
}

/*노드 삽입*/
void SSL_InsertAfter(Node* Current, Node* NewNode)
{
	NewNode->NextNode = Current->NextNode;
	Current->NextNode = NewNode;
}

/*노드 수 세기*/
int SLL_GetNodeCount(Node* Head)
{
	int Count = 0;
	Node* Current = Head;

	while (Current != nullptr)
	{
		Current = Current->NextNode;
		Count++;
	}

	return Count;
}


int main()
{
	Node *List = nullptr;
	Node *NewNode = nullptr;

	NewNode = SLL_CreateNode(117);
	SSL_AppendNode(&List, NewNode);

	NewNode = SLL_CreateNode(119);
	SSL_AppendNode(&List, NewNode);

	SLL_DestroyNode(List);

	return 0;
}

