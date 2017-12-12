# Written by Jonathan Pierik
# CSI4999
# Calculates wait times for InLine

import os
import pypyodbc
import time
#Trusted_Connection=yes
#SERVER=AFDANAJ\SQLEXPRESS
#SERVER=LAPTOP-BFLTIAPM
# CONNECTION TO THE DATABASE
def establishConnection():
    connection = pypyodbc.connect(
    'DRIVER={ODBC Driver 13 for SQL Server};SERVER=AFDANAJ\SQLEXPRESS;DATABASE=CSI4999;UID=csi4999;PWD=Temp1234;')
    return connection

#cursor = connection.cursor()


# SAVES DATA INSERTED INTO THE DATABASE
def save(connection):
    connection.commit()

# CLOSES CONNECTION TO THE DATABASE
def closeConnection(connection):
    connection.close()

# GETS THE TOTAL NUMBER OF RESTAURANTS AND STORE THERE ID IN A ARRAY
def getAmountOfRest(cursor):
    high = 0
    cursor.execute("SELECT RestaurantId FROM Restaurant")
    while True:
        row = cursor.fetchone()
        if not row:
            break
        else:
            #print(row)
            if(row[0] > high):
                high = row[0]
    count = high
    rTotal = [] * count
    sTotal = [] * count
    for i in range(0, count + 1):
        sTotal.append(0)
    rTotal.append(0)
    cursor.execute("SELECT RestaurantId FROM Restaurant")
    for row in cursor.fetchall():
        restID = row[0]
        rTotal.append(restID)
    cursor.execute("SELECT * FROM Seatings")
    for row in cursor.fetchall():
        seatCount = row[2]
        rID = row[1]
        #print(id , seatCount)
        sTotal[rID] = sTotal[rID] + seatCount
    print(sTotal)
    return sTotal



def getFreeSeats(sAvailable, cursor):
    sqlStatment = "select Seatings.RestaurantID, Seatings.maxOccupancy from Seatings inner join CurrentStatus on Seatings.TableNumber = CurrentStatus.TableNumber and Seatings.RestaurantID = CurrentStatus.RestaurantId;"
    cursor.execute(sqlStatment)
    for row in cursor.fetchall():
        restID = row[0]
        seatsTaken = row[1]
        sAvailable[restID] = sAvailable[restID] - seatsTaken
    #print(sAvailable)
    return sAvailable


def getPercentage(available, totalSeats):
    pa = [] * len(totalSeats)
    percentage = 1.0
    print(totalSeats)
    for i in range(0, len(totalSeats)):
            if totalSeats[i] != 0:
                #print(available[i], totalSeats[i])
                percentage =(available[i]*1.0) / totalSeats[i]
                percentage = percentage * 100.0
                pa.append(percentage)
            else:
                pa.append(0.0)
    return pa

#get total inliners with high priority and low priority
def getInlinersWeight(sTotal, cursor):
    highPriority = [] * len(sTotal)
    lowPriority = [] * len(sTotal)
    percentOfAffect = [] * len(sTotal)
    for i in range(0, len(sTotal)):
        highPriority.append(0)
        lowPriority.append(0)
        percentOfAffect.append(0.0)
    cursor.execute("SELECT RestaurantID, NoOfGuests, PriorityLvl FROM WaitingParty")
    for row in cursor.fetchall():
        if(row[2] == 1):
            highPriority[row[0]] = highPriority[row[0]] + row[1]
        else:
            lowPriority[row[0]] = lowPriority[row[0]] + row[1]
    for i in range(0, len(sTotal)):
        percentOfAffect[i] = (lowPriority[i] * 0.75) + (highPriority[i] * 1.0)

    #print(highPriority)
    #print(lowPriority)
    #print(percentOfAffect)
    return percentOfAffect


