<?php
include 'php/templates/first_part.php';
?>

<ul id="navigation">
	<a href="home.php" class="style"><li>Home</li></a>
	<a href="location.php" class="style"><li class="current">Location</li></a>
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
			<div id="location">
				<img src="pics/locationmap.png"/>
			</div>
			<div id="iframe">
				<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2410.0142522552615!2d6.722913316072692!3d52.84012822020715!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x47b7e010cfe19b33%3A0xe1bd06427483d7af!2sMolecaten+Park+Kuierpad!5e0!3m2!1sbg!2snl!4v1462831191433" frameborder="0" style="border:0" allowfullscreen></iframe>
			</div>

			<?php
			include 'php/templates/second_part.php';
			?>