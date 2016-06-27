<?php
require 'php/mail/PHPMailerAutoload.php';
#fold all Ctrl+K+1 unfold all Ctrl+K+J

function logged_in() {
	if(isset($_SESSION['client_id'])) {
		return true;
	} else {
		return false;
	}
}

function email_exists($email) {
	$email = check($email);
	$query = mysql_query("SELECT COUNT(`Client_id`) FROM `client` WHERE `e-mail` = '$email'");
	return (mysql_result($query, 0) == 1) ? true : false;
}

function register($email, $password, $f_name, $l_name, $phone) {
	$email = check($email);
	$password = check($password);
	$f_name = check($f_name);
	$l_name = check($l_name);
	$phone = check($phone);
	$hash = password_hash($password, PASSWORD_DEFAULT);
	$code = password_hash($email . microtime(), PASSWORD_DEFAULT);

	sendMail($email, $f_name, $code);

	mysql_query("INSERT INTO `Security` (`token`) VALUES ('$code');");

	mysql_query("INSERT INTO `client` (`Client_id`, `First_name`, `Last_name`, `Balance`, `e-mail`, `Password`, `Telephone`, `RFID`, `Client_Client_id`, `activated`) VALUES (NULL, '$f_name', '$l_name', '0', '$email', '$hash', '$phone', NULL, NULL, '0');");
}

function registerOnEntrance($email, $password, $f_name, $l_name, $phone) {
	$email = check($email);
	$password = check($password);
	$f_name = check($f_name);
	$l_name = check($l_name);
	$phone = check($phone);
	$hash = password_hash($password, PASSWORD_DEFAULT);
	$code = password_hash($email . microtime(), PASSWORD_DEFAULT);

	mysql_query("INSERT INTO `client` (`Client_id`, `First_name`, `Last_name`, `Balance`, `e-mail`, `Password`, `Telephone`, `RFID`, `Client_Client_id`, `activated`) VALUES (NULL, '$f_name', '$l_name', '0', '$email', '$hash', '$phone', NULL, NULL, '1');");
}

function client_id($email) {
	$email = check($email);
	$query = mysql_query("SELECT `Client_id` FROM `client` WHERE `e-mail` = '$email'");
	return mysql_result($query, 0, 'Client_id');
}

function client_f_name($email) {
	$email = check($email);
	$query = mysql_query("SELECT `First_name` FROM `client` WHERE `e-mail` = '$email'");
	return mysql_result($query, 0, 'First_name');
}

function client_l_name($email) {
	$email = check($email);
	$query = mysql_query("SELECT `Last_name` FROM `client` WHERE `e-mail` = '$email'");
	return mysql_result($query, 0, 'Last_name');
}

function login($email, $password) {
	$email = check($email);
	$password = check($password);
	$hash = get_password($email);

	if (password_verify($password,$hash)){
		return $email;
	} else {
		return false;
	}
}

function get_password($email){
	$query = mysql_query("SELECT `Password` FROM `client` WHERE `e-mail` = '$email'");
	return mysql_result($query, 0);
}

function confirm_hash($token){
	$query = mysql_query("SELECT COUNT(*) FROM `Security` WHERE `token` = '$token'");
	return (mysql_result($query, 0) > 0) ? true : false;
}

function free_spots() {
	$free = array();
	$lenght = mysql_result(mysql_query("SELECT COUNT(*) FROM `tent` WHERE `Client_Client_id` IS NULL"), 0);
	$query = mysql_query("SELECT `Tent_spot_id` FROM `tent` WHERE `Client_Client_id` IS NULL");

	for ($counter = 0; $counter < $lenght; $counter++) {
		$value = mysql_result($query, $counter);
		$free[] = $value;
	}
	return $free;
}

