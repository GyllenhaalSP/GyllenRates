function saveConversionHistory(key, history) {
    localStorage.setItem(key, JSON.stringify(history));
}

function getConversionHistory(key) {
    const history = localStorage.getItem(key);
    return history ? JSON.parse(history) : [];
}

function deleteConversionHistory(key) {
    localStorage.removeItem(key);
}