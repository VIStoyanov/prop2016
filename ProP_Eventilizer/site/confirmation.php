<?php
include 'php/validation/core/init.php';

$emailGuest = check($_GET['emailGuest']);
$emailHost = check($_GET['emailHost']);
$code = check($_GET['code']);

if(email_exists($emailGuest) && email_exists($emailHost)) {
	if(activated($emailGuest) && hasCamp($emailHost)) {
		if(confirm_hash($code)) {
			confirmCamp($emailGuest, $emailHost);
			$_SESSION['client'] = $mail;
			header('Location: home.php?action=confirmed');
		} else {
			echo 'Invalid data!';
		}
	} else {
		echo 'Invalid data!';
	}
} else {
	echo 'Invalid data!';
}
?>