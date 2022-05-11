// this function will generate the view for the complaint
// pass the valuse as
// object.ImageSrc = image path of the user
// object.SentDate = Date and time of the complaint
// object.UserName = UserName of the user
// object.CompanyName = Company related to this copmplaint
// object.Content = Complaint Content
function GenerateComplaintView(complaint) {
  let compl = document.createElement("div");
  compl.classList = "complaint";

  // Profile image
  let complaintProfile = document.createElement("div");
  complaintProfile.classList = "complaint-profile";

  let complaintProfileImage = document.createElement("img");
  complaintProfileImage.classList = "img-fluid complaint-profile-image";
  complaintProfileImage.src = complaint.ImageSrc;

  complaintProfile.appendChild(complaintProfileImage);

  // Complaint content wrapper
  let complaintContent = document.createElement("div");
  complaintContent.classList = "complaint-content";

  // Complaint UserName and Date wrapper
  let complUserNameDate = document.createElement("div");
  complUserNameDate.classList = "complaint-UserName-Date";

  // complaint UserName and Date
  let complUnameDate = document.createElement("div");
  let linkUserName = document.createElement("a");
  let lbl_UserName = document.createElement("span");
  lbl_UserName.textContent = complaint.UserName + "\xa0";
  lbl_UserName.classList = "complaint-UserName";
  linkUserName.appendChild(lbl_UserName);

  let complaintDate = document.createElement("span");
  complaintDate.classList = "complaint-Date";
  let Date =
    complaint.SentDate.getMonth() +
    "/" +
    complaint.SentDate.getDate() +
    "/" +
    complaint.SentDate.getYear();
  complaintDate.textContent = Date;
  complUnameDate.appendChild(linkUserName);
  complUnameDate.appendChild(complaintDate);

  let complaintCompanyWrap = document.createElement("div");
  let complaintCompanyLink = document.createElement("a");
  let complaintCompany = document.createElement("span");
  complaintCompany.classList = "complaint-company";
  complaintCompany.textContent = complaint.CompanyName + " ";
  complaintCompanyLink.appendChild(complaintCompany);
  complaintCompanyWrap.appendChild(complaintCompanyLink);

  complUserNameDate.appendChild(complUnameDate);
  complUserNameDate.appendChild(complaintCompanyWrap);

  //complaint body wrapper
  let complaintBody = document.createElement("div");
  complaintBody.classList = "complaint-body";

  let complaintParagraph = document.createElement("p");
  complaintParagraph.classList = "complaint-body";
  complaintParagraph.textContent = complaint.Content;

  complaintBody.appendChild(complaintParagraph);

  //complaintTime Wrapper
  let complaintTime = document.createElement("div");
  complaintTime.classList = "complaint-time-wrap";

  let time = moment(complaint.SentDate).fromNow();
  let lbl_complaintTime = document.createElement("span");
  lbl_complaintTime.textContent = time;
  complaintTime.appendChild(lbl_complaintTime);

  complaintContent.appendChild(complUserNameDate);
  complaintContent.appendChild(complaintBody);
  complaintContent.appendChild(complaintTime);

  compl.appendChild(complaintProfile);
  compl.appendChild(complaintContent);
  return compl;
}

//var com = new Object();

//com.ImageSrc = "/assets/img/brands/lyft.png";
//com.SentDate = new Date();
//com.UserName = "Pasan Silva";
//com.CompanyName = "Tecxick";
//com.Content = "Super satisfied";

//var compl = GenerateComplaintView(com);

//var compl_scr = document.getElementById("complaints-scroll");
//compl_scr.appendChild(compl);
//compl_scr.appendChild(compl2);
//compl_scr.appendChild(compl3);

// get complaint for the email
// if want all, dont pass the parameter
function GetComplaints(userEmail) {
  var complaintRequest = new XMLHttpRequest();

  if (userEmail != null) {
    // api/Citizen?nationalID=NID to get the user from the API
    complaintRequest.open(
      "GET",
      "http://20.92.239.229:59413/api/FindComplaints?" + "email=" + userEmail
    );
  } else {
    // get all complaints
    complaintRequest.open(
        "GET", 
        "http://20.92.239.229:59413/api/Complaints"
        );
  }
  // this will trigger when the request is loaded
  complaintRequest.onload = function() {
    // This will handlle the responce
    let response = complaintRequest.response;
    let parsedData = JSON.parse(response);
    console.log(parsedData);

    
  };
  complaintRequest.send();
}
