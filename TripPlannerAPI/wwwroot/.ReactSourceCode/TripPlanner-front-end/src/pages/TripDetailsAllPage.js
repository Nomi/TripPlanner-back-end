import styles from "./Pages.module.css";
import TripDetailsAll from "../components/TripDetailsAll/TripDetailsAll";

const TripDetailsAllPage = (props) => {
  return (
    <main className={styles["main-container"]}>
      <TripDetailsAll />
    </main>
  );
};

export default TripDetailsAllPage;
