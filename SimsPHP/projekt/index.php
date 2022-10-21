<!---------------------------------------
- Filename: index.php                   -
- Author: Moa Nygård                    -
- Kurs: SIMS 15hp                       -
- Created: 2022-10-07                   -
- Updated: 2022-10-20                   -
- Data Owner                            -
---------------------------------------->

<?php
    session_start();
    $page_title = "Startsida";
    include 'includes/db_connection.php';
    $conn = OpenCon();
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>SIMS</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="css/styles.css" type="text/css">
</head>

<body>
    <header id="mainheader">
        <h1 id="logo">Data Owner</h1>
    </header>
    <div class="contain">
        <div class="container">
                <div class="vertical-menu">
                    <a href="download.php?file=files/TestDataVisualization.csv" target="_new">Download CSV</a>              
                    <a href="download.php?file=files/TestDataVisualization.json" target="_new" download>Download JSON</a>
                    <a href="download.php?file=files/TestDataVisualization.xlsx" target="_new" download>Download xlsx</a>
                </div>
            <footer>
                <p>Data Owner - SIMS</p>
            </footer>
       </div>
        <!--/.container -->
    </div>
    <!-- /.contain -->
</body>
</html>
