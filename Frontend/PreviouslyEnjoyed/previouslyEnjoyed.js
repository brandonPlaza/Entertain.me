document.getElementById("submit").onclick = 
    function (){
        API.Get(`search?mediaTitle=${$_`#mediaTitle`.value}`).then(results => {
            console.log(results)
        });
    }

