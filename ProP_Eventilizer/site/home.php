<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8">
	<title>The No Name Festival</title>
	<link rel="stylesheet" href="css/main.css">
	<link rel="stylesheet" href="css/media.css">
	<link rel="stylesheet" href="css/year.css">
	<link rel="stylesheet" href="css/location.css">
	<link rel="stylesheet" href="css/contact.css">
	<link rel="stylesheet" href="css/register.css">
	<link rel="stylesheet" href="css/booking.css">
	<link rel="stylesheet" href="js/toasters/nuget/content/content/toastr.css">
	<!-- <link rel="stylesheet" href="css/social.css"> -->
</head>
<body>
	<script src="js/jquery-2.2.1.min.js"></script>
	<script src="js/toasters/nuget/content/scripts/toastr.min.js"></script>
	<script src="js/ajax.js"></script>
	<?php 
	include 'php/validation/core/init.php';
	if(isset($_GET['action'])) {
		if($_GET['action'] == 'loggedIn') {
			echo "<script>toastr.success('You logged in successfully', '', {'progressBar': 'true'});</script>";
		}
		if($_GET['action'] == 'activated') {
			echo "<script>toastr.success('You activated your account successfully! You are now logged in!', '', {'progressBar': 'true'});</script>";
		}
		if($_GET['action'] == 'registered') {
			echo "<script>toastr.info('You registered successfully! Please check your e-mail and activate your account!', '', {'progressBar': 'true'});</script>";
		}
		if($_GET['action'] == 'logout') {
			echo "<script>toastr.warning('You logged out successfully!', '', {'progressBar': 'true'});</script>";
		}
		if($_GET['action'] == 'booked') {
			echo "<script>toastr.success('You successfully purchased your ticket! Please check your e-mail(be sure to check spam mail too!)', '', {'progressBar': 'true'});</script>";
		}
		if($_GET['action'] == 'noAccess') {
			echo "<script>toastr.warning('You have no access here!', '', {'progressBar': 'true'});</script>";
		}
		if($_GET['action'] == 'confirmed') {
			echo "<script>toastr.success('Your friend will be joining you in the tent!', '', {'progressBar': 'true'});</script>";
		}
		if($_GET['action'] == 'moreThan5') {
			echo "<script>toastr.error('Your friend already has 5 guests, which is the maximum!', '', {'progressBar': 'true'});</script>";
		}
	}
	?>
	<div id="main_container">
		<div id="nav-bar">
			<div id="logo"><img src="pics/logo.png" alt="Company logo"></div>
			<ul id="navigation">
				<a href="home.php" class="style"><li class="current">Home</li></a>
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
					<div class="align">
						<div id="clock">
							<h1><span>No Name</span> starts in:</h2>
								<h1 id="display-clock"></h1>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div id="footer">
				<footer>
					<p>&copy; 2016 Eventilizer ALL RIGHTS RESERVED</p>
				</footer>
			</div>
		</div>
		<script src="js/clock.js"></script>
	</body>
	</html>