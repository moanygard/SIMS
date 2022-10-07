import mysql.connector
from mysql.connector import Error

try:
    connection = mysql.connector.connect(host='studentmysql.miun.se',
                                        database='mony1800',
                                        user='mony1800', 
                                        password='9a5uhyen')

    if connection.is_connected():
        db_Info = connection.get_server_info()
        print("Conencted to MySQL Server Vversion ", db_Info)

        cursor = connection.cursor()
        cursor.execute("Select databse();")
        record = cursor.fetchone()
        print("You're connected to database: ", record)

except Error as e:
    print("Error while connecting to MySQL", e)
finally:
    if connection.is_connected():
        cursor.cclose()
        connection.close()
        print("MySLQ connection is closed")