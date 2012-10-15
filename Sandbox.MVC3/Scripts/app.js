$.validator.addMethod("sqlserverdate", function (value, element, params) {
    var minDateValue = new Date(params[0]),
        maxDateValue = new Date(params[1]),
        dateValue = new Date(value);
        
    return this.optional(element) || (minDateValue <= dateValue && dateValue <= maxDateValue);
}, "");

$.validator.unobtrusive.adapters.addMinMax("sqlserverdate", "sqlserverdate", "sqlserverdate", "sqlserverdate");

