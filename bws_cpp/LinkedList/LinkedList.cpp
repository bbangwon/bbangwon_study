// LinkedList.cpp : �ܼ� ���� ���α׷��� ���� �������� �����մϴ�.
//

#include "stdafx.h"

typedef int ElementType;

struct Node
{
	ElementType Data; /* ������ �ʵ� */
	Node* NextNode;	/* ���� ��带 ����Ű�� ������ */
};

//��� ����
Node* SLL_CreateNode(ElementType NewData)
{
	Node* NewNode = new Node;

	NewNode->Data = NewData;/*�����͸� �����Ѵ�.*/
	NewNode->NextNode = nullptr;/*���� ��忡 ���� �����ʹ� nullptr�� �ʱ�ȭ�Ѵ�.*/

	return NewNode;/*����� �ּҸ� ��ȯ�Ѵ�.*/
}

//��� �Ҹ�
void SLL_DestroyNode(Node* Node)
{
	delete Node;
}

/*��� �߰�*/
void SSL_AppendNode(Node** Head, Node* NewNode)
{
	/*��� ��尡 NULL�̶�� ���ο� ��尡 Head*/
	if ((*Head) == nullptr)
	{
		*Head = NewNode;
	}
	else
	{
		/*������ ã�� NewNode�� �����Ѵ�. */
		Node* Tail = (*Head);
		while (Tail->NextNode != nullptr)
			Tail = Tail->NextNode;

		Tail->NextNode = NewNode;
	}
}

/*��� Ž��*/
Node* SSL_GetNodeAt(Node* Head, int Location)
{
	Node* Current = Head;
	while (Current != nullptr && (--Location) >= 0)
		Current = Current->NextNode;

	return Current;
}

/*��� ����*/
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

/*��� ����*/
void SSL_InsertAfter(Node* Current, Node* NewNode)
{
	NewNode->NextNode = Current->NextNode;
	Current->NextNode = NewNode;
}

/*��� �� ����*/
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

