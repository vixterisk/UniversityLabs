������
��� 3. ��������������� ���� ����� ������� �����. ������ ����� ������������ ����� ������ (�������, �������, �����). ��� ����� �������� ������������ �������. ����� ���������� ����� ����� ��������� ��������� � �����. ���� ���� ����� ���������� ��������� ���, ������� �1.
�������� ��������� �� Haskell
��� updateMarks
���� ���������� ����� ������� �������� � �������� �� ������ �����, �������������� � ������ ������ ������� = -1 �� �������� ��� � ��������, �������������� � ������ ������ ������� ������ �����, � �������� ������ (�������������� �������, �� ������������ �����) � ���������� ���������� ��� updateMarks ��� ������� �������, �������� ����������, ���������� ��������� ������ ����� � ������ �����.
����� ���� ����� �������������� ������� ����� -1 �� �������� ������ (�������������� �������, ������� ���������� + ��� ����� ����� �������������� �������� � �������) � ���������� ���������� ��� updateMarks ��� ������� �������, �������� ����������, ���������� ��������� ������ ����� � ������ �����.
����� ���� ����� �������������� ������� ������ ����� �������� ���������� � ���� ����� ����� �������������� �������� � �������, �� �������� ������ (�������������� �������, ������� ���������� + ��� ����� ����� �������������� �������� � �������) � ���������� ���������� ��� updateMarks ��� ������� �������, �������� ����������, ���������� ��������� ������ ����� � ������ �����.
����� �������� ������ (�������������� �������, �� ������������ �����) � ���������� ���������� ��� updateMarks ��� ������� �������, �������� ����������, ���������� ��������� ������ ����� � ������ �����.
��� ��� updateMarks
����������� ��������� ��������� Dijkstra � ��������� ������� ��� ������� �������, 0 ��� ������� ����������, ������� �������, ��������������� � ���� ������ ������ � ������� (��� ���� ������ ����� ����� -1, ��������� � ������ �����������) ������ �����, ������ �����
��� Dijkstra
���� ������� ������� ����� ������� �� ������� ������� ����������
����� ���� ���������� ������� �������� �� ����� -1 �� ��������� ��� Dijkstra ��� ������� ������� ��������, ���������� ������� ��������, ������������ ������ �����, �� �������� ������� ������� ������� ��������, � ������ �����
����� ���� ���������� ������� �������� �� ����� -1 �� ��������� ��� Dijkstra ��� ������� ������� ��������, ���������� ������� ��������, ������������ ������ �����, �� �������� ������� ������� ������� ��������, � ������ �����
����� ������� -1, ��� ����
����������� ������ ����� = ����������, ���� ������������ ��������� ���������� ��� updateMarks ��� ������� �������, �������� ����������, ������ ����� � ������ �����
������ ������� = ������������ ������� � ����������� ������ �� ������������ ������ 
������ ������� = ������������ ������� �� ������ ����������� ������ �� ������������ ������
��� ��� Dijkstra
�������� ��������� �� C#
� ��������� ���������� ����� ������ (Edge) ���� (�������, �������, ����������), � ����� ����� ������ (Graph), ���������� ������ ����� � ������, �������� ��������� ����.
��� GetAdjacentNodesInOrientedGraph
���������� ������ ����� ��� ���������� ����������.
��� ������� ����� � ������ ����� �����
��
���� ��������� ������� ����� ��������� � ���������� ���������� �� �������� ����� � ���������
��
������� ���������
��� ��� GetAdjacentNodesInOrientedGraph
��� AllNodesVisited
��� ������ ������� � ������ ������ �����
��
���� ������� ������� �� �������� (�������� visitedNodes �� ���� ������� = ����), ������� ��� ��������� ������� ������ 
��
������� ��� ��������� ������� �������
��� ��� AllNodesVisited
��������� Dijkstra �� ���� �������� ��� ��������� ���������� ����: ��������� ������� (startNode) � ������� ������� (desiredNode)
��� Dijkstra
���
������ ���������� ����� (marks) ��� �������, ��� ������� �������� ������, � ��������� � ������������� ��������;
������ ���������� ���������� ����� (visitedNodes) ��� �������, ��� ������� �������� ������, � ��������� � ���������� ��������;
��� ������ ������� � ������ ������ �����
��
��������� ���������� ���������� ����� �� ������� ������� �������� ������
��
������ ��������� ���������� ������� ������� (currentNode), ���������� �� �������� ��������� ���������� � ��������� ������� (startNode)
���� currentNode � �� ������� ������� � �������� ������� AllNodesVisited �� ������
��
������ ���������� ����� �� ������������� ������ �� ����� noReachableNodes �� ��������� �������
������ ���������� �������� �� ��������� ����������� ���������� �������� �������������� ����.
��� ������ ������� � ������ ������ �����
��
���� ����� ������� �� ����� -1 � �������� ����� ������ �������� � ������� ��� �� �������� 
�� �������� �������� �������� �� �������� �����, ������� ������� ��� ������� �������, ��������� ���� � ������
��
���� �������� ����� ������� �� ��������� ������ �����.
� ���������� �������� ��� ������� ������� ��������� �������
�������� ������ �������� ������ ������� ������� (adjacentNodes) � ������� ������ GetAdjacentNodesInOrientedGraph, ��������� ��� �������� ������� �������
������������� ������ �������� ������ �� �� ���������� �� ������� �������
��� ������ ������� � ������ �������� ������ ������� �������
��
���� ����� ������� ����� -1 ��� �������� ����� ������ ����� ����� ������� ������� � ���������� �� ������� 
�� �������� �������� ����� ������� �� ����� ����� ������� ������� � ���������� �� ������� 
��
��
���� ������ ����� ���� ��� �� �������� ����� ������� ������� �� ������� -1
������� ����� ������� �������
��� ��� Dijkstra
������������ ����������
��� �������� ��������� � ������������� ����� ������ ���� ����������� ���������� ������������ ��������� � ����������� �� ������ ���� ������� (�������� ����������� �������������� ���������, ����������� ��������� �������� ����������). �������� ����� �����������: ������ ������ �������������� ������������������ �������� ��� ����� �����, ��� � ������������ �����, �� ��� ���� ���� ������� ����� ����������� � ��������� ��������� ��������� ��������, �� ����������� ��� �������� ���������� (��������, ������ ������ �� ������������� ���������). 
� �� �� ����� ������������ ����� ����� ���������, �� ���� � ��� �������������� �������� �������� ����� ��� ���������� ��������� �� Haskell � ������� ���������� � �������������� ���������. ��� ������ ���� C#, ������� ���� ���������� ����� ���������� ���� ������, ������������ ������ �����, ��� �������������� �������� � ������������� ������ � ���.
�� ����������������� �������� ������ ����� ���������� ��������� �������� �� �������� ������ �������� ������� ����� ���������� �� �#.
������������ ������������ � �������
� ���� ���������� ������-���� ���������� IDE ��� ��������, �� ���� ����������� ������� ������������ ����������, ��� ������� ������������� ��������� ��� ����������� � �������� ������������ ������ ������� �������: ������������ ���� �� �������� ���������� � ������� ����� ��� ������� �������������� ��� �������, �������� ������������� ���������� �������������� � ��������� �����, ����� �� ������� ����� �������������.
� ������ �������, Visual Studio ��������� ������� ����������� �� ����� ���������, �������� �������� ����������, �������� � �������� � �������� ������� ����� � ����� ����������, ���, ����������, ������ �������� ������������ � �������: � ����� ������ �������� ����������, ��������� �� ��������� �������� � ��������� � ����� �������� ���������� ����� ������������ ��������������.
����� ����������� ����
���� ��������� ������ ���, ����������� � ���������� ��������� ��������, ���������� �� �������� �������� 65 �����, �� �# - 104 ������.
��������������, �������� ������
�� ��������� ������ ��� ��������� ������������ �����������. 
�� ����� � ������ ������ c 99 ��������� ��������� �� C# ���������� �����������, �� �������� � 6 ���
�� ����� � ������ ������ c 249 ��������� ��������� �� C# ���������� �����������, �� �������� � 8 ��� 14 ���
�� ����� � ������ ������ c 1000 ��������� ��������� �� C# ���������� �� 10 ���, �� �������� � 10 �����