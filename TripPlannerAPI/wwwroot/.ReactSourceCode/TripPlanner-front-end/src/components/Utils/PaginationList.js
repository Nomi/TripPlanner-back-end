import Pagination from "react-bootstrap/Pagination";
import styles from "./PaginationList.module.css";

const PaginationList = (props) => {
  const previousClick = () => {
    props.onPreviousHandler();
  };

  const nextClick = (event) => {
    props.onNextHandler();
  };

  const pageNumberClick = (event) => {
    props.onPageNumberHandler(event.target.text);
  };

  let active = Number(props.paginationState.currentPage);
  let totalNumPages = Math.ceil(props.sumOfTrips / props.paginationState.tripsPerPage);
  let items = [];
  active > 1 &&
    items.push(
      <Pagination.Item key="prev" onClick={previousClick}>
        Previous
      </Pagination.Item>
    );
  for (let number = 1; number <= totalNumPages; number++) {
    items.push(
      <Pagination.Item key={number} active={number === active} onClick={pageNumberClick}>
        {number}
      </Pagination.Item>
    );
  }
  active < totalNumPages &&
    items.push(
      <Pagination.Item key="next" onClick={nextClick}>
        Next
      </Pagination.Item>
    );

  return (
    <div className={styles["pagination-container"]}>
      <Pagination>{items}</Pagination>
    </div>
  );
};

export default PaginationList;
