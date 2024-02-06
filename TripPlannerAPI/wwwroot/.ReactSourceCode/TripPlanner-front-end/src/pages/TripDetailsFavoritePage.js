import styles from "./Pages.module.css";
import TripDetailsFavorite from "../components/TripDetailsFavorite/TripDetailsFavorite";

const TripDetailsFavoritePage = (props) => {
  return (
    <main className={styles["main-container"]}>
      <TripDetailsFavorite />
    </main>
  );
};

export default TripDetailsFavoritePage;
