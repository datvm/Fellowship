Blazor.registerFunction("GetStorage", (key) => {
    return localStorage.getItem(key);
});