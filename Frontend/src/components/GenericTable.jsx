import { Table } from "react-bootstrap";
import CustomButton from "./CustomButton";
import PropTypes from "prop-types";

export default function GenericTable({
  dataArray,
  onUpdate,
  onDelete,
  cutRange,
  cutRangeForIsActiveStart,
  cutRangeForIsActiveEnd,
  className,
}) {
  if (!dataArray || dataArray.lenght === 0) {
    return <p>No data to load</p>;
  }

  if (typeof dataArray[0] !== "object" || dataArray[0] === null) {
    return <p>No data to load</p>;
  }

  const columns = Object.keys(dataArray[0]);

  columns.splice(0, cutRange);
  columns.splice(cutRangeForIsActiveStart, cutRangeForIsActiveEnd);

  return (
    <>
      <Table striped bordered hover responsive className={className}>
        <thead>
          <tr>
            <th>No</th>
            {columns.map((column) => (
              <th key={column}>{column}</th>
            ))}
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {dataArray.map((row, rowIndex) => (
            <tr key={rowIndex}>
              <td>{rowIndex + 1}</td>
              {columns.map((column) => (
                <td key={column}>{row[column]}</td>
              ))}
              <td>
                <CustomButton
                  onClick={() => onUpdate(row)}
                  label="Update"
                  variant="primary"
                ></CustomButton>
                <CustomButton
                  onClick={() => onDelete(row)}
                  label="Delete"
                  variant="danger"
                ></CustomButton>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </>
  );
}

GenericTable.propTypes = {
  onUpdate: PropTypes.func.isRequired,
  onDelete: PropTypes.func.isRequired,
  cutRange: PropTypes.number,
};