import styles from "./Pages.module.css";
import LogRegisterForm from "../components/LogRegister/LogRegisterForm";

const LogRegisterPage = () => {
  return (
    <main className={styles["main-container"]}>
      <LogRegisterForm />
    </main>
  );
};

export default LogRegisterPage;
