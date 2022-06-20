import math
import numpy as np
import pandas as pd
import random as rd

def splitTrainTest (data, testPercent): #Разделить данные на обучающую и тестовую выборки
    df = data.sample(frac=1)#.reset_index(drop=True)
    number = int(testPercent * len(df))
    trainData = df.head(number)
    testData = df.tail(len(df) - number)
    return trainData, testData

def euclidian(x, y): #Нахождение расстояния через евклидову метрику
    return math.sqrt(np.sum([(a - b) ** 2 for a, b in zip(x, y)]))

def GetAlllDistancesAndClassesForTestData(testData, trainData):
    result = {k: [] for k in [*range(len(testData.index))]}
    for i in range(len(testData.index)):
        for j in range(len(trainData.index)):
            curTestPattern_parameters = np.array(testData.iloc[i, 0:4])
            curTrainPattern_parameters = np.array(trainData.iloc[j, 0:4])
            curDistance = euclidian(curTestPattern_parameters, curTrainPattern_parameters)
            newTuple = (curDistance, trainData.iloc[j, 4])
            result[i].append(newTuple)
        result[i].sort(key=lambda tup: tup[0])
    return result
k = 7
data = pd.read_csv('iris.csv')
trainData, testData = splitTrainTest (data, 0.9)
result = GetAlllDistancesAndClassesForTestData(testData, trainData)
for i in range(len(result)):
    print(testData.iloc[i])
    classList = []
    for j in range(k):
        classList.append(result[i][j][1])
    res = max(set(classList), key = classList.count)
    print("KNN result: " + res)
    print()
input("Press any key...")