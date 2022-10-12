<?php // Code to track the file using PHP, in a database,

    include 'includes/db_connection.php';
    $conn = OpenCon();

    if($conn) {
        echo "Connected Successfull";
    }

    // Prep the vars
    $file_id = $_GET['file'];
    $file_path = 'files/'. $file_id; // TestDataVisualization.csv
    echo "Filename: " . $file_id . " Filepath: " . $file_path; 

    // Save data to database
    $conn->query("INSERT INTO download_manager SET filename='". $file_id. "' ON DUPLICATE KEY UPDATE downloads=downloads+1");
    // https://tutorialzine.com/2010/02/php-mysql-download-counter INCREMENT downloads IF FILE ALREADY EXISTS

    CloseCon($conn);
    echo "Connected Closed Successfully";
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>SIMS - laddat ner???</title>
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
                    <a href="files/TestDataVisualization.csv" download>Download CSV</a>
                    <a href="files/TestDataVisualization.csv" download>Download JSON</a>
                    <a href="download_CSV.php?filename=TestDataVisualization.csv">Download File!</a>
                    <a href="files/TestDataVisualization.csv" download>Download other formats</a>
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
