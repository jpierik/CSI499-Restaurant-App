<?php


session_start();
$serverName = "AFDANAJ\SQLEXPRESS";
$connectionInfo = array("Database"=>"CSI4999", "UID"=>"csi4999", "PWD"=>"Temp1234");

$username = "csi4999";
$password = "Temp1234";
$dbname = "CSI4999";
$database = "CSI4999";

// Create connection
//$conn2 = sqlsrv_connect($serverName, $connectionInfo);
$conn = odbc_connect("Driver={SQL Server Native Client 11.0};Server=$serverName;Database=$database;", $username, $password);
// Check connection
//if (!$conn){
  //   echo "Connection could not be established.<br />";
    // die( print_r( sqlsrv_errors(), true));
//}

$USER = false;

update_info();


function output_header() {
    global $USER;
    include('header.php');
}

function output_footer() {
    include('footer.php');
}
function update_info() {
    global $USER;
//global $uname;
    // Check that there is a logged in user, and if so, setup the USER object
}

?>