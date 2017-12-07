<?php

require('library/library.php');
$partyID = $_REQUEST["pid"];
$eleID = $_REQUEST["bid"];


$sql = "SELECT RestaurantID, NoOfGuests FROM WaitingParty WHERE PartyId = '$partyID';";
$res = odbc_exec($conn, $sql);
$restid = odbc_result($res, 1);
$partySize = odbc_result($res, 2);

$tableID = ltrim(substr($eleID, -2));
$sql = "INSERT INTO CurrentStatus (TableNumber, RestaurantId, SatDate, NoOfOccupants) VALUES ('$tableID', '$restid', CURRENT_TIMESTAMP, '$partySize');";
odbc_exec($conn, $sql);

$sql = "DELETE FROM WaitingParty WHERE PartyId = '$partyID';";
odbc_exec($conn, $sql); 

echo "Success";



?>