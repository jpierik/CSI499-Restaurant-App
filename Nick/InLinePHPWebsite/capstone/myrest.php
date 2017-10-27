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
        
        <section class="banner" style="padding: 20px 0;">
                    <h1 class="text-center">My Restaurants</h1>
                   
        </section>
        
        
        
        
        <!-- CONTENT -->
        
        <main class="content">
          
            <!-- SIDEBAR -->
            
            
            
            
            <section class="articles">





            </section>
        <section class="restlist">
            
          
              <div>
                  Red Robin
              </div>
             
          
            <div style="float: right; color: red; background-color: white">
              Delete
          </div>  <div style="float: right; background-color: white">
              Edit
          </div>  <div style="float: right; background-color: white">
              View
          </div>  
         
        </section>
                    </section>
        <section class="restlist">
            
          
              <div>
                  Olive Garden
              </div>
             
          
            <div style="float: right; color: red; background-color: white">
              Delete
          </div>  <div style="float: right; background-color: white">
              Edit
          </div>  <div style="float: right; background-color: white">
              View
          </div>  
         
        </section>
                </section>
                    </section>
        <section class="restlist">
            
          
              <div>
                  Jagged Fork
              </div>
             
          
            <div style="float: right; color: red; background-color: white">
              Delete
          </div>  <div style="float: right; background-color: white">
              Edit
          </div>  <div style="float: right; background-color: white">
              View
          </div>  
         
        </section>
                </section>
                    </section>
        <section class="restlist">
            
          
              <div>
                  Art & Jakes
              </div>
             
          
            <div style="float: right; color: red; background-color: white">
              Delete
          </div>  <div style="float: right; background-color: white">
              Edit
          </div>  <div style="float: right; background-color: white">
              View
          </div>  
         
        </section>
                </section>
                    </section>
        <section class="restlist">
            
          
              <div>
                  PF Chang's
              </div>
             
          
            <div style="float: right; color: red; background-color: white">
              Delete
          </div>  <div style="float: right; background-color: white">
              Edit
          </div>  <div style="float: right; background-color: white">
              View
          </div>  
         
        </section>

        

            
            
        </main>

    </body>
    
    

</html>

<?php
output_footer();
?>