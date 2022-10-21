<?php
// https://www.cloudways.com/blog/connect-mysql-with-php/
function OpenCon()
 {
    $servername = 'localhost'; //localhost
    $username= 'root';  //root 
    $password='';
    $dbname= 'opendata';

    $conn = new mysqli($servername, $username, $password, $dbname) or die("Connect failed: %s\n". $conn -> error);
 
    return $conn;
 }
 
function CloseCon($conn)
 {
    $conn -> close();
 }
   
?>
