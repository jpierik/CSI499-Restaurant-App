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
        <div>
        <!-- BANNER -->
        
        <section class="banner">
            
            	   <?php if ($USER) { ?>
                    <h1 class="text-center">Welcome to InLine, <?=$USER?>!</h1>
                    <?php } else { ?>
                    <h1>Welcome to InLine!</h1>
                    <?php } ?>
            
            <p><b>InLine</b> is an application to manage tables at a restaraunt.</p>
        </section>
        
        <!-- CONTENT -->
        
        <main class="content">
            

           
        
            <section class="articles">
                

<!--
<article id="article-id">
    
</article>
-->

Hello
            </section>
            

        </main>
        </div>
    </body>
    

</html>

<?php
output_footer();
?>