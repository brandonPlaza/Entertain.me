var likedMedia = []

document.getElementById("submit").onclick =
    function(){
        API.Get()
    }

function getMedia(){
    // get the favourites list 
    API.Get(`favourites`).then(results => {

        // populate it
        var mainDisplay = document.getElementById("display")
        console.log(results)
        for(var i=0; i < results.length; i++){
            if(results[i].title != ""){
                var responseCard = document.createElement("div")
                responseCard.classList.add("card")
                responseCard.addEventListener("click", selectMedia)
                var cardData = document.getElementById("div")
                cardData.innerHTML = `${results[i].title}`
                responseCard.appendChild(cardData)
                mainDisplay.appendChild(responseCard)
            }
        }
    });
}

function selectMedia(element){
    console.log(element.srcElement.innerHTML)
    // submit the list to the api
    var submitToDbBtnHolder = document.getElementById("btn-holder")
    submitToDbBtnHolder.classList.add("box")
    submitToDbBtnHolder.classList.add("submit-btn")
}