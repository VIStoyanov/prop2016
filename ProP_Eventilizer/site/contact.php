<?php
include 'php/templates/first_part.php';
?>
<ul id="navigation">
	<a href="home.php" class="style"><li>Home</li></a>
	<a href="location.php" class="style"><li>Location</li></a>
	<a href="contact.php" class="style"><li class="current">Contact</li></a>
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
			<div id="contact">
				<div id="contactinfo">
					<div id="con-title"><h1>Contact Information</h1></div>

					<div id="con-info"><p> <br>  	<ins>e-mail</ins>: eventilizer@outlook.com <br> <br> 
						<ins>address</ins>: 91017 Province of Trapani,
						Italy<br><br> 
						<ins>telephone</ins>:+39 700133769</p></div>

						<div id="con-img"><img id="picture" src="pics/contact.png"/></div>
					</div>
				</div>

				<?php
				include 'php/templates/second_part.php';
				?>