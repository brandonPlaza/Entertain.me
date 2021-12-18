// empty list of liked media that will be sent to db Intersted list
var likedMedia = []

document.getElementById("submit").onclick =
    function(){
        API.Get()
    }

function getMedia(){
    // get the Interested list 
    API.Get(``).then(results => {

        // populate it
        var mainDisplay = document.getElementById("display")
        console.log(results)
        for(var i=0; i < results.length; i++){
            if(results[i].title != ""){
                
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