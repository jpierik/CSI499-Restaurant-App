<?php

require('library/library.php');
//$tableID = $_REQUEST["tid"];
$restID = $_REQUEST["rid"];
$tableID = ltrim(substr($_REQUEST["tid"], -2));

$sql = "SELECT StatusId, NoOfOccupants, Right(IsNull(CONVERT(VARCHAR(20), SatDate,100),''),7) FROM CurrentStatus WHERE TableNumber = '$tableID' AND RestaurantId = '$restID';";
$res = odbc_exec($conn, $sql);
$statusid = odbc_result($res, 1);
$occ = odbc_result($res, 2);
$satdate = odbc_result($res, 3);

$myArr = array($statusid, $occ, $satdate);
$myJSON = json_encode($myArr);
echo $myJSON;



?>