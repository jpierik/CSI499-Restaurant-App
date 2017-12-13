<?php

require('library/library.php');
$restID = $_REQUEST["rid"];

//sleep(3);

$maxTime = 600;
$rload = 1;
$sql = "SELECT Count(*) FROM WaitingParty WHERE RestaurantId = '$restID';";
$res = odbc_exec($conn, $sql);
$oldcount = odbc_result($res, 1);
/*
sleep(3);
for ($z = 0; $z <= $maxTime; $z++){
	sleep(3);
$sql = "SELECT Count(*) FROM WaitingParty WHERE RestaurantId = '$restID';";
$res = odbc_exec($conn, $sql);
$rcount = odbc_result($res, 1);
if ($rcount != $oldcount){
	$rload = 1;
	break;
}
	
}
*/
if ($rload == 1){
echo "$oldcount";
} else{
	echo "$oldcount";
}

?>