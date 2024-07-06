function validateFileSize() {
    var input = document.getElementById('coverInput');
    var maxSize = FileSettings.MaxSizeInByte;

    if (input.files && input.files.length > 0) {
        var file = input.files[0];
        if (file.size > maxSize) {
            var maxSizeInMB = (maxSize / (1024 * 1024)).toFixed(2);
            alert(`The file size should not exceed ${maxSizeInMB} MB.`);
            return false;
        }
    }
    return true;
}
