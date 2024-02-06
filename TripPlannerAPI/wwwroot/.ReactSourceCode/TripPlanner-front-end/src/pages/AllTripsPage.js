import styles from "./Pages.module.css";
import AllTrips from "../components/AllTrips/AllTrips";

const AllTripsPage = () => {
  return (
    <main className={styles["main-container"]}>
      <AllTrips />
    </main>
  );
};

export default AllTripsPage;
