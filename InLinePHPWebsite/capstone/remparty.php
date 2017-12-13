<?php

require('library/library.php');
$parttID = $_REQUEST["pid"];

$sql = "DELETE FROM WaitingParty WHERE PartyId = '$parttID';";
odbc_exec($conn, $sql);	


echo "success";



?>