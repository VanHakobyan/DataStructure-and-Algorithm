#include <iostream>
#include <stack>
#include <queue>
using namespace std;

struct node {
	int index;
	int key;
	node* next;
	node(int key_ = 0, int index_ = 0, node* next_ = 0) :key(key_), index(index_), next(next_) {}
};

class Vector {
	node *first;
public:
	Vector() :first(0) {}
	~Vector();
	void Insert(int key, int index);
	void Remove(int key);
	node* operator[](int);
	int GetSize()const;
	void Add(const Vector& A);
	friend int DotProduct(const Vector& A, const Vector& B);
};

Vector::~Vector() {
	node* iter = first;
	while (iter) {
		first = iter;
		iter = iter->next;
		delete first;
	}
}

void Vector::Insert(int key, int index) {
	if (!key) return;
	node *iter = first, *prev = 0;
	while (iter) {
		if (iter->index < index) break;
		if (iter->index == index) {
			iter->key += key;
			if (!iter->key) {
				if (!prev) first = iter->next;
				else prev->next = iter->next;
				delete iter;
			}
			return;
		}
		prev = iter;
		iter = iter->next;
	}
	if (!prev) first = new node(key, index, iter);
	else prev->next = new node(key, index, iter);
}

// unnecessary
void Vector::Remove(int index) {
	for (node *it = first, *prev = 0; it; prev = it, it = it->next) {
		if (it->index == index) {
			if (!prev) first = 0;
			else prev->next = it->next;
			delete it;
			return;
		}
	}
}

node* Vector::operator[](int index) {
	for (node *it = first; it; it = it->next)
		if (it->index == index)
			return it;
	return 0;
}

int Vector::GetSize()const {
	return first->index;
}

void Vector::Add(const Vector& A) {
	for (node *iter = A.first; iter; iter = iter->next)
		Insert(iter->key, iter->index);
}

int DotProduct(const Vector& A, const Vector& B) {
	int res = 0;
	node *p = A.first;
	node *q = B.first;
	while (p&&q) {
		if (p->index == q->index) {
			res += p->key*q->key;
			p = p->next;
			q = q->next;
		}
		else
			if (p->index < q->index) q = q->next;
			else p = p->next;
	}
	return res;
}

int main()
{
	Vector A, B;
	A.Insert(10, 5);
	A.Insert(4, 7);
	A.Insert(7, 4);
	A.Insert(1, 0);
	A.Insert(9, 3);
	A.Insert(-4, 7);
	A.Insert(-1, 0);
	B.Insert(100, 1);
	B.Insert(100, 4);
	B.Insert(100, 2);
	B.Insert(100, 3);
	B.Insert(100, 100);
	cout << A[4]->key << ends << A[4]->index << endl;
	cout << A.GetSize() << endl;
	cout << DotProduct(A, B) << endl;
	A.Add(B);
	stack<int> S1, S3, tmp;
	queue<int> S2;
	S1.push(1);
	S1.push(2);
	S1.push(3);
	S1.push(4);
	S1.push(5);
	S2.push(10);
	S2.push(11);
	S2.push(12);
	while (!S2.empty()) {
		tmp.push(S2.front());
		S2.pop();
	}
	while (!tmp.empty()) {
		S3.push(tmp.top());
		tmp.pop();
	}
	while (!S1.empty()) {
		tmp.push(S1.top());
		S1.pop();
	}
	while (!tmp.empty()) {
		S3.push(tmp.top());
		tmp.pop();
	}
	while (!S3.empty()) {
		cout << S3.top() << ends;
		S3.pop();
	}
	return 0;
}
