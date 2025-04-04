import NavBar from "../../components/NavBar";
import "../../App.css";
import { useContext, useEffect, useState } from "react";
import productService from "../../services/productService";
import useLoading from "../../hooks/useLoading";
import useError from "../../hooks/useError";
import DeleteModal from "../../components/DeleteModal";
import ErrorModal from "../../components/ErrorModal";
import GenericTable from "../../components/GenericTable";
import { getAllProducts, saveProducts, deleteProduct } from "../../storage/database"; 
import useProductStore from "../../storage/productStore";


export default function Products() {

  const { showError, showErrorModal, errors, hideError } = useError();
  const [entityToDelete, setEntityToDelete] = useState(null);
  const [showDeleteModal, setShowDeleteModal] = useState(false);

  const { products, setProducts, loadProductsFromDB } = useProductStore();

  useEffect(() => {
    loadProductsFromDB();
  }, []);

  async function deleteProduct(product) {

  }

  return (
    <>
      <NavBar></NavBar>
      <div>
        <h1 className="h1">PRODUCTS</h1>
        <GenericTable dataArray={products} cutRange={1}></GenericTable>
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
