$(document).ready(function () {
    $('.js-delete').on('click', function () {
        var btn = $(this);
        console.log('Delete button clicked:', btn.data('id'));

        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-danger mx-2',
                cancelButton: 'btn btn-light'
            },
            buttonsStyling: false
        });

        swalWithBootstrapButtons.fire({
            title: 'Are you sure you want to delete this game?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, cancel!',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                console.log('Confirmed delete for:', btn.data('id'));

                $.ajax({
                    url: `/Games/Delete/${btn.data('id')}`,
                    method: 'DELETE',
                    success: function () {
                        console.log('Successfully deleted:', btn.data('id'));

                        swalWithBootstrapButtons.fire(
                            'Deleted!',
                            'Game has been deleted.',
                            'success'
                        );

                        btn.parents('tr').fadeOut();
                    },
                    error: function (xhr, status, error) {
                        console.error('Delete failed:', status, error);

                        swalWithBootstrapButtons.fire(
                            'Oops...',
                            'Something went wrong.',
                            'error'
                        );
                    }
                });
            }
        });
    });
});
