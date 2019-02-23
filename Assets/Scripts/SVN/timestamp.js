function timestampToDate(value) {
    return new Date(value);
}

function dateToTimestamp(value) {
    return Math.round(value);
}

function timestampToTimespan(timestamp) {
    let weeks = Math.floor(timestamp / (7 * 24 * 60 * 60 * 1000));
    timestamp -= weeks * 7 * 24 * 60 * 60 * 1000;

    let days = Math.floor(timestamp / (24 * 60 * 60 * 1000));
    timestamp -= days * 24 * 60 * 60 * 1000;

    let hours = Math.floor(timestamp / (60 * 60 * 1000));
    timestamp -= hours * 60 * 60 * 1000;

    let minutes = Math.floor(timestamp / (60 * 1000));
    timestamp -= minutes * 60 * 1000;

    let seconds = Math.floor(timestamp / (1000));
    timestamp -= seconds * 1000;

    return {
        weeks,
        days,
        hours,
        minutes,
        seconds,
    };
}

function timestampToTimespanText(timestamp) {
    let timespan = timestampToTimespan(timestamp);

    if (1 < timespan.weeks) {
        return "vor " + timespan.weeks + " Wochen";
    }
    if (1 == timespan.weeks) {
        return "vor " + timespan.weeks + " Woche";
    }
    if (1 < timespan.days) {
        return "vor " + timespan.days + " Tagen";
    }
    if (1 == timespan.days) {
        return "vor " + timespan.days + " Tag";
    }
    if (1 < timespan.hours) {
        return "vor " + timespan.hours + " Stunden";
    }
    if (1 == timespan.hours) {
        return "vor " + timespan.hours + " Stunde";
    }
    if (1 < timespan.mins) {
        return "vor " + timespan.mins + " Minuten";
    }
    if (1 == timespan.mins) {
        return "vor " + timespan.mins + " Minute";
    }
    if (1 < timespan.seconds) {
        return "vor " + timespan.seconds + " Sekunden";
    }
    if (timespan.seconds <= 1) {
        return "gerade eben";
    }

    return "???";
}