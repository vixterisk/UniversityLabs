#%%
import math
import numpy as np
import pandas as pd
#%%
csv_test = pd.read_csv("fruits-and-vegetables-test.csv")
csv_train = pd.read_csv("fruits-and-vegetables-train.csv")
#%%
#Проходим по всем объектам тестовой выборки
for j in range(0, len(csv_test.index)):
    #Изначальное значение расстояния
    overall_min = -1
    #Проходим по объектам обучающей выборки
    for i in range(0, len(csv_train.index)):
        #берем текущий объект тестовой выборки, преобразуем значения свойств в строковый тип, 
        #заменяем в дробных числах символ запятой на символ точки
        csv_test_row_tmp = [sub.replace(',', '.') for sub in np.array(csv_test.iloc[j, 1:10]).astype(str)]
        #преобразуем значения свойств в вещественный тип
        csv_test_row = np.array(csv_test_row_tmp).astype(float)
        
        #берем текущий объект обучающей выборки, преобразуем значения свойств в строковый тип, 
        #заменяем в дробных числах символ запятой на символ точки
        csv_train_row_tmp = [sub.replace(',', '.') for sub in np.array(csv_train.iloc[i, 1:10]).astype(str)]
        #преобразуем значения свойств в вещественный тип
        csv_train_row = np.array(csv_train_row_tmp).astype(float)

        #Находим расстояние между объектом обучающей и тестовой выборок по евклидовой метрике 
        #(корень из суммы квадратов разности значений соответствующих свойств)
        cur_min = math.sqrt(sum([(a - b) ** 2 for a, b in zip(csv_test_row, csv_train_row)]))
        #Если текущая метрика меньше минимальной найденной на текущий момент метрики, или мин. найденная метрика отрицательная
        if overall_min == -1 or cur_min < overall_min:
        #Записываем новое значение минимальной найденной метрики и запоминаем категорию ("Фрукт"/"овощ")
            overall_min = cur_min
            overall_min_category = csv_train.iloc[i, 10]
            nearest_neighbour = csv_train.iloc[i, 0]
    print(csv_test.iloc[j, 0] + " is a " + overall_min_category + ", Euclidean distance: " + str(overall_min) + ", Nearest Neighbour: " + nearest_neighbour)
input("Press any key...")