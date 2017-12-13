<?php
require('library/library.php');
if (isset($_SESSION['user'])){
$USER = $_SESSION['user'];
}
output_header();
// echo $USER;
// echo "kkhi";
$id= false;
if ($USER) {
$sql = "SELECT userid FROM users WHERE username = '$USER';";
// old $result = $conn->query($sql);
$result = odbc_exec($conn, $sql);
$id = odbc_result($result, 1);
// $row = $result->fetch_assoc();
// $id = $row['userid'];

// debug echo $id;
}
//$qry = "SELECT userid FROM users WHERE username = 'test2'";
// $res = odbc_exec($conn, $qry);
//$tname = odbc_result($res, "userid");
//if ($tname){
//echo "hi";
//}
//php_info();

?>

<html>
    <body>
        <div class="bg">
        <!-- BANNER -->
        
        <section class="banner">

                    <h1>Welcome to InLine!</h1>
            
            <p><b>InLine</b> is an application to manage tables at a restaraunt.</p>
        </section>
        
        <!-- CONTENT -->
         </div>
        <main class="content">
            <div class="mainStuff">
				<h1>What We Provide For You:</h1>
				
				
				<div class="boxing">
				<div class="fa fa-clock-o fa-4x"style="color:#3293f7;    margin-top: 15px;"></div>
				<h3>Time Management</h3>
				<p>
				The mobile application on the other hand gives customers the ability to see their wait time in real time. This should help customers decide the best time to arrive at the restaurant and in turn limit crowding issues at the entrance of the restaurant.
				</p>
				</div>
				
				<div class="boxing">
				<div class="fa fa-users fa-4x"style="color:#3293f7;    margin-top: 15px;"></div>
				<h3>Employee Accounts</h3>
				<p>
				With employee accounts, we make it easy to manage all of your employees accross all of your restaurants. When you create an employee account, you can assign them a specific restaurant that they have access to. This allows you to organize your staff and keep your restaurants under control.
				</p>				
				</div>
				
				<div class="boxing">
				<div class="fa fa-money fa-4x"style="color:#3293f7;margin-top:15px;"></div>
				<h3>Oversee Your Empire</h3>
				<p>
				With InLine we make is easy to manage multiple restaurants. We allow you to create multiple restaurants under one login, starting at only $50 per restaraunt. Within each restaurant you can create employee accounts. InLine gives you all the flexibility you need to oversee your restaurant empire. 
				</p>				
				</div>				
						<div class="mobileCard">
						<div class="mobPic">
						<img src="/InLineWebApp/Icons/Picture1.png" height="478" width="232">
						</div>
						<div class="mobPic">
						<img src="/InLineWebApp/Icons/Picture3.png" height="478" width="232">
						</div>
						<div class="mobText">
						<div class="fa fa-android fa-5x"style="color:#3293f7;"> </div>
						<h1>Mobile Application</h1>
						<p>Our mobile application allows customers to discover your restaurants and get InLine. Running on Android and soon iOS, the customer registers a account and logs into the mobile app, from there they can search for whatever restaurant they are interested in. They can view deals near them, check the weather, or logout. From the restaurant page, they can view the average wait time, available deals for the restaurant, and finally, input their party size before getting InLine. This is all supported with an API that serves as the middleman between our database and the mobile application.
		</p>
						</div>
						</div>
						
						<div>
						<h1>
						Our prices start at just <span style="color: green">$50</span> per restaraunt. Sign up today!</h1>
						<button type="button" onclick="leaveP()">Register</button>

						</div>
				
			</div>
            

        </main>
       <script>
	   function leaveP(){
		   window.location.href = "register.php";
	   }
	   </script>
    </body>
    

</html>

<?php
output_footer();
?>