function sendMail($email, $name, $code) {
	$link = 'http://localhost/site/activation.php?email=' . $email . '&code=' . $code;
	$mail = new PHPMailer;

	//$mail->SMTPDebug = 3;                               	// Enable verbose debug output

	$mail->isSMTP();                                      // Set mailer to use SMTP
	$mail->Host = 'smtp-mail.outlook.com';  // Specify main and backup SMTP servers
	$mail->SMTPAuth = true;                               // Enable SMTP authentication
	$mail->Username = 'eventilizer@outlook.com';                 // SMTP username
	$mail->Password = 'q1w2e3r4t5';                           // SMTP password
	$mail->SMTPSecure = 'tls';                            // Enable TLS encryption, `ssl` also accepted
	$mail->Port = 587;                                    // TCP port to connect to

	$mail->setFrom('eventilizer@outlook.com', 'Eventilizer');
	$mail->addAddress($email, $name);     // Add a recipient
	//$mail->addAddress('ellen@example.com');               // Name is optional
	//$mail->addReplyTo('info@example.com', 'Information');
	//$mail->addCC('cc@example.com');
	//$mail->addBCC('bcc@example.com');

	//$mail->addAttachment('/var/tmp/file.tar.gz');         // Add attachments
	//$mail->addAttachment('/tmp/image.jpg', 'new.jpg');    // Optional name
	$mail->isHTML(true);                                  // Set email format to HTML

	$mail->Subject = 'Account activation';
	$mail->Body    = 'Hello ' . $name . ', <br/><br/> Please follow this <a href="' . $link . '">link</a> to activate your account! <br/><br/>Thank you :)';
	$mail->AltBody = 'This is the body in plain text for non-HTML mail clients';

	if(!$mail->send()) {
		echo 'Message could not be sent.';
		echo 'Mailer Error: ' . $mail->ErrorInfo;
	} else {
		echo 'Message has been sent';
	}
}


function paymentExists($payment) {
	$query = "SELECT COUNT(*) FROM `Transaction` WHERE `Transaction_id` = '$payment';";

	return (mysql_result(mysql_query($query), 0) == 1) ? true : false;
}

function paymentId() {
	$chars = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ';
	$payment = '';

	for ($i=0; $i < 20; $i++) { 
		$payment .= $chars[mt_rand(0, strlen($chars) - 1)];
	}

	if(!paymentExists($payment)){
		return $payment;
	} else {
		return paymentId();
	}
}

function activated($email) {
	$query = mysql_query("SELECT `activated` FROM `client` WHERE `e-mail` = '$email'");
	if(mysql_result($query, 0) == 1) {
		return true;
	} else {
		return false;
	}
}

function book($email, $money) {
	$email = check($email);
	$id = client_id($email);
	$f_name = client_f_name($email);
	$l_name = client_l_name($email);

	if(!hasTicket($email)){
		$barcode = generateCode();
		sendBarcode($email, $f_name, $l_name, $barcode);
		$payment = paymentId();

		mysql_query("INSERT INTO `booking` (`Ticket number`, `Client_Client_id`, `Price`) VALUES (NULL, '$id', '$money')");

		$ticket = mysql_result(mysql_query("SELECT `Ticket number` FROM `booking` WHERE `Client_CLient_id` = '$id'"), 0);

		mysql_query("UPDATE `client` SET `barcode` = '$barcode' WHERE `Client_id` = '$id'");

		mysql_query("INSERT INTO `transaction` (`Transaction_id`, `Client_Client_id`, `Amount`, `DateTime`, `Booking_Ticket number`) VALUES ('$payment', '$id', '$money', CURRENT_TIMESTAMP, '$ticket')");

		mysql_query("INSERT INTO `check` (`Client_Client_id`, `Timestamp`, `Type`) VALUES ('$id', CURRENT_TIMESTAMP, 'TOARRIVE');");
	} else {
		echo 'Already purchased ticket with that account';
	}
}

function hasTicket($email) {
	$query = mysql_query("SELECT COUNT(*) FROM `booking` WHERE `Client_Client_id` = (SELECT `Client_id` FROM `client` WHERE `e-mail` = '$email')");

	return (mysql_result($query, 0) == 1) ? true : false;
}

function hasCamp($email) {
	$query = mysql_query("SELECT COUNT(*) FROM `tent` WHERE `Client_Client_id` = (SELECT `Client_id` FROM `client` WHERE `e-mail` = '$email');");
	if(mysql_result($query, 0) > 0) {
		return true;
	} else {
		return false;
	}
}

