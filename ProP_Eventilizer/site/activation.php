<?php
include 'php/validation/core/init.php';

$mail = check($_GET['email']);
$hash = check($_GET['code']);

if(email_exists($mail)) {
	if(!activated($mail)) {
		if(confirm_hash($hash)) {
			mysql_query("UPDATE `client` SET `activated` = '1' WHERE `e-mail` = '$mail'");
			$_SESSION['client'] = $mail;
			header('Location: home.php?action=activated');
		} else {
			echo 'Invalid data!';
		}
	} else {
		echo 'Account is already activated!';
	}
} else {
	echo 'Invalid data!';
}
?>