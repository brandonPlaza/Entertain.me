
var previouslyEnjoyedMedia = []

document.getElementById("submit").onclick = 
    function (){
        API.Get(`search/?mediaTitle=${$_`#mediaTitle`.value}`).then(results => {
            //clearPreviousCards()
            var mainDisplay = document.getElementById("responseDisplay")
            console.log(results)
            for(var i = 0; i < results.length; i++){
                if(results[i].title != ""){
                    var responseCard = document.createElement("div")
                    responseCard.classList.add("card")
                    responseCard.addEventListener("click",selectMedia)
                    var cardData = document.createElement("div")
                    cardData.classList.add("card-body")
                    cardData.innerHTML = `${results[i].title}`
                    responseCard.appendChild(cardData)
                    mainDisplay.appendChild(responseCard)
                }
            }
        });
    }

function clearPreviousCards(){
    var existingCards = document.getElementsByClassName("card")
    if(existingCards.length === 0) return;
    // existingCards.forEach(element => {
    //     element.remove()
    // });
    for(var i = 0; i < existingCards.length; i++){
        existingCards[i].remove()
    }
}

function selectMedia(element){
    console.log(element.srcElement.innerHTML)
    if(previouslyEnjoyedMedia < 2){
        var submitToDatabaseBtnHolder = document.getElementById("btn-holder")
        submitToDatabaseBtnHolder.classList.add("box")
        submitToDatabaseBtnHolder.classList.add("sendPreferencesBtn")

        var submitToDatabaseBtn = document.createElement("button")
        submitToDatabaseBtn.classList.add('button')
        submitToDatabaseBtn.classList.add('box')
        submitToDatabaseBtn.innerHTML = "Get your recommendations!"
        submitToDatabaseBtn.addEventListener("click",sendPreferencesToAPI)

        submitToDatabaseBtnHolder.appendChild(submitToDatabaseBtn)
    }

    if(element.srcElement.style.backgroundColor === 'green'){
        previouslyEnjoyedMedia.splice(previouslyEnjoyedMedia.indexOf(media => media === element.srcElement.innerHTML), 1)
        console.log(previouslyEnjoyedMedia)
        element.srcElement.style.backgroundColor = 'white'
    }
    else{
        previouslyEnjoyedMedia.push(element.srcElement.innerHTML)
        console.log(previouslyEnjoyedMedia)
        element.srcElement.style.backgroundColor = 'green'
    }
}

function flipElementColor(element){
    if(element.style.backgroundColor === 'green'){
        element.style.backgroundColor = 'white'
    }
    else{
        element.style.backgroundColor = 'green'
    }
}

function sendPreferencesToAPI(){
    
}
