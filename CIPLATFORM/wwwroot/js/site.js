function GetCity() {
    console.log("het");
    var countryId = $('#countryId').find(":selected").val();
    /*    debugger;*/
    $.ajax({
        url: "/Platform/GetCitys",
        method: "GET",
        data: {
            "countryId": countryId
        },
        success: function (data) {
            data = JSON.parse(data);
            $("#selectCityList").empty();
            document.getElementById("selectCityList").innerHTML += `
        <option value=${name}> City </option>
        `;
            data.forEach((name) => {
                document.getElementById("selectCityList").innerHTML += `
        <option value=${name.CityId} >${name.Name}</option>
        `;
            })
        },
        error: function (e) {
            console.log("Bye city");
            alert('Error');
        },
    });
}


function temp() {
    var checkedcntryvalues = [];
    var div1 = document.getElementById("countryId");
    var list = div1.getElementsByTagName("option");
    for (i = 0; i < list.length; i++) {
        if (list[i].selected) {
            checkedcntryvalues.push(list[i].value);
        }

    }
    console.log(checkedcntryvalues);


    var checkedvalues = [];
    var div = document.getElementById("selectCityList");
    var list = div.getElementsByTagName("option");
    for (i = 0; i < list.length; i++) {
        if (list[i].selected) {
            checkedvalues.push(list[i].value);
        }

    }
    console.log(checkedvalues);





    var checkedthemevalues = [];
    var div2 = document.getElementById("theme");
    var list = div2.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            checkedthemevalues.push(list[i].value);
        }

    }
    console.log(checkedthemevalues);



    var checkedskillvalues = [];
    var div3 = document.getElementById("skill");
    var list = div3.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            checkedskillvalues.push(list[i].value);
        }

    }
    console.log(checkedskillvalues);



    var search = document.getElementById("searchb").value;
    console.log(search)


    var sort = document.getElementById("sort").value;
    console.log(sort)


    $.ajax({
        url: '/Platform/Filter',
        type: "POST", // POST

        data: {
            'cityId': checkedvalues,
            'countryId': checkedcntryvalues,
            'themeId': checkedthemevalues,
            'skillId': checkedskillvalues,
            'search': search,
            'sort': sort
        },
        dataType: "html", // return datatype like JSON and HTML
        success: function (data) {


            $("#filter").empty();
            console.log("grid Hii");
            $("#filter").html(data);
            //$("#grid-view").empty();
            //console.log("grid Hii");
            //$("#grid-view").html(data);
            //$("#list-view").empty();
            //console.log("list Hii");
            //$("#list-view").html(data);

            var div1 = document.getElementById("list-view");
            div1.style.display = 'none';
        },
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}





window.onload = opengrid();
function opengrid() {
    console.log("Grid");
    var div1 = document.getElementById("list-view");
    var div2 = document.getElementById("grid-view");
    div1.style.display = 'none';
    div2.style.display = 'block';
    console.log("Done");
}
function openlist() {
    console.log("List");
    var div1 = document.getElementById("list-view");
    var div2 = document.getElementById("grid-view");
    div1.style.display = 'block';
    div2.style.display = 'none';
    console.log("Done");
}



//function preventBack() { window.history.forward(); }
//setTimeout("preventBack()", 0);
//window.onunload = function () { null }



function AddMissionToFavourite(missionId) {
   
    $.ajax({

        url: '/Platform/AddMissionToFavourite',
        method: "POST",
        data: {
            'missionId': missionId,
        },
        success: function (missions) {

            console.log("hii")
            if (missions == true) {
                $('#addToFav').removeClass();
                $('#addToFav').addClass("bi bi-heart-fill");
                $('#addToFav').css("color", "red");
           /*     document.getElementById(missionId).className.removeClass();*/
                document.getElementById(missionId).className="bi bi-heart-fill text-danger";
              /*  document.getElementById(missionId).className=.st("color", "red");*/
            }
            else {
                $('#addToFav').css("color", "black");
                $('#addToFav').removeClass();
                $('#addToFav').addClass("bi bi-heart");


                //document.getElementById(missionId).className.css("color", "black");
                //document.getElementById(missionId).className.removeClass();
                document.getElementById(missionId).className = "bi bi-heart";
            }
        },
        error: function (request, error) {
            console.log("Bye city");
            alert('Error');
        },
    });
}

function applyMission(missionId) {
    debugger
    $.ajax({

        url: '/Platform/applyMission',
        method: "POST",
        data: {
            'missionId': missionId,
        },
        success: function (missions) {
            debugger
            if (missions == true) {
                console.log("done");

                $('#applyMission').prop('disabled', true);
                $('#applyMission').text("Your Request has been sent for Approve");
                $('#applyMission').css("color", "red");
                document.getElementById("ok").innerHTML += `Applied Successfully...`;
            }
        },
        error: function (request, error) {
            console.log("function not working");
            document.getElementById("ok").innerHTML += `You've already Applied...`;

            alert('Error');
        },

    });

}


function comment(missionid) {

    //var crd = document.getElementById("comment");
    var comnt = $("#comment_text").val();

    /*    var missionId = document.getElementsByClassName("mission_id").value;*/
    console.log("kkkkkkkkkkkkkkkkkkkkk");
    console.log(comnt);


    $.ajax({
        url: "/Platform/AddComment",
        type: "POST", // POST
        data: {
            'obj': missionid,
            'comnt': comnt
        },
        dataType: "html", // return datatype like JSON and HTML
        success: function (data) {

            $("#comment").html();
            console.log("Added ");
            window.location.reload();

        },
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}

function recommandToCoWorker(x) {
    //var toUserId = $('#recommand').find(":checked").val();
    var Missiond = x;
    var toUserId = [];
    var recommand = document.getElementById("recommand");
    var list = recommand.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            toUserId.push(list[i].value);
        }

    }

    /* debugger;*/
    $.ajax({
        url: "/Platform/RecommandToCoWorker",
        method: "Post",
        data: {
            "toUserId": toUserId,
            "mid": Missiond
        },
        success: function (data) {
            console.log(toUserId);

        }
        ,
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}

function recommandStory(x) {
    //var toUserId = $('#recommand').find(":checked").val();
    var Storyd = x;
    var toUserId = [];
    var recommand = document.getElementById("recommand");
    var list = recommand.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            toUserId.push(list[i].value);
        }

    }

    /* debugger;*/
    $.ajax({
        url: "/Platform/RecommandStory",
        method: "Post",
        data: {
            "toUserId": toUserId,
            "sid": Storyd
        },
        success: function (data)
        {
            console.log(toUserId);

        }
        ,
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}

