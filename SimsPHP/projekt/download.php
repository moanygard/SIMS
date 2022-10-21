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
 
    curl_setopt($curl, CURLOPT_FAILONERROR, true);

    curl_setopt($curl, CURLOPT_SSL_VERIFYPEER, false);

    // EXECUTE:
    $result = curl_exec($curl);
    if(curl_errno($curl)){
      $error = curl_error($curl);
      echo $error;
    }


    if(!$result){die("Connection Failure");}
      curl_close($curl);
    return $result;
  }

  function getFileFormat ($filename) {
    $path = "$filename";
    $extension = pathinfo($path, PATHINFO_EXTENSION);

    return $extension;
  }

  include 'includes/db_connection.php';
  $conn = OpenCon();

  if($conn) {
      echo "Connected Successfull\n";
  }
   
  // Prep the vars
  // Get URL
  $protocol = ((!empty($_SERVER['HTTPS']) && $_SERVER['HTTPS'] != 'off') || $_SERVER['SERVER_PORT'] == 443) ? "https://" : "http://";
  $uri = parse_url($_SERVER["REQUEST_URI"], PHP_URL_PATH);
  $url = $protocol . $_SERVER['HTTP_HOST'] . $uri;//$_SERVER['REQUEST_URI'];
  echo $url; // Outputs: Full URL

  if (isset($_GET['file'])) {
    // Get filename
    $file = $_GET['file'];

    if (file_exists($file) && is_readable($file)) {

      $file_path = $file; // TestDataVisualization.csv  'files/'. 
      echo "Filename: " . $file . " Filepath: " . $file_path;

      // Get file type
      $format = getFileFormat($file_path);
      echo "Format is: " . $format;

      switch($format){
        case 'csv':
          echo "Case is csv";
          header('Content-Type: application/csv');
          $idFormat = 2;
          break;
        case 'json':
          echo "Case is json";
          header('Content-Type: application/json');
          $idFormat = 1;
          break;
        case 'xlsx':
          echo "Case is xlsx";
          header('Content-Type: application/xlsx');
          $idFormat = 5;
          break;
      }    
      header("Content-Disposition: attachment; filename=\"$file\"");
      readfile($file);
    }
  }

  // Set variables
  $language = "English";
  $idLanguage = 2;

  $theme = "Cars and Wheels";
  $idTheme = '';

  // Save data to database
  $stmt = $conn->prepare("INSERT INTO data_usage (data_url, data_name, data_theme_name, data_format_name, data_language_name) VALUES (?,?, ?, ?, ?)");
  $stmt->bind_param("sssss", $url, $file, $theme, $format, $language);
  $stmt->execute();

  CloseCon($conn);
  echo "Connected Closed Successfully";

  ///////////////////////// DO API POST /////////////////////////////////

  $conn = OpenCon();

  if($conn) {
      echo "Connected Successfull\n";
  }
  // Save data to database
  $sql = "SELECT DATE_FORMAT(date_of_usage, '%Y-%m-%dT%H:%i:%s.823Z') AS 'dt' FROM data_usage WHERE id_use = (SELECT MAX(id_use) FROM data_usage)";
  $res = $conn->query($sql);

  if($res->num_rows > 0){
    $row = $res->fetch_assoc();
    $datetime = $row['dt'];
  }

  CloseCon($conn);
  echo "Connected Closed Successfully";

  $APIurl = "https://localhost:7076/api/datausages";

  // Prepare variables
  $idDataOwner = 4;
  $idOpenData = 1;

  $data_array =  array(
      "openDataId" => $idOpenData,
      "dateOfUsage" => $datetime,
      "dataFormatId" => $idFormat,
      "languageId" => $idLanguage,
      "dataFormat" => array(
        "idDataFormat" => $idFormat,
        "dataFormatName" => "String"
      ),
      "language" => array(
        "idDataLanguage" => $idLanguage,
        "dataLanguageName" => "string"
      ),
      "openData" => array(
        "idData" => $idOpenData,
        "dataUrl" => $url,
        //"dataOpenLicense" => 0,
        //"dataOwnerId" => $idDataOwner,
        //"updateFrequencyId" => 0,
        //"dataThemeId" => 0,
        "dataOwner" => array(
          "dataOwnerName" => "String"
        ),
        "dataTheme" => array(
          //"idDataTheme" => 0,
          "dataThemeName" => "string"
        ),
        "updateFrequency" => array(
          //"idUpdateFrequency" => 0,
          "updateFrequencyName" => "string"
        )
      )
  );

  $jsonEnc = json_encode($data_array);
  
  $myfile = fopen("jsonEncoded.txt", "w") or die("Unable to open file!");
  //fwrite($myfile, $jsonEnc);//$jsonEnc);
  fwrite($myfile, $SQLdatetime);//$jsonEnc);
  fclose($myfile);

  $make_call = callAPI($APIurl, json_encode($data_array));

  $response = json_decode($make_call, true);

  $myResponseFile = fopen("response.txt", "w") or die("Unable to open file!");
  fwrite($myResponsefile, $response);// $response);

  fclose($myResponsefile);
?>
