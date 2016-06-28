// AddressManager.cpp : 콘솔 응용 프로그램에 대한 진입점을 정의합니다.
//

#include "stdafx.h"
#include <iostream>

using namespace std;

struct OwnerInfo
{
	char name[20];				// 이름
	char phoneNumber[13];		// 전화번호
	char email[30];				// 이메일
	char address[50];			// 주소
	char twitterAccount[20];	// 트위터 계정
};

struct ContactInfo
{
	char name[20];				// 이름
	char phoneNumber[13];		// 전화번호
	char email[30];				// 이메일
	char address[50];			// 주소
};

OwnerInfo owner;
ContactInfo contacts[100];
int contactNumber = 0;

void inputOwnerInfo()
{
	cout << "소유자 이름: ";
	cin >> owner.name;

	cout << "소유자 전화번호: ";
	cin >> owner.phoneNumber;

	cout << "소유자 이메일: ";
	cin >> owner.email;

	cout << "소유자 주소: ";
	cin >> owner.address;

	cout << "소유자 트위터 계정: ";
	cin >> owner.twitterAccount;

	cout << "입력이 완료 되었습니다." << endl;
}

void outputOwnerInfo()
{
	cout << "소유자 이름: " << owner.name << endl;
	cout << "소유자 전화번호: " << owner.phoneNumber << endl;
	cout << "소유자 이메일: " << owner.email << endl;
	cout << "소유자 주소: " << owner.address << endl;
	cout << "소유자 트위터 계정: " << owner.twitterAccount << endl;
}

void addContact()
{
	cout << "연락처 이름: ";
	cin >> contacts[contactNumber].name;

	cout << "연락처 전화번호: ";
	cin >> contacts[contactNumber].phoneNumber;

	cout << "연락처 이메일: ";
	cin >> contacts[contactNumber].email;

	cout << "연락처 주소: ";
	cin >> contacts[contactNumber].address;

	contactNumber++;
	cout << "입력이 완료 되었습니다." << endl;
}

void outputContactList()
{
	int i;

	for (i = 0; i < contactNumber; i++)
	{
		cout << i << " : " << contacts[i].name << endl;
	}
}

void runOwnerMenu()
{
	int menuNum;

	do
	{
		cout << "1. 소유자 정보 입력" << endl;
		cout << "2. 소유자 정보 조회" << endl;
		cout << "3. 이전 화면" << endl;
		cin >> menuNum;

		switch (menuNum)
		{
		case 1:
			inputOwnerInfo();
			break;
		case 2:
			outputOwnerInfo();
			break;
		case 3:
			cout << "이전 화면으로 돌아 갑니다." << endl;
			break;
		default:
			cout << "잘못 입력 했습니다. 다시 해주세요" << endl;
			break;
		}
	} while (menuNum != 3);
}

void runContactMenu()
{
	int menuNum;

	do
	{
		cout << "1. 연락처 추가 입력" << endl;
		cout << "2. 연락처 목록 조회" << endl;
		cout << "3. 이전 화면" << endl;
		cin >> menuNum;

		switch (menuNum)
		{
		case 1:
			addContact();
			break;
		case 2:
			outputContactList();
			break;
		case 3:
			cout << "이전 화면으로 돌아 갑니다." << endl;
			break;
		default:
			cout << "잘못 입력 했습니다. 다시 해주세요" << endl;
			break;
		}
	} while (menuNum != 3);
}

void runMainMenu()
{
	int menuNum;

	do {
		cout << "1. 소유자 관리 기능" << endl;
		cout << "2. 연락처 관리 기능" << endl;
		cout << "3. 프로그램 종료" << endl;
		cin >> menuNum;

		switch (menuNum)
		{
		case 1:
			runOwnerMenu();
			break;
		case 2:
			runContactMenu();
			break;
		case 3:
			cout << "프로그램을 종료합니다." << endl;
			break;
		default:
			cout << "잘못 입력 했습니다. 다시 해주세요" << endl;
			break;
		}
	} while (menuNum != 3);



}
int main()
{
	runMainMenu();
    return 0;
}

