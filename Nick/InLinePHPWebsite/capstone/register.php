<!DOCTYPE HTML>
<?php
require('library/library.php');
if (isset($_SESSION['user'])){
$USER = $_SESSION['user'];
}

output_header();


$email = false;
$uname = false;
$pass = false;
$pass2 = false;

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
	
	if (!filter_var($email, FILTER_VALIDATE_EMAIL)) {
        echo "<center> check email format </center>";
        $error=true;
    }
    
    if ($testEmail[0]) {
        echo "<center> this email is already being used </center>";
        $error=true;
    }
    
    if ($testUser[0]) {
        echo "<center> this username is already being used </center>";
           $error=true;
    }
    
    if (!preg_match('/^[\w]{1,16}$/', $uname)) {
        echo "<center>" . "username can only have letters and numbers, and 1 - 16 characters" . "</center>";
           $error=true;
    }
    
    if (!preg_match('/^[\w]{8,16}$/', $pass)) {//limits to a-z, A-Z, 0-9, 8 - 16 characters
        echo "<center>" . "password can only have letters and numbers, and 8 - 16 characters" . "</center>";
           $error=true;
    }
    
    if ($pass != $pass2){
	    echo "<center>" . "passwords did not match" . "</center>";
           $error=true;
    } 
    
    if (!$error) {
        $sql = "INSERT INTO users (email, password, username) VALUES ('$email', '$pass', '$uname');";
        $result = $conn->query($sql);
     
        header("Location:login.php");
        $success = "<center>" . "User Created!" . "</center>";
        echo $success;
    }
}

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
                        <input type="text" placeholder="Email" name="email" required>
                    
                        <input type="text" placeholder="Username" name="username" required>
                    
                        <input type="password" placeholder="Password" name="password" required>
                        
                        <input type="password" placeholder="Confirm Password" name="passConf" required>
                    </div>
                
                    <div class="container" style="background-color:#1f1f1f">
                        <button type="submit" name="submission">Register</button>
                        <button type="submit" name="cancel" class="cancel" formnovalidate>Cancel</button>
                    </div>
                </form>
            </div>
        </center>
    </body>
</html>