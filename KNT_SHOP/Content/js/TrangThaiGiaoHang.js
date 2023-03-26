function get_updated() {
    fetch('http://127.0.0.1:5000/shipment_data' , {
        method: 'GET',
        headers: {
            'contentype': 'application/json'
        }
    })
        .then(res => res.json())
        .then(data => {
            var list = {}
            data.forEach(element=> list.push(element))
        })
    fetch('https://localhost:44376/Invoice/FetchAndSaveData', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(list)
    })
        .then(res => res.json())
        .then(result => console.log(result))
        .catch(error => console.error(error));
}


window.addEventListener('load', function () {
    get_updated();
})