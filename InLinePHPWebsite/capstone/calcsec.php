<?php

require('library/library.php');
$parttID = $_REQUEST["pid"];

$sql = "SELECT CURRENT_TIMESTAMP, AddTime FROM WaitingParty WHERE PartyId = '$parttID';";
$res = odbc_exec($conn, $sql);
$curtime = odbc_result($res, 1);
$addtime = odbc_result($res, 2);

$curtime = strtotime($curtime);
$addtime = strtotime($addtime);
$diffInSec = $curtime - $addtime;

//if ($diffInSec >= 3600){
//	echo "hour";
//} else {
echo $diffInSec;
//}


?>