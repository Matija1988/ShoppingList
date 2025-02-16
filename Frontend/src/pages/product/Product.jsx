import NavBar from "../../components/NavBar";
import "../../App.css";
import { useEffect, useState } from "react";
import productService from "../../services/productService";
import useLoading from "../../hooks/useLoading";
import useError from "../../hooks/useError";
import DeleteModal from "../../components/DeleteModal";
import ErrorModal from "../../components/ErrorModal";
import GenericTable from "../../components/GenericTable";

export default function Products() {
  const [products, setProducts] = useState([]);

  const { hideLoading, showLoading } = useLoading();
  const { showError, showErrorModal, errors, hideError } = useError();
  const [entityToDelete, setEntityToDelete] = useState(null);

  const [showDeleteModal, setShowDeleteModal] = useState(false);

  async function getProducts() {
    showLoading();
    const response = await productService.readAll("products");
    if (!response.ok) {
        hideLoading();
      showError(response.data);
    }
    setProducts(response.data);
    hideLoading();
  }

  useEffect(() => {
    getProducts();
  }, []);

  async function deleteProduct(product) {

  }

  return (
    <>
      <NavBar></NavBar>
      <div>
        <h1 className="h1">PRODUCTS</h1>
        <GenericTable dataArray={products}></GenericTable>
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
