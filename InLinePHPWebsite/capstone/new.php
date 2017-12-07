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

// echo $USER;
// echo "debug ";
if (isset($_REQUEST['sll0'])) {
    // echo "debug ";
//    echo $_REQUEST['sll0'];
}


if (isset($_REQUEST['submission'])) {	
	$restName = $_REQUEST['restname'];
	$restAddress = $_REQUEST['restaddress'];
	$countTables = 0;
	
	
for($z = 0; $z <= 24; $z++){
    $str = "sll" . $z;
    $tables[$z] = $_REQUEST[$str];
	if ($tables[$z] !== "empty") {
		$countTables = $countTables + 1;
	}
}

$sql = "INSERT INTO Restaurant (Address, Name, NoOfTables, OwnerId, CurrentWait) VALUES ('$restAddress', '$restName', '$countTables', '$id', '0');";
odbc_exec($conn, $sql);

$sql = "SELECT RestaurantID FROM Restaurant WHERE Address = '$restAddress' AND OwnerId = '$id';";
$res = odbc_exec($conn, $sql);
$restID = odbc_result($res, 1);


// echo $tables[2];
// var_dump($tables);

for($z = 0; $z <= 24; $z++){
	
	$tableNum = $z;
	$maxO = '';
	$tableType = '';
	if ($tables[$z] == "vertbar"){
		$maxO = 2;
		$tableType = "vertbar";
	}
		else if ($tables[$z] == "4seat"){
			$maxO = 4;
			$tableType = "4seat";
		}
		else if ($tables[$z] == "8seat"){
			$maxO = 8;
			$tableType = "8seat";
		}
		else if ($tables[$z] == "2seat"){
			$maxO = 2;
			$tableType = "2seat";
		}
		else {
			$maxO = 0;
			$tableType = "empty";
		}
	
	
	
 $sql = "INSERT INTO Seatings (TableNumber, RestaurantID, maxOccupancy, TableType) VALUES ('$tableNum', '$restID', '$maxO', '$tableType');";
 odbc_exec($conn, $sql);
}

header("Location:myrest.php");

}



?>

<html>
    <body>
        
        <!-- BANNER -->
        
        <section class="banner" style="padding: 30px 0;">
                    <h1 class="text-center">Create New Restaurant</h1>
                    <p>Use this page to create a new restaurant layout</p>
        </section>
        
        
        
        
        <!-- CONTENT -->
        
        <main class="content">
<form>            
            <!-- SIDEBAR -->
            
            
            
            
            <section class="articles">
<h2>Restaurant Name:</h2>                
<input class ="newinput" type="text" placeholder="Restaurant Name" name="restname" required>
<h2>Restaurant Address:</h2>                
<input class ="newinput" type="text" placeholder="Restaurant Address" name="restaddress" required>
<button type="submit" name="submission" >Save</button>
            </section>


        
<section class="tables">
                    <?php 
for ($x = 0; $x <= 24; $x++) {?>

    <div class="floating-box">
        <img id="vbar<?php echo "$x";?>" src="Icons/testbar.jpg" alt="Smiley face" height="150" width="150" class="icons" onclick="reset2(<?php echo "$x";?>)">
        <img id="4per<?php echo "$x";?>" src="Icons/4personsquare.jpg" alt="Smiley face" height="150" width="150" class="icons" onclick="reset2(<?php echo "$x";?>)">
        <img id="blank<?php echo "$x";?>" src="Icons/Blank.jpg" alt="Smiley face" height="150" width="150" class="icons" style="visibility: hidden"onclick="reset2(<?php echo "$x";?>)">
        <img id="2perv<?php echo "$x";?>" src="Icons/2pervertical.jpg" alt="Smiley face" height="150" width="150" class="icons" onclick="reset2(<?php echo "$x";?>)">
		<img id="8perv<?php echo "$x";?>" src="Icons/8round.jpg" alt="Smiley face" height="150" width="150" class="icons" onclick="reset2(<?php echo "$x";?>)">
		
        <div class="styled-select">

<select id="sll<?php echo "$x";?>" name="sll<?php echo "$x";?>" onchange="setInner(document.getElementById('sll<?php echo "$x";?>').options[document.getElementById('sll<?php echo "$x";?>').selectedIndex].text, <?php echo "$x";?>)">
  <option value="empty">Empty</option>
  <option value="vertbar">Vertical Bar</option>
  <option value="8seat">8 Seat Table</option>
  <option value="4seat">4 Seat Table</option>
  <option value="2seat">2 Seat Table</option>
</select>
        </div>
        <div id="box<?php echo "$x";?>" style="visibility: hidden">Floating box</div>
        
        </div>

    <?php
} 
?>
</form>

            </section>
            
            
        </main>

    </body>
    
    
    <script>
function setInner(v, w) {
    
document.getElementById("box" + w).innerHTML = v;
if (v=="Vertical Bar"){
document.getElementById("vbar" + w).style.visibility = "visible"; 
  // alert(v);
}

else if (v=="4 Seat Table"){
document.getElementById("4per" + w).style.visibility = "visible";     
}
else if (v=="2 Seat Table"){
document.getElementById("2perv" + w).style.visibility = "visible";     
}
else if (v=="8 Seat Table"){
document.getElementById("8perv" + w).style.visibility = "visible";     
}
else if (v=="Empty"){
document.getElementById("blank" + w).style.visibility = "visible";    
}
}

function reset2(id){
    document.getElementById("sll" + id).value = 'empty';
    document.getElementById("vbar" + id).style.visibility = "hidden"; 
    document.getElementById("4per" + id).style.visibility = "hidden"; 
    document.getElementById("blank" + id).style.visibility = "hidden"; 
    document.getElementById("2perv" + id).style.visibility = "hidden"; 
	document.getElementById("8perv" + id).style.visibility = "hidden"; 
}
</script>
</html>

<?php
output_footer();
?>