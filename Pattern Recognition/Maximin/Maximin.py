import math
import numpy as np
import pandas as pd
import random as rd
def euclidian(x, y): #Нахождение расстояния через евклидову метрику
    return math.sqrt(np.sum([(a - b) ** 2 for a, b in zip(x, y)]))

def AverageDistance(csv_data, centers): #Среднее расстояние между центрами кластеров
    distance = 0
    for i in range(0, len(centers)):
        for j in range(0, len(centers)):
            if (j > i):
                distance += euclidian(np.array(csv_data.iloc[centers[i], 0:4]), np.array(csv_data.iloc[centers[j], 0:4]))
    return (distance / len(centers))

def Clusterization(csv_data, patterns, centers): # Проходим по образам, не являющимися центрами и относим их к соответствующему кластеру (с ближайшим центром). Возвращаем словарь вида "Центр кластера":"Лист образов кластера"
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

csv_data = pd.read_csv('iris.csv')
patterns = [] #Множество всех образов
for i in range(len(csv_data.index)):
    patterns.append(i)
new_center = rd.randint(0, len(csv_data.index) - 1) #Центр нового кластера
centers = []
centers.append(new_center)
patterns.remove(new_center)
continue_flag = True

while continue_flag:
    sum = 0
    number = 0
    maxDistance = -1
    for i in range(0, len(patterns)):
        minDistance = -1
        csv_data_curPattern_parameters = np.array(csv_data.iloc[patterns[i], 0:4])
        for j in range(0, len(centers)):
            csv_data_curCenter_parameters = np.array(csv_data.iloc[centers[j], 0:4])
            curDistance = euclidian(csv_data_curPattern_parameters, csv_data_curCenter_parameters)
            if minDistance == -1 or curDistance < minDistance:
                 minDistance = curDistance
        sum += minDistance
        number += 1
        if maxDistance == -1 or minDistance > maxDistance:
            maxDistance = minDistance
            new_center = patterns[i]
    if len(centers) == 1 or maxDistance > AverageDistance(csv_data, centers):
        centers.append(new_center)
        patterns.remove(new_center)
    else:
        continue_flag = False   
    
result = Clusterization(csv_data, patterns, centers)
pd.set_option('display.max_rows', 150)
for i in range(0, len(centers)):
    print("\nЦентр кластера #" + str(i+1))
    print(csv_data.iloc[centers[i]])
    print(csv_data.iloc[result[centers[i]]])
input("Press any key...")