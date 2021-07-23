// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function ShowFilterPanel() {
    var check = document.getElementById("Filter").checked;
    var panel = document.getElementById("FilterPanel");
    if (check) {
        panel.style.display = 'block';
        // panel.style.visibility = 'visible';
    } else {
        panel.style.display = 'none';
        //panel.style.visibility = 'hidden';
    }
}

function Contains(text, variable) {
    if (text.indexOf(variable) >= 0) {
        return 1;
    } else {
        return 0;
    }
}

function CleanFilter() {
    var pars = document.getElementsByTagName("p");
    for (var i = 0; i < pars.length; i++) {
        pars[i].style.display = "block";
    };
}

//function HandleJSresponse(respText) {
//    alert(responseText);
//}

async function GetScript(link) {
    var evt = (evt) ? evt : ((event) ? event : null);
    evt.preventDefault();
    evt.stopPropagation();

    //download(link, "script1");
    //alert(file);
    var xhr = new XMLHttpRequest();
    xhr.open("GET", link);

    xhr.setRequestHeader('chlen', 'https://tinder.com/');
    
    
    xhr.onreadystatechange = function() {
         if (xhr.readyState === XMLHttpRequest.DONE && xhr.status === 200) {
             console.log("the script text content is", xhr.responseText);
         }
    };
    
    xhr.send();
 
    // var myHeaders = new Headers();
    // myHeaders.set('origin', 'https://tinder.com/');
    // var myInit = { 
    //     method: 'GET',
    //     headers: myHeaders,
    //     //mode: 'cors',
    //     cache: 'default',
    //     referrerPolicy: 'no-referrer'
    // };
    // const url = link;
    // let myRequest = await new Request(url, myInit);
        //.then( blob => {
        //    var file = window.URL.createObjectURL(blob);
       //     window.location.assign(file);
        //});
    //alert(myRequest);    
}

async function* makeTextFileLineIterator(fileURL) {
    const utf8Decoder = new TextDecoder('utf-8');
    var myHeaders = new Headers();
    myHeaders.append('Content-Type', 'text/plain;charset=UTF-8');
    myHeaders.append('origin', 'https://tinder.com/');

    var myInit = { 
    method: 'GET',
    headers: myHeaders//,
    // //mode: 'cors',
    // //cache: 'default' 
    };
    const response = await fetch(fileURL,myInit);
    const reader = response.body.getReader();
    let { value: chunk, done: readerDone } = await reader.read();
    chunk = chunk ? utf8Decoder.decode(chunk) : '';
  
    const re = /\n|\r|\r\n/gm;
    let startIndex = 0;
    let result;
  
    for (;;) {
      let result = re.exec(chunk);
      if (!result) {
        if (readerDone) {
          break;
        }
        let remainder = chunk.substr(startIndex);
        ({ value: chunk, done: readerDone } = await reader.read());
        chunk = remainder + (chunk ? utf8Decoder.decode(chunk) : '');
        startIndex = re.lastIndex = 0;
        continue;
      }
      yield chunk.substring(startIndex, result.index);
      startIndex = re.lastIndex;
    }
    if (starprocessLinetIndex < chunk.length) {
      // last line didn't end in a newline char
      yield chunk.substr(startIndex);
    }
  }
  
async function run(url) {
    for await (let line of makeTextFileLineIterator(url)) {
      alert(line);
    }
}
  

  

function HandleFilterParams() {
    var str = document.getElementById("filparams").value;
    var lessArr = new Array();
    var moreArr = new Array();
    var exclude = new Array();
    var include = new Array();
    arr = str.split(/[^>\d+<=>-]/g);
    for (var i = 0; i < arr.length; i++) {
        if (arr[i] == "" || arr[i] == null || arr[i] == '-' || arr[i] == '<' || arr[i] == '>' || arr[i] == '=' || arr[i] == '<=' || arr[i] == '<=') {
            arr.splice(i, 1);
        }
    };
    arr.forEach(element => {
        if (element[0] == "-") {
            var vr = element.substring(1);
            if (Contains(vr, "-")) {
                var text = element.substring(1);
                var rng = text.split('-');
                text = "R" + rng[0] + "-" + rng[1];
                exclude.push(text);
            } else {
                exclude.push(element.substring(1));
            }
            return;
        }
        if (element[0] == '<') {
            if (element[1] != '=') {
                lessArr.push(element.substring(1));
            } else {
                var num = Number.parseInt(element.substring(2)) + 1;
                lessArr.push(num);
            }
            return;
        }
        if (element[0] == '>') {
            if (element[1] != '=') {
                moreArr.push(element.substring(1));
            } else {
                var num = Number.parseInt(element.substring(2)) - 1;
                moreArr.push(num);
            }
            return;
        }
        if (Contains(element, "-")) {
            var rng = element.split('-');
            var text = "R" + rng[0] + "-" + rng[1];
            include.push(text);
        } else {
            include.push(element);
        }
    });
    //if (exclude.length > 0) alert("Values in excluded: " + exclude);
    //if (lessArr.length > 0) alert("Values in lessArr: " + lessArr);
    //if (include.length > 0) alert("Values in include: " + include);
    //if (moreArr.length > 0) alert("Values in moreArr: " + moreArr);
    var pars = document.getElementsByTagName("p");
    var remPars = new Array();
    var length = pars.length;
    for (var i = 0; i < length; i++) {
        var rmv = 0;
        lessArr.forEach(el => {
            if (Number.parseInt(pars[i].id.substring(3)) >= Number.parseInt(el)) {
                rmv = 1;
            }
        });
        moreArr.forEach(el => {
            if (Number.parseInt(pars[i].id.substring(3)) <= Number.parseInt(el)) {
                rmv = 1;
            }
        });
        exclude.forEach(el => {
            if (el[0] != 'R') {
                if (Number.parseInt(pars[i].id.substring(3)) == Number.parseInt(el)) {
                    rmv = 1;
                }
            } else {
                var str = el.substring(1);
                var rng = str.split('-');
                if (Number.parseInt(pars[i].id.substring(3)) >= Number.parseInt(rng[0]) && Number.parseInt(pars[i].id.substring(3)) <= Number.parseInt(rng[1])) {
                    rmv = 1;
                }
            }
        });
        include.forEach(el => {
            if (el[0] != 'R') {
                if (Number.parseInt(pars[i].id.substring(3)) == Number.parseInt(el)) {
                    rmv = 0;
                }
            } else {
                var rng = el.substring(1).split('-');
                if (Number.parseInt(pars[i].id.substring(3)) >= Number.parseInt(rng[0]) && Number.parseInt(pars[i].id.substring(3)) <= Number.parseInt(rng[1])) {
                    rmv = 0;
                }
            }
        });
        if (rmv) {
            pars[i].style.display = 'none';
        }
    }
}