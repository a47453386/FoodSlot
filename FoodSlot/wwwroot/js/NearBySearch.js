const response = await fetch(
    '/StoreSearch/Nearby',
    {
        method: 'POST',

        headers: {
            'Content-Type': 'application/json'
        },

        body: JSON.stringify({
            keyword: '拉麵',
            latitude: 25.033,
            longitude: 121.5654,
            radius: 1500
        })
    });

const stores = await response.json();

console.log(stores);