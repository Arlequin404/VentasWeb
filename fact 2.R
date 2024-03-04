#EJEMPLO DE ANALISIS FACTORIAL

library(psych)
library(corrplot)
library(corrr)

data(mtcars)

View(data)
summary(data)

#Verificar que es viable realizar un Analisis Factorial
mcorre <- cor(data)
View(mcorre)
cov(data)
corrplot(cor(data), order = "hclust", tl.col='black', tl.cex=1)
#La función bartlett.test() realiza la prueba de esfericidad de Bartlett, que evalúa si la matriz de correlación es una matriz de identidad. La función KMO() calcula el índice de adecuación de Kaiser-Meyer-Olkin, que mide la adecuación de los datos para el análisis factorial.
bartlett.test(data)
KMO(data)


##Extraer los factores
## Determinar el numero de factores que se usaran
eigen(mcorre)
## Determinacion de los factores
AnalisisF <- factanal(data, factors = 4, rotation = "none", scores = "Bartlett")
AnalisisF
## Rotacion de los factores
AnalisisF <- factanal(data, factors = 4, rotation = "varimax", scores = "Bartlett")
AnalisisF

