import Dexie from "dexie";

const db = new Dexie("ShopListDb");

db.version(1).stores({
  products: "id, name, unitprice, dateupdated",
});

export async function getAllProducts() {
  return await db.products.toArray();
}

export async function saveProducts(products) {
    const validProducts = products
    .filter(product => product.id) 
    .map(product => ({
      id: product.id, 
      name: product.name,
      unitPrice: product.unitPrice,
      dateUpdated: product.dateUpdated
    }));

    console.log("Saving valid products to Dexie:", validProducts);

  await db.products.clear();
  await db.products.bulkPut(products);
}

export async function deleteProduct(id) {
  await db.products.delete(id);
}

export default db;
