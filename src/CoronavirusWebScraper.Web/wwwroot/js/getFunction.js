export default async function ApiGetFunction(url,) {
    try {
        const response = await fetch(url);
        if (response.ok == false) {
            const error = await response.json();
            throw new Error(error.message);
        }
        try {
            const data = await response.json();
            return data;
        } catch (err) {
            return response;
        }
    } catch (err) {
        console.error(err.message);
    }
}
