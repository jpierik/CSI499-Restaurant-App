<!DOCTYPE HTML>
<?php
require('library/library.php');

if (isset($_SESSION['user'])){
$USER = $_SESSION['user'];
}
output_header();
if ($USER) {
$sql = "SELECT id FROM users WHERE username = '$USER';";
$result = $conn->query($sql);
$row = $result->fetch_assoc();
$id = $row['id'];
}


$email = false;
$uname = false;
$pass = false;

if (isset($_REQUEST['cancel'])) {
    header("Location:index.php");
}

if (isset($_REQUEST['submission'])) {

	if (isset($_REQUEST['email']) && $_REQUEST['email']) {
		$email = strip_tags($_REQUEST['email']);
	}
	
	if (isset($_REQUEST['username']) && $_REQUEST['username']) {
		$uname = strip_tags($_REQUEST['username']);
	}

	if (isset($_REQUEST['password']) && $_REQUEST['password']) {
		$pass = strip_tags($_REQUEST['password']);
	}
	
	if (isset($_REQUEST['passConf']) && $_REQUEST['passConf']) {
		$pass2 = strip_tags($_REQUEST['passConf']);
	}  

    $result = $conn->query("SELECT email FROM users WHERE email='$email'");
	$testEmail = $result->fetch_array(MYSQLI_NUM);
	
	$result = $conn->query("SELECT username FROM users WHERE username='$uname'");
	$testUser = $result->fetch_array(MYSQLI_NUM);

    if ($email){
        if (!filter_var($email, FILTER_VALIDATE_EMAIL)) {
            echo "<center> check email format </center>";
        } else if ($testEmail[0]) {
            echo "<center> this email is already being used </center>";
        } else {
            $sql = "UPDATE users SET email = '$email' WHERE id = '$id';";
            $result = $conn->query($sql);
        }
    }
    
    if ($uname){
        if ($testUser[0]) {
            echo "<center> this username is already being used </center>";
        } else if (!preg_match('/^[\w]{1,16}$/', $uname)) {
            echo "<center>" . "username can only have letters and numbers, and 1 - 16 characters" . "</center>";
        } else {
            $sql = "UPDATE users SET username = '$uname' WHERE id = '$id';";
            $result = $conn->query($sql);
            $_SESSION['user'] = $uname;
        }
    // $USER = $uname;
    }

    if ($pass && $pass2){
        if (!preg_match('/^[\w]{8,16}$/', $pass)) {//limits to a-z, A-Z, 0-9, 8 - 16 characters
            echo "<center>" . "password can only have letters and numbers, and 8 - 16 characters" . "</center>";
        } else if ($pass != $pass2){
    	    echo "<center>" . "passwords did not match" . "</center>";
        } else {
            $sql = "UPDATE users SET password = '$pass' WHERE id = '$id';";
            $result = $conn->query($sql);
        }
     
    }
        
}


// $_SESSION['user'] = $_REQUEST['username'];
 //   header("Location:settings.php");
   // didjd();
   

update_info();

?>

<html>
    <head>
        <link rel="stylesheet" type="text/css" href="library/stylesheets/headerPagesStyle.css">
    </head>
    <body>
        <center>
            <div class="topSpacer"></div>
            <form>
                <div class="container">
                    <label><b>Change Email</b></label>
                    <input type="text" placeholder="New Email" name="email" >
                
                    <label><b>Change Username</b></label>
                    <input type="text" placeholder="New Username" name="username">
                
                    <label><b>Change Password</b></label>
                    <input type="password" placeholder="New Password" name="password">
                    
                    <input type="password" placeholder="Confirm Password" name="passConf">
                
                </div>
            
                <div class="container" style="background-color:#1f1f1f">
                    <button type="submit" name="submission">Save Changes</button>
                    <button type="submit" name="cancel" class="cancel">Cancel</button>
                </div>
            </form>
        </center>
    </body>
</html>