function sendCampConfirm($emailGuest, $emailHost, $code) {
	$link = 'http://localhost/site/confirmation.php?emailGuest=' . $emailGuest . '&emailHost=' . $emailHost . '&code=' . $code;

	$hostName = client_f_name($emailHost);

	$name = client_f_name($emailGuest) . ' ' . client_l_name($emailGuest);

	$mail = new PHPMailer;

	//$mail->SMTPDebug = 3;                               	// Enable verbose debug output

	$mail->isSMTP();                                      // Set mailer to use SMTP
	$mail->Host = 'smtp-mail.outlook.com';  // Specify main and backup SMTP servers
	$mail->SMTPAuth = true;                               // Enable SMTP authentication
	$mail->Username = 'eventilizer@outlook.com';                 // SMTP username
	$mail->Password = 'q1w2e3r4t5';                           // SMTP password
	$mail->SMTPSecure = 'tls';                            // Enable TLS encryption, `ssl` also accepted
	$mail->Port = 587;                                    // TCP port to connect to

	$mail->setFrom('eventilizer@outlook.com', 'Evenetilizer');
	$mail->addAddress($emailHost, $name);     // Add a recipient
	//$mail->addAddress('ellen@example.com');               // Name is optional
	//$mail->addReplyTo('info@example.com', 'Information');
	//$mail->addCC('cc@example.com');
	//$mail->addBCC('bcc@example.com');

	//$mail->addAttachment('/var/tmp/file.tar.gz');         // Add attachments
	//$mail->addAttachment('/tmp/image.jpg', 'new.jpg');    // Optional name
	$mail->isHTML(true);                                  // Set email format to HTML

	$mail->Subject = 'Account activation';
	$mail->Body    = 'Hello ' . $hostName . ', <br/><br/> ' . $name . ' wants to join you in your tent. Please follow this <a href="' . $link . '">link</a> to confirm! <br/><br/>Thank you, <br/>Evenetilizer team';
	$mail->AltBody = 'This is the body in plain text for non-HTML mail clients';

	if(!$mail->send()) {
		echo 'Message could not be sent.';
		echo 'Mailer Error: ' . $mail->ErrorInfo;
	} else {
		echo 'Message has been sent';
	}
}

function hasMoreThan5($id) {
	$query = mysql_query("SELECT COUNT(*) FROM `client` WHERE `Client_Client_id` = '$id'");

	return (mysql_result($query, 0) >= 5) ? true : false;
}

function bookFriend($email, $friend) {
	$email = check($email);
	$friend = check($friend);

	$friendId = client_id($friend);
	if(!hasMoreThan5($friendId)) {
		if(email_exists($friend) && hasCamp($friend) && !hasTicket($email)) {		
			$id = client_id($email);
			$f_name = client_f_name($email);
			$l_name = client_l_name($email);
			$code = password_hash($email . microtime(), PASSWORD_DEFAULT);		
			sendCampConfirm($email, $friend, $code);

			$barcode = generateCode();
			sendBarcode($email, $f_name, $l_name, $barcode);

			$payment = paymentId();
			$payment2 = paymentId();

			mysql_query("INSERT INTO `Security` (`token`) VALUES ('$code');");

			mysql_query("INSERT INTO `booking` (`Ticket number`, `Client_Client_id`, `Price`) VALUES (NULL, '$id', 55)");

			$ticket = mysql_result(mysql_query("SELECT `Ticket number` FROM `booking` WHERE `Client_CLient_id` = '$id'"), 0);

			mysql_query("UPDATE `client` SET `barcode` = '$barcode' WHERE `Client_id` = '$id'");

			mysql_query("INSERT INTO `transaction` (`Transaction_id`, `Client_Client_id`, `Amount`, `DateTime`, `Booking_Ticket number`) VALUES ('$payment', '$id', 55, CURRENT_TIMESTAMP, '$ticket')");

			mysql_query("INSERT INTO `transaction` (`Transaction_id`, `Client_Client_id`, `Amount`, `DateTime`) VALUES ('$payment2', '$id', 20, CURRENT_TIMESTAMP)");

			mysql_query("INSERT INTO `check` (`Client_Client_id`, `Timestamp`, `Type`) VALUES ('$id', CURRENT_TIMESTAMP, 'TOARRIVE');");

			return true;
		} else {
			return false;
		}
	} else {
		return 'moreThan5';
	}
	
}

function confirmCamp($emailGuest, $emailHost) {
	$emailGuest = check($emailGuest);
	$emailHost = check($emailHost);

	$guest = client_id($emailGuest);
	$host = client_id($emailHost);

	mysql_query("UPDATE `client` SET `Client_Client_id` = '$host' WHERE `Client_id` = '$guest';");
}

