<?php

$con  = mysqli_connect('localhost', 'root', 'root', 'players');

if(mysqli_connect_errno()){
    echo  "1";
    exit();
}

$username = $_POST["name"];

$namecheckquery = "SELECT name, defaultPlayerSpeed , defaultEnemyHealth , defaultEnemySpeed ,
 defaultEnemyDamage , defaultActivationDistance , defaultFirstRound , defaultMazeSize  FROM playerstable WHERE name = '" .$username . "' ;" ;

$namecheck = mysqli_query($con , $namecheckquery) or die("2: Name check query failed");

if (mysqli_num_rows($namecheck) != 1){
    echo "5: Either no user with name, or more then one";
    exit();
}
$existinginfo = mysqli_fetch_assoc($namecheck);

echo "0|" . $existinginfo["defaultPlayerSpeed"]. "|"  . $existinginfo["defaultEnemyHealth"]. "|" 
.$existinginfo["defaultEnemySpeed"]. "|" .$existinginfo["defaultEnemyDamage"]. "|" .$existinginfo["defaultActivationDistance"]. "|" 
.$existinginfo["defaultFirstRound"]. "|" .$existinginfo["defaultMazeSize"]. "|";

?>