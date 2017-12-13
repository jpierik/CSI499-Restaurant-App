<?php

require('library/library.php');
//$tableID = $_REQUEST["tid"];
$restID = $_REQUEST["rid"];
$dealName = $_REQUEST["dealname"];
$dealDesc = $_REQUEST["dealdesc"];
$dealCat = $_REQUEST["dealcat"];
$dealPri = $_REQUEST["dealpri"];

$sql = "INSERT INTO Deals (RestaurantId, Title, Descript, category, priority) VALUES ('$restID', '$dealName', '$dealDesc', '$dealCat', '$dealPri');";
$res = odbc_exec($conn, $sql);

$sql = "SELECT DealId FROM Deals WHERE RestaurantId = '$restID' AND Descript = '$dealDesc';";
$res = odbc_exec($conn, $sql);
$dealID = odbc_result($res, 1);

echo $dealID;



?>