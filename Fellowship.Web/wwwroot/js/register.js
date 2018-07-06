Blazor.registerFunction("GetStorage", (key) => {
    return localStorage.getItem(key);
});

Blazor.registerFunction("SetStorage", (key, value) => {
    return localStorage.setItem(key, value);
});

Blazor.registerFunction("NavigateTo", (url) => {
    window.location.href = url;
});