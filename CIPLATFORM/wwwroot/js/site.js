    function GetCity(){
        console.log("het");
    var countryId = $('#countryId').find(":selected").val();
    debugger;
    $.ajax({
        url:"/Platform/GetCity",
    method: "GET",
    data: {
        "countryId":countryId
    },
    success:function(data){
        data = JSON.parse(data);

    $("#selectCityList").empty();
    data.forEach((name)=> {
        document.getElementById("selectCityList").innerHTML += `
    <option value=${name} >${name.Name}</option>
    `;
    })
    },
    error: function(request,error){
        console.log(error);
    }
    })
    }

