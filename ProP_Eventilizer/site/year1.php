<?php
include 'php/templates/first_part.php';
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
  <div id="dropdown"><li class="current">Media</li>
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
      <article id="cc-slider">
        <input checked="checked" name="cc-slider" id="slide1" type="radio">
        <input name="cc-slider" id="slide2" type="radio">
        <input name="cc-slider" id="slide3" type="radio">
        <input name="cc-slider" id="slide4" type="radio">
        <input name="cc-slider" id="slide5" type="radio">
        <div id="cc-slides">
          <div id="overflow">
            <div class="inner">
              <article>
                <img src="pics/img6.jpg"> 
              </article>
              <article>          
                <img src="pics/img7.jpg">
              </article>
              <article>          
                <img src="pics/img8.jpg">
              </article>
              <article>          
                <img src="pics/img9.jpg">
              </article>
              <article>          
                <img src="pics/img10.jpg">
              </article>
            </div>
          </div>
        </div>
        <div id="controls">
          <label for="slide1"></label>
          <label for="slide2"></label>
          <label for="slide3"></label>
          <label for="slide4"></label>
          <label for="slide5"></label>
        </div>
      </article>

      <?php
      include 'php/templates/second_part.php';
      ?>