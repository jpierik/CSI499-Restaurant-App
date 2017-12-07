<?php

require('library/library.php');
$statusID = $_REQUEST["sid"];

$sql = "SELECT TableNumber, RestaurantId, SatDate FROM CurrentStatus WHERE StatusId = '$statusID';";
$res = odbc_exec($conn, $sql);
$TableNum = odbc_result($res, 1);
$RestID = odbc_result($res, 2);
$SatDate = odbc_result($res, 3);

$sql = "INSERT INTO StatusHistory (TableNumber, RestaurantId, SatDate, ClearDate) VALUES ('$TableNum', '$RestID', '$SatDate', CURRENT_TIMESTAMP);";
odbc_exec($conn, $sql);

$sql = "DELETE FROM CurrentStatus WHERE StatusId = '$statusID';";
odbc_exec($conn, $sql);

echo "Success";


?>