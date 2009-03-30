function alertName(ov){
var studentName = document.nameForm.nameField.value // store text field value in variable

   if(studentName != "") {// if field is not empty
   alert("Hello, " + studentName + "! So nice to meet you.") // concatenate text field value with this string
   }
   else{
   alert("Please Enter Your Name!")
   }
   ov.strBlankMsg = "error message"
}