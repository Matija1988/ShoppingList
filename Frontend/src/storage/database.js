import Dexie from "dexie";

const db = new Dexie("ShopListDb");

db.version(3).stores({
  products: "id, name, unitprice, dateupdated",
  shopLists: "shopListId, shopListName, dateUpdated, isActive, shopListTotalValue, products",
});

db.version(3).upgrade((tx) => {
  return tx.shopLists.toCollection().modify((shopList) => {
    shopList.products = shopList.products || [];
  });
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

export async function addShopList(shopLists) {
  if (!Array.isArray(shopLists)) {
    console.error("Expected an array, but got:", shopLists);
    return; 
  }

  console.log("Valid shopLists received:", shopLists);

  const validShopList = shopLists
  .filter(shopList => shopList.shopListId)
  .map(shopList => ({
    shopListId: shopList.shopListId,
    shopListName: shopList.shopListName,
    dateUpdated: shopList.dateUpdated,
    isActive: shopList.isActive,
    shopListTotalValue: shopList.shopListTotalValue,
    products: shopList.products || [],
  }));

  console.log("Saving valid shopLists to Dexie: ", validShopList);

  await db.shopLists.clear();
  await db.shopLists.bulkPut(validShopList);
};

export async function getAllShopLists() {
  return await db.shopLists.toArray();
};

export async function deleteShopList (shopListId) {
  return await db.shopLists.delete(shopListId);
};

export async function updateShopList (shopListId, updatedData) {
  return await db.shopLists.update(shopListId, updatedData);
};

export default db;
