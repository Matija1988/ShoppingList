import Dexie from "dexie";

const db = new Dexie("ShopListDb");

db.version(2).stores({
  products: "id, name, unitprice, dateupdated",
  shopLists: "++shopListId, shopListName, dateUpdated, isActive, shopListTotalValue",
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

db.addShopList = async (shopList) => {
  try {
    return await db.shopLists.add(shopList);
  } catch (error) {
    console.error("Failed to add shop list:", error);
  }
};

db.getAllShopLists = async () => {
  return await db.shopLists.toArray();
};

db.deleteShopList = async (shopListId) => {
  return await db.shopLists.delete(shopListId);
};

db.updateShopList = async (shopListId, updatedData) => {
  return await db.shopLists.update(shopListId, updatedData);
};

export default db;
