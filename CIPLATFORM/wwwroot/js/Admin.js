function searchuser(pg, key) {
    if (pg == undefined) {
        pg = 1;
    }
    if (key == "user") {
        var search = document.getElementById("searchuser").value;
    }
    if (key == "cms") {
        var search = document.getElementById("searchcms").value;
    }
    if (key == "mission") {
        var search = document.getElementById("searchmission").value;
    }
    if (key == "theme") {
        var search = document.getElementById("searchtheme").value;
    }
    if (key == "skill") {
        var search = document.getElementById("searchskill").value;
    }
    if (key == "application") {
        var search = document.getElementById("searchapplication").value;
    }
    if (key == "story") {
        var search = document.getElementById("searchstory").value;
    }
    console.log(search)

    $.ajax({
        type: "POST", // POST
        url: '/Admin/UserFilter',
        data: {
            'search': search,
            'pg': pg,
            'key': key,
        },
        dataType: "html", // return datatype like JSON and HTML
        success: function (data) {

            console.log(data);
            if (key == "user") {
                $("#hi").empty();
                $("#hi").html(data);
            }
            else if (key == "cms") {
                $("#hi2").empty();
                $("#hi2").html(data);
            }
            else if (key == "mission") {
                $("#hi3").empty();
                $("#hi3").html(data);
            }
            else if (key == "theme") {
                $("#hi4").empty();
                $("#hi4").html(data);
            }
            else if (key == "skill") {
                $("#hi5").empty();
                $("#hi5").html(data);
            }
            else if (key == "application") {
                $("#hi6").empty();
                $("#hi6").html(data);
            }
            else if (key == "story") {
                $("#hi7").empty();
                $("#hi7").html(data);
            }
        },
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}

function getdata(x, id) {

    var page = document.getElementById(x);

    var addForm = page.querySelector("#edit");



    $.ajax({
        url: "/Admin/EditForm",
        method: "Post",
        data:
        {
            "id": id,
            "page": x,
        },
        success: function (data) {

            console.log(data);
            var htmlObject = document.createElement('div');
            htmlObject.innerHTML = data;

            var abc = htmlObject.querySelector("#edit");
            abc.style.display = "block";

            console.log(abc);
            console.log(addForm);

            addForm.replaceWith(abc);
            if (x == "nav-cms") {
                var abc = document.getElementById("cms2");
                CKEDITOR.replace(abc);
            }
            if (x == "nav-user") {                $("#profileImageInput1").on('change', function () {
                    console.log("2");
                    readURL1(this);

                });            }
        },
        error: function (e) {
            debugger
            console.log("Bye");
            alert('Error');
        },
    });

}


function GetsCity(x) {
    if (x != undefined) {
        var countryId = $('#countryId' + x).find(":selected").val();
        var city = "selectCityList" + x;
    }
    else {
        var countryId = $('#countryId').find(":selected").val();
        var city = "selectCityList";
    }
        $.ajax({
            url: "/Platform/GetCitys",
            method: "GET",
            data: {
                "countryId": countryId
            },
            success: function (data) {
                data = JSON.parse(data);
                $("#"+city).empty();
                
                document.getElementById(city).innerHTML += `
        <option value=${name}> City </option>
        `;
                data.forEach((name) => {
                    document.getElementById(city).innerHTML += `
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
