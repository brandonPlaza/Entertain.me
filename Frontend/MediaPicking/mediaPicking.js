var favouriteTitles = []
var allRecommended = []

var interestedIn = []

API.Get(`Search/favourites`).then(results => {
    favouriteTitles = results
    console.log(favouriteTitles)
    pickRandomFav()
});

function pickRandomFav() {
    var randomId = Math.floor(Math.random() * favouriteTitles.length)
    console.log(randomId)
    titlesDidLoad(favouriteTitles[randomId])
}

function titlesDidLoad(randomId) {
    API.Get(`Recommendation?movieIDs=${randomId}`).then(results => {
        allRecommended = results
        console.log(allRecommended)

        var mainDisplay = document.getElementById("mainDisplay")
        for (var i = 0; i < results.length; i++) {
            if (results[i].title != "") {

                var container = document.createElement("div")
                container.classList.add("box")

                var mainResponseCard = document.createElement("div")
                mainResponseCard.classList.add("card")
                mainResponseCard.style = "width: 18rem;"
                mainResponseCard.addEventListener("click",selectMedia)

                var responseBody = document.createElement("div")
                responseBody.classList.add("card-body")
                responseBody.style = "padding:50px;"
                responseBody.id = results[i].id

                var title = document.createElement("h5")
                title.classList.add("card-title")
                title.innerHTML = results[i].title

                var description = document.createElement("p")
                description.classList.add("card-text")
                description.innerHTML = results[i].overview

                responseBody.appendChild(title)
                responseBody.appendChild(description)
                mainResponseCard.appendChild(responseBody)
                container.appendChild(mainResponseCard)
                mainDisplay.appendChild(container)
            }
        }
    });
}

function selectMedia(element){
    console.log(element.srcElement.innerHTML)
    if(interestedIn < 2){
        var submitToDatabaseBtnHolder = document.getElementById("btn-holder")
        submitToDatabaseBtnHolder.classList.add("box")
        submitToDatabaseBtnHolder.classList.add("sendPreferencesBtn")

        var submitToDatabaseBtn = document.createElement("button")
        submitToDatabaseBtn.classList.add('button')
        submitToDatabaseBtn.classList.add('box')
        submitToDatabaseBtn.innerHTML = "View your watchlist!"
        submitToDatabaseBtn.addEventListener("click",sendWatchlistToAPI)

        submitToDatabaseBtnHolder.appendChild(submitToDatabaseBtn)
    }

    if(element.srcElement.style.backgroundColor === 'green'){
        interestedIn.splice(interestedIn.indexOf(media => media === parseInt(element.srcElement.id)), 1)
        console.log(previouslyEnjoyedMedia)
        element.srcElement.style.backgroundColor = 'white'
    }
    else{
        interestedIn.push(parseInt(element.srcElement.id))
        console.log(interestedIn)
        element.srcElement.style.backgroundColor = 'green'
    }
}

function sendWatchlistToAPI(){
    API.Post(`search/addToWatchlist`, interestedIn).then(results => {
        //window.location.replace("http://127.0.0.1:5500/Frontend/MediaPicking/index.html")
    });
}