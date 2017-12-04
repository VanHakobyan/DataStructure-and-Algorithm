#include <iostream>
#include <queue>
using namespace std;
struct TreeNode {
	TreeNode *left;
	TreeNode *right;
	int data;

	TreeNode(int d, TreeNode *l = 0, TreeNode*r = 0) {
		data = d;
		left = l;
		right = r;
	}
};
