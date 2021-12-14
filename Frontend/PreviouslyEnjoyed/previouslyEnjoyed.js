
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
        previouslyEnjoyedMedia.push(element.srcElement.innerHTML)
    }

    if(element.srcElement.style.backgroundColor === 'green'){
        element.srcElement.style.backgroundColor = 'white'
    }
    else{
        element.srcElement.style.backgroundColor = 'green'
    }
}
