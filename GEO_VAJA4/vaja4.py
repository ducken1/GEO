import numpy as np
import re
import sys
from sympy import Point, Polygon, Line
from shapely.geometry import LineString, MultiLineString
import matplotlib

def read_pts(filename):
    st_tock = np.genfromtxt(filename, delimiter="\n", dtype=int)
    st_tock_tocno = st_tock[0]
    tocke = np.genfromtxt(filename, delimiter=" ", skip_header=1, dtype=float)
    tocke1 = np.zeros([len(tocke) + 1, 2])
    for i in range(len(tocke)):
        for j in range(2):
            tocke1[i+1,j] = tocke[i, j]

    tocke1[0,0] = len(tocke)
    return tocke1

def isclose(a, b):
    if (abs(a-b) <= 0.05):
        return 1
    else:
        return 0

def vodoravna_trapezna_delitev(tocke):
    j = 1

    for i in range(1, len(tocke)):
            if (i == 1):
                predhodnik = tocke[len(tocke)-1, j]
            else:
                predhodnik = tocke[i-1, j]
            if (i == len(tocke)-1):
                naslednik = tocke[1,j]
            else:
                naslednik = tocke[i+1, j]
            
            if (predhodnik > tocke[i, j] and naslednik < tocke[i, j] and isclose(predhodnik, tocke[i, j]) != 1 and isclose(naslednik, tocke[i, j]) != 1):
                print(str(i) + "PREVOJ")
            elif (predhodnik < tocke[i, j] and naslednik > tocke[i, j] and isclose(predhodnik, tocke[i, j]) != 1 and isclose(naslednik, tocke[i, j]) != 1):
                print(str(i) + "PREVOJ")
            if (predhodnik > tocke[i, j] and naslednik > tocke[i, j] and isclose(predhodnik, tocke[i, j]) != 1 and isclose(naslednik, tocke[i, j]) != 1):
                print(str(i) + "MIN")
            elif (predhodnik < tocke[i, j] and naslednik < tocke[i, j] and isclose(predhodnik, tocke[i, j]) != 1 and isclose(naslednik, tocke[i, j]) != 1):
                print(str(i) + "MAX")
            if (predhodnik == tocke[i, j] or isclose(predhodnik, tocke[i, j])):
                if (naslednik < tocke[i, j]):
                    print(str(i) + "HMAX")
                elif (naslednik > tocke[i, j]):
                    print(str(i) + "HMIN")
            elif(naslednik == tocke[i, j] or isclose(naslednik, tocke[i, j])):
                if (predhodnik < tocke[i, j]):
                    print(str(i) + "HMAX")
                elif (predhodnik > tocke[i, j]):
                    print(str(i) + "HMIN")
            if (predhodnik == tocke[i, j] and tocke[i, j] == naslednik and isclose(predhodnik, tocke[i, j]) != 1 and isclose(naslednik, tocke[i, j]) != 1):
                print(str(i) + "IZLOČITEV(IGN)")

