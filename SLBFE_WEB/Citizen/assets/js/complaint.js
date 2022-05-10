function GenerateView(complaint){
    let compl = document.createElement("div");
    compl.classList = "complaint";

    // Profile image 
    let complaintProfile = document.createElement("div");
    complaintProfile.classList = "complaint-profile";

        let complaintProfileImage = document.createElement("img");
        complaintProfileImage.classList = "img-fluid complaint-profile-image";
        complaintProfileImage.src = complaint.imageSrc;

    complaintProfile.appendChild(complaintProfileImage);

    // Complaint content wrapper
    let complaintContent = document.createElement("div");
    complaintContent.classList = "complaint-content";

        // Complaint Username and date wrapper
        let complUsernameDate = document.createElement("div");
        complUsernameDate.classList = "complaint-username-date";

            // complaint username and date
            let complUnameDate = document.createElement("div");
                let linkUsername = document.createElement("a");
                    let lbl_username = document.createElement("span");
                        lbl_username.textContent = complaint.username + "&nbsp;";
                        lbl_username.classList = "complaint-username";
                linkUsername.appendChild(lbl_username);

                let complaintDate = document.createElement("span");
                complaintDate.classList = "complaint-date";
                let date  = complaint.date.getMonth() + "/" + complaint.date.getDate() + "/" + complaint.date.getYear();
                complaintDate.textContent = date;
            complUnameDate.appendChild(linkUsername);
            complUnameDate.appendChild(complaintDate);
            
            let complaintCompanyWrap = document.createElement("div");
                let complaintCompanyLink = document.createElement("a");
                    let complaintCompany = document.createElement("span");
                    complaintCompany.classList = "complaint-company";
                    complaintCompany.textContent = complaint.company;
                complaintCompanyLink.appendChild(complaintCompany);
            complaintCompanyWrap.appendChild(complaintCompanyLink);

        complUsernameDate.appendChild(complUnameDate);
        complUsernameDate.appendChild(complaintCompanyWrap);

        //complaint body wrapper
        let complaintBody = document.createElement("div");
        complaintBody.classList = "complaint-body";

            let complaintParagraph = document.createElement("p");
            complaintParagraph.classList = "complaint-body";
            complaintParagraph.textContent = complaint.body;

        complaintBody.appendChild(complaintParagraph);

        //complaintTime Wrapper
        let complaintTime = document.createElement("div");
        complaintTime.classList = "complaint-time-wrap";

            let time = moment(complaint.date).fromNow();
            let lbl_complaintTime = document.createElement("span");
            

}

function GetComplaints(userEmail){

}

function GetComplaintsCompany(){

}