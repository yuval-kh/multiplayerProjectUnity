<?php

$con  = mysqli_connect('localhost', 'root', 'root', 'players');

if(mysqli_connect_errno()){
    echo  "1";
    exit();
}

$username = $_POST["name"];
$newscore = $_POST["score"];

$namecheckquery = "SELECT name FROM playerstable WHERE name='" .$username ."';" ;
$namecheck = mysqli_query($con , $namecheckquery) or die("2: Name check query failed");
if (mysqli_num_rows($namecheck) != 1){
    echo "5: Either no user with name, or more then one";
    exit();
}

$updatequery = "UPDATE playerstable SET score = " . $newscore . " WHERE name = '" . $username . "';";
mysqli_query($con , $updatequery) or die("7: Save query failed ");

echo "0";

?>