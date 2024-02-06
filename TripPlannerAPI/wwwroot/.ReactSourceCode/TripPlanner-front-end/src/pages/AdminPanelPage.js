import styles from "./Pages.module.css";
import AdminPanel from "../components/AdminPanel/AdminPanel";

const AdminPanelPage = () => {
  return (
    <main className={styles["main-container"]}>
      <AdminPanel />
    </main>
  );
};

export default AdminPanelPage;
