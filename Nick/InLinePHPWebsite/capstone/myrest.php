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

$sql = "SELECT RestaurantId, Name FROM Restaurant WHERE OwnerId = '$id';";
$result = odbc_exec($conn, $sql);




if (isset($_REQUEST['submission'])) {
 echo "hi";
for($z = 0; $z <= 24; $z++){
    $str = "sll" . $z;
    $tables[$z] = $_REQUEST[$str];
}
echo $tables[2];
var_dump($tables);
}

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

            </section>
			
			
			<?php 
			
			while (odbc_fetch_row($result)){
$rid = odbc_result($result, 1);
$rname = odbc_result($result, 2);


echo '

        <section class="restlist">          
              <div style="margin-top: 20px;">
                  '.$rname.'
              </div>
            <div style="float: right; ">
              
			  <button class="restl" type="submit" name="view" style="margin: 0px; border: 2px solid #FFF; border-radius: 5px; font-size: x-large;" onClick="moving1('.$rid.');");">Delete</button>
          </div>  <div style="float: right;">
              
			  <button class="restl" type="submit" name="view" style="margin: 0px; border: 2px solid #FFF; border-radius: 5px; font-size: x-large;" onClick="moving1('.$rid.');");">Edit</button>
          </div>      <div style="float: right;">
              
			  <button class="restl" type="submit" name="view" style="margin: 0px; border: 2px solid #FFF; border-radius: 5px; font-size: x-large;" onClick="moving1('.$rid.');");">View</button>
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
		</script>
   
    
    

</html>

<?php
output_footer();
?>