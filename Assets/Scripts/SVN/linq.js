Array.prototype.first = function (lambda) {
    let result = [];

    for (let i = 1; i <= this.length; i++) {
        let item = this[i - 1];

        if (!lambda || lambda(item)) {
            result.push(item);
        }
    }

    return result[0];
};

Array.prototype.last = function (lambda) {
    let result = [];

    for (let i = 1; i <= this.length; i++) {
        let item = this[i - 1];

        if (!lambda || lambda(item)) {
            result.push(item);
        }
    }

    return result[result.length - 1];
};

Array.prototype.where = function (lambda) {
    let result = [];

    for (let i = 1; i <= this.length; i++) {
        let item = this[i - 1];

        if (lambda(item)) {
            result.push(item);
        }
    }

    return result;
};

Array.prototype.select = function (lambda) {
    let result = [];

    for (let i = 1; i <= this.length; i++) {
        let item = this[i - 1];
        result.push(lambda(item));
    }

    return result;
};

Array.prototype.count = function (lambda) {
    let result = 0;

    for (let i = 1; i <= this.length; i++) {
        let item = this[i - 1];

        if (!lambda || lambda(item)) {
            result++;
        }
    }

    return result;
};