

//Fetching user input credentials
function LoginUser(){
    
    var loginData = new Object();
    var isEmailAvaialable = false;
    var isPasswordAvailable = false;
    
    var Email = document.getElementById("email");
    var Password = document.getElementById("password");
    

    if (
      isNullOrEmpty(Email, 'emError', 'Email')){
        isEmailAvaialable = true;
        loginData.Email = Email.value;
        
      }
      else{
        console.log("Email is empty!");
      }

      if(isNullOrEmpty(Password, "psError", "Password")){
        isPasswordAvailable = true;
        loginData.Passwordhash = CryptoJS.MD5(Password.value).toString();
      }
      else{
        console.log("Password is empty!");
      }  

        $.get('https://www.cloudflare.com/cdn-cgi/trace', function(data) {
        // Convert key-value pairs to JSON
        // https://stackoverflow.com/a/39284735/452587
        data = data.trim().split('\n').reduce(function(obj, pair) {
          pair = pair.split('=');
          return obj[pair[0]] = pair[1], obj;
        }, {});
        console.log(data);
        loginData.IPAddress = data.ip;
        loginData.Country = data.loc;

      });

      if(isEmailAvaialable && isPasswordAvailable){
      
        var validateRequest = new XMLHttpRequest();  

        validateRequest.open('GET', 'http://20.211.42.249:59413/api/isCitizen?' + "email="+loginData.Email);
        
        // after loading this request
        validateRequest.onload = function() {

          // Lets try to look and see wether the user is successfullyt registred in the server
          var response = validateRequest.response;
          var parsedData = JSON.parse(response);
          console.log(parsedData);

          // This request will return with the user object of there is a user with that
          // National ID
          // so, if there is more than 0 that means a object is returned from the server
          // If that happens, redirect the user to dashboard
          if (parsedData> 0){
            console.log("is citizen!");
          }
        };

        validateRequest.send();
        
      }
      else {
        console.log("Data is not ready!");
      }
      


}


// gets all the error message paragraphs
var errorMessages = document.getElementsByClassName("error");

// this will set all the error message paragraphs to display none 
// to make the not visible 
for (let i = 0; i < errorMessages.length; i++) {
  errorMessages[i].style.display = "none";
}



// First as same as the aboue function, this will check wether the email is null or empty
// if it is not empty, 
// Will check wether the email is in correct format or not using the regex string. 
// if it is empty or if it is not in the correct format, this will who the related error paragraph
function validateEmail(element, errorID) {
    if (element.value == null || element.value.trim() == "") { // If it is empty
      var errorV = document.getElementsByClassName(errorID)[0];
      errorV.innerHTML = "Email Cannot Be Empty";
      errorV.style.display = "block";
    } else { 
      const emailREGX = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  
      var isValid = emailREGX.test(String(element.value).toLowerCase());
  
      if (isValid) { // if it is in the correct format
        var errorV = document.getElementsByClassName(errorID)[0];
        errorV.style.display = "none";
        return true;
      } else { // it is not valid
        var errorV = document.getElementsByClassName(errorID)[0];
        errorV.innerHTML = "Please enter a valid email";
        errorV.style.display = "block";
      }
    }
  
    return false;
  }

  
  // This function will check wether the input value is null or empty
// and set the currosponding error message paragraps to show it is empty
// element = input control
// ErrorID = Uniques class name for the Error Display Paragraph
// ErrorText = What is the control abaut
// Sample Error Message = First Name Cannot Be Empty
function isNullOrEmpty(element, errorID, errorText) {
    if (element.value == null || element.value.trim() == "") {
      var errorV = document.getElementsByClassName(errorID)[0];
      errorV.innerHTML = errorText + " Cannot Be Empty";
      errorV.style.display = "block";
    } else {
      var errorV = document.getElementsByClassName(errorID)[0];
      errorV.style.display = "none";
      return true;
    }
  
    return false;
  }

