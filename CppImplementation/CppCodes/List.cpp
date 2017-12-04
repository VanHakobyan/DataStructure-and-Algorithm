#include <iostream>
#include <stack>
using namespace std;

struct node {
	int key;
	node* next;
	node(int key_ = 0, node *next_ = 0) :key(key_), next(next_) {}
};

class ListInt {
	node *first;
	int size;
	int gcd_binary(int, int)const;
public:
	ListInt() :first(0), size(0) {}
	~ListInt();
	void Insert(int elem, int k);
	void Remove(int elem);
	int GCD()const;//amenamec @ndh baj
	void Merge(ListInt& A, ListInt& B); //miavorum
};

ListInt::~ListInt() {
	node *iter = first;
	while (iter) {
		first = iter;
		iter = iter->next;
		delete first;
	}
}

void ListInt::Insert(int elem, int k) {
	node *newnode = new node(elem);
	if (!first) first = newnode;
	else {
		if (k == 0) {
			newnode->next = first;
			first = newnode;
		}
		else {
			node *iter = first;
			int i = 1;
			while (iter->next && i<k) {
				iter = iter->next;
				i++;
			}
			newnode->next = iter->next;
			iter->next = newnode;
		}
	}
	size++;
}

void ListInt::Remove(int elem) {
	for (node *i = first, *p = 0; i; p = i, i = i->next) {
		if (i->key == elem) {
			if (!p) first = i->next;
			else p->next = i->next;
			size--;
			delete i;
		}
	}
}

int ListInt::gcd_binary(int m, int n)const {
	if (m == n) return m;
	if (m>n) return gcd_binary(m - n, m);
	else return gcd_binary(n - m, m);
}

int ListInt::GCD()const {
	if (!first) return 0;
	int k = first->key;
	for (node *i = first; i; i = i->next)
		k = gcd_binary(i->key, k);
	return k;
}

void ListInt::Merge(ListInt& A, ListInt& B) {
	node *i, *j, *k = new node;
	node *q = k;
	for (i = A.first, j = B.first; i&&j; k = k->next) {
		if (i->key < j->key) {
			k->next = new node(i->key);
			i = i->next;
		}
		else {
			k->next = new node(j->key);
			j = j->next;
		}
	}
	if (!i) i = j;
	while (i) {
		k->next = new node(i->key);
		i = i->next;
		k = k->next;
	}
	first = q->next;
	delete q;
}

int main() {
	ListInt A, B;
	A.Insert(10, 0);
	A.Insert(20, 1);
	A.Insert(30, 3);
	A.Insert(40, 4);
	A.Insert(50, 5);
	cout << A.GCD() << endl;
	B.Insert(15, 1);
	B.Insert(25, 2);
	B.Insert(35, 3);
	cout << B.GCD() << endl;
	ListInt C;
	C.Merge(A, B);
	char calculate[] = "5552**+";
	stack<int> x;
	for (int i = 0; i != strlen(calculate); i++) {
		if (calculate[i] >= '0' && calculate[i] <= '9')
			x.push(calculate[i] - '0');
		else {
			int k = x.top();
			x.pop();
			switch (calculate[i]) {
			case '+':	x.top() += k;
				break;
			case '-':	x.top() -= k;
				break;
			case '*':	x.top() *= k;
				break;
			case '/':	x.top() /= k;
				break;
			}
		}
	}
	int res = x.top();
	cout << endl << res << endl;
	return 0;
}
