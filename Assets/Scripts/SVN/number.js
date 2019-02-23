Number.prototype.toDigits = function (digits) {
    return this.toString().toDigits(digits);
};

String.prototype.toDigits = function (digits) {
    var result = this;
    while (result.length < digits) {
        result = "0" + result;
    }
    return result;
};

function isNumeric(value) {
    return !isNaN(parseFloat(value)) && isFinite(value);
}