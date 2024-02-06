import styles from "./Pages.module.css";
import FavoriteTrips from "../components/FavoriteTrips/FavoriteTrips";

const FavoriteTripsPage = () => {
  return (
    <main className={styles["main-container"]}>
      <FavoriteTrips />
    </main>
  );
};

export default FavoriteTripsPage;
