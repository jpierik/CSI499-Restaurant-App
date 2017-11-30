<?php
require('library/library.php');
if (isset($_SESSION['user'])){
$USER = $_SESSION['user'];
}
output_header();
if (isset($_GET["rid"])) {
$_SESSION['currentRest'] = htmlspecialchars($_GET["rid"]);
$tempid = htmlspecialchars($_GET["rid"]);	
} else if ($_SESSION['acclevel'] == '1') {
	$sql = "SELECT RestaurantId FROM users WHERE username = '$USER';";
	$result = odbc_exec($conn, $sql);
	$_SESSION['currentRest'] = odbc_result($result, 1);
	$tempid =  odbc_result($result, 1);
} else{
	$tempid = $_SESSION['currentRest'];
}



if (isset($_REQUEST['submission'])) {
	$pname = $_REQUEST['partyname'];
	$psize = $_REQUEST['partysize'];
	 if (!preg_match('/^[1-9][0-9]{0,1}$/', $psize)) {
        echo "<center>" . "Party Size incorrect format" . "</center>";
    } else {
	$prid = $_REQUEST['restid'];
	$sql = "INSERT INTO WaitingParty (FullName, RestaurantID, NoOfGuests, PriorityLvl, AddTime) VALUES ('$pname', '$prid', '$psize', '1', CURRENT_TIMESTAMP);";
	odbc_exec($conn, $sql);
header("Location:restaurant.php");
	}
}

if (isset($_GET['remid'])) {
$xy = $_GET['remid'];
$sql = "DELETE FROM WaitingParty WHERE PartyId = '$xy';";
odbc_exec($conn, $sql);	
header("Location:restaurant.php");
}

$sql = "SELECT Count(*) FROM WaitingParty WHERE RestaurantId = '$tempid';";
$res = odbc_exec($conn, $sql);
$oldcount = odbc_result($res, 1);

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
function checkTable($tnum, $resid, $conn1){
$sql1 = "SELECT RestaurantId FROM CurrentStatus WHERE RestaurantID = '$resid' AND TableNumber = '$tnum';";
$res = odbc_exec($conn1, $sql1);
$res1 = odbc_result($res, 1);
if ($res1 == $resid) {
	$checkt = true;
	//echo $checkt;
return $checkt;
}
}
$boxstyle = 'box-shadow:inset 0px 0px 0px 5px #3399cc;';

$sql = "SELECT FullName, NoOfGuests, PartyId FROM WaitingParty WHERE RestaurantID = '$tempid';";
$result = odbc_exec($conn, $sql);


?>

