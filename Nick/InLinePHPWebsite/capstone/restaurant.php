<?php
require('library/library.php');
if (isset($_SESSION['user'])){
$USER = $_SESSION['user'];
}
output_header();
if (isset($_GET["rid"])) {
$_SESSION['currentRest'] = htmlspecialchars($_GET["rid"]);
$tempid = htmlspecialchars($_GET["rid"]);	
} else {
	$tempid = $_SESSION['currentRest'];
}



if (isset($_REQUEST['submission'])) {
	$pname = $_REQUEST['partyname'];
	$psize = $_REQUEST['partysize'];
	$prid = $_REQUEST['restid'];
	$sql = "INSERT INTO WaitingParty (FullName, RestaurantID, NoOfGuests, PriorityLvl, AddTime) VALUES ('$pname', '$prid', '$psize', '1', CURRENT_TIMESTAMP);";
	odbc_exec($conn, $sql);
	header("Location:restaurant.php");
}

if (isset($_GET['remid'])) {
$xy = $_GET['remid'];
$sql = "DELETE FROM WaitingParty WHERE PartyId = '$xy';";
odbc_exec($conn, $sql);	
header("Location:restaurant.php");
}

$sql = "SELECT Name FROM Restaurant WHERE RestaurantId = '$tempid'";
$result = odbc_exec($conn, $sql);
$restName = odbc_result($result, 1);


$sql = "SELECT TableType FROM Seatings WHERE RestaurantID = '$tempid' ORDER BY TableNumber ASC";
$result = odbc_exec($conn, $sql);
	$z = 0;
while (odbc_fetch_row($result)){
$layout[$z] = odbc_result($result, 1);
$z = $z+1;
}

$sql = "SELECT FullName, NoOfGuests, PartyId FROM WaitingParty WHERE RestaurantID = '$tempid';";
$result = odbc_exec($conn, $sql);


?>

<html>
    <body>
        
        <!-- BANNER -->
        
        <section class="banner" style="padding: 5px 0;">
		
		
                    <h1 class="text-center"><?=$restName?></h1>
                   
        </section>
        
        
        
        
        <!-- CONTENT -->
        
        <main class="content">
          
            <!-- SIDEBAR -->
            
			
			
            <aside class="sidebar">
			
			<h2>Tables</h2>
			
			  <?php for ($x = 0; $x <= 24; $x++) {?>

			<div class="floating-box" style="width: 100px; height: 100px; border: 1px solid #000;">
			
			<?php if ($layout[$x] == "vertbar"){ ?>
			<img id="vbar<?php echo "$x";?>" src="Icons/testbar.jpg" height="100" width="100" style="visibility: visible" class="icons" >	
			<?php } else if ($layout[$x] == "4seat"){ ?>
			<img id="4per<?php echo "$x";?>" src="Icons/4personsquare.jpg" height="100" width="100" style="visibility: visible" class="icons" >	
			<?php } else if ($layout[$x] == "2seat"){ ?>
			<img id="2perv<?php echo "$x";?>" src="Icons/2pervertical.jpg" height="100" width="100" style="visibility: visible" class="icons">	
			<?php } else { ?>
			<img id="blank<?php echo "$x";?>" src="Icons/Blank.jpg" height="100" width="100" class="icons" style="visibility: visible">	
			<?php } ?>	
			</div>
			    <?php } ?>
			</aside>
            
			
			<div class="newparty"> 
			<form>
			<h2>Party Name:</h2>                
<input class ="newinput" type="text" placeholder="Party Name" name="partyname" required>
<h2>Party Size:</h2>                
<input class ="newinput" type="text" placeholder="Party Size" name="partysize" style="width: 43%;"required>
<input class ="newinput" type="text" value="<?php echo "$tempid"?>" name="restid" style="display: none;"required>
<button type="submit" name="submission" >Add Party</button>
</form>
			</div>
			
			
			
			
			
			
			
			
			<div class="waitlist">
			<h2 style="text-align: center;">Party List</h2>
						<?php 
			
			while (odbc_fetch_row($result)){
$rfname = odbc_result($result, 1);
$rnum = odbc_result($result, 2);
$rpid = odbc_result($result, 3);


echo '

					
					<div class="waititem" id="'.$rpid.' "draggable="true">
						<div style="float: left; margin-left: 5px; display: inline-flex;">
							<div id="partyname">
							'.$rfname.'
							</div>
							<div id="partysize" style="margin-left: 5px;">
							'.$rnum.'
							</div>
						</div>
						<div style="float: right; margin-right: 5px; line-height: 53px;">
							<div class="fa fa-times xhover" onClick="reply_remove('.$rpid.')"></div>
						</div>
					</div>



';
}
			
			
			?>

			</div>


        

            
            
        </main>

    </body>
    
    <script>
	function reply_remove(clicked_id)
{
window.location.href = "restaurant.php?remid=" + clicked_id;

//alert(clicked_id);
}
	</script>

</html>

<?php
output_footer();
?>