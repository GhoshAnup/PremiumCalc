
$("#DateOfBirth").blur(function () {
    var dob = document.getElementById('DateOfBirth');
    var age = document.getElementById('Age');
    if (moment(dob.value, 'DD/MM/YYYY', true).isValid()) {
        var year = Math.floor(moment(new Date()).diff(moment(dob.value, "DD/MM/YYYY"), 'years', true));
        var month = Math.floor(moment(new Date()).diff(moment(dob.value, "DD/MM/YYYY"), 'months', true));
        if (year > 0)
            age.value = year;
        else if (month > 0) {
            age.value = "0." + month;
        }
        else {
            dob.value = "";
            dob.focus();
        }
    }
    else {
        dob.value = "";
    }
});

