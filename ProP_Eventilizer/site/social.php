<?php
include 'php/templates/first_part.php';
if(isset($_SESSION['booked']) == false ) {
	header('Location: home.php?action=noAccess');
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
			<a href="social.php" class="style"><li class="current">Social</li></a>';
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
			<div class="twitter">
				<div class="moveAround">
					<a class="twitter-timeline"  href="https://twitter.com/search?q=%40EventilizerInc" data-widget-id="747410326438612992">Tweets about @EventilizerInc</a>
					<script>!function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0],p=/^http:/.test(d.location)?'http':'https';if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src=p+"://platform.twitter.com/widgets.js";fjs.parentNode.insertBefore(js,fjs);}}(document,"script","twitter-wjs");</script>				
				</div>
			</div>

<?php
include 'php/templates/second_part.php';
?>