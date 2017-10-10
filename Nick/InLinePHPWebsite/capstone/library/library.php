<?php


session_start();
$servername = "127.0.0.1";
$username = "nickj99";
$password = "capstone";
$dbname = "capstone";
// Create connection
$conn = mysqli_connect($servername, $username, $password, $dbname);
// Check connection
if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());

}



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