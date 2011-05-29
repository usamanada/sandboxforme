<?php
 $to = "enquiries@forbiddencupcakes.com.au";
 $from = "info@forbiddencupcakes.com.au";
 $message = $_POST["message"];
 $headers = "From:" . $from;
 mail($to,$subject,$message,$headers);
 echo "Mail Sent.";
 ?> 