<html>
    <body class="noselect" onload="checkList(<?php echo $tempid . "," . $oldcount; ?>)">
        
        <!-- BANNER -->
        
        <section class="banner" style="padding: 5px 0;">
		
		
                    <h1 class="text-center"><?=$restName?></h1>
                   
        </section>
        
        
        
        
        <!-- CONTENT -->
        
        <main class="content" >
          
            <!-- SIDEBAR -->
            
			<div></div>
			
            <aside class="sidebar">
			
			<h2>Tables</h2>
			
			  <?php for ($x = 0; $x <= 24; $x++) {
				  $taken = checkTable($x, $tempid, $conn);
				  
				  ?>

			<div id="<?php if ($taken) { echo "taken $layout[$x] . $x";} else {echo "$layout[$x] . $x";}?>" class="floating-box" onclick="pullInfo(this, <?php echo $tempid;?>);" ondrop="drop(event, this)" ondragleave="endDragOver(this)" ondragover="allowDrop(event, this)" style="width: 100px; height: 100px; border: 1px solid #000; <?php if ($taken){echo $boxstyle;}?> ">
			
				<?php if ($layout[$x] == "vertbar"){ ?>
				<img id="vbar<?php echo "$x";?>" src="Icons/testbar.jpg" height="100" width="100" style="visibility: visible" class="icons zindex" >	
				<?php } else if ($layout[$x] == "4seat"){ ?>
				<img id="4per<?php echo "$x";?>" src="Icons/4personsquare.jpg" height="100" width="100" style="visibility: visible" class="icons zindex" >	
				<?php } else if ($layout[$x] == "8seat"){ ?>
				<img id="8per<?php echo "$x";?>" src="Icons/8round.jpg" height="100" width="100" style="visibility: visible" class="icons zindex" >	
				<?php } else if ($layout[$x] == "2seat"){ ?>
				<img id="2perv<?php echo "$x";?>" src="Icons/2pervertical.jpg" height="100" width="100" style="visibility: visible" class="icons zindex">	
				<?php } else { ?>
				<img id="blank<?php echo "$x";?>" src="Icons/Blank.jpg" height="100" width="100" class="icons zindex" style="visibility: visible;">	
				<?php } ?>	
			</div>
			    <?php } ?>
			</aside>
            
			
			<div class="newparty"> 
			<form>
				<h2 id="food">Party Name:</h2>                
				<input class ="newinput" type="text" placeholder="Party Name" name="partyname" required>
				<h2>Party Size:</h2>                
				<input class ="newinput" type="text" placeholder="Party Size" name="partysize" style="width: 43%;"required>
				<input class ="newinput" type="text" value="<?php echo "$tempid"?>" name="restid" style="display: none;"required>
				<button type="submit" style="margin-top: 25px;"name="submission">Add Party</button>
			</form>
			
			<div class="fade_div">
			<h1>Table<br> Cleared</h1>
			</div>
			<div id="pInfo">
			<div onclick="remInfo()" class="fa fa-times fa-3x xhover"></div>
			<h2>Table Info</h2> 
			<h2>Occupants: <span id="infoOcc">4</span></h2> 
			<h2>Time Sat: <span id="satTime"></span></h2>
			<button onclick="remTable(this)"type="input" name="cleartable" class="remTableButton">Clear Table</button>

			</div>
			
			</div>
			
			
			
			
			
			
			
			
			<div class="waitlist">
			<h2 style="text-align: center;">Party List</h2>
						<?php 
			
			while (odbc_fetch_row($result)){
$rfname = odbc_result($result, 1);
$rnum = odbc_result($result, 2);
$rpid = odbc_result($result, 3);


echo '

					
					<div class="waititem" id="'.$rpid.'"draggable="true" ondragstart="drag(event, this)">
						<div style="float: left; margin-left: 5px; display: inline-flex;">
							<div id="partyname">
							'.$rfname." -  " . '
							</div>
							<div id="partysize" style="margin-left: 5px;">
							'.$rnum.'
							</div>
						</div>
						<div style="float: right; margin-right: 5px; line-height: 51px;">
							<span id="timerm'.$rpid.'">00</span>:<span id="timers'.$rpid.'">00</span>
							<div style="line-height:17px;"class="fa fa-times fa-lg xhover" onClick="reply_remove('.$rpid.')"></div>
						</div>
					</div>



';
}	
			?>
			</div>


        

            
            
        </main>

    </body>
    
    <script>
	var prevClick = 300;
	var oldcount = 0;
	
function reply_remove(clicked_id) {
		oldcount = oldcount - 1;
		var xmlhttp = new XMLHttpRequest();
	   xmlhttp.onreadystatechange = function(){
		   if (this.readyState == 4 && this.status == 200) {
			 $('#'+clicked_id).hide();
		   }
	   };
	   xmlhttp.open("GET","remparty.php?pid=" + clicked_id, true);
	   xmlhttp.send();
}


function srtTimer(){
	var waitArr = document.getElementsByClassName("waititem");
	var i = 0;
	
	for(i=0;i < waitArr.length; i++){
stimer(waitArr[i]);
	}
}
function stimer(el){
	
	var sec = 0;
	var timerm = 'timerm'+el.id.trim();
	var timers = 'timers'+el.id.trim();
		var xmlhttp = new XMLHttpRequest();
	   xmlhttp.onreadystatechange = function(){
		   if (this.readyState == 4 && this.status == 200) {
					sec = this.responseText;
					//document.getElementById(spanId).innerHTML = this.responseText;
		   }
	   };
	   xmlhttp.open("GET","calcsec.php?pid=" + el.id, true);
	   xmlhttp.send();
	   function pad ( val ) { return val > 9 ? val : "0" + val; }
    setInterval( function(){
        document.getElementById(timers).innerHTML=pad(++sec%60);
        document.getElementById(timerm).innerHTML=pad(parseInt(sec/60,10));
    }, 1000);
}
function allowDrop(ev, el) {
	if ((!(el.id).includes("empty")) && (!(el.id).includes("taken"))){
    ev.preventDefault();
	el.style.boxShadow="inset 0px 0px 0px 5px #03af00";
	}
	else{
	el.style.boxShadow="inset 0px 0px 0px 5px #ff0000";
	}
	//el.setAttribute("style","background-color: green;");
}

