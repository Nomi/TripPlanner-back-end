import styles from "./AdminPanel.module.css";
import AllUsers from "./AllUsers";
import Typography from "@mui/joy/Typography";

const AdminPanel = () => {

  return (
    <section className={styles["posts-section"]}>
      <div className={styles["posts"]}>
        <div className={styles["title-container"]}>
          <Typography level="h6">
            Browse through all users
          </Typography>
        </div>
        <AllUsers />
      </div>
    </section>
  );
};

export default AdminPanel;
