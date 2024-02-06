import styles from "./Pages.module.css";
import LogRegisterFormAdmin from "../components/LogRegister/LogRegisterFormAdmin";

const LogRegisterPage = () => {
  return (
    <main className={styles["main-container"]}>
      <LogRegisterFormAdmin />
    </main>
  );
};

export default LogRegisterPage;
