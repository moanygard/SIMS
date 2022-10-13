<!---------------------------------------
- Filename: index.php                   -
- Author: Moa Nygård                    -
- Kurs: SIMS 15hp                       -
- Created: 2022-10-07                   -
- Updated: 2022-10-11                   -
- Dummy Data Owner                      -
---------------------------------------->

<?php
    session_start();
    $page_title = "Startsida";
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
                    <a href="download.php?file=TestDataVisualization.csv">Download CSV</a>
                    <a href="download.php?file=TestDataVisualization.json" download>Download JSON</a>
                    <a href="download.php?file=TestDataVisualization.xlsx" download>Download xlsx</a>
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
