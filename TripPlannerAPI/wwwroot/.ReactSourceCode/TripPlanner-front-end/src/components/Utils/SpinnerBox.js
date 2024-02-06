import Spinner from "react-bootstrap/Spinner";

import styles from "./SpinnerBox.module.css";

const SpinnerBox = () => {
  return (
    <div className={styles["loading-spinner-container"]}>
    <Spinner animation="border" role="status">
      <span className="visually-hidden">Loading</span>
    </Spinner>
    </div>
  );
};

export default SpinnerBox;
