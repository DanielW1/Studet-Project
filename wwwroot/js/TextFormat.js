function razorNameAndFormat(value) {
    var val = value.split("&#x2B;").join("+");
    
    return val.split("&#x142;").join("ł");
}