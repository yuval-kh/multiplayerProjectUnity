<?php

$con  = mysqli_connect('localhost', 'root', 'root', 'players');

if(mysqli_connect_errno()){
    echo  "1";
    exit();
}

$username = $_POST["name"];
$PlayerSpeed = $_POST["PlayerSpeed"];
$EnemyHealth = $_POST["EnemyHealth"];
$EnemySpeed = $_POST["EnemySpeed"];
$EnemyDamage = $_POST["EnemyDamage"];
$ActivationDistance = $_POST["ActivationDistance"];
$FirstRound = $_POST["FirstRound"];
$MazeSize = $_POST["MazeSize"];

$namecheckquery = "SELECT name FROM playerstable WHERE name='" .$username ."';" ;
$namecheck = mysqli_query($con , $namecheckquery) or die("2: Name check query failed");
if (mysqli_num_rows($namecheck) != 1){
    echo "5: Either no user with name, or more then one";
    exit();
}

$updatequery = "UPDATE playerstable SET defaultPlayerSpeed = " . $PlayerSpeed . " WHERE name = '" . $username . "';";
mysqli_query($con , $updatequery) or die("7: Save query failed, PlayerSpeed ");

$updatequery = "UPDATE playerstable SET defaultEnemyHealth = " . $EnemyHealth . " WHERE name = '" . $username . "';";
mysqli_query($con , $updatequery) or die("7: Save query failed, EnemyHealth ");

$updatequery = "UPDATE playerstable SET defaultEnemySpeed = " . $EnemySpeed . " WHERE name = '" . $username . "';";
mysqli_query($con , $updatequery) or die("7: Save query failed, EnemySpeed ");

$updatequery = "UPDATE playerstable SET defaultEnemyDamage = " . $EnemyDamage . " WHERE name = '" . $username . "';";
mysqli_query($con , $updatequery) or die("7: Save query failed, EnemyDamage ");

$updatequery = "UPDATE playerstable SET defaultActivationDistance = " . $ActivationDistance . " WHERE name = '" . $username . "';";
mysqli_query($con , $updatequery) or die("7: Save query failed, ActivationDistance ");

$updatequery = "UPDATE playerstable SET defaultFirstRound = " . $FirstRound . " WHERE name = '" . $username . "';";
mysqli_query($con , $updatequery) or die("7: Save query failed, FirstRound ");

$updatequery = "UPDATE playerstable SET defaultMazeSize = " . $MazeSize . " WHERE name = '" . $username . "';";
mysqli_query($con , $updatequery) or die("7: Save query failed, MazeSize ");


echo "0";

?>