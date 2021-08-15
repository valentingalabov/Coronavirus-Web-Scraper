/*Export get request function.*/
export default async function ApiGetFunction(path) {
    try {
        const response = await fetch(window.location.origin + path);
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
