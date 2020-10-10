"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/comment").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("Send", function (UserName,Text) {
    var encodedMsg = "<p>Логин:".concat(UserName, "</p>", "<p>Коментарий:", Text, "</p><br/>");
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
    connection.invoke("Send", UserName, Text);//.catch(function (err) {
        //return console.error(err.toString());
//}
//);

    event.preventDefault();
});