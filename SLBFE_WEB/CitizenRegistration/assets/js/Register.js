//This gets the phone input to assign the country selection
var input = document.querySelector("#phone");
// This will set the country flags and countri codes inside the input element
// Used geoIpLookpu functions to autometically select the Country using IP
var phone = intlTelInput(input, {
  initialCountry: "auto",
  geoIpLookup: function(success, failure) {
    $.get("https://ipinfo.io", function() {}, "jsonp").always(function(resp) {
      var countryCode = resp && resp.country ? resp.country : "us";
      success(countryCode);
    });
  }
});

// this will trigger when the country is chsanged
input.addEventListener("countrychange", function() {});

// gets all the error message paragraphs
var errorMessages = document.getElementsByClassName("error");

// this will set all the error message paragraphs to display none 
// to make the not visible 
for (let i = 0; i < errorMessages.length; i++) {
  errorMessages[i].style.display = "none";
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

function validateNID(element, errorID) {
  if (element.value == null || element.value.trim() == "") {
    var errorV = document.getElementsByClassName(errorID)[0];
    errorV.innerHTML = "National ID Cannot Be Empty";
    errorV.style.display = "block";
  } else {
    const nidREGX = /^[0-9]{12}$|^([0-9]{9}v)$/gi;

    var isValid = nidREGX.test(String(element.value).toLowerCase());

    if (isValid) {
      var errorV = document.getElementsByClassName(errorID)[0];
      errorV.style.display = "none";
      return true;
    } else {
      var errorV = document.getElementsByClassName(errorID)[0];
      errorV.innerHTML = "Please enter a valid National ID Number";
      errorV.style.display = "block";
    }
  }
  return false;
}

function validatePhone(elementID, errorID) {
  var element = document.getElementById(elementID);
  console.log(phone.getSelectedCountryData());

  if (element.value == null || element.value.trim() == "") {
    var errorV = document.getElementsByClassName(errorID)[0];
    errorV.innerHTML = "Phone Number Cannot Be Empty";
    errorV.style.display = "block";
    console.log("Phone Number Null");
  } else {
    const phoneREGX = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;

    var isValid = phoneREGX.test(String(element.value).toLowerCase());

    if (isValid) {
      var errorV = document.getElementsByClassName(errorID)[0];
      errorV.style.display = "none";
      return true;
    } else {
      var errorV = document.getElementsByClassName(errorID)[0];
      errorV.innerHTML = "Please enter a valid Phone Number";
      errorV.style.display = "block";
      console.log("Phone Number Wrong");
    }
  }
  return false;
}

function validateLocation() {
  var latI = document.getElementById("lat");
  var lngI = document.getElementById("lng");

  var errorV = document.getElementsByClassName("locError")[0];

  if (latI.value == null || latI.value == "" || lngI.value == null || lngI.value == "") {
    errorV.innerHTML = "Please Select your Location";
    errorV.style.display = "block";
  } else {
    errorV.style.display = "none";
    return true;
  }
  return false;
}

function CreateQualificationInput() {
  var inp = document.createElement("input");

  inp.classList = "form-control qualification-input";
  inp.type = "text";
  inp.placeholder = "Eg. Software Engineer";
  inp.addEventListener("blur", function() {
    validateQualification(this);
  });
  return inp;
}

function AddQualificationInput() {
  var qList = document.getElementById("qualifications");

  qList.appendChild(CreateQualificationInput());
}

function validateQualification(element) {
  if (element.value == null || element.value.trim() == "") {
    element.remove();
    console.log("Rem Qual");
  }
}

function validatePass(){
  var newPass = document.getElementById("password");
  var rePass = document.getElementById("rePassword");
  var newPassError = document.getElementsByClassName("psError")[0];
  var rePassError = document.getElementsByClassName("rpsError")[0];
  newPassError.style.display = "none";
  rePassError.style.display = "none";
  if(newPass.value == null || newPass.value.trim() == ""){
    newPassError.innerHTML = "Password Cannot Be Empty";
    newPassError.style.display = "block";
  }
  else{
    newPassError.style.display = "none";
    if(rePass.value.length > 0){
      if(newPass.value == rePass.value){
        console.log("passwordMatch");
        newPassError.style.display = "none";
        return true;
      }
      else{
        newPassError.innerHTML = "Password did not match";
        newPassError.style.display = "block";
      }
    }
  }
  if(rePass.value == null || rePass.value.trim() == ""){
    rePassError.innerHTML = "Password Cannot Be Empty";
    rePassError.style.display = "block";
  }
  else{
    rePassError.style.display = "none";
    if(newPass.value.length > 0){
      if(newPass.value == rePass.value){
        console.log("passwordMatch");
        rePassError.style.display = "none";
        return true;
      }
      else{
        rePassError.innerHTML = "Password did not match";
        rePassError.style.display = "block";
      }
    }
  }

  return false;
}

function RegisterUser() {
  var newCitizen = new Object();
  var isMandatoryAvailable = false;
  var isEmailAvaialable = false;
  var isNIDAvailable = false;
  var isPhoneAvilable = false;

  // Cannot be null
  var Firstname = document.getElementById("firstName");
  var Lastname = document.getElementById("lastName");
  var Birthdate = document.getElementById("birthdate");
  var AddressL1 = document.getElementById("addressL1");
  var StateProvince = document.getElementById("stateProvince");
  var City = document.getElementById("city");
  var ZipCode = document.getElementById("postalCode");
  var Latitudes = document.getElementById("lat");
  var Longitudes = document.getElementById("lng");
  var CV = document.getElementById("fileCV");
  var BirthCertificate = document.getElementById("fileBirthCertificate");
  var Passport = document.getElementById("filePassport");

  if (
    isNullOrEmpty(Firstname, "fnError", "First Name") &&
    isNullOrEmpty(Lastname, "lnError", "Last Name") &&
    isNullOrEmpty(Birthdate, "bdError", "Birthdate") &&
    isNullOrEmpty(AddressL1, "adrError", "Address") &&
    isNullOrEmpty(StateProvince, "spError", "State/ Province") &&
    isNullOrEmpty(City, "ctError", "City") &&
    isNullOrEmpty(ZipCode, "zipError", "Zip Code") &&
    isNullOrEmpty(CV, "cvError", "CV") &&
    isNullOrEmpty(BirthCertificate, "bsError", "Birth Certificate") &&
    isNullOrEmpty(Passport, "passpError", "Passport") &&
    validateLocation()
  ) {
    newCitizen.FirstName = Firstname.value;
    newCitizen.LastName = Lastname.value;
    newCitizen.BirthDate = Birthdate.value;
    newCitizen.AddressL1 = AddressL1.value;
    newCitizen.StateProvince = StateProvince.value;
    newCitizen.City = City.value;
    newCitizen.ZipCode = ZipCode.value;
    
    var Location = new Object();
    Location.Longitudes= Longitudes.value;
    Location.Latitudes = Latitudes.value;

    newCitizen.MapLocation = Location;
    newCitizen.FilePathCV = CV.value.split(/(\\|\/)/g).pop();
    newCitizen.FilePathBirthCertificate = BirthCertificate.value
      .split(/(\\|\/)/g)
      .pop();
    newCitizen.FilePathPassport = Passport.value.split(/(\\|\/)/g).pop();
    isMandatoryAvailable = true;
  } else {
    console.log("Missing Mandatory Data");
    isMandatoryAvailable = false;
  }

  //special
  var NationalID = document.getElementById("nationalId");
  if (validateNID(NationalID, "niError")) {
    newCitizen.NationalID = NationalID.value;
    isNIDAvailable = true;
  } else {
    console.log("Missing NID Data");
    isNIDAvailable = false;
  }

  var EmailAddress = document.getElementById("email");

  if (validateEmail(EmailAddress, "emError")) {
    newCitizen.Email = EmailAddress.value;
    isEmailAvaialable = true;
  } else {
    console.log("Missing Email Data");
    isEmailAvaialable = false;
  }

  var PhoneNumber = document.getElementById("phone");

  if (validatePhone("phone", "phError")) {
    newCitizen.PhoneNumber = PhoneNumber.value;
    isPhoneAvilable = true;
  } else {
    console.log("Missing Phone Data");
    isPhoneAvilable = false;
  }

  // optinal
  var AddressL2 = document.getElementById("addressL2");
  newCitizen.AddressL2 = filterEmpty(AddressL2.value);
  var CurrentProfession = document.getElementById("currenProfession");
  newCitizen.CurrentProfession = filterEmpty(CurrentProfession.value);
  var Affiliation = document.getElementById("affiliation");
  newCitizen.Affiliation = filterEmpty(Affiliation.value);

  var Qualifications = document.getElementsByClassName("qualification-input");
  // get the qualifications in to a string array from the html element array
  var qualStrings = []; // array to store qualifications
  for (let i = 0; i < Qualifications.length; i++) {
    // push the values to the string
    qualStrings.push(filterEmpty(Qualifications[i].value));
  }

  newCitizen.Qualifications = qualStrings;

  // get the qualification files in to a string array
  var FilesQualifications = document.getElementById("filesQualifications");
  // temp array to store complete file paths
  var FilesFullNames = FilesQualifications.files;

  // filenames Array
  var QualFileNames = [];
  console.log(FilesFullNames);
  for (let i = 0; i < FilesFullNames.length; i++) {
    // filer only the file names
    var filename = FilesFullNames[i].name;
    QualFileNames.push(filename);
  }

  newCitizen.FilePathQualifications = QualFileNames;

  var password = document.getElementById("password");

  if(validatePass()){
    newCitizen.Password = CryptoJS.MD5(password).toString();
  }

  console.log(newCitizen);

  // create teh request 
  var request = new XMLHttpRequest();

  // open the request for register Citizen
  request.open("POST", "http://20.92.239.229:59413/api/Citizen");

  // In here, First, we validate the inputes 
  if (
    isMandatoryAvailable &&
    isEmailAvaialable &&
    isNIDAvailable &&
    isPhoneAvilable
  ) {

    // Just a feedback for debugging purposes
    console.log("Request Sent");

    // Set the headers as JSON 
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");

    // This will send the newCitizen as JOSON Object in the body of the request
    request.send(JSON.stringify(newCitizen));

    request.onreadystatechange = function() { // Call a function when the state changes.
      if (this.readyState === XMLHttpRequest.DONE && this.status === 200) {
        var validateRequest = new XMLHttpRequest();  

        validateRequest.open('GET', 'http://20.92.239.229:59413/api/Citizen');
        
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
          if (parsedData.length > 0){
            setCookie("user", parsedData[0].Email, 1);
            window.location.replace("/Citizen");

          }
          else{
            alert("Server Error! Could Not Register. Please try again in few minutes.");
          }
        };

        validateRequest.send("nationalID="+newCitizen.NationalID);
      }
  }

  }
}

function filterEmpty(string) {
  if (string == null || string.trim() == "") {
    return "";
  } else {
    return string;
  }
}
