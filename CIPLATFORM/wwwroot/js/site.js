function GetsCity() {
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

function GetCity() {    var countryIds = [];    var countrydiv = document.getElementById("countryId");    console.log(countrydiv);    var list = countrydiv.getElementsByTagName("input");    for (i = 0; i < list.length; i++) {        if (list[i].checked) {            countryIds.push(list[i].id);        }    }    console.log("countryids:" + countryIds);    /* var countryId = $('#countryId').find(":selected").val();*/    $.ajax({        url: "/Platform/GetCitys",        method: "POST",        data: {            'countryId': countryIds        },        success: function (data) {            data = JSON.parse(data);            console.log(data);            $("#selectCityList").empty();            // document.getElementById("selectCityList").innerHTML += `            //<option value=${name}> City </option>            //`;            document.getElementById("selectCityList").innerHTML += `<ul class="dropdown-menu">`;            data.forEach((name) => {                // document.getElementById("selectCityList").innerHTML += `                //<option value=${name.CityId} >${name.Name}</option>                //`;                document.getElementById("selectCityList").innerHTML += `<li><input type="checkbox" name="city" id="${name.CityId}" class="city_${name.CityId}" value="${name.Name}" />${name.Name}</li>`;            })            document.getElementById("selectCityList").innerHTML += `</ul>`;        }        ,        error: function (e) {            console.log("Bye");            alert('Error');        },    });}function filterBadges() {    $("#filter-button").empty();    $('input[name="country"]:checked').each(function () {        document.getElementById("filter-button").innerHTML += `<button class="filter rounded-pill border" id="${this.value}" ><div style="width:max-content">${this.value} <i onclick="removeFilter(${this.id},'country')" class="bi bi-x"></i></div></button>`    });    $('input[name="city"]:checked').each(function () {        document.getElementById("filter-button").innerHTML += `<button class="filter rounded-pill border" id="${this.value}" ><div style="width:max-content">${this.value} <i onclick="removeFilter(${this.id},'city')" class="bi bi-x"></i></div></button>`    });    $('input[name="theme"]:checked').each(function () {        document.getElementById("filter-button").innerHTML += `<button class="filter rounded-pill border" id="${this.value}"><div style="width:max-content">${this.value} <i onclick="removeFilter(${this.id},'theme')" class="bi bi-x"></i></div></button>`    });    $('input[name="skill"]:checked').each(function () {        document.getElementById("filter-button").innerHTML += `<button class="filter rounded-pill border" id="${this.value}"><div style="width:max-content">${this.value} <i onclick="removeFilter(${this.id},'skill')" class="bi bi-x"></i></div></button>`    });    document.getElementById("filter-button").innerHTML += `<button class="clearall p-0 rounded-pill border" onclick="clearAll()">Clear all</button>`}function removeFilter(checkboxId, type) {    console.log("rm" + checkboxId);    if (type == 'country') {        $(".country_" + checkboxId).prop("checked", false);        GetCity();    }    if (type == 'city') {        $(".city_" + checkboxId).prop("checked", false);    }    if (type == 'theme') {        $(".theme_" + checkboxId).prop("checked", false);    }    if (type == 'skill') {        $(".skill_" + checkboxId).prop("checked", false);    }    temp();    filterBadges();}function clearAll() {    $('input[name="country"]:checked').each(function () {        $(".country_" + this.id).prop("checked", false);    });    $('input[name="city"]:checked').each(function () {        $(".city_" + this.id).prop("checked", false);    });    $('input[name="theme"]:checked').each(function () {        $(".theme_" + this.id).prop("checked", false);    });    $('input[name="skill"]:checked').each(function () {        $(".skill_" + this.id).prop("checked", false);    });    filterBadges();    temp();    $(".clearall").addClass("d-none");}var view = 1;$(document).ready(function () {    $("#list").click(function () {        view = 2;        temp();        console.log(view);    });    $("#grid").click(function () {        view = 1;        temp();    });})


function temp(pg) {    if (pg == undefined) {        pg = 1;    }    console.log(pg);    var checkedcntryvalues = [];    var div1 = document.getElementById("countryId");    var list = div1.getElementsByTagName("input");    for (i = 0; i < list.length; i++) {        if (list[i].checked) {            checkedcntryvalues.push(list[i].id);        }    }    console.log(checkedcntryvalues);    var checkedvalues = [];    var div = document.getElementById("selectCityList");    var list = div.getElementsByTagName("input");    for (i = 0; i < list.length; i++) {        if (list[i].checked) {            checkedvalues.push(list[i].id);        }    }    console.log(checkedvalues);    var checkedthemevalues = [];    var div2 = document.getElementById("theme");    var list = div2.getElementsByTagName("input");    for (i = 0; i < list.length; i++) {        if (list[i].checked) {            checkedthemevalues.push(list[i].id);        }    }    console.log(checkedthemevalues);    var checkedskillvalues = [];    var div3 = document.getElementById("skill");    var list = div3.getElementsByTagName("input");    for (i = 0; i < list.length; i++) {        if (list[i].checked) {            checkedskillvalues.push(list[i].id);        }    }    console.log(checkedskillvalues);    var search = document.getElementById("searchb").value;    console.log(search)    var sort = document.getElementById("sort").value;    console.log(sort)       console.log(sort)    $.ajax({        type: "POST", // POST        url: '/Platform/Filter',        data: {            'cityId': checkedvalues,            'countryId': checkedcntryvalues,            'themeId': checkedthemevalues,            'skillId': checkedskillvalues,            'search': search,            'sort': sort,            'pg': pg,            'view': view,        },        dataType: "html",        success: function (data) {            $("#filter").empty();            console.log("grid Hii");            $("#filter").html(data);            var div1 = document.getElementById("list-view");            div1.style.display = 'none';        },        error: function (e) {            console.log("Bye");            alert('Error');        },    });}
function story(pg) {
    if (pg == undefined) {
        pg = 1;
    }
    var search = document.getElementById("searchb").value;
    console.log(search)
    //debugger
    $.ajax({
        type: "POST", // POST
        url: '/Platform/StoryFilter',
        data: {
            'search': search,
            'pg': pg,
        },
        dataType: "html", // return datatype like JSON and HTML
        success: function (data) {

            console.log(data);
            $("#StoryFilter").empty();
            $("#StoryFilter").html(data);
        },
        error: function (e) {
            /*    debugger*/
            console.log("Bye");
            alert('Error');
        },
    });
}
//window.onload = opengrid();
//function opengrid() {
//    console.log("Grid");
//    var div1 = document.getElementById("list-view");
//    var div2 = document.getElementById("grid-view");
//    div1.style.display = 'none';
//    div2.style.display = 'block';
//    console.log("Done");
//}
//function openlist() {
//    console.log("List");
//    var div1 = document.getElementById("list-view");
//    var div2 = document.getElementById("grid-view");
//    div1.style.display = 'block';
//    div2.style.display = 'none';
//    console.log("Done");
//}
function AddMissionToFavourite(missionId) {
    
    $.ajax({

        url: '/Platform/AddMissionToFavourite',
        method: "POST",
        data: {
            'missionId': missionId,
        },
        success: function (c) {

            if (c == true) {
                $('#addToFav').removeClass();
                $('#addToFav').addClass("bi bi-heart-fill");
                $('#addToFav').css("color", "red");
                toastr.options = {
                    "closeButton": true,
                    "progressBar": true
                };
                toastr.success("Added To the favourite");
                document.getElementById('add_' + missionId).className = "bi bi-heart-fill text-danger";

                //document.getElementById("x(" + missionId).className = "bi bi-heart-fill text-danger";
            }
            else {
                $('#addToFav').css("color", "black");
                $('#addToFav').removeClass();
                $('#addToFav').addClass("bi bi-heart");
                toastr.options = {
                    "closeButton": true,
                    "progressBar": true
                };
                toastr.error('Remove From the favourite');
                document.getElementById('add_' + missionId).className = "bi bi-heart";
            }

        },
        error: function (request, error) {
            console.log("Bye city");
            alert('Error');
        },

    });

}
//function AddMissionToFavourite(missionId) {

//    $.ajax({

//        url: '/Platform/AddMissionToFavourite',
//        method: "POST",
//        data: {
//            'missionId': missionId,
//        },
//        success: function (c) {

//            console.log("hii")
//            if (c == true) {
//                $('#addToFav').removeClass();
//                $('#addToFav').addClass("bi bi-heart-fill");
//                $('#addToFav').css("color", "red");

//                document.getElementById(missionId).className = "bi bi-heart-fill text-danger";
//            }
//            else {
//                $('#addToFav').css("color", "black");
//                $('#addToFav').removeClass();
//                $('#addToFav').addClass("bi bi-heart");

//                document.getElementById(missionId).className = "bi bi-heart";
//            }
//        },
//        error: function (request, error) {
//            console.log("Bye city");
//            alert('Error');
//        },
//    });
//}
function applyMission(missionId) {
    //debugger
    $.ajax({

        url: '/Platform/applyMission',
        method: "POST",
        data: {
            'missionId': missionId,
        },
        success: function (missions) {
            //debugger
            if (missions == true) {
                console.log("done");
                toastr.options = {
                    "closeButton": true,
                    "progressBar": true
                };
                $('#applyMission').prop('disabled', true);
                $('#applyMission').text("Your Request has been sent for Approve");
                $('#applyMission').css("color", "red");
                /*document.getElementById("ok").innerHTML += `Applied Successfully...`;*/


                toastr.success('Applied successfully');
            }
        },
        error: function (request, error) {
            console.log("function not working");
            /* document.getElementById("ok").innerHTML += `You've already Applied...`;*/
            toastr.options = {
                "closeButton": true,
                "progressBar": true
            };
            toastr.success('function not working');
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


            toastr.options = {
                "closeButton": true,
                "progressBar": true
            };
            $("#comment").html();
            console.log("Added ");
            toastr.success('Comment Added  successfully');
            /*setTimeout(function () { window.location.reload(); }, 3000);*/
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
            toastr.options = {
                "closeButton": true,
                "progressBar": true
            };
            console.log(toUserId);
            toastr.success('Email send  successfully');

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
        success: function (data) {
            toastr.options = {
                "closeButton": true,
                "progressBar": true
            };
            console.log(toUserId);
            toastr.success('Email send  successfully');


        }
        ,
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}

function Sdata() {
    var div1 = document.getElementById("getData");
    var selectedOption = div1.options[div1.selectedIndex];
    var selectedValue = selectedOption.value;
    console.log(selectedValue);

    //debugger
    $.ajax(
        {
            type: "POST", // POST
            url: '/Platform/CheckData',
            data: {
                'mid': selectedValue,
            },


            success: function (data) {
                console.log(data);

                data = JSON.parse(data);
                console.log(data);
                if (data != null) {
                    {
                        if (data.story.PublishedAt != null) {
                            let myDateObj = new Date(data.story.PublishedAt);
                            const year = myDateObj.getFullYear();
                            const month = String(myDateObj.getMonth() + 1).padStart(2, '0');
                            const day = String(myDateObj.getDate()).padStart(2, '0');
                            const formattedDate = `${year}-${month}-${day}`;
                            data.PublishedAt = formattedDate;
                        }


                        $("#formGroupExampleInput").val(data.story.Title);
                        $("#sDate").val(data.PublishedAt);
                        $("#surl").val(data.url);


                        if (data.simg != null) {
                            function toDataUrl(url, callback) {
                                console.log(url);
                                var newUrl = url;
                                var xhr = new XMLHttpRequest();
                                xhr.onload = function () {
                                    callback(xhr.response);
                                };
                                xhr.open('GET', newUrl);
                                xhr.responseType = 'blob';
                                xhr.send();
                            }


                            const dT = new DataTransfer();
                            let image;



                            //debugger
                            console.log(data.simg);
                            let images = ""
                            data.simg.forEach((image, index) => {
                                /*returnImage = image;*/
                                showImg = "/images/A/" + image;
                                toDataUrl(showImg, function (x) {
                                    image1 = x;

                                    dT.items.add(new File([image1], image, {
                                        type: "image/png"
                                    }));
                                    imagesArray.push(new File([image1], image, {
                                        type: "image/png"
                                    }));
                                    //debugger
                                    document.querySelector('#imageupload').files = dT.files;
                                    //document.querySelector('#imageupload').files = dT.files;
                                });
                                images += `<div class="image"><img src="${showImg}" alt="image"><span onclick="deleteImage(${index})">&times;</span></div>`

                            })
                            output.innerHTML = images;


                        }
                        else {
                            output.innerHTML = "No Images Are Choosen";
                        }
                        CKEDITOR.instances.editor.setData(data.story.Description);

                    }
                }
                else {
                    $("#formGroupExampleInput").val("");
                    $("#sDate").val("");
                    $("#surl").val("");
                    output.innerHTML = "No Images Are Choosen";
                    CKEDITOR.instances.editor.setData("");
                }
                //debugger
            },

            error: function (e) {
                debugger
                alert('Error');
            },
        }
    );
}

function getActivity(x) {
    console.log("TimeSheet!!!!!!!!");
    {
        if (x > 0) {
            $.ajax({
                url: "/Profile/getActivity",
                method: "Post",
                data:
                {
                    "tid": x,
                },
                success: function (data) {
                    console.log(data);
                    //debugger
                    $("#TimesheetTime").empty();
                    $("#TimesheetTime").html(data);
                },
                error: function (e) {
                    console.log("Bye");
                    alert('Error');
                },
            });
        }
        else {
            const myForm = document.querySelector('#timesheetform');

            myForm.querySelectorAll('.form-control').forEach((element, index) => {
                element.value = "";
            });
        }
    }
}

function getgoalActivity(x) {
    console.log("TimeSheet!!!!!!!!");
    {
        if (x > 0) {
            $.ajax({
                url: "/Profile/getGoalActivity",
                method: "Post",
                data:
                {
                    "tid": x,
                },
                success: function (data) {
                    console.log(data);
                    //debugger
                    $("#TimesheetGoal").empty();
                    $("#TimesheetGoal").html(data);
                },
                error: function (e) {
                    console.log("Bye");
                    alert('Error');
                },
            });
        }
        else {
            const myForm = document.querySelector('#goalsheetform');

            myForm.querySelectorAll('.form-control').forEach((element, index) => {
                element.value = "";
            });
        }
    }
}



//function abc(x) {
//    console.log("TimeSheet!!!!!!!!");
//    console.log(x);
//    debugger
//        $.ajax({
//            url: "/Profile/DeleteActivity",
//            method: "Post",
//            data:
//            {
//                "tid": x,
//            },
//            success: function (data) {
//                console.log(data);
//                debugger

//            },
//            error: function (e) {
//                console.log("Bye");
//                alert('Error');
//            },
//        });
//}



function settingsForNotification() {    var Value;    var settings = [];    $('input[name="sn"]:checked').each(function () {        Value = this.value;        settings.push(Value);    });    console.log(settings);    $.ajax({        url: "/Platform/settings",        method: "post",        data: {            settings: settings,        },        success: function () {            toastr.success("You have changed your notification setting!!");        },        error: function () {            toastr.error("Something went wrong!!");        }    });}function getsettings() {    $.ajax({        url: "/Platform/getsettings",        method: "post",        data: {                   },        success: function (data) {            data = JSON.parse(data);            console.log(data);            $("#RecommendedMission").prop("checked", data.RecommendedMission);            $("#Story").prop("checked", data.Story);            $("#NewMission").prop("checked", data.NewMission);            $("#RecommendedStory").prop("checked", data.RecommendedStory);            $("#MissionApplication").prop("checked", data.MissionApplication);            $("#EmailNotification").prop("checked", data.EmailNotification);        },        error: function () {            toastr.error("Something went wrong!!");        }    });
}


function getnotification() {
    $.ajax({        url: "/Platform/getnotification",        method: "post",        data: {        },        success: function (data) {                       console.log(data);                        $("#newnoti").empty();            $("#newnoti").html(data);                        var abc = document.querySelector("#fromdisplay").textContent;            console.log(abc);            $("#todisplay").text(abc);        },        error: function () {            toastr.error("Something went wrong!!");        }    });
}


function readNotification(x, y) {    $.ajax({        url: "/Platform/readNotification",        method: "post",        data: {            "id": y,            "status": x        },        success: function (data) {        },        error: function () {            toastr.error("Something went wrong!!");        }    });    return true;}