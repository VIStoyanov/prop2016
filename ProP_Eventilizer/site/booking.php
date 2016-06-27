<?php
include 'php/templates/first_part.php';
if(isset($_SESSION['client']) == false || $_SESSION['booked'] == '1') {
	header('Location: home.php?action=noAccess');
}

$email = $_SESSION['client'];
if(empty($_POST) == false) {
	if($_POST['spot'] == 1) {
		if($_POST['decision'] == 'friend') {
			$friendEmail = $_POST['friend'];
			if(bookFriend($email, $friendEmail) == false) {
				echo "<script>toastr.error('No user with that e-mail or this user doesn\'t own a camping spot!');</script>";
			} else if(bookFriend($email, $friendEmail) == 'moreThan5') {
				header('Location: home.php?action=moreThan5');
			} else {
				$_SESSION['booked'] = '1';
				header('Location: home.php?action=booked');
			}
		} 
		if($_POST['decision'] == 'reserve') {
			$campSpot = $_POST['camping-spot'];
			if(bookCamp($email, $campSpot)) {				
				$_SESSION['booked'] = '1';
				header('Location: home.php?action=booked');
			}
		}
	} else {
		book($email, 55);
		$_SESSION['booked'] = '1';
		header('Location: home.php?action=booked');
	}
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
			<a href="booking.php" class="style"><li class="current">Bookings</li></a>';
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
			<div class="hide" id="this-one">
				<img src="pics/location.png" alt="Free camping spots" class="hide the-other-one">
			</div>
			<form id="purchase" action="booking.php" method="post">
				<div class="ticket">					
					<h1>Buy a ticket</h1>
					<div class="noKur">
						<ul>
							<li class="includes topSpace">The ticket is valid for the whole weekend!</li>
							<li class="includes">Access to the event and all the stages!</li>
							<li class="includes">Free internet access!</li>
							<li class="includes">Event account through which you pay at shops!</li>
							<li class="includes">Put money into your event account on the event!</li>
							<li class="includes">No cards accepted on the event!</li>
						</ul>						
						
						<h1 id="camp-style">Camping</h1>

						<h1>Do you want a camping spot?</h1>
						<ul>
							<li class="camp-price">+20€ if you are a guest</li>
							<li class="camp-price">+30€ if you reserve a camp spot</li>
						</ul>						
						<label><input type="checkbox" id="spot" name="spot" value="0">Yes, I want a camping spot!</label>
						<p id="total">Total: <span>55</span>€</p>
					</div>
				</div>
				<div class="camping">
					<div class="hide">
						<h2>Reserve a camp or be a guest</h2>
						
						<div id="box1">
							<label><input type="radio" name="decision" value="friend" id="75">I have a friend with a reservation</label>
							<p>Provide your friend's e-mail</p>
							<input type="text" name="friend">
						</div>
						<div id="box2">
							<label><input type="radio" name="decision" value="reserve" id="85">I want to reserve a spot!</label>
							<select id="select" name="camping-spot">
								<?php
								foreach (free_spots() as $key => $value) {
									echo "<option value=\"" . $value . "\">Camping spot №" . $value . "</option>";
								}
								?>
							</select>
							<p>Already booked tent spots won't be displayed</p>
							<a id="toggle"><p>Click here to see the camping spots</p></a>
						</div>
					</div>
				</div>
				<div class="buy">
					<input type="submit" value="Buy ticket" id="btn">
				</div>
			</form>
			<?php
			include 'php/templates/second_part.php';
			?>
