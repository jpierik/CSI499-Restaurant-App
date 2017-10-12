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
$sql = "SELECT id FROM users WHERE username = '$USER';";
$result = $conn->query($sql);
$row = $result->fetch_assoc();
$id = $row['id'];


}
?>

<html>
    <body>
        
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
        
    </body>
    

</html>

<?php
output_footer();
?>