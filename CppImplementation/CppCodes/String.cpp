#include <iostream>
#include <queue>
#include <stack>
using namespace std;

struct node {
	node* prev;
	char key;
	node* next;
	node(char key_ = 0, node* prev_ = 0, node* next_ = 0) :key(key_), prev(prev_), next(next_) {}
};

class String {
	node* head;
	node* tail;
	void AddDollarBefore(node *pos);
public:
	String() :head(new node), tail(head) {}
	~String();
	void Insert(char elem);
	/*void Remove(char elem);*/
	void Strrev();
	void Fixnum();
	void print()const;
};

String::~String() {
	node *iter = head;
	while (iter) {
		head = iter;
		iter = iter->next;
		delete head;
	}
}

void String::Insert(char elem) {
	node *newnode = new node(elem);
	tail->next = newnode;
	newnode->prev = tail;
	tail = newnode;
}

/* // unnecessary
void String::Remove(char elem) {
for(node* i=head->next;i;i=i->next) {
if(i->key==elem) {
i->prev->next = i->next;
if(i->next)
i->next->prev = i->prev;
else tail = i->prev;
delete i;
return;
}
}
}*/

void String::Strrev() {
	node *i = head->next;
	tail = i;
	while (i) {
		swap(i->prev, i->next);
		head->next = i;
		i = i->prev;
	}
	head->next->prev = head;
	tail->next = 0;
}

void String::Fixnum() {
	int numcnt = 0;
	int cnt = 0;
	node *i, *tmp = 0;
	for (i = head->next; i; i = i->next) {
		tmp = i;
		if (i->key != ' ' && i->key != '\0') {
			if (i->key >= '0' && i->key <= '9')
				numcnt++;
			cnt++;
		}
		else {
			if (cnt && cnt == numcnt) {
				while (numcnt--) tmp = tmp->prev;
				AddDollarBefore(tmp);
			}
			cnt = 0;
			numcnt = 0;
		}
	}
}

void String::AddDollarBefore(node *pos) {
	node *newnode = new node('$', pos->prev, pos);
	pos->prev->next = newnode;
	pos->prev = newnode;
}

void String::print()const {
	for (node *i = head->next; i; i = i->next)
		cout << i->key;
	cout << endl;
}

int main() {
	String A;
	char str[] = "99   0  Hello  world! 123 456 78152012000000000000000000000000 02a20 02021 sd02  02fd 207000 000";
	for (int i = 0; i != strlen(str) + 1; i++)
		A.Insert(str[i]);
	//A.Remove('a');
	//A.Remove('!');
	A.print();
	A.Fixnum();
	A.print();
	A.Strrev();
	A.print();
	queue<int> Q1, Q2;
	stack<int> helper;
	Q1.push(5);
	Q1.push(1);
	Q1.push(3);
	Q1.push(4);
	Q1.push(2);
	while (!Q1.empty()) {
		int key = Q1.front();
		Q1.pop();
		while (!helper.empty() && helper.top() <= key) {
			Q1.push(helper.top());
			helper.pop();
		}
		helper.push(key);
	}
	while (!helper.empty()) {
		Q2.push(helper.top());
		helper.pop();
	}
	while (!Q2.empty()) {
		cout << Q2.front() << ends;
		Q2.pop();
	}
	return 0;
}
