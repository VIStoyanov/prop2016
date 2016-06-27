<?php
include 'php/templates/first_part.php';

if(logged_in() === false) {
	if (empty($_POST) === false) {
		$email = $_POST['email1'];
		$password = $_POST['password1'];

		if(empty($email) === true || empty($password) === true) {
			$errors[] = 'E-mail and password are required';
		}else if(email_exists($email) === false) {
			$errors[] = 'Invalid e-mail or password';
		} else if (activated($email)) {
			$login = login($email, $password);
			if(login($email, $password) === false) {
				$errors[] = 'Invalid e-mail or password';
			} else {
				$_SESSION['client'] = login($email, $password);
				if(hasTicket($email)) {
					$_SESSION['booked'] = '1';
				}
				header('Location: home.php?action=loggedIn');
				exit();
			}
		} else {
			$errors[] = 'Account is not activated! Please check your e-mail!';
		}
	} 
} else {
	$errors[] = 'You are already logged in';
}
?>

<ul id="navigation">
	<a href="home.php" class="style"><li>Home</li></a>
	<a href="location.php" class="style"><li>Location</li></a>
	<a href="contact.php" class="style"><li>Contact</li></a>
	<?php
	if(!isset($_SESSION['client'])) {
		echo '<a href="register.php" class="style"><li>Register</li></a>
		<a href="login.php" class="style"><li class="current">Log in</li></a>';
	} else {
		if(isset($_SESSION['booked'])) {
			echo '<a href="logout.php" class="style"><li>Log out</li></a>
			<a href="social.php" class="style"><li>Social</li></a>';
		} else {
			echo '<a href="logout.php" class="style"><li>Log out</li></a>
			<a href="booking.php" class="style"><li>Bookings</li></a>';
		}						
	}
	?>
	<div id="dropdown"><li>Media</li>
		<div class="dropdown-content">
			<a href="year.php" class="hidden">2014</a>
			<a href="year1.php" class="hidden">2015</a>
		</div>
	</div>
</ul>
</div>
<div id="content">
	<div id="heading">
		<img src="pics/title.png" alt="Festival title">
	</div>
	<div id="page-content">
		<div id="remove">
			<div class="form">
				<form id="input-form" method="post" action="login.php">
					<?php 
					if(empty($errors) === false){
						$string = '';
						foreach ($errors as &$value) {
							$string .= $value;
						}
						echo "<script>toastr['error']('" . $string . "', '', {'progressBar': 'true', 'timeOut': '100000'});</script>";
					}
					?>
					<h1>E-mail</h1>
					<input type="text" name="email1">
					<h1>Password</h1>
					<input type="password" name="password1">
					<input type="submit" value="Log in" id="submit-btn">
				</form>
			</div>

			<?php
			include 'php/templates/second_part.php';
			?>