import { useEffect, useState } from "react";
import NavBar from "../../components/NavBar";
import useError from "../../hooks/useError";
import ErrorModal from "../../components/ErrorModal";
import DeleteModal from "../../components/DeleteModal";
import useShopListStore from "../../storage/shopListStore";
import GenericTable from "../../components/GenericTable";

export default function ShopList() {
  const { showError, showErrorModal, errors, hideError } = useError();
  const [entityToDelete, setEntityToDelete] = useState(null);
  const [showDeleteModal, setShowDeleteModal] = useState(false);

  const { shopLists, setShopLists, loadShopListsFromDB } = useShopListStore();

  useEffect(() => {
    loadShopListsFromDB();
  }, []);

  async function deleteProduct(product) {}

  return (
    <>
      <NavBar></NavBar>
      <div>
        <h1>1</h1>
        <h1>ShopList</h1>
        {shopLists && shopLists.length > 0 ? (
          shopLists.map((shopList) => (
            <div key={shopList.shopListId}>
              <h3>{shopList.shopListName}</h3>
              <p>{shopList.dateUpdated}</p>
              <p>Total: {shopList.shopListTotalValue}</p>
              <ul>
                {shopList.products && shopList.products.length > 0 ? (
                  shopList.products.map((productDetail, index) => (
                    <li key={index}>
                      <div>{productDetail.product?.name}</div>
                      <div>Quantity: {productDetail.productQuantity}</div>
                      <div>Total Value: {productDetail.totalValue}</div>
                    </li>
                  ))
                ) : (
                  <li>No products available</li>
                )}
              </ul>
            </div>
          ))
        ) : (
          <p>Loading shop lists...</p>
        )}
      </div>
      <ErrorModal
        show={showErrorModal}
        onHide={hideError}
        errors={errors}
      ></ErrorModal>
      <DeleteModal
        show={showDeleteModal}
        handleClose={() => setShowDeleteModal(false)}
        handleDelete={deleteProduct}
        entity={entityToDelete}
      ></DeleteModal>
    </>
  );
}
