function GetLSInfo() {
    $("#<%= hdn_username.%> ").val(localStorage.getItem('nusr'));
    $("#<%= btn_populateUserData.ClientID %>").click();
}

function toggleLoader(show) {
    if (show) {
        $('.cd-popup3').addClass('is-visible');
    }
    else {
        $('.cd-popup3').removeClass('is-visible');
    }
}

function closePopup2() {
    $('.cd-popup2').removeClass('is-visible');
    redirectToAllCourses();
}

function redirectToAllCourses() {
    window.location.href = 'http://sp2019:8005/dpc/Pages/ListOfCourse.aspx';
}