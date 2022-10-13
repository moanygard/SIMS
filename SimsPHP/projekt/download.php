<?php // Code to track the file using PHP, in a database,

  function callAPI($url, $data){
    $curl = curl_init();
    curl_setopt($curl, CURLOPT_POST, 1);
    if ($data)
    {
      curl_setopt($curl, CURLOPT_POSTFIELDS, $data);
    }
    // OPTIONS:
    curl_setopt($curl, CURLOPT_URL, $url);
    curl_setopt($curl, CURLOPT_HTTPHEADER, array(
       'Content-Type: application/json',
    ));
    curl_setopt($curl, CURLOPT_RETURNTRANSFER, 1);
    curl_setopt($curl, CURLOPT_HTTPAUTH, CURLAUTH_BASIC);
    // EXECUTE:
    $result = curl_exec($curl);
    if(!$result){die("Connection Failure");}
      curl_close($curl);
    return $result;
  }    

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

    $url = "https://localhost:7076/api/datalanguages";

    $data_array =  array(
      "idDataLanguage"        => 105,
      "dataLangugageName"         => "Finish"
    );

    $make_call = callAPI($url, json_encode($data_array));
    $response = json_decode($make_call, true);
    $errors   = $response['response']['errors'];
    $data     = $response['response']['data'][0];
   
    echo "IS IT WORKING?";


// close curl resource to free up system resources
// (deletes the variable made by curl_init)
curl_close($curl);

?>
