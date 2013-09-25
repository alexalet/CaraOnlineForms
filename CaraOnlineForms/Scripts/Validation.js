function ValidatePassword(pwd, confirm)
{
    if (pwd.length < 6)
        return "Password is too short. Minimum length of password - 6 characters";
    if (/[A-Z]/.test(pwd) == false)
    {
        return "Password must contain at least one character in Upper Case"
    }

    if (/[a-z]/.test(pwd) == false) {
        return "Password must contain at least one character in Lower Case"
    }

    if (/[0-9]/.test(pwd) == false) {
        return "Password must contain at least one Number"
    }

    if (/[^0-9,a-z,A-Z]/.test(pwd) == false) {
        return "Password must contain at least one Special Character"
    }

    if (pwd != confirm) {
        DisplayErrors("'Confirm Password' doesn't match Password", 'Error');
        return;
    }

    return "";
}//--end ValidatePassword --

function ValidateEmail(email)
{
    var ck_email = /^([\w-]+(?:\.[\w-]+)*)@@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i
    return ck_email.test(email);
           
}//--end ValidateEmail --