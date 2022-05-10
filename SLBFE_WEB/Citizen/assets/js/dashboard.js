function getUser(){
    document.getElementById("user-interface").style.display = "none";
    document.getElementById("loading-screen").style.display = "flex";

    //let userEmail = getCookie("user");
    let userEmail = "pasananuththara19@gmail.com";

    // if the userCookie Exists
    if(userEmail != null && userEmail != "")
    {
        var userRequest = new XMLHttpRequest();

        // api/Citizen?nationalID=NID to get the user from the API
        userRequest.open('GET', 'http://20.92.239.229:59413/api/FindCitizen?'+ "email="+userEmail);

        // this will trigger when the request is loaded
        userRequest.onload = 
            function(){
                // This will handlle the responce
                let response = userRequest.response;
                let parsedData = JSON.parse(response);
                console.log(parsedData);

                // This request will return with the user object of there is a user with that
                // National ID
                if (parsedData.length > 0){
                    LoadUserToView(parsedData[0]);
                    document.getElementById("user-interface").style.display = "flex";
                    document.getElementById("loading-screen").style.display = "none";
                }
                else{
                    // if there is no valid user, that means the user is not logged in
                    alert("Error! Could Not Load User. Please try again in few minutes.");
                    window.location.href = "/Login";

                }
            }
    }
    else{
        // if there is no valid user, that means the user is not logged in
        alert("Please Login First");
        window.location.href = "/Login";
    }

    userRequest.send();

}

var ProfileName = document.getElementById("profileName");
var Greeting = document.getElementById("greeting");

// Lets load the user details in to the dashboard 
function LoadUserToView(user){
    console.log(user);
    // Combine the name to get the full name
    let username = user.FirstName + " " + user.LastName;
    ProfileName.innerHTML = username;
    Greeting.innerHTML = "Hello, " + username;
}

getUser();
