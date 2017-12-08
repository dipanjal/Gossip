$(function () {

    $("#email").focusout(function () {
        isEmpty("#email", "email is required");
        isValidate("#email", "^[a-zA-Z0-9]+([._-][a-zA-Z0-9]+)?@[a-zA-Z0-9]{2,50}([.][a-z]{2,5})+$", "Insert a valid email address");
    });

    $("#fullname").focusout(function () {
        isEmpty("#fullname", "name is required");
        isValidate("#fullname", "^([A-z]+[.]?( [A-z]+)+)+$", "Insert a valid name Ex: Dipanjal Maitra");
    });

    $("#password").focusout(function () {
        isEmpty("#password", "password is required");
        isValidate("#password", "^(.){5,}$","Password Length minimum:5");
        //isValidate("#fullname", "^([A-z]+[.]?( [A-z]+)+)+$", "Insert a valid name");
    });


});

$(function () {
    $("#uname").focusout(function () {
        isEmpty("#uname", "username is required");
    });

    $("#upassword").focusout(function () {
        isEmpty("#upassword", "password is required");
    });
});

function isEmpty(inputid, msg)
{
    var inputVal = $(inputid).val();
    var labelid = "#label_"+inputid.replace(/#/g, '');
    if (inputVal == ""){
        $(labelid).html(msg);
    }
    else {
        $(labelid).html("");
    }
    
}

function isValidate(inputid,pattern,msg)
{
    var inputVal = $(inputid).val();
    var labelid = "#label_" + inputid.replace(/#/g, '');
    if (!inputVal.match(pattern)) {
        if (inputVal != "")
        {
            $(labelid).html(msg);
        }
        //alert(msg);
    }
    else {
        $(labelid).html("");
    }
}