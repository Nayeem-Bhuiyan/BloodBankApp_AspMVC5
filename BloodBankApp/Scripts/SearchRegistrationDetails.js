$(document).ready(function () {
    $("#btnRegSearch").click(function () {
        var SearchParam = $("#SearchParam_NewRegistration").val();
        console.log(SearchParam);
        var obj = '';
        if (SearchParam != null) {
            obj = {
                EmailNumber: SearchParam,
                Password: SearchParam,
                DonorName: SearchParam,
                Phone: SearchParam,
                Address: SearchParam,
                GenderName: SearchParam,
                GroupName: SearchParam,
                Qualification: SearchParam

            }
        } else {
            alert("please insert search parameter");
        }


        $.post('/Search/SearchRegitrationDetails', obj, function (data) {

            if (data.length <= 0) {
                $("#Div_Search_Registration").hide();
                window.location.replace("/NewRegistrations/Index");
                alert("No data has found!!");

            }
            console.log(data);
            var html = '';

            $.each(data, function (key, item) {

                html += '<tr><td>' + item.DonorName + '</td><td>' + item.GenderName + '</td><td>' + item.EmailNumber + '</td><td>' + item.Password + '</td><td>' + item.Phone + '</td><td>' + item.Age + '</td><td>' + item.Address + '</td><td>' + item.Qualification + '</td><td>' + item.GroupName + '</td><td><img src="BloodBankApp/' + item.ImageUrl + '" alt="Alternate Text" width="70" height="70" /></td><td><button type="button" class="btn btn-danger glyphicon glyphicon-trash" onclick="DeleteRegistration(' + item.RegId + ')">Delete</button></td></tr>';

            })

            $("#Div_Search_Registration").show();
            $("#Div_table_Registration").hide();
            $("#tbl_RegistrationData").append(html);


            
        })

    })

    

    $("#btnBack").click(function () {
        window.location.replace("/NewRegistrations/Index");
    });






});