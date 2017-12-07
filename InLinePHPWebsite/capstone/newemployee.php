<!DOCTYPE HTML>
<?php
require('library/library.php');
if (isset($_SESSION['user'])){
$USER = $_SESSION['user'];
}

if ($USER) {
$sql = "SELECT userid FROM users WHERE username = '$USER';";
$result = odbc_exec($conn, $sql);
$id = odbc_result($result, 1);
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
	if (isset($_REQUEST['restaurant']) && $_REQUEST['restaurant']) {
		$restaurant = strip_tags($_REQUEST['restaurant']);
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
	
	$sql = "SELECT email FROM users WHERE email='$email'";
	$result = odbc_exec($conn, $sql);
	$testEmail = odbc_result($result, 1);
	// $testEmail = $result->fetch_array(MYSQLI_NUM);
	
	$sql = "SELECT username FROM users WHERE username='$uname'";
	$result = odbc_exec($conn, $sql);
	$testUser = odbc_result($result, 1);
	// $testUser = $result->fetch_array(MYSQLI_NUM);
	
	if (!filter_var($email, FILTER_VALIDATE_EMAIL)) {
        echo "<center> check email format </center>";
        $error=true;
    }
    
    if ($testEmail) {
        echo "<center> this email is already being used </center>";
        $error=true;
    }
    
    if ($testUser) {
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
        $sql = "INSERT INTO users (email, pwd, username, restaurantid, alevel) VALUES ('$email', '$pass', '$uname', '$restaurant', '1');";
        odbc_exec($conn, $sql);
     
        header("Location:index.php");
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
                <form class="form1">
                    <div class="container">
						<label><b>Employee Email</b></label>
                        <input class="oldinput" type="text" placeholder="Email" name="email" required>
						<label><b>Employee Username</b></label>
                        <input class="oldinput" type="text" placeholder="Username" name="username" required>
						<label><b>Employee Restaurant</b></label>
						<select class="oldinput" name="restaurant" required>
						
						<?php
						$sql = "SELECT Name, RestaurantId FROM Restaurant WHERE OwnerId = '$id';";
						$result = odbc_exec($conn, $sql); 
									while (odbc_fetch_row($result)){
										$rname = odbc_result($result, 1);
										$rid = odbc_result($result, 2);
										echo '<option value="'.$rid.'">'.$rname.'</option>';
									}
							?>
						</select>
						<label><b>Employee Password</b></label>
                        <input class="oldinput" type="password" placeholder="Password" name="password" required>
                        
                        <input class="oldinput" type="password" placeholder="Confirm Password" name="passConf" required>
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