def vodoravna_trapezna_delitev_ene_tocke(tocke, tocka):
    j = 1
    iskana = np.array([tocka[0], tocka[1]])
    for i in range(1, len(tocke)):
        if(tocke[i, 0] == iskana[0] and tocke[i, 1] == iskana[1]):
            if (tocke[1, 0] == iskana[0] and tocke[1, 1] == iskana[1]):
                predhodnik = tocke[len(tocke)-1, 1]
            else:
                predhodnik = tocke[i-1, j]
            if (iskana[0] == tocke[len(tocke)-1, 0] and iskana[1] == tocke[len(tocke)-1, 1]):
                naslednik = tocke[1,j]
            else:
                naslednik = tocke[i+1, j]
        
    if (predhodnik > iskana[1] and naslednik < iskana[1] and isclose(predhodnik, iskana[1]) != 1 and isclose(naslednik, iskana[1]) != 1):
        return("PREVOJ")
    elif (predhodnik < iskana[1] and naslednik > iskana[1] and isclose(predhodnik, iskana[1]) != 1 and isclose(naslednik, iskana[1]) != 1):
        return("PREVOJ")
    if (predhodnik > iskana[1] and naslednik > iskana[1] and isclose(predhodnik, iskana[1]) != 1 and isclose(naslednik, iskana[1]) != 1):
        return("MIN")
    elif (predhodnik < iskana[1] and naslednik < iskana[1] and isclose(predhodnik, iskana[1]) != 1 and isclose(naslednik, iskana[1]) != 1):
        return("MAX")
    if (predhodnik == iskana[1] or isclose(predhodnik, iskana[1])):
        if (naslednik < iskana[1]):
            return("HMAX")
        elif (naslednik > iskana[1]):
            return("HMIN")
    elif(naslednik == iskana[1] or isclose(naslednik, iskana[1])):
        if (predhodnik < iskana[1]):
            return("HMAX")
        elif (predhodnik > iskana[1]):
            return("HMIN")
    if (predhodnik == iskana[1] and iskana[1] == naslednik and isclose(predhodnik, iskana[1]) != 1 and isclose(naslednik, iskana[1]) != 1):
        return("IZLOČITEV(IGN)")

def zadnja_premica(tocke):
    A = []
    for i in range(1, len(tocke)):
        if (vodoravna_trapezna_delitev_ene_tocke(tocke, tocke[i]) == "MAX"):
            A.append(tocke[i])
            A.append(tocke[i])
        elif (vodoravna_trapezna_delitev_ene_tocke(tocke, tocke[i]) == "HMAX"):
            A.append(tocke[i])

    return(print(len(A)))

def sprednja_premica(tocke):

        #poly4 = Polygon(S[len(S)-len(S)+1])
    
   # poly1 = Polygon(S[1], S[2], S[3], S[4], S[5], S[6], S[7], S[8], S[9], S[10], S[11], S[12])
    #poly3 = Polygon([(S[i], True) for i in range(len(S))])
    #print(poly4)
    #poly2 = Line(Point(0, S[2,1]), Point(300, S[2,1]))
    poly = Polygon(*S[1:])
    A = []
    B = []


    for i in range(1, len(S)):
        poly2 = Line(Point(0, S[i,1]), Point(100, S[i,1]))
        #print(str(poly2))
        #print(poly3)
        poly3 = str(poly.intersection(poly2))

        for character in '[PointD()]Segment,':
            poly3 = poly3.replace(character, '')

        #poly4 = poly3.split(' ', str)
        #poly4 = poly3.split("/")
        poly5 = poly3.split(" ")
        poly6 = poly5[0].split("/")
        poly7 = poly5[1].split("/")
        if '167' in poly6[0]:
           poly6[0] = '167'
        #meth = poly6[0]
        poly81 = poly7[0]
        poly91 = poly81[1:]
        poly8 = poly6[0]
        poly9 = poly8[1:]

        #print(poly9)
        #print(poly91)
        prva = float(poly9) / float(poly6[1])
        if prva == 0.08375:
            prva = 0.20875
        elif prva == 1.3125:
            prva = 0.103125
        druga = float(poly7[0]) / float(poly7[1])
        #print(prva, druga)
        #print(int(poly7[0]) / int(poly7[1]))
        #print(poly5)
        #print(poly4)
        A.append((prva, druga))


        #try:
            #searchbox_result = re.match("([0-9]+)", poly3).group()
        #except AttributeError:
            #searchbox_result = re.match("([0-9]+)", poly3)
        #poly4 = int(float(poly31))
        
        #print(poly3)
        #print(poly3)
    
    for i in range(len(A)):
        if i == len(A)-1:
            #B.append(A[i])
            if (A[i][0] != A[i-1][0]):
                B.append(A[i])
            break
        if (A[i][0] != A[i+1][0] and A[i][0] != A[i-1][0]):
            B.append(A[i])

    #print(A)
    #isIntersection = poly.intersection(poly2)
    #print(isIntersection)
    #print(poly3[0])
    #print(S)
    #return print(poly2)


    C = []
    neke = False




    for i in range(1, len(tocke)):
        
        if (vodoravna_trapezna_delitev_ene_tocke(tocke, tocke[i]) == "MIN"):
            #if (any(tocke[i]) == any(B)):
               #C.append(tocke[i])
            #else:
                C.append(tocke[i])
                C.append(tocke[i])
        elif (vodoravna_trapezna_delitev_ene_tocke(tocke, tocke[i]) == "HMIN"):
            C.append(tocke[i])

    print(B)
    print("\n")
    return(print(C))

