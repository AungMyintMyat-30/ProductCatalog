function menu_dashboard() {
    // Menu Item Active Method
    $('#menu_dashboard').addClass("active");
}

function menu_master_category() {
    // Menu Item Active Method
    $('#menu_master_collapse').addClass("menu-open");

    // Menu Item Active Method
    $('#menu_master').addClass("active");

    // Menu Item Extract Collapse
    $('#menu_master_category').addClass("active");
}

function numberWithCommas(x) {
    if (x === null || x === undefined) {
        return '0';
    }
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function getDateFormat(data) {
    if (data === null || data === undefined) {
        return '';
    }
    return data.substring(8, 10) + '/' + getMonthName(data.substring(5, 7)) + '/' + data.substring(0, 4);
}

function getMonthName(no) {
    if (no == '01') {
        return "Jan";
    }
    else if (no == '02') {
        return 'Feb';
    }
    else if (no == '03') {
        return 'Mar';
    }
    else if (no == '04') {
        return 'Apr';
    }
    else if (no == '05') {
        return 'May';
    }
    else if (no == '06') {
        return 'Jun';
    }
    else if (no == '07') {
        return 'Jul';
    }
    else if (no == '08') {
        return 'Aug';
    }
    else if (no == '09') {
        return 'Sep';
    }
    else if (no == '10') {
        return 'Oct';
    }
    else if (no == '11') {
        return 'Nov';
    }
    else if (no == '12') {
        return 'Dec';
    }
}