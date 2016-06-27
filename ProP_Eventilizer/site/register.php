<?php
include 'php/templates/first_part.php';

if(logged_in() === false) {
	if (empty($_POST) === false) {
		$firstname = $_POST['firstname'];
		$lastname = $_POST['lastname'];
		$phone = $_POST['telephone'];
		$email = $_POST['email'];
		$password = $_POST['password'];
		$repeat = $_POST['repeat'];

		if(empty($email) === false && empty($password) === false && empty($firstname) === false && empty($lastname) === false && empty($phone) === false) {
			if(preg_match('/\\s/', $firstname) == true) {
				$errors[] = 'First name cannot contain any spaces';
			}
			if(preg_match('/\\s/', $lastname) == true) {
				$errors[] = 'Last name cannot contain any spaces';
			}
			if(strlen($password) < 6) {
				$errors[] = 'Password must be longer than 6 charecters';
			}
			if($password !== $repeat) {
				$errors[] = 'Your passwords do not match';
			}
			if(filter_var($email, FILTER_VALIDATE_EMAIL) === false) {
				$errors[] = 'Invalid email';
			}
			if(email_exists($email) === true) {
				$errors[] = 'E-mail is already being used';
			}
			if(strlen($phone) > 13) {
				$errors[] = 'Please enter a valid number';
			}
		} else {
			$errors[] = 'All fields are required';
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
		<a href="login.php" class="style"><li>Log in</li></a>';
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
				<?php 
				if(empty($errors) === true &&  empty($_POST) === false) {
					register($email, $password, $firstname, $lastname, $phone);
					header('Location: home.php?action=registered');
				} else {
					if(empty($errors) === false){
						$string = '';
						foreach ($errors as &$value) {
							$string .= $value . '<br><br>';
						}
						echo "<script>toastr['error']('" . $string . "', '', {'progressBar': 'true', 'timeOut': '100000'});</script>";
					}
				}
				?>
				<form id="input-form" method="post" action="register.php">
					<h1>First name:</h1>
					<input type="text" name="firstname">
					<h1>Last name:</h1>
					<input type="text" name="lastname">
					<h1>Password:</h1>
					<input type="password" name="password">
					<h1>Repeat password:</h1>
					<input type="password" name="repeat">
					<h1>E-mail:</h1>
					<input type="text" name="email">
					<h1>Telephone:</h1>
					<input type="text" name="telephone">
					<input type="submit" value="Register" id="submit-btn">
				</form>
			</div>

			<?php
			include 'php/templates/second_part.php';
			?>