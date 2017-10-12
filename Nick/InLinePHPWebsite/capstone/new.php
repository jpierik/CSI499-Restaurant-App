<?php
require('library/library.php');
if (isset($_SESSION['user'])){
$USER = $_SESSION['user'];
}
output_header();

// echo $USER;
// echo "debug ";
if (isset($_REQUEST['sll0'])) {
    // echo "debug ";
//    echo $_REQUEST['sll0'];
}


if (isset($_REQUEST['submission'])) {
 echo "hi";
for($z = 0; $z <= 24; $z++){
    $str = "sll" . $z;
    $tables[$z] = $_REQUEST[$str];
}
echo $tables[2];
var_dump($tables);
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

        <div class="styled-select">

<select id="sll<?php echo "$x";?>" name="sll<?php echo "$x";?>" onchange="setInner(document.getElementById('sll<?php echo "$x";?>').options[document.getElementById('sll<?php echo "$x";?>').selectedIndex].text, <?php echo "$x";?>)">
  <option value="empty">Empty</option>
  <option value="vertbar">Vertical Bar</option>
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
}
</script>
</html>

<?php
output_footer();
?>