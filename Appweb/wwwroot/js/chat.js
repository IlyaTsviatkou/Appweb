class Message {
    constructor(UserID, Text, ItemID) {
        this.UserID = UserID;
        this.Text = Text;
        this.ItemID = ItemID;
    }
}

// userName is declared in razor page.
const userInput = document.getElementById('userid');
const textInput = document.getElementById('Text');
const itemInput = document.getElementById('itemid');
const chat = document.getElementById('chat');
const messagesQueue = [];

document.getElementById('btnCm').addEventListener('onclick', () => {
    var currentdate = new Date();
    when.innerHTML =
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })
});

function clearInputField() {
    messagesQueue.push(textInput.value);
    textInput.value = "";
}

function sendMessage() {
    let text = messagesQueue.shift() || "";
    if (text.trim() === "") return;

    let userid = userInput.value;
    let itemid = itemInput.value;
    let message = new Message(userid, text, itemid);
    sendMessageToHub(message);
}

function addMessageToChat(message) {
    let isCurrentUserMessage = message.userName === username;

    let container = document.createElement('div');
    container.className = "card";
    let sender = document.createElement('div');
    sender.className = "card-header";
    sender.innerHTML = message.UserID;
    let text = document.createElement('div');
    text.className = "card-body";
    text.innerHTML = message.Text;
    container.appendChild(sender);
    container.appendChild(text);
    chat.appendChild(container);
}
