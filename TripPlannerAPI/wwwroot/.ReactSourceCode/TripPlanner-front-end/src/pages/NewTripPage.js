import styles from "./Pages.module.css";
import NewTrip from "../components/NewTrip/NewTrip";

const NewTripPage = (props) => {
  return (
    <main className={styles["main-container"]}>
      <NewTrip />
    </main>
  );
};

export default NewTripPage;
