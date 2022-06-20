import math
import numpy as np
import pandas as pd
import random as rd

def euclidian(x, y): #Нахождение расстояния через евклидову метрику
    return math.sqrt(np.sum([(a - b) ** 2 for a, b in zip(x, y)]))

def Clusterization(csv_data, patterns, centers): #Выделение кластеров по предложенному набору центров
    result = {k: [] for k in centers}
    for i in range (0, len(patterns)):
        minDistance = -1
        csv_data_curPattern_parameters = np.array(csv_data.iloc[patterns[i], 0:4])
        for j in range(0, len(centers)):
            csv_data_curCenter_parameters = np.array(csv_data.iloc[centers[j], 0:4])
            curDistance = euclidian(csv_data_curPattern_parameters, csv_data_curCenter_parameters)
            if minDistance == -1 or curDistance < minDistance:
                minDistance = curDistance
                cur_claster = centers[j]
        result[cur_claster].append(i)
    return result

def FindNewCenters(csv_data, result): #Нахождение нового набора центров исходя из условия, что сумма квадратов расстояния между всеми образами и новым центром должна быть минимальна
    newCenters = []
    for i in result:
        minSum = -1
        for possibleNewCenter in result[i]:
            Sum = 0
            for curPattern in result[i]:
                if curPattern != possibleNewCenter:
                    csv_data_curPattern_parameters = np.array(csv_data.iloc[curPattern, 0:4])
                    csv_data_possibleNewCenter_parameters = np.array(csv_data.iloc[possibleNewCenter, 0:4])
                    curDistance = euclidian(csv_data_curPattern_parameters, csv_data_possibleNewCenter_parameters)
                    Sum += curDistance * curDistance
            if minSum == -1 or Sum < minSum:
                minSum = Sum
                curNewCenter = possibleNewCenter
        newCenters.append(curNewCenter)
    return newCenters

k = 4
csv_data = pd.read_csv('iris.csv')
patterns = [] #Множество всех образов
centers = [] #Множество всех центров
for i in range(len(csv_data.index)):
    if i < k:
        centers.append(i)
    else:
        patterns.append(i)
continue_flag = True

while continue_flag:
    result = Clusterization(csv_data, patterns, centers)
    newCenters = FindNewCenters(csv_data, result)
    if set(newCenters) == set(centers):
        continue_flag = False
    else:
        for center in centers:
            patterns.append(center)
        centers = newCenters
        for center in centers:
            patterns.remove(center)
result = Clusterization(csv_data, patterns, centers)
for i in range(0, len(centers)):
    print("\nЦентр кластера #" + str(i+1))
    print(csv_data.iloc[centers[i]])
    print(csv_data.iloc[result[centers[i]]])
input("Press any key...")