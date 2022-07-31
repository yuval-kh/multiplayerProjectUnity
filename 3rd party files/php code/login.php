<?php

$con  = mysqli_connect('localhost', 'root', 'root', 'players');

if(mysqli_connect_errno()){
    echo  "1";
    exit();
}

$username = $_POST["name"];
$password = $_POST["password"];

$namecheckquery = "SELECT name, salt, hash  FROM playerstable WHERE name='" .$username ."';" ;

$namecheck = mysqli_query($con , $namecheckquery) or die("2: Name check query failed");

if (mysqli_num_rows($namecheck) != 1){
    echo "5: Either no user with name, or more then one";
    exit();
}

$existinginfo = mysqli_fetch_assoc($namecheck);
$salt = $existinginfo["salt"];
$hash = $existinginfo["hash"];

$loginhash = crypt($password, $salt);
if($hash != $loginhash){
    echo "6: Incorrect password";
    exit();
}

echo "0\t";

?>