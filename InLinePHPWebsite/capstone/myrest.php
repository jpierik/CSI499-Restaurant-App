<?php
require('library/library.php');
if (isset($_SESSION['user'])){
$USER = $_SESSION['user'];
}
output_header();

if ($USER) {
$sql = "SELECT userid FROM users WHERE username = '$USER';";
$result = odbc_exec($conn, $sql);
$id = odbc_result($result, 1);
}

$sql = "SELECT RestaurantId, Name, CurrentWait FROM Restaurant WHERE OwnerId = '$id';";
$result = odbc_exec($conn, $sql);


$ffont = '"Veranda"';
?>

<html>
    <body>
        
        <!-- BANNER -->
        
        <section class="banner" style="padding: 20px 0;">
                    <h1 class="text-center">My Restaurants</h1>
                   
        </section>
        
        
        
        
        <!-- CONTENT -->
        
        <main class="content">
          
            <!-- SIDEBAR -->
            
            
            
            
            <section class="articles">
<div id="water"><h2></h2></div>
            </section>
			
			
			<?php 
			
			while (odbc_fetch_row($result)){
$rid = odbc_result($result, 1);
$rname = odbc_result($result, 2);
$cwait = odbc_result($result, 3);


echo '

        <section id="'.$rid.'"class="restlist">          
              <div style="margin-top: 20px;">
                  '.$rname.'
              </div>
			  
            <div style="float: right; ">
			  <button class="restl xhover" type="submit" name="view" style="margin: 0px; border: 2px solid #FFF; border-radius: 5px; font-size: x-large;" onClick="remRest1('.$rid.');");">Delete</button>
          </div>      

		  <div style="float: right;">
			  <button class="restl" type="submit" name="view" style="margin: 0px; border: 2px solid #FFF; border-radius: 5px; font-size: x-large;" onClick="moving1('.$rid.');");">View</button>
          </div>  
            <div style="float: right; ">
			<span style="line-height: 62px; font-size: x-large; color: #bdefff;">Current Wait: ' .$cwait. ' Minutes</span>
          </div>   		  
        </section>



';
}
			
			
			?>
			<!--
        <section class="restlist">          
              <div>
                  Red Robin
              </div>
            <div style="float: right; color: red; background-color: white">
              Delete
          </div>  <div style="float: right; background-color: white">
              Edit
          </div>  <div style="float: right; margin-top: 0px;">
              
			  <button type="submit" name="view" style="margin: 0px; border: 2px solid #FFF; font-weight: bold; border-radius: 5px; font-size: x-large; font-family: "Veranda", sans-serif;" onClick="moving1(<?php echo "$rid";?>);");">View</button>
          </div>   
		  
        </section>
              -->   


            
            
        </main>

		</body>
		
		<script>
		function moving1(v){
			var yy = "./restaurant.php?rid="
			var x = yy + v
	window.location = x;
		}
		
		function remRest1(x){
			if (confirm("Are you sure you want to remove this restaurant?") == true) {
						//alert(x);	
						var xmlhttp = new XMLHttpRequest();
	   xmlhttp.onreadystatechange = function(){
		   if (this.readyState == 4 && this.status == 200) {
			    document.getElementById(x).style.display = "none";
				document.getElementById("water").innerHTML = this.responseText;
		   }
	   };
	   xmlhttp.open("GET","remrest.php?rid=" + x, true);
	   xmlhttp.send();
			}
		}
		</script>
   
    
    

</html>

<?php
output_footer();
?>