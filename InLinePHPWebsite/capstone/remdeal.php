<?php

require('library/library.php');
$restID = $_REQUEST["rid"];


$sql = "DELETE FROM Deals WHERE DealId = '$restID';";
$res = odbc_exec($conn, $sql);

echo "Restaurant and all associated employees successfully deleted";



?>