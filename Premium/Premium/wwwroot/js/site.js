
$("#DateOfBirth").blur(function () {
    var dob = $.trim($('#DateOfBirth').val());
    if (moment(dob, 'DD/MM/YYYY', true).isValid()) {        
        var year = Math.floor(moment(new Date()).diff(moment(dob, "DD/MM/YYYY"), 'years', true));
        var month = Math.floor(moment(new Date()).diff(moment(dob, "DD/MM/YYYY"), 'months', true));       
        if (year > 0)
            $("#Age").val(year);
        else if (month > 0) {
            $('#Age').val("0." + month);
        }
        else {
            $('#DateOfBirth').val("");
            dob.focus();
        }
    }
    else {
        $('#DateOfBirth').val("");
    }
});

$("#Occupation").on("change", function (e) {
    var customerName = $.trim($('#Name').val());
    var age = $.trim($('#Age').val());
    var dateOfBirth = $.trim($('#DateOfBirth').val());
    var deathSumInsured = $.trim($('#SumInsured').val());
    var factor = $(this).children("option:selected").val();

    if (customerName == '' || age == '' || dateOfBirth == '' || factor == '' || deathSumInsured == '') {
        RemoveMessage("All fields are mandatory.");
        return false;
    }

    if (customerName.length < 3 || customerName.length > 50) {
        RemoveMessage("Name length must be between 3 between 50.");
    }
    else {
        if (!/^[a-zA-Z ]*$/.test(customerName)) {
            RemoveMessage("Only alphabets are allowed.");
            return false;
        }
    }

    if (age < 16 || age > 100) {
        RemoveMessage("Age must be between 16 between 100.");
        return false;
    }

    if (deathSumInsured < 1000 || deathSumInsured > 10000000) {
        RemoveMessage("Death Sum Insured value must be between $1000 and $100,00,000");     
        return false;
    }
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '/Home/CalculatePremium',
        data: { Name: customerName, Age: age, DateOfBirth: dateOfBirth, FactorRating: factor, SumInsured: deathSumInsured },
        success: function (data) {
            if (data.isSuccess) {
                if ($("#divMessage span").length > 0) {
                    $('#divMessage').find('span').remove()
                    var response = "Your monthly premium will be " + data.premium;
                    $("#divMessage").append("<span class='badge badge-success'>" + response + '</span>');
                }
                else {
                    var response = "Your monthly premium will be " + data.premium;
                    $("#divMessage").append("<span class='badge badge-success' id='msgSpan'>" + response + '</span>');
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            if ($("#divMessage span").length > 0) {
                $('#divMessage').find('span').remove()
                var response = "Premium could not be calculated due to an error. ";
                $("#divMessage").append("<span class='badge badge-danger'>" + response + '</span>');
            }
        }
    });
});

$("#btnClear").on("click", function (e) {
   
    $('#Name').val("");
    $('#Age').val("");
    $('#SumInsured').val("");
    $('#DateOfBirth').val("");
    //$('#Occupation').prop('selectedIndex', 0);
});

function RemoveMessage(message) {
    if ($("#divMessage span").length > 0) {
        $('#divMessage').find('span').remove()
        $("#divMessage").append("<span class='badge badge-danger'>" + message + '</span>');
    }
    return false;
}