#print(getAmountOfRest())
#getFreeSeats(seats)
# need to assign fixed wait time to restaurants
def assignWaitTime(available, totalSeats, weight, cursor):
    waitTime = 0
    restaurantId = 0
    adjustedWeight = [] * len(available)
    availableWithWeight = [] * len(available)
    sqlStatement = "UPDATE Restaurant SET currentWait = ? WHERE RestaurantId = ?"
    for i in range(0, len(available)):
        adjustedWeight.append(0.0)
        availableWithWeight.append(0.0)
    for i in range(1, len(weight)):
        if(totalSeats[i] != 0):
            availableWithWeight[i] = ((available[i] - weight[i]) / totalSeats[i]) * 100.0
        else:
            availableWithWeight[i] = 0.0
        adjustedWeight[i] = available[i] - weight[i]
    print"Weight: ", weight
    print"Available: ", available
    print"Adjusted Weight: ", availableWithWeight
    for i in range(1, len(available)):
        restaurantId = i
        #print(totalSeats)
        if((totalSeats[i] > 0) & (weight[i] >= 20.0)):
            if(availableWithWeight[i] >= 70.0):
                waitTime = 0
                cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" %(waitTime, restaurantId))
            elif(availableWithWeight[i] >= 65.0):
                waitTime = 5
                cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" % (waitTime, restaurantId))
            elif(availableWithWeight[i] >= 55.0):
                waitTime = 10
                cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" % (waitTime, restaurantId))
            elif (availableWithWeight[i] >= 45.0):
                waitTime = 15
                cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" % (waitTime, restaurantId))
            elif (availableWithWeight[i] >= 35.0):
                waitTime = 20
                cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" % (waitTime, restaurantId))
            elif (availableWithWeight[i] >= 30.0):
                waitTime = 25
                cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" % (waitTime, restaurantId))
            elif (availableWithWeight[i] >= 25.0):
                waitTime = 30
                cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" % (waitTime, restaurantId))
            elif (availableWithWeight[i] >= 20.0):
                waitTime = 35
                cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" % (waitTime, restaurantId))
            elif (availableWithWeight[i] >= 15.0):
                waitTime = 40
                cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" % (waitTime, restaurantId))
            elif (availableWithWeight[i] >= 10.0):
                waitTime = 45
                cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" % (waitTime, restaurantId))
            elif (availableWithWeight[i] >= 5.0):
                waitTime = 50
                cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" % (waitTime, restaurantId))
            elif (availableWithWeight[i] >= 0.0):
                waitTime = 55
                cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" % (waitTime, restaurantId))
        elif (availableWithWeight[i] < 0):
            waitTime = 60
            cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" % (waitTime, restaurantId))
        elif ((totalSeats[i] > 0) & (weight[i] <= 20.0)):
            if (availableWithWeight[i] >= 70.0):
                waitTime = 0
                cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" %(waitTime, restaurantId))
            elif(availableWithWeight[i] >= 40.0):
                waitTime = 5
                cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" % (waitTime, restaurantId))
            elif(availableWithWeight[i] >= 20.0):
                waitTime = 10
                cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" % (waitTime, restaurantId))
            elif (availableWithWeight[i] > 0.0):
                waitTime = 15
                cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" % (waitTime, restaurantId))
            else:
                waitTime = 20
                cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" % (waitTime, restaurantId))
        #else:
            #waitTime = 0
            #cursor.execute("UPDATE Restaurant SET currentWait = %s WHERE RestaurantId = %s" % (waitTime, restaurantId))
        
        


#Main Loop
loopCount = 0
cycleCount = 0
clearCount = 0
while(True):
    time1 = time.clock()
    connection = establishConnection()
    cursor = connection.cursor()
    seats = getAmountOfRest(cursor)
    test = getFreeSeats(seats, cursor)
    percentSeatsAvailable = getPercentage(test, getAmountOfRest(cursor))
    inlinersWeight = getInlinersWeight(seats, cursor)
    assignWaitTime(getFreeSeats(seats, cursor), getAmountOfRest(cursor), inlinersWeight, cursor)
    #percentSeatsAvailable
    loopCount = loopCount + 1
    if (loopCount == 1000000):
        loopCount = 0
        cycleCount = cycleCount + 1
    print"Loop Count: ", loopCount
    print"Cycle Count: ", cycleCount
    save(connection)
    closeConnection(connection)
    totalTime = time.clock() - time1
    print"Runtime: ", totalTime
    print""
    time.sleep(5)
    if(clearCount == 5):
        clearCount = 0
        os.system('CLS')
    else:
        clearCount = clearCount + 1


