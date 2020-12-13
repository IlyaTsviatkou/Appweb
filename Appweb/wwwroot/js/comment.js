"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/comment").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("Send", function (UserName, Text, ItemID, UserID) {

    var encodedMsg = "<div class=\"card\"><div class=\"card-header\"> ".concat(UserName, "<label id=\"name-user\" value=\"", UserName, "\"", UserName, "</label ></div><div class=\"card-body\">", Text, "</div> </div>");

   
    var li = document.createElement("li");
    li.innerHTML = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var UserName = document.getElementById("name-user").textContent;
    var Text = document.getElementById("Text").value;
    var ItemID = document.getElementById("ItemID").value;
    var UserID = document.getElementById("UserID").value;
    connection.invoke("Send", UserName, Text, ItemID,UserID);//.catch(function (err) {
    //return console.error(err.toString());
    //}
    //);

    event.preventDefault();
});