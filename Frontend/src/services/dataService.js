import { httpService, readAll } from "./httpService";

export async function fetchAllProducts() {
    try {
        const response = await httpService.readAll("products")
    }
    catch (err) {

    }
}