#include <iostream>
#include <fstream>
#include <string>
#include <iomanip>
using namespace std;

struct  TNode {
	int Data;
	TNode *Left, *Right;
};
typedef TNode* PNode;

void Add(PNode & root, int n, ofstream &file)
{
	if (root == NULL)
	{
		root = new TNode;
		root->Data = n;
		root->Left = root->Right = NULL; 
		file << "DONE" << endl;
	}
	else if (n < root->Data) Add(root->Left, n, file);
	else if (n > root->Data) Add(root->Right, n, file);
	else file << "ALREADY" << endl;
}

PNode Search(PNode root, int n, ofstream &file)
{
	if (root == NULL)
	{
		file << "NO" << endl;
		return NULL;
	}
	else if (n == root->Data)
	{
		file << "YES" << endl;
		return root;
	}
	else if (n < root->Data) return Search(root->Left, n, file);
	else if (n > root->Data) return Search(root->Right, n, file);
}

void FindReplacement(PNode &replacementRoot, PNode &rootToReplace)
{
	if (replacementRoot->Right != NULL) FindReplacement(replacementRoot->Right, rootToReplace);
	else {
		rootToReplace->Data = replacementRoot->Data;
		rootToReplace = replacementRoot;
		replacementRoot = replacementRoot->Left;
	}
}

void Delete(PNode &root, int n, ofstream &file)
{
	PNode rootToDelete;
	if (root == NULL) file << "CANNOT" << endl;
	else if (n < root->Data) return Delete(root->Left, n, file);
	else if (n > root->Data) return Delete(root->Right, n, file);
	else {
		rootToDelete = root;
		if (rootToDelete->Right == NULL) root = rootToDelete->Left;
		else if (rootToDelete->Left == NULL) root = rootToDelete->Right;
		else FindReplacement(rootToDelete->Left, rootToDelete);
		delete rootToDelete; 
		file << "DONE" << endl;
	}
}

void PrintTree(PNode root, int level, ostream &file)
{
	if (root == NULL) return;
	PrintTree(root->Left, level + 1, file);
	for (int i = 0; i < level; i++)
		file << ".";
	file << root->Data << endl;
	PrintTree(root->Right, level + 1, file);
}

int main()
{
	ifstream inputFile("input.txt");
	ofstream outputFile("output.txt");
	PNode root = NULL;
	string command;
	int n;
	setlocale(LC_ALL, "Russian");
	while (inputFile >> command)
	{
		if (command == "PRINTTREE") PrintTree(root, 0, outputFile);
		else {
			inputFile >> n;
			if (command == "ADD") Add(root, n, outputFile);
			else if (command == "SEARCH") Search(root, n, outputFile);
			else if (command == "DELETE") Delete(root, n, outputFile);
		}
	}
	inputFile.close();
	outputFile.close();
	return 0;
}