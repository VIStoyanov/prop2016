<?php
if(!isset($_GET['token'])){
	header('Location: home.php?action=noAccess');
} else if($_GET['token'] != '') {
	header('Location: home.php?action=noAccess');
} else {
	
}
include 'php/templates/first_part.php';
?>