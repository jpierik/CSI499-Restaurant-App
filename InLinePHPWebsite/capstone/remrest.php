<?php

require('library/library.php');
$restID = $_REQUEST["rid"];


$sql = "DELETE FROM users WHERE RestaurantId = '$restID' AND alevel = '1';";
$res = odbc_exec($conn, $sql);

$sql = "DELETE FROM WaitingParty WHERE RestaurantId = '$restID';";
$res = odbc_exec($conn, $sql);

$sql = "DELETE FROM StatusHistory WHERE RestaurantId = '$restID';";
$res = odbc_exec($conn, $sql);

$sql = "DELETE FROM Deals WHERE RestaurantId = '$restID';";
$res = odbc_exec($conn, $sql);

$sql = "DELETE FROM CurrentStatus WHERE RestaurantId = '$restID';";
$res = odbc_exec($conn, $sql);

$sql = "DELETE FROM Seatings WHERE RestaurantId = '$restID';";
$res = odbc_exec($conn, $sql);

$sql = "DELETE FROM Restaurant WHERE RestaurantId = '$restID';";
$res = odbc_exec($conn, $sql); 

echo "Restaurant and all associated employees successfully deleted";



?>