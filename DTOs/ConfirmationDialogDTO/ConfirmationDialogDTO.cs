namespace CodelineStore.DTOs.ConfirmationDialogDTO
{
    public class ConfirmationDialogDTO
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string ConfirmButtonText { get; set; } = "Confirm";
        public string CancelButtonText { get; set; } = "Cancel";
    }

}
