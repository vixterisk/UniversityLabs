Individual work "Trees"

What should be done?
Write a program that implements actions in the binary search tree "Insert", "Delete", "Find" (by value).
The program must process requests of the following types:
ADD n
If the specified number is not yet in the tree, insert it and display the word "DONE", if it already exists, leave the tree as it is and display the word "ALREADY".
Delete n
If the specified number is in the tree, delete it and display the word "DONE", if not, leave the tree as it is and display the word "CANNOT". When deleting, exchange the value with the maximum element of the left subtree.
SEARCH n
If the specified number is in the tree, output the word "YES", if not, output the word "NO". The tree does not change.
PRINTTREE
Display the tree sideways like this:
left subtree,
root,
right subtree.

Before each root, dots are printed in an amount equal to the depth level.
Input and output
- The source data is in a file, each line of which contains one of the queries ADD n, DELETE n, SEARCH n and PRINTTREE.
- For each query, output the result to the second file:
- a word indicating the success of the operation, or
- tree for PRINTTREE request.

Algorithm
Being in a loop, we read the file line by line to the end, in each line we read the first word:
If the current command is PRINTTREE, print the tree and go to the next iteration.
If the command is different, we read the number N as the second parameter, after which, having recognized the command, we pass to the corresponding method.