//Fetching user input credentials
function LoginUser(){
    var Email = document.getElementById("email").value;
    var Password = document.getElementById("password").value;
    
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

      //Checking whether entered citizen is registered or not
      if(isEmailAvaialable && isPasswordAvailable){
      
        var isCitizenRequest = new XMLHttpRequest();  

        isCitizenRequest.open('GET', 'http://20.211.42.249:59413/api/isCitizen?' + "email="+loginData.Email);
        
        // after loading this request
        isCitizenRequest.onload = function() {

          // Lets try to look and see wether the user is successfullyt registred in the server
          var response = isCitizenRequest.response;
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

        isCitizenRequest.send();
        
      }
      else {
        console.log("Data is not ready!");
      }

      //Checking whether entered officer is registered or not
      if(isEmailAvaialable && isPasswordAvailable){
      
        var isOfficerRequest = new XMLHttpRequest();  

        isOfficerRequest.open('GET', 'http://20.211.42.249:59413/api/isOfficer?' + "email="+loginData.Email);
        
        // after loading this request
        isOfficerRequest.onload = function() {

          // Lets try to look and see wether the user is successfullyt registred in the server
          var response = isOfficerRequest.response;
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

        isOfficerRequest.send();
        
      }
      else {
        console.log("Data is not ready!");
      }

      //Checking whether entered commpany is registered or not
      if(isEmailAvaialable && isPasswordAvailable){
      
        var isCommpanyRequest = new XMLHttpRequest();  

        isCommpanyRequest.open('GET', 'http://20.211.42.249:59413/api/isCommpany?' + "email="+loginData.Email);
        
        // after loading this request
        isCommpanyRequest.onload = function() {

          // Lets try to look and see wether the user is successfullyt registred in the server
          var response = isCommpanyRequest.response;
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

        isCommpanyRequest.send();
        
      }
      else {
        console.log("Data is not ready!");
      }

      
      


}




