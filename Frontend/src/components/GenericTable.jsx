import {
  Col,
  Container,
  Form,
  InputGroup,
  Pagination,
  Row,
  Table,
} from "react-bootstrap";
import CustomButton from "./CustomButton";
import PropTypes from "prop-types";
import { useMemo, useState } from "react";
import moment from "moment";

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

  const [page, setPage] = useState(1);
  const [search, setSearch] = useState("");
  const [pageSize, setPageSize] = useState(10);
  const [decimalFilter, setDecimalFilter] = useState({});
  const [sortColumn, setSortColumn] = useState(null);
  const [sortDirection, setSortDirection] = useState("asc");

  const columns = Object.keys(dataArray[0]);

  columns.splice(0, cutRange);
  columns.splice(cutRangeForIsActiveStart, cutRangeForIsActiveEnd);

  const decimalColumns = useMemo(() => {
    return columns.filter((col) =>
      dataArray.some(
        (row) => typeof row[col] === "number" && row[col] % 1 !== 0
      )
    );
  }, [dataArray, columns]);

  const filteredData = useMemo(() => {
    return dataArray.filter((row) => {
      if (
        search &&
        !Object.values(row).some((value) =>
          value?.toString().toLowerCase().includes(search.toLowerCase())
        )
      ) {
        return false;
      }

      for (const col of decimalColumns) {
        if (
          (decimalFilter[col]?.min !== undefined &&
            row[col] < decimalFilter[col].min) ||
          (decimalFilter[col]?.max !== undefined &&
            row[col] > decimalFilter[col].max)
        ) {
          return false;
        }
      }

      return true;
    });
  }, [dataArray, search, decimalFilter, decimalColumns]);

  const totalPages = Math.ceil(filteredData.length / pageSize);

  const paginatedData = useMemo(() => {
    let sortedData = [...filteredData];

    if (sortColumn) {
      sortedData.sort((a, b) => {
        const valueA = a[sortColumn];
        const valueB = b[sortColumn];
  
        if (valueA == null || valueB == null) return 0;
  
        if (typeof valueA === "number" && typeof valueB === "number") {
          return sortDirection === "asc" ? valueA - valueB : valueB - valueA;
        } else if (moment(valueA, moment.ISO_8601, true).isValid() && moment(valueB, moment.ISO_8601, true).isValid()) {
          return sortDirection === "asc"
            ? moment(valueA).diff(moment(valueB))
            : moment(valueB).diff(moment(valueA));
        } else {
          return sortDirection === "asc"
            ? valueA.toString().localeCompare(valueB.toString())
            : valueB.toString().localeCompare(valueA.toString());
        }
      });
    }
    const startIndex = (page - 1) * pageSize;
    return sortedData.slice(startIndex, startIndex + pageSize);
  }, [filteredData, page, pageSize, sortColumn, sortDirection]);

  function handleDecimalFilterChange(e, column, type) {
    const value = e.target.value ? parseFloat(e.target.value) : undefined;
    setDecimalFilter((prev) => ({
      ...prev,
      [column]: { ...prev[column], [type]: value },
    }));
  }

  function formatCellValue(column, value) {
    if (
      column.toLowerCase().includes("date") ||
      column.toLowerCase().includes("time")
    ) {
      return moment(value).format("DD-MM-YYYY");
    }
    return value;
  }

  function handlePageSizeChange(e) {
    setPageSize(Number(e.target.value));
    setPage(1);
  }

  function handleSort(column) {
    if (sortColumn === column) {
      setSortDirection(sortDirection === "asc" ? "desc" : "asc");
    } else {
      setSortColumn(column);
      setSortDirection("asc");
    }
  }

  return (
    <>
      <Container className="main-container">
        <Row className="align-items-center">
          <Col md={6} xs={12}>
            <Form>
              <InputGroup className="mt-3 mb-2">
                <Form.Control
                  placeholder="Filter by name..."
                  onChange={(e) => setSearch(e.target.value)}
                  className="searchLabel"
                />
              </InputGroup>
            </Form>
          </Col>

          <Col md={2} xs={6} className="d-flex justify-content-start">
            <Form.Select
              onChange={handlePageSizeChange}
              value={pageSize}
              className="mt-3 mb-2"
              style={{ minWidth: "120px" }}
            >
              <option value="10">Show 10</option>
              <option value="25">Show 25</option>
              <option value="50">Show 50</option>
              <option value="100">Show 100</option>
            </Form.Select>
          </Col>
        </Row>

        {decimalColumns.length > 0 && (
          <Row className="mb-3">
            {decimalColumns.map((col) => (
              <Col md={6} xs={12} key={col}>
                <Form.Label style={{ color: "yellow" }}>
                  {col} range:
                </Form.Label>
                <InputGroup style={{ width: "99%" }}>
                  <Form.Control
                    type="number"
                    placeholder="Min"
                    onChange={(e) => handleDecimalFilterChange(e, col, "min")}
                  />
                  <Form.Control
                    type="number"
                    placeholder="Max"
                    onChange={(e) => handleDecimalFilterChange(e, col, "max")}
                  />
                </InputGroup>
              </Col>
            ))}
          </Row>
        )}

        <Table
          striped
          bordered
          hover
          responsive
          className={className}
          style={{ width: "100%" }}
        >
          <thead>
            <tr>
              <th>No</th>
              {columns.map((column) => (
                <th
                  key={column}
                  onClick={() => handleSort(column)} 
                  style={{ cursor: "pointer" }} 
                >
                  {column}
                  {sortColumn === column && (
                    <span>{sortDirection === "asc" ? " ↑" : " ↓"}</span>
                  )}
                </th>
              ))}
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {paginatedData.map((row, rowIndex) => (
              <tr key={rowIndex}>
                <td>{(page - 1) * pageSize + rowIndex + 1}</td>
                {columns.map((column) => (
                  <td key={column}>{formatCellValue(column, row[column])}</td>
                ))}
                <td>
                  <CustomButton
                    onClick={() => onUpdate(row)}
                    label="Update"
                    variant="primary"
                  />
                  <CustomButton
                    onClick={() => onDelete(row)}
                    label="Delete"
                    variant="danger"
                  />
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
        {totalPages > 1 && (
          <div
            style={{
              display: "flex",
              justifyContent: "center",
              marginTop: "10px",
            }}
          >
            <Pagination size="lg" className="pagination">
              <Pagination.First
                onClick={() => setPage(1)}
                disabled={page === 1}
              />
              <Pagination.Prev
                onClick={() => setPage(page - 1)}
                disabled={page === 1}
              />

              {Array.from({ length: totalPages }, (_, index) => index + 1)
                .filter(
                  (num) =>
                    Math.abs(num - page) < 3 || num === 1 || num === totalPages
                )
                .map((num, index, arr) => (
                  <Pagination.Item
                    key={num}
                    active={num === page}
                    onClick={() => setPage(num)}
                  >
                    {num}
                  </Pagination.Item>
                ))}

              <Pagination.Next
                onClick={() => setPage(page + 1)}
                disabled={page === totalPages}
              />
              <Pagination.Last
                onClick={() => setPage(totalPages)}
                disabled={page === totalPages}
              />
            </Pagination>
          </div>
        )}
      </Container>
    </>
  );
}

GenericTable.propTypes = {
  onUpdate: PropTypes.func.isRequired,
  onDelete: PropTypes.func.isRequired,
  cutRange: PropTypes.number,
};
