a = [1,2,3,4,5]
#print(a[0])

A =  [  [1,3,5], 
        [2,4,6],  
        [5,4,3]   ]
#print(A[1][1])

for j in range(len(A)):
    for i in range(len(A[j])):
        if i == j: print(A[i][j])
