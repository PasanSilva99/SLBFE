var input = document.querySelector("#phone");
window.intlTelInput(input, {
  separateDialCode: true
});

function Registeruser() {
  var Firstname = document.getElementById("firstName").value;
  var Lastname = document.getElementById("lastName").value;
  var NationalID = document.getElementById("firstName").value;
  var EmailAddress = document.getElementById("email").value;
  var PhoneNumber = document.getElementById("phone").value;
  var Birthdate = document.getElementById("birthdate").value;
  var NewPassword = document.getElementById("password").value;
  var RePassword = document.getElementById("rePassword").value;
  var AddressL1 = document.getElementById("addressL1").value;
  var AddressL2 = document.getElementById("addressL2").value;
  var StateProvince = document.getElementById("stateProvince").value;
  var City = document.getElementById("city").value;
  var ZipCode = document.getElementById("postalCode").value;
  var Latitudes = document.getElementById("lat").value;
  var Longitudes = document.getElementById("lng").value;
  var CurrentProfession = document.getElementById("currenProfession").value;
  var Affiliation = document.getElementById("affiliation").value;
  var Qualifications = document.getElementsByClassName("qualification-input");
  var CV = document.getElementById("fileCV").file;
  var BirthCertificate = document.getElementById("fileBirthCertificate").file;
  var Passport = document.getElementById("filePassport").file;
  var FilesQualifications = document.getElementById("filesQualifications").files;

  

}
