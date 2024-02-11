let fullHeight = function () {

    $('.js-fullheight').css('height', $(window).height());
    $(window).resize(function () {
        $('.js-fullheight').css('height', $(window).height());
    });

};
fullHeight();

$('#sidebarCollapse').on('click', function () {
    $('#sidebar').toggleClass('active');
});

function MessageErrorAlert(msg = "") {
    if (msg !== "")
        $("#msgError").text(msg);

    const toastError = document.getElementById('toastError')
    const toast = bootstrap.Toast.getOrCreateInstance(toastError)
    toast.show()
}

function MessageSuccessAlert(msg = "") {
    if (msg !== "")
        $("#msgSuccess").text(msg);

    const toastSuccess = document.getElementById('toastSuccess')
    const toast = bootstrap.Toast.getOrCreateInstance(toastSuccess)
    toast.show()
}