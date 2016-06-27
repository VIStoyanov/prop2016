<?php
$connection_error = 'No connection to the database';
mysql_connect('localhost', 'root', '') or die($connection_error);
mysql_select_db('mydb') or die ($connection_error);
?>