function bookCamp($email, $spot) {
	$email = check($email);
	if(!hasTicket($email)){		
		$spot = check($spot);
		$id = client_id($email);
		$f_name = client_f_name($email);
		$l_name = client_l_name($email);

		$barcode = generateCode();
		sendBarcode($email, $f_name, $l_name, $barcode);

		$payment = paymentId();
		$payment2 = paymentId();


		mysql_query("INSERT INTO `booking` (`Ticket number`, `Client_Client_id`, `Price`) VALUES (NULL, '$id', 55)");

		mysql_query("UPDATE `tent` SET `Client_Client_id` = '$id' WHERE `Tent_spot_id` = '$spot';");

		$ticket = mysql_result(mysql_query("SELECT `Ticket number` FROM `booking` WHERE `Client_CLient_id` = '$id'"), 0);

		mysql_query("INSERT INTO `transaction` (`Transaction_id`, `Client_Client_id`, `Amount`, `DateTime`, `Booking_Ticket number`) VALUES ('$payment', '$id', 55, CURRENT_TIMESTAMP, '$ticket')");

		mysql_query("INSERT INTO `transaction` (`Transaction_id`, `Client_Client_id`, `Amount`, `DateTime`) VALUES ('$payment2', '$id', 30, CURRENT_TIMESTAMP)");

		mysql_query("UPDATE `client` SET `barcode` = '$barcode' WHERE `Client_id` = '$id'");

		mysql_query("INSERT INTO `check` (`Client_Client_id`, `Timestamp`, `Type`) VALUES ('$id', CURRENT_TIMESTAMP, 'TOARRIVE');");

		return true;
	} else {
		return false;
	}
}

function generateCode() {
	$code = rand(1000000000, 9999999999);
	$query = mysql_query("SELECT COUNT(*) FROM `client` WHERE `barcode` = '$code'");

	if(mysql_result($query, 0) == 1) {
		return $code = generateCode();
	} else {
		return $code;
	}
}

function sendBarcode($email, $f_name, $l_name, $code) {
	//generate the PDF file with the unique barcode
	$pdf = generatePDF($f_name, $l_name, $code);

	$mail = new PHPMailer;

	//$mail->SMTPDebug = 3;                               	// Enable verbose debug output

	$mail->isSMTP();                                      // Set mailer to use SMTP
	$mail->Host = 'smtp-mail.outlook.com';  // Specify main and backup SMTP servers
	$mail->SMTPAuth = true;                               // Enable SMTP authentication
	$mail->Username = 'eventilizer@outlook.com';                 // SMTP username
	$mail->Password = 'q1w2e3r4t5';                           // SMTP password
	$mail->SMTPSecure = 'tls';                            // Enable TLS encryption, `ssl` also accepted
	$mail->Port = 587;                                    // TCP port to connect to

	$mail->setFrom('eventilizer@outlook.com', 'Eventilizer');
	$mail->addAddress($email, $name);     // Add a recipient
	//$mail->addAddress('ellen@example.com');               // Name is optional
	//$mail->addReplyTo('info@example.com', 'Information');
	//$mail->addCC('cc@example.com');
	//$mail->addBCC('bcc@example.com');

	//$mail->addAttachment($pdf);         // Add attachments
	//$mail->addAttachment('C:/xampp/htdocs/site/php/validation/core/functions/logo.png');          //Оptional name
	$mail->addAttachment('tickets/' . $code . '.pdf');
	$mail->isHTML(true);                                  // Set email format to HTML

	$mail->Subject = 'Your ticket';
	$mail->Body    = 'Dear ' . $f_name . ' ' . $l_name . ', <br/><br/>Your ticket is attached to this e-mail. Please be sure to print it out and bring it at the entrance of the event along with your ID. <br/><br/>Best regards, <br/>Eventilizer team';
	$mail->AltBody = 'This is the body in plain text for non-HTML mail clients';

	if(!$mail->send()) {
		echo 'Message could not be sent.';
		echo 'Mailer Error: ' . $mail->ErrorInfo;
	} else {
		echo 'Message has been sent';
	}
}

