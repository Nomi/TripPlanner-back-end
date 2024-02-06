import styles from "./Pages.module.css";
import Home from "../components/Home/Home";

const HomePage = (props) => {
  return (
    <main className={styles["home-container"]}>
      <Home isTest={props.isTest} />
    </main>
  );
};

export default HomePage;