function endDragOver (el) {
	if (!(el.id).includes("taken")){
	el.style.boxShadow="none";
	} else {
	el.style.boxShadow="inset 0px 0px 0px 5px #3399cc";	
	}
}

function drag(ev, el){
	ev.dataTransfer.setData("text/plain", el.id);
	//alert(el.id);
}
function drop(ev, ele) {
	oldcount = oldcount - 1;
    ev.preventDefault();
   var data = ev.dataTransfer.getData("text");
   
   ele.setAttribute("id", 'taken ' + ele.id);
  // alert(ele.id);
   ele.style.boxShadow="inset 0px 0px 0px 5px #3399cc";
   //alert(data);
   
   if (data > 0) {
	   var xmlhttp = new XMLHttpRequest();
	   xmlhttp.onreadystatechange = function(){
		   if (this.readyState == 4 && this.status == 200) {
			   //document.getElementById("food").innerHTML = this.responseText;
			   document.getElementById(data).style.display = "none";
		   }
	   };
	   xmlhttp.open("GET","seattable.php?pid=" + data + "&bid=" + ele.id, true);
	   xmlhttp.send();
	   
   }
   
}

function pullInfo(el, rid){
	if ((el.id).includes("taken")){
		if (prevClick != 300){
		document.getElementById(prevClick).style.boxShadow="inset 0px 0px 0px 5px #3399cc";
		}
		prevClick = el.id;
		el.style.boxShadow="inset 0px 0px 0px 5px #f44336";
		document.getElementById("pInfo").style.visibility = "visible";
		
		var xmlhttp = new XMLHttpRequest();
	   xmlhttp.onreadystatechange = function(){
		   if (this.readyState == 4 && this.status == 200) {
				var myObj = JSON.parse(this.responseText);
			    document.getElementById("satTime").innerHTML = myObj[2];
				document.getElementById("infoOcc").innerHTML = myObj[1];
				document.getElementsByClassName("remTableButton")[0].id = myObj[0];
		   }
	   };
	   xmlhttp.open("GET","tableinfo.php?tid=" + el.id + "&rid=" + rid, true);
	   xmlhttp.send();
	}
}
function remInfo(){
	document.getElementById("pInfo").style.visibility = "hidden";
	document.getElementById(prevClick).style.boxShadow="inset 0px 0px 0px 5px #3399cc";
}

function remTable (el){
document.getElementById(prevClick).style.boxShadow="none";
var newid = prevClick.slice(6);
document.getElementById(prevClick).id = newid;
prevClick = 300;
document.getElementById("pInfo").style.visibility = "hidden";

		var xmlhttp = new XMLHttpRequest();
	   xmlhttp.onreadystatechange = function(){
		   if (this.readyState == 4 && this.status == 200) {
			 //   document.getElementById("food").innerHTML = this.responseText;
		   }
	   };
	   xmlhttp.open("GET","cleartable.php?sid=" + el.id, true);
	   xmlhttp.send();
	   
	   
$('.fade_div').finish().show().delay(1200).fadeOut("fast");
}
function sleep(ms) {
  return new Promise(resolve => setTimeout(resolve, ms));
}
async function checkList(rd, oc){
//alert(rd);
srtTimer();
var mx = 0;
  oldcount = oc; 
  while (mx <= 150) { 
 // console.log('Taking a break...');
  await sleep(3000);
 // console.log('Two second later');
		var xmlhttp = new XMLHttpRequest();
	   xmlhttp.onreadystatechange = function(){
		   if (this.readyState == 4 && this.status == 200) {
			    if (this.responseText != oldcount){
					//alert(this.responseText);
					window.location.href = "restaurant.php";
				}
		   }
	   };
	   xmlhttp.open("GET","checkList.php?rid=" + rd, true);
	   xmlhttp.send();
	   mx = mx + 1;
  }
  console.log('done');
}

	</script>

</html>

<?php
output_footer();
?>