function generatePDF($f_name, $l_name, $code) {
	require 'php/pdf/tcpdf/tcpdf.php';

	class MYPDF extends TCPDF {

		//Page header
		public function Header() {
			// Logo
			$image_file = K_PATH_IMAGES.'ticket_logo.jpg';
			$this->Image($image_file, 10, 10, 15, '', 'JPG', '', 'T', false, 300, '', false, false, 0, false, false, false);
			// Set font
			$this->SetFont('helvetica', 'B', 20);
			// Title
			$this->Cell(0, 15, 'Eventilizer', 0, false, 'C', 0, '', 0, false, 'M', 'M');
		}

		// Page footer
		public function Footer() {
			// Position at 15 mm from bottom
			$this->SetY(-15);
			// Set font
			$this->SetFont('helvetica', 'I', 8);
			// Page number
			$this->Cell(0, 10, 'Page '.$this->getAliasNumPage().'/'.$this->getAliasNbPages(), 0, false, 'C', 0, '', 0, false, 'T', 'M');
		}
	}

	// create new PDF document
	$pdf = new MYPDF(PDF_PAGE_ORIENTATION, PDF_UNIT, PDF_PAGE_FORMAT, true, 'UTF-8', false);

	// set document information
	$pdf->SetCreator(PDF_CREATOR);
	$pdf->SetAuthor('Eventilizer 2016');
	$pdf->SetTitle('Eventilizer');
	$pdf->SetSubject('Ticket');
	$pdf->SetKeywords('ticket, event, Eventilizer');

	// set default header data
	$pdf->SetHeaderData(PDF_HEADER_LOGO, PDF_HEADER_LOGO_WIDTH, PDF_HEADER_TITLE, PDF_HEADER_STRING);

	// set header and footer fonts
	$pdf->setHeaderFont(Array(PDF_FONT_NAME_MAIN, '', PDF_FONT_SIZE_MAIN));
	$pdf->setFooterFont(Array(PDF_FONT_NAME_DATA, '', PDF_FONT_SIZE_DATA));

	// set default monospaced font
	$pdf->SetDefaultMonospacedFont(PDF_FONT_MONOSPACED);

	// set margins
	$pdf->SetMargins(PDF_MARGIN_LEFT, PDF_MARGIN_TOP, PDF_MARGIN_RIGHT);
	$pdf->SetHeaderMargin(PDF_MARGIN_HEADER);
	$pdf->SetFooterMargin(PDF_MARGIN_FOOTER);

	// set auto page breaks
	$pdf->SetAutoPageBreak(TRUE, PDF_MARGIN_BOTTOM);

	// set image scale factor
	$pdf->setImageScale(PDF_IMAGE_SCALE_RATIO);

	// set some language-dependent strings (optional)
	if (@file_exists(dirname(__FILE__).'/lang/eng.php')) {
		require_once(dirname(__FILE__).'/lang/eng.php');
		$pdf->setLanguageArray($l);
	}

	// ---------------------------------------------------------

	// set font
	$pdf->SetFont('times', 'BI', 12);

	// add a page
	$pdf->AddPage('P', 'A6');

	// set some text to print
	$txt = '

	Mr/Mrs ' . $f_name . ' ' . $l_name . ' this is your ticket. Please be sure to print it out and present it with your ID card at the entrance of the event.

	Have fun,
	Eventilizer team';

	// print a block of text using Write()
	$pdf->Write(0, $txt, '', 0, 'C', true, 0, false, false, 0);

	$style = array(
		'position' => 'C',
		'align' => 'C',
		'stretch' => false,
		'fitwidth' => true,
		'cellfitalign' => '',
		'border' => false,
		'hpadding' => 'auto',
		'vpadding' => 'auto',
		'fgcolor' => array(0,0,0),
	    'bgcolor' => false, //array(255,255,255),
	    'text' => true,
	    'font' => 'helvetica',
	    'fontsize' => 8,
	    'stretchtext' => 4
	    );

	$pdf->Cell(0, 0, '', 0, 1);
	$pdf->write1DBarcode($code, 'C128', '', '', '', 18, 0.4, $style, 'N');

	// ---------------------------------------------------------

	//Close and output PDF document
	$pdf->Output('C:/xampp/htdocs/site/tickets/' . $code . '.pdf', 'F');
}

function bookOnEntrance($email, $password, $f_name, $l_name, $phone) {
	$email = check($email);
	$password = check($password);
	$f_name = check($f_name);
	$l_name = check($l_name);
	$phone = check($phone);
	$id = Client_id($email);

	registerOnEntrance($email, $password, $f_name, $l_name, $phone);

	book($email, 65);
}
?>