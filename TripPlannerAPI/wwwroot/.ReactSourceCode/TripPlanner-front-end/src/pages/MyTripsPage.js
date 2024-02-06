import styles from "./Pages.module.css";
import MyTrips from "../components/MyTrips/MyTrips";

const MyTripsPage = () => {
  return (
    <main className={styles["main-container"]}>
      <MyTrips />
    </main>
  );
};

export default MyTripsPage;
