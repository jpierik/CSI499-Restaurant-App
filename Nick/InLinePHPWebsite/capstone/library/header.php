<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>InLine</title>

        <!--CSS Stylesheet, Google Font, Font Awesome -->
        <link rel="stylesheet" href="library/stylesheets/mainStyle.css?<?php echo time(); ?>">
        <link rel="stylesheet" href="library/stylesheets/headerStyle.css?<?php echo time(); ?>">
        <link rel="stylesheet" type="text/css" href="library/stylesheets/headerPagesStyle.css?<?php echo time(); ?>">
		<link href="https://fonts.googleapis.com/css?family=Yanone+Kaffeesatz" rel="stylesheet">
        <link href="https://fonts.googleapis.com/css?family=Space+Mono:400,400i,700,700i" rel="stylesheet">
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
        
        <!-- jQuery -->
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
     
    </head>
    
    <body>
        
        <!-- Header -->
        <header>
            <a href="index.php">
                
                <!-- Title -->
                <h1><b>I</b>n<b>L</b>ine</h1>

                
            </a>
            
            <nav>
           
                
                <?php if (($USER) && ($_SESSION['acclevel'] == '0')) { ?>
           <!--     
                <a href="addarticle.php" class="green">
                    <i class="fa fa-plus"></i> Add New
                </a>
                -->
                
                 <a href="new.php" class="red">
                    <i class="fa fa-plus"></i> New Restaurant
                </a>
                <a href="myrest.php" class="red">
                    <i class="fa fa-server"></i> My Restaurants
                </a>
                <a href="settings.php" class="gray">
                    <i class="fa fa-gear"></i> User Settings
                </a>
                <a href="newemployee.php" class="green">
                    <i class="fa fa-user-plus"></i> New Employee
				</a>
                <a href="login.php?logout=1&url=index.php" class="red">
                    <i class="fa fa-sign-out"></i> Logout
                </a>
                
              
        <?php } else if (($USER) && ($_SESSION['acclevel'] == '1')) { ?>

                <a href="restaurant.php" class="red">
                    <i class="fa fa-cutlery"></i> Restaurant
                </a>
                <a href="settings.php" class="gray">
                    <i class="fa fa-gear"></i> User Settings
                </a>
                <a href="login.php?logout=1&url=index.php" class="red">
                    <i class="fa fa-sign-out"></i> Logout
                </a>
			
			
        <?php } else { ?>       

             <a href="register.php" class="green">
                    <i class="fa fa-user-plus"></i> Register
            </a>
                
             <a href="login.php" class="red">
                    <i class="fa fa-sign-in"></i> Login
            </a>		
                <?php } 
                // echo $USER; 
                ?>


            </nav>
            
        </header>
    
    </body>
</html>