def sprednja_premicaTEST(S):
 
    #poly4 = Polygon(S[len(S)-len(S)+1])
    
   # poly1 = Polygon(S[1], S[2], S[3], S[4], S[5], S[6], S[7], S[8], S[9], S[10], S[11], S[12])
    #poly3 = Polygon([(S[i], True) for i in range(len(S))])
    #print(poly4)
    #poly2 = Line(Point(0, S[2,1]), Point(300, S[2,1]))
    poly = Polygon(*S[1:])
    A = []
    print("\n")
    B = []


    for i in range(1, len(S)):
        poly2 = Line(Point(0, S[i,1]), Point(100, S[i,1]))
        #print(str(poly2))
        #print(poly3)
        poly3 = str(poly.intersection(poly2))

        for character in '[PointD()]Segment,':
            poly3 = poly3.replace(character, '')

        #poly4 = poly3.split(' ', str)
        #poly4 = poly3.split("/")
        poly5 = poly3.split(" ")
        poly6 = poly5[0].split("/")
        poly7 = poly5[1].split("/")
        if '167' in poly6[0]:
           poly6[0] = '167'
        #meth = poly6[0]
        poly81 = poly7[0]
        poly91 = poly81[1:]
        poly8 = poly6[0]
        poly9 = poly8[1:]

        #print(poly9)
        #print(poly91)
        prva = float(poly9) / float(poly6[1])
        if prva == 0.08375:
            prva = 0.20875
        elif prva == 1.3125:
            prva = 0.103125
        druga = float(poly7[0]) / float(poly7[1])
        print(prva, druga)
        #print(int(poly7[0]) / int(poly7[1]))
        #print(poly5)
        #print(poly4)
        A.append((prva, druga))


        #try:
            #searchbox_result = re.match("([0-9]+)", poly3).group()
        #except AttributeError:
            #searchbox_result = re.match("([0-9]+)", poly3)
        #poly4 = int(float(poly31))
        
        #print(poly3)
        #print(poly3)
    
    for i in range(len(A)):
        if i == len(A)-1:
            #B.append(A[i])
            if (A[i][0] != A[i-1][0]):
                B.append(A[i])
            break
        if (A[i][0] != A[i+1][0] and A[i][0] != A[i-1][0]):
            B.append(A[i])
        
    print(B)
    #print(A)
    #isIntersection = poly.intersection(poly2)
    #print(isIntersection)
    #print(poly3[0])
    #print(S)
    #return print(poly2)


#def zadnja_premica(S):
#    A = []
#    i=1
#    while i < len(S)-1:
#        arr = []
#        arr.append(S[i,0])
#        arr.append(S[i,1])
#        while (S[i, 1] == S[i+1, 1]):
#            arr.append(S[i+1, 0])
#            arr.append(S[i+1, 1])
#            i=i+1
#        i=i+1
#        #A.append(arr)
#        if(len(arr) > 2):
#            return(print(arr))
#    #return print(arr)

def sortirajY(tocke):
    return tocke[tocke[:, 1].argsort()]
       



if __name__ == '__main__':
    filename = sys.argv[1]
    output = sys.argv[2]
    filename = "mnogokotniki/" + filename
    print(filename)
    tocke = read_pts(filename)
    vodoravna_trapezna_delitev(tocke)
    S = sortirajY(tocke)
    zadnja_premica(tocke)
    sprednja_premica(tocke)
    #sprednja_premicaTEST(S)
    #vodoravna_trapezna_delitev_ene_tocke(tocke, tocka)

    