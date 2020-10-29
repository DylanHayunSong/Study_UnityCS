<?php
    $dataString = $_POST['dataString'];
    echo $dataString;
    $myFile = fopen("JsonData.json", "w");
    file_put_contents("JsonData.json", $dataString);
?>