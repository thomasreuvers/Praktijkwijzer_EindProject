$(document).ready(function () {

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });

    // Create reservation
    // Basically reset all the fields
    $('.create').click(function() {
        $('#formOption').val("");
        $('#createUpdateReservationId').val("");
        $('#EmailAddress').val("");
        $('#PhoneNumber').val("");
        $('#ArriveDate').val("");
        $('#DepartureDate').val("");
        $(".reservationEdit").text("Reserveren");
        $('.reservationBtn').val("Reserveer");
    });

    // Deleting reservation
    $('.delete').click(function () {
        var id = $(this).data("reservation");
        $('#deleteReservationId').val(id);
    });

    // Updating reservation
    $('.update').click(function() {
        var id = $(this).data("reservation");
        var arriveDate = $(this).data("arrivedate");
        var departureDate = $(this).data("departuredate");
        var emailAddress = $(this).data("emailaddress");
        var phoneNumber = $(this).data("phonenumber");


        $(".reservationEdit").text("Update");
        $('.reservationBtn').val("Update");

        $('#FormOption').val("EDIT");
        $('#ReservationId').val(id);
        $('#EmailAddress').val(emailAddress);
        $('#PhoneNumber').val(phoneNumber);
        $('#ArriveDate').val(arriveDate);
        $('#DepartureDate').val(departureDate);

    });

    $('.handle').click(function() {
        var id = $(this).data('complaintid');

        $("#FormOption").val("HANDLE");
        $("#Id").val(id);
    });

    $('.viewComplaint').click(function() {
        var description = $(this).data("description");
        var title = $(this).data("title");

        $('.complaintTitle').text(title);
        $('.description').text(description);
    });
}); 
