import styles from "./Pages.module.css";
import Help from "../components/Help/Help";

const HelpPage = () => {
  return (
    <main className={styles["main-container"]}>
      <Help />
    </main>
  );
};

export default HelpPage;
