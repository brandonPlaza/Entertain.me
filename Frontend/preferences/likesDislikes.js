// empty list of liked media that will be sent to db Intersted list
var watchlist = []

API.Get(`search/getWatchlist`).then(results => {
    var display = document.getElementById("responseDisplay")
    console.log(results)
    for(var i=0; i < results.length; i++){
        // populate list on page
        var responseDiv = document.createElement("div")
        responseDiv.classList.add("div")
        responseDiv.addEventListener("click",mediaSelect)
        var responseData = document.createElement("div")
        responseData.classList.add("div-body")
        responseData.innerHTML = `${results[i].title}`
        responseData.id = results[i].id
        responseDiv.appendChild(responseData)
        display.appendChild(responseDiv)
    }
});


function selectMedia(element){
    console.log(element.srcElement.innerHTML)
    // submit the list to the api
    var submitToDbBtnHolder = document.getElementById("btn-holder")
    submitToDbBtnHolder.classList.add("box")
    submitToDbBtnHolder.classList.add("submit-btn")
}