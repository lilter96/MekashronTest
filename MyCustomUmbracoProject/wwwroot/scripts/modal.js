document.addEventListener("DOMContentLoaded", function() {
    const handleModalFadeOut = (modalElement) => {
        if (modalElement != null && modalElement.classList.contains('show')) {
            setTimeout(() => {
                modalElement.classList.remove('show');
                setTimeout(() => {
                    modalElement.style.display = 'none';
                }, 300);
            }, 2000);
        }
    };

    const handleModalClose = (modalElement, closeButtons) => {
        closeButtons.forEach(button => {
            button.addEventListener("click", () => {
                modalElement.style.display = "none";
                modalElement.classList.remove("show");
            });
        });
    };


    const errorModal = document.getElementById('errorModal');
    const successModal = document.getElementById('successModal');

    const errorCloseButtons = document.querySelectorAll("#errorModal .btn-close");
    const successCloseButtons = document.querySelectorAll("#successModal .btn-close");
    
    handleModalFadeOut(errorModal);
    handleModalFadeOut(successModal);

    handleModalClose(errorModal, errorCloseButtons);
    handleModalClose(successModal, successCloseButtons);
});
