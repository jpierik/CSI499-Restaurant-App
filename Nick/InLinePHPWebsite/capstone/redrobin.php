<?php
require('library/library.php');
if (isset($_SESSION['user'])){
$USER = $_SESSION['user'];
}
output_header();

// echo $USER;
// echo "kkhi";
?>

<html>
    <body>
        
        <!-- BANNER -->
        
        <section class="banner">
                    <h1 class="text-center">Red Robin</h1>
                    <p>Red Robin.</p>
        </section>
        
        <!-- CONTENT -->
        
        <main class="content">
            
            <!-- SIDEBAR -->
Hello
            <!-- ARTICLE LIST -->

            
        </main>
        
    </body>
</html>

<?php
output_footer();
?>