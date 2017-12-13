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
if (isset($_GET["rid"])) {
$restid = $_GET["rid"];
	
}
$sql = "SELECT Name FROM Restaurant WHERE RestaurantId = '$restid';";
$result = odbc_exec($conn, $sql);
$restname = odbc_result($result, 1);

$sql = "SELECT Title, Descript, DealId FROM Deals WHERE RestaurantId = '$restid';";
$result = odbc_exec($conn, $sql);


?>

<html>
    <body>
        
        <!-- BANNER -->
        
        <section class="bannero" style="padding: 20px 0;box-shadow:none;background: #ececec;">
                    <h1 class="text-center"><?=$restname?> Deals/Promotions</h1>
                   
        </section>
        
        
        
        
        <!-- CONTENT -->
        
        <main class="content">
          
            <!-- SIDEBAR -->
            
            
            
            
            <section class="articles">
			
			<h1>New Deal:</h1>
			<form onSubmit="addDeal(event, <?=$restid?>)">
			<div class="container" style="margin-top: 10px">
                    <label><b>Deal Name</b></label>
                    <input id="dealname" class="oldinput" type="text" placeholder="Deal Name" name="email" required>
                
                    <label><b>Deal Description</b></label>
                    <input id="dealdesc" class="oldinput" type="text" placeholder="Deal Description (e.g. half off drinks)" name="username" required>
					<label><b>Category</b></label>
					<select id="dealCat" class="oldinput" name="restaurant" required>
						<option value="0">0-Dinner Food</option>
						<option value="1">1-Drinks/Alcohol</option>
						<option value="2">2-Appetizers</option>
						<option value="3">3-Desserts</option>					
					</select>
					<label><b>Deal Priority</b></label>
					<select id="dealPri" class="oldinput" name="restaurant" required>
						<option value="0">0-Show Last</option>
						<option value="1">1-Show First</option>
					</select>
					<button type="submit"style="box-shadow: 1px 1px 14px 3px #cccccc;font-size: 15px;" name="submission">Add Deal</button>
					</div>
					</form>
					</section>
			
			<div id="dealslist">
			<h1 style="text-align: center">Current Deals:</h1>
			<?php 
			
			while (odbc_fetch_row($result)){
$dealName = odbc_result($result, 1);
$dealDesc = odbc_result($result, 2);
$dealID = odbc_result($result, 3);

echo '

        <section id="'.$dealID.'"class="restlist" style="height: 100px">          
              <div style="margin-top: 15px;">
                  '.$dealName.'
              </div>
			  
            <div style="float: right; ">
			  <button class="restl xhover" type="submit" name="view" style="margin-top: 16px;margin-right:10px;box-shadow: 1px 1px 14px 3px #e2e2e2;font-size: large;" onClick="remRest1('.$dealID.');");">Delete</button>
          </div>      
            <div style="float: left; ">
			<p style="line-height: 28px; font-size: medium;color: #5a5a5a;width:625px;margin-top:-8px;">'.$dealDesc.'</p>
          </div>  	  
        </section>



';
}
			
			
			?> 
			</div>
        <section id="cop"class="restlist" style="height: 100px; visibility: hidden; position: absolute;">          
              <div id="dealNames"style="margin-top: 15px;">
                  
              </div>
			  
            <div style="float: right; ">
			  <button id="dealbtn" class="restl xhover" type="submit" name="view" style="margin-top: 16px;margin-right:10px;box-shadow: 1px 1px 14px 3px #e2e2e2;font-size: large;" onClick="remRest1();">Delete</button>
          </div>      
            <div style="float: left; ">
			<p id="pp" style="line-height: 28px; font-size: medium;color: #5a5a5a;width:625px;margin-top:-8px;"></p>
          </div>  	  
        </section>

            
            
        </main>

		</body>
		
		<script>
		
		function remRest1(x){
			if (confirm("Are you sure you want to remove this deal?") == true) {
						//alert(x);	
						var xmlhttp = new XMLHttpRequest();
	   xmlhttp.onreadystatechange = function(){
		   if (this.readyState == 4 && this.status == 200) {
			    document.getElementById(x).style.display = "none";
				//document.getElementById("water").innerHTML = this.responseText;
		   }
	   };
	   xmlhttp.open("GET","remdeal.php?rid=" + x, true);
	   xmlhttp.send();
			}
		}
		
		
		
		
		function addDeal(ev, rid){
			ev.preventDefault();
			var cop = document.getElementById("cop");
			var cln = cop.cloneNode(true);
			
			var dealName = document.getElementById("dealname").value;
			var dealDesc = document.getElementById("dealdesc").value;
			var dealCat = document.getElementById("dealCat").value;
			var dealPri = document.getElementById("dealPri").value;
			document.getElementById("dealslist").appendChild(cln);
			var dealID = '';
									var xmlhttp = new XMLHttpRequest();
	   xmlhttp.onreadystatechange = function(){
		   if (this.readyState == 4 && this.status == 200) {
			    dealID = this.responseText;
				//alert(dealID);
				document.getElementById("cop").id = dealID;
				document.getElementById(dealID).style.visibility = "unset";
				document.getElementById(dealID).style.position = "unset";
				document.getElementById("dealNames").innerHTML = dealName;
				document.getElementById("pp").innerHTML = dealDesc;
				document.getElementById("dealNames").id = '';
				document.getElementById("pp").id = '';
				document.getElementById("dealbtn").setAttribute("onClick", "remRest1(" + dealID +")");
				document.getElementById("dealbtn").id = '';
				
		   }
	   };
	   xmlhttp.open("GET","adddeal.php?rid=" + rid + "&dealname=" + dealName + "&dealdesc=" + dealDesc + "&dealcat=" + dealCat + "&dealpri=" + dealPri, true);
	   xmlhttp.send();
			
			
			
			
			document.getElementById("dealname").value = '';
			document.getElementById("dealdesc").value = '';
		}
		</script>
   
    
    

</html>

<?php
output_footer();
?>