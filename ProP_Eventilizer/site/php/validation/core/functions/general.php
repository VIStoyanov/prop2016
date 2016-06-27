<?php
function check($data) {
	return mysql_real_escape_string($data);
}

function output_errors($errors) {
	return '<ul class="errors-list"><li>' . implode('</li><li>', $errors) . '</li></ul>';
}
?>