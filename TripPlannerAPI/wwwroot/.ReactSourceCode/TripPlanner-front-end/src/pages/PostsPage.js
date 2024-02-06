import styles from "./Pages.module.css";
import Posts from "../components/Posts/Posts";

const TripDetailsMyPage = () => {
  return (
    <main className={styles["main-container"]}>
      <Posts />
    </main>
  );
};

export default TripDetailsMyPage;
