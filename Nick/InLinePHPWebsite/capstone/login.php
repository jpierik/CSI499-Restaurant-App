<!DOCTYPE HTML>
<?php



require('library/library.php');
output_header();

if (isset($_REQUEST['logout'])) {
    session_unset();
    session_destroy();

    $url = 'index.php';

    header( 'Location: '.$url, true, '307');
}

//if (isset($_SESSION['user'])){
//$USER = $_SESSION['user'];

//$sql = "SELECT FName FROM users WHERE username = '$USER';";
//$result = $conn->query($sql);
       //  $row = $result->fetch_assoc();
// echo $row["FName"];
// $uname =  $row["FName"];

//}

if (isset($_REQUEST['cancel'])) {
    header("Location:index.php");
}

// echo "hi";
// Setup a login form, and check the username and password.
if (isset($_REQUEST['submission'])) {
// $password = $_POST['password'];
$usercorrect = false;
$passcorrect = false;
$passwd = '';
$username = '';
//	$sql_params = array();
	// For each input check if it set, if so set the data object and the params object.
	if (isset($_REQUEST['username']) && $_REQUEST['username']) {
		$username = strip_tags($_REQUEST['username']);
		 $sql = "SELECT * FROM users WHERE username = '$username';";
		 $result = odbc_exec($conn, $sql);
		 $res = odbc_result($result, 1);
       //  $result = $conn->query($sql);
         if(! $res )
{
  die('Could not get data: ');
}
else { 
// echo "hij";
// echo $username;
}
//$orca = $result->num_rows;
//echo $orca;
if (odbc_num_rows($result) > 0) {
		//	echo "username correct";
			$usercorrect = true;
		}
	} else {
		echo "Username Incorrect";
	//	frgrg();
	}
	
	// For each input check if it set, if so set the data object and the params object.
	if (isset($_REQUEST['city']) && $_REQUEST['city']) {
		$passwd = strip_tags($_REQUEST['city']);
		 $sql = "SELECT * FROM users WHERE pwd = '$passwd' AND username = '$username';";
		 $result = odbc_exec($conn, $sql);
       //  $result = $conn->query($sql);
	   $res = odbc_result($result, 1);
         if(! $res )
{
  die('Could not get data: ' . odbc_error());
}
// else { 
//echo "fff";
//}
if (odbc_num_rows($result) > 0) {
			echo "pass correct";
			$passcorrect = true;
		}
	} else {
		echo "Password Incorrect";
	}
	if ($passcorrect && $usercorrect){ echo "Login Success"; 

$_SESSION['user'] = $_REQUEST['username'];
$_SESSION['currentRest'] = false; 
    header("Location:index.php");
   // didjd();
    }
    
    else {echo "<center>Username or password incorrect</center>";}
	
// didjd();
	}
   

update_info();


// output_header('home');
?>

<html>
    <head>
        <link rel="stylesheet" type="text/css" href="library/stylesheets/headerPagesStyle.css">
    </head>
    <body>
        <div class="topSpacer"></div>
        <center>
            <form class="form1">
                <div class="container">
                    <input class="oldinput" type="text" placeholder="Username" name="username" required>
                
                    <input class="oldinput" type="password" placeholder="Password" name="city" required>
                
                </div>
            
                <div class="container" style="background-color:#1f1f1f">
                    <button type="submit" name="submission">Login</button>
                    <button type="submit" name="cancel" class="cancel" formnovalidate>Cancel</button>
                </div>
            </form>
        </center>
    </body>
</html>