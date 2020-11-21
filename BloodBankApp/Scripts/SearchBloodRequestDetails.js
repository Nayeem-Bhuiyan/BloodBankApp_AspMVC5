$(document).ready(function () {
    $("#btnSearch").click(function() {
        var SearchParam = $("#SearchParam_bloodRequest").val();
        var obj = '';
        if (SearchParam!=null) {
            obj = {
                Name: SearchParam,
                Phone: SearchParam,
                Problem: SearchParam,
                Address: SearchParam,
                HospitalName: SearchParam,
                BloodRequestDate: SearchParam,
                BloodNeedDate: SearchParam,
                GroupName: SearchParam,
                GenderName: SearchParam,

            }
        } else {
            alert("please insert search parameter");
        }


        $.post('/Search/SearchBloodRequestDetails', obj, function (data) {

            if (data.length <= 0) {
                $("#Search_Div").hide();
                window.location.replace("/BloodRequests/Index");
                alert("No data has found!!");
                
            }
            console.log(data);
            var html = '';

            $.each(data, function (key, item) {


                var RequestDate = item.BloodRequestDate;

                var completedDateText = RequestDate;
                var completedDate = new Date(parseInt(completedDateText.replace("/Date(", "").replace(")/")));
                var dd = completedDate.getDate();
                var mm = completedDate.getMonth() + 1; //January is 0! 
                var yyyy = completedDate.getFullYear();
                if (dd < 10) { dd = '0' + dd; }
                if (mm < 10) { mm = '0' + mm; }


                var NeedDate = item.BloodNeedDate;

                var completedDateText = NeedDate;
                var completedDate = new Date(parseInt(completedDateText.replace("/Date(", "").replace(")/")));
                var dd1 = completedDate.getDate();
                var mm1 = completedDate.getMonth() + 1; //January is 0! 
                var yyyy1 = completedDate.getFullYear();
                if (dd1 < 10) { dd1 = '0' + dd1; }
                if (mm1 < 10) { mm1= '0' + mm1; }


                html += '<tr><td>' + item.Name + '</td><td>' + item.Phone + '</td><td>' + item.Problem + '</td><td>' + item.Address + '</td><td>' + item.HospitalName + '</td><td>' + mm + ' / ' + dd + ' /' + yyyy + '</td><td>' + mm1 + ' / ' + dd1 + ' /' + yyyy1 +'</td><td>' + item.GroupName + '</td><td>' + item.GenderName + '</td><td><button type="button" class="btn btn-warning" onclick="DeleteData(' + item.id + ')">Delete</button></td></tr>';

            })

            $("#Search_Div").show();
            $("#Div_tblBloodRequestIndex").hide();

            $("#SearchResult_tbl_BloodRequestData").append(html);



        })

    })



    $("#btnBack").click(function () {
        window.location.replace("/BloodRequests/Index");
    });






});


function DeleteData(id)
{
    var ans = confirm("are you sure You want to delete this record!!");

    if (ans) {
        $.post("/Search/DeleteConfirm/" + id, {}, function (data) {
            console.log(data);
            window.location.replace("/BloodRequests/Index");
        });
    }

    
}